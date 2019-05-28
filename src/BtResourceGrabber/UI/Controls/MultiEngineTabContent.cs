using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentOwl.BetterListView;

namespace BtResourceGrabber.UI.Controls
{
	using System.Diagnostics;
	using System.Runtime.Remoting.Contexts;
	using System.Threading;
	using System.Threading.Tasks;

	using BRG;
	using BRG.Engines;
	using BRG.Engines.Entities;
	using BRG.Entities;
	using BRG.Service;
	using BtResourceGrabber.Entities;
	using BtResourceGrabber.Service;
	using BtResourceGrabber.UI.Controls.ResourceListView;
	using BtResourceGrabber.UI.Dialogs;
	using BtResourceGrabber.UI.Dialogs.ConfigUi;

	using Preview;

	partial class MultiEngineTabContent : FunctionalUserControl
	{
		/// <summary>
		/// 选择发生了变化
		/// </summary>
		public event EventHandler<ResourceSelectionEventArgs> ResourceSelectionChanged;

		/// <summary>
		/// 引发 <see cref="ResourceSelectionChanged" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		protected virtual void OnResourceSelectionChanged(ResourceSelectionEventArgs ea)
		{
			var handler = ResourceSelectionChanged;
			if (handler != null)
				handler(this, ea);
		}

		public MultiEngineTabContent(params IResourceProvider[] providers)
		{
			RawEngines = providers;

			InitializeComponent();

			if (!Program.IsRunning) return;

			//mark 事件
			InitMark();
			//引擎
			InitSearch();
			//选区
			InitSelection();
			//预览
			InitPreview();
			//举报
			InitReport();
			//插件
			ServiceManager.Instance.InvokeRegisterContextMenuAddin(ctxMarkType, providers);
			ctxMarkType.Opening += (s, e) =>
			{
				var items = lv.GetItemsForOperation();
				var reses = items?.Select(x => x.Resource).ToArray();

				ServiceManager.Instance.InvokeCallOnOpeningContextMenu(ctxMarkType, reses);
			};
		}

		#region Preview

		PreviewContext _previewContext;

		/// <summary>
		/// 初始化预览
		/// </summary>
		void InitPreview()
		{
			_previewContext = new PreviewContext(this, lv);
			lv.FocusedItemChanged += (s, e) =>
			{
				_previewContext.UpdatePreview(lv.FocusedItem?.Resource);
			};
		}

		#endregion


		#region 搜索

		public IResourceProvider[] RawEngines { get; private set; }

		public List<EngineSearchContext> Engines { get; set; }

		int _maxSearchPage, _currentSearchedPage;
		string _currentKeyWord;
		string[] _keys;



		void InitSearch()
		{
			Engines = RawEngines.Select(s => new EngineSearchContext(s)).ToList();
			lv.AttachEngines(RawEngines);
			pWarning.KeepCenter();

			//加入列表
			using (panStatus.CreateBatchOperationDispatcher())
			{
				for (var i = 0; i < Engines.Count; i++)
				{
					var ps = new EngineStatus(Engines[i]);
					panStatus.Controls.Add(ps);
					ps.Location = new Point(i * 20, 1);

					tt.SetToolTip(ps, "引擎：" + Engines[i].Provider.Info.Name);
				}
			}

			btnCancelLoad.Click += (s, e) =>
			{
				if (!this.Question("当前正在搜索，需要取消当前搜索吗？", true))
				{
					return;
				}

				Engines.Where(x => !x.Provider.Disabled).ForEach(x =>
				{
					if (x.IsBusy)
						x.Cancel();
				});
			};
			btnCancelLoad.Enabled = false;
			Engines.ForEach(s =>
			{
				s.SearchComplete += S_SearchComplete;
				s.SearchCancelled += S_SearchCancelled;
				s.SearchFailed += S_SearchFailed;
			});
			lv.LoadComplete += (s, e) => RefreshSearchStatus();
		}


