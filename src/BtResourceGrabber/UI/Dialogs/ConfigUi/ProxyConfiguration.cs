namespace BtResourceGrabber.UI.Dialogs.ConfigUi
{
	using System;
	using System.Windows.Forms;
	using BRG;
	using BRG.Entities;
	using BtResourceGrabber.Entities;

	public partial class ProxyConfiguration : Form
	{
		public ProxyConfiguration()
		{
			InitializeComponent();

			Load += ProxyConfiguration_Load;
			btnOk.Click += btnOk_Click;
		}

		void btnOk_Click(object sender, EventArgs e)
		{
			var opt = AppContext.Instance.Options;
			opt.EnableProxy = chkEnableProxy.Checked;
			opt.UseSameProxy = chkSame.Checked;

			opt.CommonProxy = opt.CommonProxy ?? new ProxyItem();
			opt.GfwProxy = opt.GfwProxy ?? new ProxyItem();

			opt.CommonProxy.Host = txtCommonHost.Text;
			opt.CommonProxy.Port = (int)nudCommonPort.Value;
			opt.CommonProxy.UserName = txtCommonUser.Text;
			opt.CommonProxy.Password = txtCommonPwd.Text;
			opt.GfwProxy.Host = txtGfwHost.Text;
			opt.GfwProxy.Port = (int)nudGfwPort.Value;
			opt.GfwProxy.UserName = txtGfwUser.Text;
			opt.GfwProxy.Password = txtGfwPwd.Text;

			DialogResult = DialogResult.OK;
			Close();
		}

		void ProxyConfiguration_Load(object sender, EventArgs e)
		{
			var opt = AppContext.Instance.Options;
			chkEnableProxy.Checked = opt.EnableProxy;
			chkSame.Checked = opt.UseSameProxy;

			gpCommon.AddDataBinding(chkEnableProxy, s => s.Enabled, s => s.Checked);
			gpGfw.Enabled = !chkSame.Checked;
			chkSame.CheckedChanged += (s, ex) =>
			{
				gpGfw.Enabled = !chkSame.Checked;
			};

			if (opt.CommonProxy != null)
			{
				txtCommonHost.Text = opt.CommonProxy.Host;
				nudCommonPort.Value = opt.CommonProxy.Port;
				txtCommonUser.Text = opt.CommonProxy.UserName;
				txtCommonPwd.Text = opt.CommonProxy.Password;
			}
			if (opt.GfwProxy != null)
			{
				txtGfwHost.Text = opt.GfwProxy.Host;
				nudGfwPort.Value = opt.GfwProxy.Port;
				txtGfwUser.Text = opt.GfwProxy.UserName;
				txtGfwPwd.Text = opt.GfwProxy.Password;
			}
			chkUseAllGfw.AddDataBinding(opt, s => s.Checked, s => s.UseGfwProxyForAll);
		}
	}
}
