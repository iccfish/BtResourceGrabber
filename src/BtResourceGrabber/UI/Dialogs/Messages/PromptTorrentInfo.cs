namespace BtResourceGrabber.UI.Dialogs.Messages
{
	using System;
	using System.Linq;
	using System.Text.RegularExpressions;
	using System.Web;
	using System.Windows.Forms;
	using BRG;
	using BRG.Engines.Entities;
	using BtResourceGrabber.Entities;

	partial class PromptTorrentInfo : FunctionalForm
	{
		public PromptTorrentInfo()
		{
			InitializeComponent();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			var txt = txtInfo.Text;
			var hashes = Regex.Matches(txt, @"([a-z\d]{40}).*?(&dn=([^&=\s]+))?", RegexOptions.IgnoreCase).Cast<Match>().ToArray();

			if (!hashes.Any())
			{
				this.Information("没有找到任何种子链接哦。");
				return;
			}

			var items = hashes.Select(s => new ResourceInfo()
			{
				Hash = s.Groups[1].Value.ToUpper(),
				Title = HttpUtility.UrlDecode(s.Groups[3].Success ? s.Groups[3].Value : s.Groups[1].Value)
			}).GroupBy(s => s.Hash).Select(s => s.First()).ToArray();

			AppContext.Instance.ResourceOperation.AccquireDownloadTorrent(items);
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
