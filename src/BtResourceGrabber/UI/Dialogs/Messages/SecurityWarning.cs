namespace BtResourceGrabber.UI.Dialogs.Messages
{
	using System;
	using System.Windows.Forms;
	using BRG;

	public partial class SecurityWarning : Form
	{
		public SecurityWarning()
		{
			InitializeComponent();
		}

		private void SecurityWarning_Load(object sender, EventArgs e)
		{
			var opt = AppContext.Instance.Options;
			chkDismiss.AddDataBinding(opt, s => s.Checked, s => s.DismissSecurityWarning);
			opt.DismissSecurityWarning = true;
		}

		/// <summary>
		/// 检测确认是否需要显式安全警告
		/// </summary>
		internal static void CheckShowSecurityWarning()
		{
			if (AppContext.Instance.Options.DismissSecurityWarning)
				return;

			new SecurityWarning().ShowDialog();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
