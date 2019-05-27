using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BtResourceGrabber.UI.Controls.ResourceListView
{
	using ComponentOwl.BetterListView;

	class PaddingLoadingItem : BetterListViewItem
	{
		public PaddingLoadingItem()
		{
			Image = Properties.Resources.wait;

			Text = "正在加载中....";
		}
	}
}
