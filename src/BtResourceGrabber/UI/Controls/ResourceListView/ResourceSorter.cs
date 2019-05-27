namespace BtResourceGrabber.UI.Controls.ResourceListView
{
	using System;
	using System.Collections;
	using System.Windows.Forms;
	using BRG;
	using BRG.Entities;
	using BtResourceGrabber.Entities;
	using ComponentOwl.BetterListView;

	class ResourceListViewItemSorter : BetterListViewItemComparer, IComparer
	{
		public bool SuspendSort { get; set; }

		#region Implementation of IComparer

		/// <summary>
		/// 比较两个对象并返回一个值，指示一个对象是小于、等于还是大于另一个对象。
		/// </summary>
		/// <returns>
		/// 一个带符号整数，它指示 <paramref name="x"/> 与 <paramref name="y"/> 的相对值，如下表所示。值含义小于零<paramref name="x"/> 小于 <paramref name="y"/>。零<paramref name="x"/> 等于 <paramref name="y"/>。大于零<paramref name="x"/> 大于 <paramref name="y"/>。
		/// </returns>
		/// <param name="x">要比较的第一个对象。</param><param name="y">要比较的第二个对象。</param><exception cref="T:System.ArgumentException"><paramref name="x"/> 和 <paramref name="y"/> 都不实现 <see cref="T:System.IComparable"/> 接口。- 或 -<paramref name="x"/> 和 <paramref name="y"/> 的类型不同，它们都无法处理与另一个进行的比较。</exception>
		public int Compare(object x, object y)
		{
			if (SuspendSort)
				return 0;

			var lx = (ListViewItem)x;
			var ly = (ListViewItem)y;

			var sx = (IResourceInfo)lx.Tag;
			var sy = (IResourceInfo)ly.Tag;

			if (sx.MatchWeight != sy.MatchWeight)
			{
				return sy.MatchWeight - sx.MatchWeight;
			}

			var opt = AppContext.Instance.Options;
			var weight = opt.SortDirection == 1 ? -1 : 1;

			switch (opt.SortType)
			{
				case SortType.Default:
					return 0;	//默认就是按照网站显示的顺序，不排序
				case SortType.Title:
					return string.Compare(sx.Title, sy.Title, StringComparison.OrdinalIgnoreCase) * weight;
				case SortType.PubDate:
					if (sx.UpdateTime == null && sy.UpdateTime == null)
						return 0;
					if (sx.UpdateTime == null ^ sy.UpdateTime == null)
						return (sx.UpdateTime == null ? -1 : 1) * weight;
					return (int)((sx.UpdateTime.Value - sy.UpdateTime.Value).TotalMinutes) * -1;
				//case BtResourceGrabber.Entities.SortType.TorrentSize:
				//	break;
				case SortType.FileSize:
					var s1 = sx.DownloadSizeValue ?? sx.DownloadSizeCalcauted;
					var s2 = sy.DownloadSizeValue ?? sx.DownloadSizeCalcauted;

					return (s1 == s2 ? 0 : s1 < s2 ? -1 : 1) * weight;
				//case BtResourceGrabber.Entities.SortType.LeechCount:
				//	break;
				//case BtResourceGrabber.Entities.SortType.SeederCount:
				//	break;
				default:
					break;
			}

			return 0;
		}

		#endregion

		/// <summary>
		/// Compare two sub-items.
		/// </summary>
		/// <param name="subItemA">First sub-item to compare.</param><param name="subItemB">Second sub-item to compare.</param><param name="sortMethod">Item comparison method.</param><param name="order">Sort order.</param>
		/// <returns>
		/// Comparison result.
		/// </returns>
		protected override int CompareSubItems(BetterListViewSubItem subItemA, BetterListViewSubItem subItemB, BetterListViewSortMethod sortMethod, int order)
		{
			if (SuspendSort)
				return 0;

			var lx = subItemA.Item as ResourceListViewItem;
			var ly = subItemB.Item as ResourceListViewItem;

			if (lx.ParentItem != null && ly.ParentItem != null)
			{
				return lx.Resource.CompareTo(ly.Resource);
			}

			//do not compare subitems

			if (lx.ParentItem != null || ly.ParentItem != null)
				return 0;

			var sx = lx.Resource;
			var sy = ly.Resource;


			if (sx.MatchWeight != sy.MatchWeight)
			{
				return sy.MatchWeight - sx.MatchWeight;
			}


			var opt = AppContext.Instance.Options;
			var weight = order;

			switch (opt.SortType)
			{
				case SortType.Default:
					return 0;	//默认就是按照网站显示的顺序，不排序
				case SortType.Title:
					return string.Compare(sx.Title, sy.Title, StringComparison.OrdinalIgnoreCase) * weight;
				case SortType.PubDate:
					if (sx.UpdateTime == null && sy.UpdateTime == null)
						return 0;
					if (sx.UpdateTime == null ^ sy.UpdateTime == null)
						return (sx.UpdateTime == null ? -1 : 1) * weight;
					return (int)((sx.UpdateTime.Value - sy.UpdateTime.Value).TotalMinutes) * -1;
				//case BtResourceGrabber.Entities.SortType.TorrentSize:
				//	break;
				case SortType.FileSize:
					var s1 = sx.DownloadSizeValue ?? EngineUtility.ToSize(sx.DownloadSize);
					var s2 = sy.DownloadSizeValue ?? EngineUtility.ToSize(sy.DownloadSize);

					return (s1 == s2 ? 0 : s1 < s2 ? -1 : 1) * weight;
				//case BtResourceGrabber.Entities.SortType.LeechCount:
				//	break;
				//case BtResourceGrabber.Entities.SortType.SeederCount:
				//	break;
				default:
					break;
			}

			return 0;
		}
	}
}
