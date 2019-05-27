using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BtResourceGrabber.UI.Dialogs.ConfigUi
{
	using BRG;

	partial class HistoryOption : ConfigControl
	{
		public HistoryOption()
		{
			InitializeComponent();

			Text = "历史记录";
			Image = Properties.Resources.clock_16;

			chkLogDownload.Text += $" [{AppContext.Instance.DownloadHistory.Count:N0}记录]";
			chkRecSearchHistory.Text += $" [{AppContext.Instance.Options.SearchTermsHistory.Count:N0}关键字]";

			var opt = AppContext.Instance.Options;

			chkLogDownload.AddDataBinding(opt, s => s.Checked, s => s.LogDownloadHistory);
			chkRecSearchHistory.AddDataBinding(opt, s => s.Checked, s => s.LogSearchHistory);

			btnClearDownloadHistory.Click += (s, e) =>
			{
				if (!this.Question("确定要清空所有下载记录吗？", true))
					return;

				AppContext.Instance.DownloadHistory.Clear();
				Information("操作成功。");
			};
			btnClearHistory.Click += (s, e) =>
			{
				if (!this.Question("确定要清空所有搜索记录吗？", true))
					return;

				AppContext.Instance.Options.SearchTermsHistory = new List<string>();
				AppContext.Instance.Options.LastSearchKey = "";
				Information("操作成功。");
			};
		}

	}
}
