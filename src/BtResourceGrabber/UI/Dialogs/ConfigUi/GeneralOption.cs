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
	using System.Diagnostics;
	using BRG;

	partial class GeneralOption : ConfigControl
	{
		public GeneralOption()
		{
			InitializeComponent();

			Text = "常规选项";
			Image = Properties.Resources.home_16;

			Load += GeneralOption_Load;
		}

		private void GeneralOption_Load(object sender, EventArgs e)
		{
			InitState();
		}

		void InitState()
		{
			var opt = AppContext.Instance.Options;

			chkTabMultiLine.AddDataBinding(opt, s => s.Checked, s => s.MultilineEngineTab);
			chkUsingFloat.AddDataBinding(opt, s => s.Checked, s => s.UsingFloatTip);
			chkEnableFilterDirective.AddDataBinding(opt, s => s.Checked, s => s.EnableSearchDirective);
			chkEnablePreview.AddDataBinding(opt, s => s.Checked, s => s.EnablePreviewIfPossible);

			lnkHelpOp.Click += (x, y) =>
			{
				try
				{
					Process.Start("http://www.fishlee.net/soft/bt_resouce_grabber/index.html#C-345");
				}
				catch (Exception)
				{
				}
			};
		}

	}
}