		/// <summary>
		/// 开始加载更多
		/// </summary>
		void BeginLoadMore()
		{
			pWarning.Visible = HasAnyUnsupportKey(_currentKeyWord);
			if (pWarning.Visible)
				return;

			if (Engines.Any(s => !s.Provider.Disabled && s.IsBusy))
			{
				if (!this.Question("当前正在搜索，需要取消当前搜索并重新搜索吗？", true))
				{
					return;
				}

				Engines.Where(s => !s.Provider.Disabled).ForEach(s =>
				  {
					  if (s.IsBusy)
						  s.Cancel();
				  });
			}

			lblSearchProgress.Text = "准备搜索...";
			lblSearchProgress.ForeColor = Color.Green;

			//初始化各参数
			_maxSearchPage = Engines.Where(s => !s.Provider.Disabled).Max(s => s.CurrentLoadedPage) + AppContext.Instance.Options.LoadPages;
			var isUnicodeKey = EngineUtility.IsUnicodeKey(_currentKeyWord);
			foreach (var engine in Engines)
			{
				if (engine.Provider.Disabled || (engine.CurrentLoadedPage > 0 && !engine.HasMore) || (!engine.Provider.SupportUnicode && isUnicodeKey))
					continue;

				engine.SearchKey = _currentKeyWord;
				engine.CurrentLoadedPage++;
				engine.SearchedCount = 1;
				engine.ErrorCount = 0;
				if (engine.CurrentLoadedPage > _maxSearchPage)
				{
					continue;
				}
				Interlocked.Increment(ref _currentWorkingClients);
				_currentSearchedPage++;
				engine.DoSearch();
				RefreshSearchStatus();
			}
			RefreshSearchStatus();
			if (_currentWorkingClients == 0)
			{
				ResourceOperation.MainForm.ShowFloatTip("没有更多可搜索的资源 :-(");
			}
		}

		private void S_SearchFailed(object sender, EventArgs e)
		{
			var ctx = sender as EngineSearchContext;
			Interlocked.Decrement(ref _currentWorkingClients);
			Debug.WriteLine($"Provider[{ctx.Provider.Info.Name}] search failed on page [{ctx.CurrentLoadedPage}]");

			if (ctx.ErrorCount++ <= AppContext.Instance.Options.MaxRetryCount)
			{
				Interlocked.Increment(ref _currentWorkingClients);
				_currentSearchedPage++;
				ctx.SearchedCount++;
				ctx.CurrentLoadedPage++;
				ctx.DoSearch();
			}
			RefreshSearchStatus();
		}

		private void S_SearchCancelled(object sender, EventArgs e)
		{
			var ctx = sender as EngineSearchContext;

			Debug.WriteLine($"Provider[{ctx.Provider.Info.Name}] search cancelled on page [{ctx.CurrentLoadedPage}]");
			Interlocked.Decrement(ref _currentWorkingClients);
			RefreshSearchStatus();
		}


		private void S_SearchComplete(object sender, EventArgs e)
		{
			var ctx = sender as EngineSearchContext;
			Interlocked.Decrement(ref _currentWorkingClients);
			Debug.WriteLine($"Provider[{ctx.Provider.Info.Name}] search completed on page [{ctx.CurrentLoadedPage}]");

			var result = ctx.Result;
			if (result != null)
			{
				//加载
				var validItems = result.Where(s => !AppContext.Instance.Options.StrictFilter || _keys.Any(x => s.Title.Contains(x, StringComparison.OrdinalIgnoreCase))).ToArray();
				lv.AppendItems(validItems);
			}

			if (ctx.CurrentLoadedPage < _maxSearchPage && ctx.HasMore)
			{
				ctx.CurrentLoadedPage++;
				Interlocked.Increment(ref _currentWorkingClients);
				_currentSearchedPage++;
				ctx.SearchedCount++;
				ctx.DoSearch();
			}
			RefreshSearchStatus();
		}


		int _currentWorkingClients = 0;

		void RefreshSearchStatus()
		{
			btnCancelLoad.Enabled = _currentWorkingClients > 0;

			var desc = "";
			if (_currentWorkingClients > 0)
			{
				desc = $"正在进行 {_currentSearchedPage:N0} 次搜索({_currentWorkingClients}个引擎正在搜索)，已获得 {lv.Items.Count:N0} 条结果...";
			}
			else
			{
				desc = $"已完成 {_currentSearchedPage:N0} 次搜索，{lv.Items.Count:N0} 条结果。" + (Engines.Any(s => !s.Provider.Disabled && s.HasMore) ? "可能还有更多结果，点击“搜索资源”继续搜索。" : "");
			}
			lblSearchProgress.Text = desc;
			//pgSearchProgress.Visible = _currentWorkingClients > 0;
			//pgSearchProgress.Value = Engines.Max(s => s.SearchedCount);
		}

