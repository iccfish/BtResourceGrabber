namespace BRG.Engines.BuildIn.UI.Engines.Rarbg
{
	using System;
	using System.Windows.Forms;
	using BRG.Entities;

	public partial class RarbgConfig : Form
	{
		EnginePropertyCollection _property;

		public RarbgConfig(EnginePropertyCollection property)
		{
			InitializeComponent();

			_property = property;
			chkSkipVerify.Checked = _property.ContainsKey("skip_bot_check");
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (chkSkipVerify.Checked ^ _property.ContainsKey("skip_bot_check"))
			{
				if (chkSkipVerify.Checked)
					_property.Add("skip_bot_check", "");
				else
					_property.Remove("skip_bot_check");
			}

			Close();
		}
	}
}
