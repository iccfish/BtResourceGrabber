using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BtResourceGrabber.Entities;
using BtResourceGrabber.Handlers;
using BtResourceGrabber.Service;
using BtResourceGrabber.UI.Dialogs;
using FSLib.Windows.Forms;

namespace BtResourceGrabber.UI.Controls
{
	using ComponentOwl.BetterListView;

	public partial class EngineTabContent : FunctionalUserControl
	{
		public ITorrentResourceProvider TorrentResourceProvider { get; private set; }

		string _lastSearchKey;
		int _lastLoadPage;
		string _engineProperty;
		ResourceListViewItemSorter _sorter;
		Dictionary<string, ListViewItem> _itemCache = new Dictionary<string, ListViewItem>();

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

		public EngineTabContent(ITorrentResourceProvider provider)
		{
			InitializeComponent();

			TorrentResourceProvider = provider;
			_engineProperty = GetTorrentProviderDesc();
			lblSearchProgress.Text = "引擎特性：" + _engineProperty;

			InitSort();
			//lv.ListViewItemSorter = _sorter = new ResourceListViewItemSorter();
			//lv.ColumnClick += (s, e) =>
			//{
			//	if (e.Column == 2 || e.Column == 3)
			//		return;

			//	var sortType = e.Column == 0 ? SortType.Title : e.Column == 1 ? SortType.FileSize : SortType.PubDate;
			//	if (sortType == Context.Instance.Options.SortType)
			//	{
			//		Context.Instance.Options.SortDirection = 1 - Context.Instance.Options.SortDirection;
			//	}
			//	else
			//	{
			//		Context.Instance.Options.SortType = sortType;
			//		Context.Instance.Options.SortDirection = 0;
			//	}
			//};
			lblCancelSearch.Click += (s, e) =>
			{
				if (!Question("当前正在搜索，需要取消当前搜索吗？", true))
				{
					return;
				}

				CancelCurrentLoad();
			};

			////注册排序
			//Context.Instance.Options.PropertyChanged += (s, e) =>
			//{
			//	if (e.PropertyName == "SortType" || e.PropertyName == "SortDirection")
			//	{
			//		lv.Sort();
			//	}
			//};
			lv.SelectedIndexChanged += lv_SelectedIndexChanged;
			lv.MouseDoubleClick += (s, e) =>
			{
				if (lv.SelectedItems.Count == 0)
					return;

				ctxMarkType.Show(lv.PointToScreen(e.Location));
			};

			//图标
			ilLv.Images.Add(FSLib.Windows.Utility.Get20PxImageFrom16PxImg(Properties.Resources.File___Torrent));
			ctxMarkType.Opening += (s, e) =>
			{
				if (lv.SelectedItems.Count == 0)
				{
					e.Cancel = true;
					return;
				}
			};
			mViewContent.Enabled = TorrentResourceProvider.SupportLookupTorrentContents;
			mViewDetail.Click += (s, e) => TorrentOperation.ViewDetail(TorrentResourceProvider, SelectedResouce);
			mCopyMagnet.Click += (s, e) => TorrentOperation.CopyMagnetLink(CurrentSelection.Select(x => x.Key).ToArray());
			mViewContent.Click += (s, e) => TorrentOperation.ViewTorrentContents(TorrentResourceProvider, SelectedResouce);
			mDownloadTorrent.Click += (s, e) => TorrentOperation.AccquireDownloadTorrent(CurrentSelection.Select(x => x.Key).ToArray());
			mSearchBaiDu.Click += (s, e) => TorrentOperation.BaiduSearch(SelectedResouce);
			mSearchGoogle.Click += (s, e) => TorrentOperation.GoogleSearch(SelectedResouce);
			mCopyHash.Click += (s, e) => TorrentOperation.CopyHash(CurrentSelection.Select(x => x.Key).ToArray());
			mCopyTitle.Click += (s, e) => TorrentOperation.CopyTitle(CurrentSelection.Select(x => x.Key).ToArray());

			TorrentOperation.TorrentMarked += TorrentOperation_TorrentMarked;
			TorrentOperation.RequestRefreshMarkCollection += (s, e) => RefreshMarkMenu();
			RefreshMarkMenu();
			mMarkColor.DropDownOpening += mMarkColor_DropDownOpening;
			mMarkOption.Click += (s, e) =>
			{
				using (var dlg = new Option())
				{
					dlg.CurrentTabIndex = 1;
					dlg.ShowDialog();
				}
			};
			mMarkNone.Click += SetMarkHandler;
			pWarning.KeepCenter();
		}