		/// <summary>
		/// 如果搜索关键字变化，则重新加载
		/// </summary>
		/// <param name="key"></param>
		public void ReloadIfKeyChanged(string key)
		{
			if (key == _currentKeyWord)
				return;

			ResetSearchStatus(key);
			BeginLoadMore();
		}

		public void ContinueLoad(string key)
		{
			if (key != _currentKeyWord)
			{
				ResetSearchStatus(key);
			}

			BeginLoadMore();
		}

		/// <summary>
		/// 清空当前的搜索状态
		/// </summary>
		/// <param name="key"></param>
		void ResetSearchStatus(string key)
		{
			lv.Items.Clear();
			_itemsCache.Clear();
			_maxSearchPage = 0;
			_currentKeyWord = key;
			_keys = _currentKeyWord.Split(new[] { ' ', ',', '.', '　', '/', '\\', '-' }, StringSplitOptions.RemoveEmptyEntries);
			Engines.ForEach(s => s.CurrentLoadedPage = 0);
		}

		/// <summary>
		/// 强制重新加载
		/// </summary>
		/// <param name="key"></param>
		public void ForeceReload(string key)
		{
			_currentKeyWord = null;
			ReloadIfKeyChanged(key);
		}

		bool HasAnyUnsupportKey(string key)
		{
			if (RawEngines.Any(s => s.Disabled || s.SupportUnicode))
				return false;
			return EngineUtility.IsUnicodeKey(key);
		}


		#endregion

		#region 当前的选区

		/// <summary>
		/// 获得当前的选区
		/// </summary>
		public List<ResourceIdentity> CurrentSelection
		{
			get { return lv.SelectedFullItems; }
		}

		/// <summary>
		/// 获得当前选中的资源
		/// </summary>
		public ResourceIdentity SelectedResouce
		{
			get
			{
				var sl = CurrentSelection;
				if (sl.Count == 0)
					return null;

				return CurrentSelection[0];
			}
		}

		void InitSelection()
		{
			lv.SelectedIndexChanged += (s, e) =>
			{
				var ea = new ResourceSelectionEventArgs(CurrentSelection);

				OnResourceSelectionChanged(ea);
			};
		}

		#endregion


		#region 项缓存

		Dictionary<string, List<BetterListViewItem>> _itemsCache = new Dictionary<string, List<BetterListViewItem>>(StringComparer.OrdinalIgnoreCase);

		List<BetterListViewItem> GetItemsByHash(string hash, bool init = false)
		{
			var result = _itemsCache.GetValue(hash);
			if (result == null && init)
			{
				result = new List<BetterListViewItem>();
				_itemsCache.Add(hash, result);
			}

			return result;
		}

		#endregion

		#region 标记

