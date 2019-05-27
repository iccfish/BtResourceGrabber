namespace BtResourceGrabber.UI.Dialogs.ConfigUi
{
	using System;
	using System.Windows.Forms;
	using BRG;
	using BRG.Entities;
	using BtResourceGrabber.Entities;

	partial class SelectMark : FunctionalForm
	{
		HashMark _hashMark;
		bool _add;

		public SelectMark()
		{
			InitializeComponent();

			FormClosing += SelectMark_FormClosing;
			Load += SelectMark_Load;
		}

		void SelectMark_Load(object sender, EventArgs e)
		{
			_add = _hashMark == null;
			txtName.ReadOnly = !_add;
		}

		void SelectMark_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				if (string.IsNullOrEmpty(txtName.Text))
				{
					Information("需要输入标记的名称哦。");
					e.Cancel = true;
					return;
				}
				if (_add && AppContext.Instance.Options.HashMarks.ContainsKey(txtName.Text))
				{
					Information("标记的名字重复了哦。");
					e.Cancel = true;
					return;
				}
			}

			_hashMark = new HashMark(btnChangeColor.ForeColor);
			_hashMark.BackColor = btnChangeBg.BackColor;
		}

		private void btnChangeColor_Click(object sender, EventArgs e)
		{
			cd.Color = btnChangeColor.ForeColor;
			if (cd.ShowDialog() != DialogResult.OK)
				return;

			btnChangeColor.ForeColor = cd.Color;
		}

		public HashMark HashMark
		{
			get { return _hashMark; }
			set
			{
				if (value == null || _hashMark == value)
					return;

				_hashMark = value;
				btnChangeColor.ForeColor = value.Color;
				btnChangeBg.BackColor = value.BackColor;
			}
		}

		public string MaskName
		{
			get { return txtName.Text; }
			set { txtName.Text = value; }
		}

		private void btnChangeBg_Click(object sender, EventArgs e)
		{
			cd.Color = btnChangeBg.BackColor;
			if (cd.ShowDialog() != DialogResult.OK)
				return;

			btnChangeBg.BackColor = cd.Color;
		}
	}
}
