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
	using System.Windows.Forms.VisualStyles;
	using BRG;
	using BRG.Entities;

	partial class NetworkOption : ConfigControl
	{
		public NetworkOption()
		{
			InitializeComponent();

			Text = "网络选项";
			Image = Properties.Resources.globe_16;
			var opt = AppContext.Instance.Options;

			nudTimeout.Value = opt.NetworkTimeout;
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

		/// <summary>
		/// 请求保存
		/// </summary>
		/// <returns></returns>
		public override bool Save()
		{
			var opt = AppContext.Instance.Options;
			opt.EnableProxy = chkEnableProxy.Checked;
			opt.UseSameProxy = chkSame.Checked;
			opt.NetworkTimeout = (int)nudTimeout.Value;

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

			return base.Save();
		}
	}
}