		void InitMark()
		{
			ctxMarkType.Opening += (s, e) =>
			{
				if (lv.FocusedItem == null)
				{
					e.Cancel = true;
					return;
				}

				var items = lv.SelectedFullItems;
				var rawItems = lv.SelectedItems;
				mDownloadTorrent.Visible = items.Any(x => x.BasicInfo.ResourceType == ResourceType.BitTorrent);
				mOpenDownloadPage.Visible = items.Any(x => x.BasicInfo.ResourceType == ResourceType.NetDisk);
				mCopyDownloadLink.Visible = items.Any(x => !x.BasicInfo.HasSubResources);//items.Any(x => x.BasicInfo.ResourceType == ResourceType.Ed2K);
				tsMultiResNotLoad.Visible = rawItems.Any(x => x.Resource.HasSubResources && x.Resource.SubResources == null);

				var itemView = lv.FocusedItem?.TopItem.ResourceIdentities.FirstOrDefault()?.FindResourceToViewDetail();
				mViewContent.Enabled = itemView?.ResourceType == ResourceType.BitTorrent;
				mViewDetail.Enabled = !string.IsNullOrEmpty(itemView?.Provider?.GetDetailUrl(itemView));

				//resources
				mMarkDone.Enabled = mMarkUndone.Enabled = mMarkColor.Enabled = items.Any(x => x.BasicInfo.ResourceType == ResourceType.Ed2K || x.BasicInfo.ResourceType == ResourceType.BitTorrent);
			};
			mViewDetail.Click += (s, e) =>
			{
				var resource = lv.FocusedItem?.TopItem.ResourceIdentities.FirstOrDefault()?.FindResourceToViewDetail();
				if (resource != null)
					AppContext.Instance.ResourceOperation.ViewDetail(resource.Provider, resource);
			};
			mCopyDownloadLink.Click += (s, e) => AppContext.Instance.ResourceOperation.CopyDownloadLink(CurrentSelection.Select(x => x.FindResourceToCopyLink()).ToArray());
			mMarkDone.Click += (s, e) => AppContext.Instance.ResourceOperation.MarkDone(CurrentSelection.Select(x => x.FindResourceToCopyLink()).ToArray());
			mMarkUndone.Click += (s, e) => AppContext.Instance.ResourceOperation.MarkUndone(CurrentSelection.Select(x => x.FindResourceToCopyLink()).ToArray());
			tsMultiResNotLoad.Click += (s, e) =>
			{
				foreach (var item in lv.SelectedItems.Where(x => x.Resource.HasSubResources && x.Resource.SubResources == null))
				{
					item.IsExpanded = true;
				}
			};
			mViewContent.Click += (s, e) =>
			{
				var resource = lv.FocusedItem?.TopItem.ResourceIdentities.FirstOrDefault()?.FindResourceToViewDetail();
				if (resource != null)
					AppContext.Instance.ResourceOperation.ViewTorrentContents(resource.Provider, resource);
			};
			mDownloadTorrent.Click += (s, e) =>
			{
				var items = lv.SelectedFullItems;
				AppContext.Instance.ResourceOperation.AccquireDownloadTorrent(items.SelectMany(x => x.FindResourceDownloadInfo()).ToArray());
			};
			mOpenDownloadPage.Click += (s, e) =>
			{
				var items = lv.SelectedFullItems;
				AppContext.Instance.ResourceOperation.OpenDownloadPage(items.SelectMany(x => x.FindResourceDownloadInfo()).ToArray());
			};
			lv.MouseDoubleClick += (s, e) =>
			{
				var item = lv.FocusedItem;
				if (item == null)
					return;

				if (item.ChildItems.Count > 0)
					item.IsExpanded = !item.IsExpanded;
				else
					ctxMarkType.Show(lv.PointToScreen(e.Location));
			};
			lv.ItemExpand += (s, e) =>
			{
				var item = e.Item as ResourceListViewItem;
				if (item.Resource.HasSubResources && item.Resource.SubResources == null)
				{
					Task.Factory.StartNew(() => item.Resource.Provider.LoadSubResources(item.Resource));
				}
			};

			AppContext.Instance.RequestRefreshMarkCollection += (s, e) => RefreshMarkMenu();
			RefreshMarkMenu();
			mMarkColor.DropDownOpening += mMarkColor_DropDownOpening;
			mMarkOption.Click += (s, e) =>
			{
				using (var dlg = new ConfigCenter())
				{
					dlg.SelectedConfig = dlg.FindConfigUI<MarkOption>().First();
					dlg.ShowDialog();
				}
			};
			mMarkNone.Click += SetMarkHandler;
		}

		void mMarkColor_DropDownOpening(object sender, EventArgs e)
		{
			//仅选择第一个
			var res = SelectedResouce;
			if (res == null)
			{
				return;
			}

			var curType = AppContext.Instance.GetResourceMarkName(res.BasicInfo);
			mViewContent.Enabled = res.FindResourceToViewDetail() != null;
			mMarkColor.DropDownItems.OfType<ToolStripMenuItem>().ForEach(_ => _.Checked = false);
			if (string.IsNullOrEmpty(curType))
			{
				mMarkNone.Checked = true;
			}
			else
			{
				var item = mMarkColor.DropDownItems.OfType<ToolStripMenuItem>().FirstOrDefault(_ => _.Tag != null && _.Text == curType);
				if (item != null)
					item.Checked = true;
			}
		}

