using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BtResourceGrabber.UI.Controls.ResourceListView
{
	using System.ComponentModel;
	using System.Diagnostics;
	using System.Drawing;
	using System.Windows.Forms;
	using BRG;
	using BRG.Engines.Entities;
	using BRG.Entities;
	using BRG.Service;
	using BtResourceGrabber.Entities;
	using BtResourceGrabber.Service;
	using ComponentOwl.BetterListView;

	partial class ListView : BetterListView
	{

		#region 设计器方法

		private bool ShouldSerializeColumns()
		{
			return false;
		}
		private bool ShouldSerializeFont()
		{
			return false;
		}


		private bool ShouldImageList()
		{
			return false;
		}

		private bool ShouldImageListColumns()
		{
			return false;
		}

		private bool ShouldImageListGroups()
		{
			return false;
		}


		#endregion

		/// <summary>
		/// 提供类
		/// </summary>
		public IResourceProvider[] Providers { get; private set; }

		Timer _timer = new Timer();

		public ListView()
		{
			InitializeComponent();

			_timer.Interval = 300;
			_timer.Tick += (s, e) =>
			{
				AppendItemsCore();
			};
		}

		public void AttachEngines(IResourceProvider[] providers)
		{
			Providers = providers;

			InitSort();
			InitIcons();
			InitMark();
		}

		#region 图标

		void InitIcons()
		{
			//ilLv.Images.Add("torrent", FSLib.Windows.Utility.Get20PxImageFrom16PxImg(Properties.Resources.File___Torrent));
			//ilLv.Images.Add("torrent_multi", FSLib.Windows.Utility.Get20PxImageFrom16PxImg(Properties.Resources.torrent_multi));
			ilLv.Images.Add("torrent", Properties.Resources.magnet);
			ilLv.Images.Add("torrent_multi", Properties.Resources.torrent_multi);
			ilLv.Images.Add("multiresource", Properties.Resources.multiresource);
			ilLv.Images.Add("ed2k", Properties.Resources.Emule);

			ilLv.Images.Add("wp_baidu", Properties.Resources.favicon_bdp);
			ilLv.Images.Add("wp_qihu", Properties.Resources.favicon_yunpan);
			ilLv.Images.Add("wp_xl", Properties.Resources.favicon_xlshare);
			ilLv.Images.Add("wp_xf", Properties.Resources.favicon_qqshare);

			//添加多重结果的图标
			Providers.ForEach(s =>
			{
				if (s.Info.Icon == null)
					return;

				//ilLv.Images.Add($"e_{s.Info.Name}", FSLib.Windows.Utility.Get20PxImageFrom16PxImg(s.Info.Icon));
				ilLv.Images.Add($"e_{s.Info.Name}", s.Info.Icon);
			});
		}

		#endregion

		#region 项

		Queue<IResourceInfo> _queue = new Queue<IResourceInfo>(1000);

		/// <summary>
		/// 向结果中添加项
		/// </summary>
		/// <param name="items"></param>
		public void AppendItems(IEnumerable<IResourceInfo> items)
		{
			//AppendItemsCore();
			_timer.Enabled = false;
			_queue.EnqueueMany(items);
			_timer.Enabled = true;
		}

		/// <summary>
		/// 向结果中添加项
		/// </summary>
		// <param name="items"></param>
		private void AppendItemsCore()
		{
			if (_queue.Count == 0)
				return;

			_timer.Enabled = false;
			using (this.CreateBatchOperationDispatcher())
			{
				var st = new Stopwatch();
				var guid = new Guid();
				Trace.TraceInformation("开始刷新界面列表操作，GUID={0}", guid);
				BeginUpdate();

				//foreach (var item in items)
				while (_queue.Count > 0)
				{
					var item = _queue.Dequeue();
					var lvitem = item.Hash.IsNullOrEmpty() ? null : GetItemsByHash(item.Hash);
					if (lvitem == null)
					{
						lvitem = new ResourceListViewItem(item, Providers.Length > 1);
						if (!item.Hash.IsNullOrEmpty())
						{
							_itemsCache.Add(item.Hash, lvitem);
						}

						if (!item.IsHashLoaded)
						{
							item.DetailLoaded += ResourceOperation.MainForm.SafeInvoke((s, e) =>
							{
								var tinfo = s as IResourceInfo;
								if (!_itemsCache.ContainsKey(tinfo.Hash))
								{
									var titem = Items.Cast<ResourceListViewItem>().FirstOrDefault(x => x.Resource == tinfo);
									if (titem != null)
									{
										_itemsCache.Add(tinfo.Hash, titem);
										titem.RefreshDownloadStatus();
										titem.RefreshIllegalStatus();
									}
								}
							});
						}
						item.DownloadedChanged += (s, e) =>
						{
							var tinfo = s as IResourceInfo;
							GetItemsByHash(tinfo?.Hash)?.RefreshDownloadStatus();
						};
						item.VerifyStateChanged += (s, e) =>
						{
							var tinfo = s as IResourceInfo;
							GetItemsByHash(tinfo?.Hash)?.RefreshIllegalStatus();
						};

						lvitem.RefreshDownloadStatus();
						lvitem.RefreshIllegalStatus();
						Items.Add(lvitem);
					}
					else
					{
						Items.Remove(lvitem);
						lvitem.AppendSubResult(item);
						Items.Add(lvitem);
					}
				}
				Trace.TraceInformation("结束列表添加，开始调整列宽，GUID={0}，耗时={1} ms", guid, st.ElapsedMilliseconds);
				AutoResizeColumns(BetterListViewColumnHeaderAutoResizeStyle.ColumnContent);
				Trace.TraceInformation("结束调整列宽，开始排序，GUID={0}，耗时={1} ms", guid, st.ElapsedMilliseconds);
				Sort();
				Trace.TraceInformation("排序完成，开始界面布局，GUID={0}，耗时={1} ms", guid, st.ElapsedMilliseconds);
				EndUpdate();
				Trace.TraceInformation("加载结束，GUID={0}，耗时={1} ms", guid, st.ElapsedMilliseconds);
				OnLoadComplete();
			}
			_timer.Enabled = true;
		}

		#endregion

		#region 标记

		void InitMark()
		{
			AppContext.Instance.TorrentMarked += TorrentOperation_TorrentMarked;
			AppContext.Instance.ResouceDownloaded += TorrentOperation_ResouceDownloaded;
			AppContext.Instance.ResouceUndownload += TorrentOperation_ResouceUndownload;

			Disposed += (s, e) =>
			{
				AppContext.Instance.TorrentMarked += TorrentOperation_TorrentMarked;
				AppContext.Instance.ResouceDownloaded += TorrentOperation_ResouceDownloaded;
				AppContext.Instance.ResouceUndownload -= TorrentOperation_ResouceUndownload;
			};
		}

		private void TorrentOperation_ResouceUndownload(object sender, ResourceEventArgs e)
		{
			GetItemsByHash(e.Torrent.Hash)?.SetUndownload();
		}

		private void TorrentOperation_ResouceDownloaded(object sender, ResourceEventArgs e)
		{
			GetItemsByHash(e.Torrent.Hash)?.RefreshDownloadStatus();
		}

		void TorrentOperation_TorrentMarked(object sender, TorrentMarkEventArgs e)
		{
			var info = e.Torrent;
			GetItemsByHash(info.Hash)?.ApplyMark(e.MaskName, e.Mark);
		}

		#endregion

		#region 排序

		void InitSort()
		{
			ItemComparer = new ResourceListViewItemSorter();
			SortedColumnsRowsHighlight = BetterListViewSortedColumnsRowsHighlight.ShowAlways;
			ColorSortedColumn = Color.FromArgb(255, 245, 245, 245);
			AutoSizeItemsInDetailsView = true;

			//设置是否允许排序
			foreach (var column in Columns)
			{
				if (column.DisplayIndex == 4 || column.DisplayIndex == 5 || column.DisplayIndex == 1 || column.DisplayIndex == 2)
				{
					column.Style = BetterListViewColumnHeaderStyle.Nonclickable;
				}
				else
				{
					column.Style = BetterListViewColumnHeaderStyle.Sortable;
				}
			}

			AfterItemSort += (s, e) =>
			{
				if (!e.ColumnClicked)
					return;

				var columnIndex = e.SortList[0].ColumnIndex;
				var sortType = columnIndex == 0 ? SortType.Title : columnIndex == 3 ? SortType.FileSize : SortType.PubDate;
				AppContext.Instance.Options.SortType = sortType;
				AppContext.Instance.Options.SortDirection = e.SortList[0].OrderAscending ? 0 : 1;
			};
			AppContext.Instance.Options.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == "SortType" || e.PropertyName == "SortDirection")
				{
					InitSortInfo();
					Sort();
				}
			};
			InitSortInfo();
		}

		void InitSortInfo()
		{
			var opt = AppContext.Instance.Options;
			var sortList = new BetterListViewSortList();

			var sortColumn = opt.SortType == SortType.FileSize ? 3 : opt.SortType == SortType.PubDate ? 6 : 0;
			sortList.Set(sortColumn, opt.SortDirection == 0);
			SortList = sortList;
		}

		#endregion

		#region 项缓存

		Dictionary<string, ResourceListViewItem> _itemsCache = new Dictionary<string, ResourceListViewItem>(StringComparer.OrdinalIgnoreCase);

		ResourceListViewItem GetItemsByHash(string hash)
		{
			if (string.IsNullOrEmpty(hash))
				return null;

			return _itemsCache.GetValue(hash);
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new List<ResourceListViewItem> SelectedItems
		{
			get { return base.SelectedItems.OfType<ResourceListViewItem>().ToList(); }
		}

		public List<ResourceIdentity> SelectedFullItems
		{
			get
			{
				return base.SelectedItems.OfType<ResourceListViewItem>().SelectMany(s => s.ResourceIdentities).ToList();
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ResourceListViewItem FocusedItem
		{
			get { return base.FocusedItem as ResourceListViewItem; }
		}

		#endregion

		#region 项目的公开方法

		public void ClearItems()
		{
			_itemsCache.Clear();
			Items.Clear();
		}

		#endregion

		public event EventHandler LoadComplete;

		/// <summary>
		/// 引发 <see cref="LoadComplete" /> 事件
		/// </summary>
		protected virtual void OnLoadComplete()
		{
			var handler = LoadComplete;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		public List<ResourceListViewItem> GetItemsForOperation()
		{
			var items = SelectedItems;
			if (items == null || items.Count == 0)
			{
				if (FocusedItem == null)
					return null;

				items = new List<ResourceListViewItem>() { FocusedItem };
			}

			return items.SelectMany(s =>
			{
				if (s.ParentItem == null)
				{
					//顶级的，看是不是有子资源
					if (s.Resource.HasSubResources)
					{
						return s.ChildItems.OfType<ResourceListViewItem>();
					}
					else
					{
						return new ResourceListViewItem[1] { s };
					}
				}
				else
				{
					//有顶级的，那么看看顶级是不是子项目。如果是的话，则返回顶级，否则返回自己
					if (s.ParentItem.Resource.HasSubResources)
						return new[] { s };
					else return new[] { s.ParentItem };
				}
			}).ToList();
		}

		ResourceListViewItem GetTopItem(ResourceListViewItem item)
		{
			while (item.ParentItem != null)
				item = item.ParentItem;

			return item;
		}
	}
}