		void InitSort()
		{
			lv.ItemComparer = new ResourceListViewItemSorter();
			lv.SortedColumnsRowsHighlight = BetterListViewSortedColumnsRowsHighlight.ShowAlways;
			lv.ColorSortedColumn = Color.FromArgb(255, 245, 245, 245);
			lv.MultiSelect = false;
			lv.AutoSizeItemsInDetailsView = true;
			lv.SortList = new BetterListViewSortList();

			//设置是否允许排序
			foreach (var column in lv.Columns)
			{
				if (column.DisplayIndex == 1 || column.DisplayIndex == 3 || column.DisplayIndex == 4)
				{
					column.Style = BetterListViewColumnHeaderStyle.None;
				}
				else
				{
					column.Style = BetterListViewColumnHeaderStyle.Sortable;
				}
			}

			lv.AfterItemSort += (s, e) =>
			{
				if (!e.ColumnClicked)
					return;

				var columnIndex = e.SortList[0].ColumnIndex;
				var sortType = columnIndex == 0 ? SortType.Title : columnIndex == 2 ? SortType.FileSize : SortType.PubDate;
				Context.Instance.Options.SortType = sortType;
				Context.Instance.Options.SortDirection = e.SortList[0].OrderAscending ? 0 : 1;
			};
			InitSortInfo();
			Context.Instance.Options.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == "SortType" || e.PropertyName == "SortDirection")
				{
					InitSortInfo();
					lv.Sort();
				}
			};
		}

		void InitSortInfo()
		{
			lv.SortList.Clear();

			var opt = Context.Instance.Options;

			var sortColumn = opt.SortType == SortType.FileSize ? 2 : opt.SortType == SortType.PubDate ? 5 : 0;
			lv.SortList.Add(sortColumn, opt.SortDirection == 0);
		}


		void mMarkColor_DropDownOpening(object sender, EventArgs e)
		{
			//仅选择第一个
			var res = SelectedResouce;
			if (res == null)
			{
				return;
			}

			var curType = Context.Instance.GetResourceMarkName(SelectedResouce);
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

		void TorrentOperation_TorrentMarked(object sender, TorrentMarkEventArgs e)
		{
			var info = e.Torrent;
			var item = _itemCache.GetValue(info.Hash);

			if (item != null)
			{
				item.ForeColor = e.Mark == null ? lv.ForeColor : e.Mark.Color;
				item.BackColor = e.Mark == null ? lv.BackColor : e.Mark.BackColor;
				item.SubItems[3].Text = e.MaskName;
			}
		}

		void RefreshMarkMenu()
		{
			var keepItems = mMarkColor.DropDownItems.Cast<ToolStripItem>().Take(2).ToArray();
			var afterItems = mMarkColor.DropDownItems.Cast<ToolStripItem>().Skip(mMarkColor.DropDownItems.Count - 2).ToArray();

			mMarkColor.DropDownItems.Clear();
			mMarkColor.DropDownItems.AddRange(keepItems);

			var markItems = Context.Instance.Options.HashMarks.Select(s => (ToolStripItem)new ToolStripMenuItem(s.Key) { ForeColor = s.Value.Color, BackColor = s.Value.BackColor, Tag = s }).ToArray();
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
				var selectedResources = lv.SelectedItems.Cast<ListViewItem>().ToArray();
				foreach (var listViewItem in selectedResources)
				{
					TorrentOperation.SetTorrentMask(mark, listViewItem.Tag as ITorrentResourceInfo);
				}
			}
		}


		/// <summary>
		/// 获得当前选中的资源
		/// </summary>
		public ITorrentResourceInfo SelectedResouce
		{
			get
			{
				var sl = CurrentSelection;
				if (sl.Count == 0)
					return null;

				return sl[0].Key;
			}
		}

		/// <summary>
		/// 获得当前的选区
		/// </summary>
		public List<KeyValuePair<ITorrentResourceInfo, ListViewItem>> CurrentSelection
		{
			get { return lv.SelectedItems.Cast<ListViewItem>().Select(s => new KeyValuePair<ITorrentResourceInfo, ListViewItem>(s.Tag as ITorrentResourceInfo, s)).ToList(); }
		}

		void lv_SelectedIndexChanged(object sender, EventArgs e)
		{
			var ea = new ResourceSelectionEventArgs(CurrentSelection, TorrentResourceProvider);

			OnResourceSelectionChanged(ea);
		}


		/// <summary>
		/// 如果搜索关键字变化，则重新加载
		/// </summary>
		/// <param name="key"></param>
		public void ReloadIfKeyChanged(string key)
		{
			if (key == _lastSearchKey)
				return;

			_lastLoadPage = 0;
			_result = null;
			_lastSearchKey = key;
			lv.Items.Clear();
			_itemCache.Clear();
			BeginLoadMore();
		}

		/// <summary>
		/// 强制重新加载
		/// </summary>
		/// <param name="key"></param>
		public void ForeceReload(string key)
		{
			_lastSearchKey = null;
			_lastLoadPage = 0;
			ReloadIfKeyChanged(key);
		}

		bool HasAnyUnsupportKey(string key)
		{
			if (TorrentResourceProvider.SupportUnicode)
				return false;

			return key.Any(s => s > 255);
		}

		void BeginLoadMore()
		{
			pWarning.Visible = HasAnyUnsupportKey(_lastSearchKey);
			if (pWarning.Visible)
				return;

			if (_loadingThread != null && _loadingThread.IsAlive)
			{
				if (!Question("当前正在搜索，需要取消当前搜索并重新搜索吗？", true))
				{
					return;
				}

				CancelCurrentLoad();
			}

			pgSearchProgress.Visible = true;
			pgSearchProgress.Value = 0;
			lblSearchProgress.Text = "准备搜索...";
			lblSearchProgress.ForeColor = Color.Green;

			//log
			Context.Instance.Track.ReportSerach(_lastSearchKey, TorrentResourceProvider.Info.Name);

			_loadingThread = new Thread(() =>
			{
				try
				{
					LoadCore();

					Invoke(new Action(() =>
					{
						lblSearchProgress.ForeColor = SystemColors.ControlText;
						lblSearchProgress.Text = string.Format("已搜索 {0} 次，共 {1:N0} 条记录。{2}。双击或右击资源可打开操作菜单。",
																_lastLoadPage, _result.Count,
																(_lastResultGroup != null && _lastResultGroup.HasMore ? "还有更多资源，点击“搜索更多”继续向后搜索" : "没有更多可供搜索的资源"));

					}));
				}
				catch (ThreadAbortException)
				{
					Invoke(new Action(() =>
					{
						lblSearchProgress.ForeColor = Color.Red;
						lblSearchProgress.Text = string.Format("已取消搜索，已搜索 {0} 次", _lastLoadPage);
					}));
				}
				catch (Exception)
				{
					//加载失败
					Invoke(new Action(() =>
					{
						lblSearchProgress.ForeColor = Color.Red;
						lblSearchProgress.Text = String.Format("搜索时出现错误，已搜索 {0} 次", (_lastLoadPage));
					}));
				}
				finally
				{
					Invoke(new Action(() =>
					{
						lblCancelSearch.Visible = false;
						pgSearchProgress.Visible = false;
						btnLoadMore.Enabled = _lastResultGroup != null && _lastResultGroup.HasMore;
						lv.Sort();
					}));
				}
			});
			lblCancelSearch.Visible = true;
			_loadingThread.Start();
		}

		void CancelCurrentLoad()
		{
			if (_loadingThread == null || !_loadingThread.IsAlive)
				return;

			_loadingThread.Abort();
			_loadingThread = null;
		}

		Thread _loadingThread;
		List<ITorrentResourceInfo> _result;
		IResourceSearchInfo _lastResultGroup;

		void LoadCore()
		{
			var page = _lastLoadPage;
			var option = Context.Instance.Options;
			var pagecount = option.LoadPages;
			var keys = _lastSearchKey.Split(new[] { ' ', ',', '.', '　', '/', '\\', '-' }, StringSplitOptions.RemoveEmptyEntries);

			_result = _result ?? new List<ITorrentResourceInfo>();
			for (var i = 0; i < pagecount; i++)
			{
				Context.Instance.Statistics.UpdateRunningStatistics(true, TorrentResourceProvider, 1, 0, 0, 0);
				page++;
				Invoke(new Action(() =>
				{
					lblSearchProgress.Text = string.Format("正在进行第 {0} 次搜索...", page);
					pgSearchProgress.Maximum = pagecount;
					pgSearchProgress.Value = i + 1;
				}));

				var list = TorrentResourceProvider.Load(_lastSearchKey, option.SortType, option.SortDirection, option.PageSize, page);
				if (list == null)
				{
					Context.Instance.Statistics.UpdateRunningStatistics(true, TorrentResourceProvider, 0, 0, 0, 1);
					_lastLoadPage = page - 1;
					//失败。。。
					throw new LoadFailedException();
				}
				//挂起排序？
				_sorter.SuspendSort = list.SortType != null;
				Context.Instance.Statistics.UpdateRunningStatistics(true, TorrentResourceProvider, 0, 0, 1, 0);

				var validItems = list.Where(s => !Context.Instance.Options.StrictFilter || keys.Any(x => s.Title.Contains(x, StringComparison.OrdinalIgnoreCase))).ToArray();
				_result.AddRange(validItems);
				_lastResultGroup = list;
				Invoke(new Action(() => AppendItems(validItems)));

				_lastLoadPage = page;
				if (list.Count == 0 || !list.HasMore)
				{
					//没有资源
					break;
				}
			}
		}

		public void RefreshList()
		{
			lv.Items.Clear();
			_itemCache.Clear();
			AppendItems(_result);
			lv.Sort();
		}

		void AppendItems(IEnumerable<ITorrentResourceInfo> items)
		{
			using (lv.CreateBatchOperationDispatcher())
			{
				var lvitems = items.Where(s => s.Hash.IsNullOrEmpty() || !_itemCache.ContainsKey(s.Hash)).Select(CreateListViewItem).ToArray();
				foreach (var listViewItem in lvitems)
				{
					if (!(listViewItem.Tag as ITorrentResourceInfo).Hash.IsNullOrEmpty())
						_itemCache.Add((listViewItem.Tag as ITorrentResourceInfo).Hash, listViewItem);
				}
				lv.Items.AddRange(lvitems);
				lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			}
		}

		ListViewItem CreateListViewItem(ITorrentResourceInfo info)
		{
			var item = new ListViewItem(new[]
			{
				info.Title.GetSubString(100),
				info.DownloadSizeValue == null ? info.DownloadSize : info.DownloadSizeValue.Value.ToSizeDescription(),
				info.FileCount==null?"" :info.FileCount.Value.ToString("N0"),
				"",
				info.UpdateTime == null ? info.UpdateTimeDesc : info.UpdateTime.Value.ToString("yyyy-MM-dd")
			});
			item.ImageIndex = 0;
			item.Tag = info;
			item.UseItemStyleForSubItems = true;

			if (!info.IsHashLoaded)
			{
				info.DetailLoaded += TorrentOperation.MainForm.SaveInvoke((s, e) =>
				 {
					 {
						 var tinfo = s as ITorrentResourceInfo;
						 if (!_itemCache.ContainsKey(tinfo.Hash))
						 {
							 _itemCache.Add(tinfo.Hash, lv.Items.Cast<ListViewItem>().FirstOrDefault(x => x.Tag == tinfo));

							 RefreshItemMask(_itemCache[tinfo.Hash], tinfo);
						 }
					 }
				 });
			}
			else
				RefreshItemMask(item, info);
			return item;
		}

		/// <summary>
		/// 刷新指定项的标记
		/// </summary>
		/// <param name="item"></param>
		/// <param name="info"></param>
		void RefreshItemMask(ListViewItem item, ITorrentResourceInfo info)
		{
			string maskName;
			var mask = Context.Instance.GetResourceMark(info, out maskName);
			if (mask != null)
			{
				item.ForeColor = mask.Color;
				item.BackColor = mask.BackColor;
				item.SubItems[3].Text = maskName;
			}
			else
			{
				item.ForeColor = SystemColors.WindowText;
				item.BackColor = SystemColors.Window;
				item.SubItems[3].Text = "";
			}

		}

		private void btnLoadMore_Click(object sender, EventArgs e)
		{
			BeginLoadMore();
		}

		string GetTorrentProviderDesc()
		{
			var desc = new string[5];
			var prov = TorrentResourceProvider;
			desc[0] = prov.SupportSortType == null ? "不支持原生排序" : "支持部分排序方式";
			desc[1] = prov.SupportUnicode ? "支持非英文" : "不支持非英文";
			desc[2] = prov.RequireBypassGfw ? "需要科学上网" : "可以直接访问";
			desc[3] = prov.SupportCustomizePageSize ? "支持修改每页记录数" : "不支持修改每页记录数";
			desc[4] = prov.SupportLookupTorrentContents ? "支持查看资源内容" : "不支持查看资源内容";

			return desc.JoinAsString(",");
		}
	}
}