		void RefreshMarkMenu()
		{
			var keepItems = mMarkColor.DropDownItems.Cast<ToolStripItem>().Take(2).ToArray();
			var afterItems = mMarkColor.DropDownItems.Cast<ToolStripItem>().Skip(mMarkColor.DropDownItems.Count - 2).ToArray();

			mMarkColor.DropDownItems.Clear();
			mMarkColor.DropDownItems.AddRange(keepItems);

			var markItems = AppContext.Instance.Options.HashMarks.Select(s => (ToolStripItem)new ToolStripMenuItem(s.Key) { ForeColor = s.Value.Color, BackColor = s.Value.BackColor, Tag = s }).ToArray();
			mMarkColor.DropDownItems.AddRange(markItems);
			foreach (var toolStripItem in markItems)
			{
				toolStripItem.Click += SetMarkHandler;
			}

			if (!markItems.Any())
			{
				var emptyItem = new ToolStripMenuItem("(没有已添加的标记...)");
				emptyItem.Enabled = false;
				mMarkColor.DropDownItems.Add(emptyItem);
			}

			mMarkColor.DropDownItems.AddRange(afterItems);
		}

		void SetMarkHandler(object sender, EventArgs e)
		{
			var menu = sender as ToolStripMenuItem;
			var mark = menu.Tag == null ? null : menu.Text;

			using (lv.CreateBatchOperationDispatcher())
			{
				var selectedResources = lv.SelectedItems.Select(s => s.Resource).Distinct().ToArray();
				AppContext.Instance.ResourceOperation.SetTorrentMask(mark, selectedResources);
			}
		}

		#endregion

		#region 举报

		/// <summary>
		/// 初始化举报
		/// </summary>
		void InitReport()
		{
			ctxMarkType.Opening += CtxMarkType_Opening;
		}
		private void CtxMarkType_Opening(object sender, CancelEventArgs e)
		{
			var items = lv.GetItemsForOperation();

			if (items == null || items.Count == 0)
			{
				mReportState.Text = "没有选择项";
				mReportState.Enabled = false;
				mReportState.ForeColor = Color.Gray;
				return;
			}

			if (items.Count > 1)
			{
				mReportState.Text = "(选择了多项)";
				mReportState.Enabled = false;
				mReportState.Image = null;
				mReportState.ForeColor = Color.Gray;
			}
			else
			{
				mReportState.Enabled = true;
				var item = items.FirstOrDefault();

				switch (item.Resource.VerifyState)
				{
					case VerifyState.Unknown:
					case VerifyState.None:
						mReportState.Image = Properties.Resources.question_shield;
						mReportState.Text = "此资源安全性未知, 请谨慎下载";
						mReportState.ForeColor = Color.Blue;
						break;
					case VerifyState.Verified:
						mReportState.Image = Properties.Resources.tick_shield;
						mReportState.Text = "已被初步认定为安全资源";
						mReportState.ForeColor = Color.Green;
						break;
					case VerifyState.Reported:
						mReportState.Image = Properties.Resources.exclamation_shield;
						mReportState.Text = $"已被 {item.Resource.ReportNum:N0} 人举报，请谨慎下载";
						mReportState.ForeColor = Color.OrangeRed;
						break;
					case VerifyState.Illegal:
						mReportState.Image = Properties.Resources.cross_shield;
						mReportState.Text = $"已被初步认定为有害资源，请谨慎下载";
						mReportState.ForeColor = Color.Red;
						break;
					case VerifyState.Fake:
						mReportState.Image = Properties.Resources.cross_shield;
						mReportState.Text = $"已被初步认定为虚假资源，请核实有效后下载";
						mReportState.ForeColor = Color.Brown;
						break;
					case VerifyState.AutoIllegal:
						mReportState.Image = Properties.Resources.minus_shield;
						mReportState.Text = $"助手自动判定为有害资源，请谨慎下载";
						mReportState.ForeColor = Color.Red;
						break;
					case VerifyState.AutoFake:
						mReportState.Image = Properties.Resources.minus_shield;
						mReportState.Text = $"助手自动判定为虚假资源，请核实有效后下载";
						mReportState.ForeColor = Color.Brown;
						break;
					default:
						break;
				}
			}
		}

		#endregion
	}
}
