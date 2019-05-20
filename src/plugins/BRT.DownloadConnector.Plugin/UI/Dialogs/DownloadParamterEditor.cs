using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BRT.DownloadConnector.Plugin.UI.Dialogs
{
	public partial class DownloadParamterEditor : Form
	{
		public DownloadParamterEditor()
		{
			InitializeComponent();

			Load += DownloadParamterEditor_Load;
			btnOk.Click += BtnOk_Click;
			btnBrowser.Click += BtnBrowser_Click;
		}

		private void BtnOk_Click(object sender, EventArgs e)
		{
			if (txtName.Text.IsNullOrEmpty())
			{
				MessageBox.Show(this, "请输入这货的名字 ￣□￣｜｜", "￣□￣｜｜", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			if (txtPath.Text.IsNullOrEmpty())
			{
				MessageBox.Show(this, "请输入可执行路径 ￣□￣｜｜", "￣□￣｜｜", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			DownloadParameter = DownloadParameter ?? new DownloadParameter();
			DownloadParameter.Path = txtPath.Text;
			DownloadParameter.Parameter = txtArgs.Text;

			DialogResult = DialogResult.OK;
			Close();
		}

		private void DownloadParamterEditor_Load(object sender, EventArgs e)
		{
			if (DownloadParameter == null)
				return;

			txtPath.Text = DownloadParameter.Path;
			txtArgs.Text = DownloadParameter.Parameter ?? "";
		}

		private void BtnBrowser_Click(object sender, EventArgs e)
		{
			if (ofd.ShowDialog() == DialogResult.OK)
				txtPath.Text = ofd.FileName;
		}

		public DownloadParameter DownloadParameter { get; set; }

		public string DownloadName
		{
			get { return txtName.Text; }
			set
			{
				txtName.Text = value;
				txtName.ReadOnly = !value.IsNullOrEmpty();
			}
		}
	}
}
