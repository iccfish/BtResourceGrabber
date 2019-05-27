namespace BtResourceGrabber.UI.Dialogs.Messages
{
	using System;
	using System.Windows.Forms;
	using BRG;

	public partial class License : Form
	{
		public License()
		{
			InitializeComponent();
			t.Tick += t_Tick;
		}

		void t_Tick(object sender, EventArgs e)
		{
			if (left-- <= 0)
			{
				btnOk.Text = _btnText;
				btnOk.Enabled = true;
			}
			else
			{
				btnOk.Text = _btnText + " (" + (left + 1) + ")";
			}
		}

		string _btnText;
		int left;

		private void License_Load(object sender, EventArgs e)
		{
			lic.Rtf = SR.license;

			var opt = AppContext.Instance.Options;
			if (opt.FirstRun)
			{
				btnOk.Enabled = false;
				_btnText = btnOk.Text;
				btnOk.Text += " (5)";
				left = 4;
				t.Start();
			}
		}
	}
}
