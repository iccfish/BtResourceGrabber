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
	public partial class DownloadParameterConfig : Form
	{
		public Dictionary<string, DownloadParameter> Parameters { get; set; }

		public DownloadParameterConfig()
		{
			InitializeComponent();

			Load += DownloadParameterConfig_Load;
			btnAdd.Click += BtnAdd_Click;
			btnEdit.Click += BtnEdit_Click;
			btnOk.Click += BtnOk_Click;
			btnDelete.Click += BtnDelete_Click;
			lv.SelectedIndexChanged += Lv_SelectedIndexChanged;
			lv.MouseDoubleClick += (s, e) =>
			{
				BtnEdit_Click(null, null);
			};
			btnEdit.Enabled = btnDelete.Enabled = false;
		}

		private void Lv_SelectedIndexChanged(object sender, EventArgs e)
		{
			btnEdit.Enabled = btnDelete.Enabled = lv.SelectedItems.Count > 0;
		}

		private void BtnDelete_Click(object sender, EventArgs e)
		{
			foreach (var listViewItem in lv.SelectedItems.Cast<ListViewItem>().ToArray())
			{
				listViewItem.Remove();
				Parameters.Remove(listViewItem.Text);
			}
		}

		private void BtnOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void BtnEdit_Click(object sender, EventArgs e)
		{
			var item = lv.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
			if (item == null)
				return;

			var dp = item.Tag as DownloadParameter;
			using (var dlg = new DownloadParamterEditor() { DownloadName = item.Text, DownloadParameter = dp })
			{
				dlg.ShowDialog(this);
				item.SubItems[1].Text = dp.Path;
			}
		}

		private void BtnAdd_Click(object sender, EventArgs e)
		{
			using (var dlg = new DownloadParamterEditor())
			{
				if (dlg.ShowDialog(this) != DialogResult.OK)
					return;

				if (Parameters.ContainsKey(dlg.DownloadName))
				{
					var lvitem = lv.Items.Cast<ListViewItem>().First(s => s.Tag == Parameters[dlg.DownloadName]);
					lvitem.Tag = dlg.DownloadParameter;
					lvitem.SubItems[1].Text = dlg.DownloadParameter.Path;
					Parameters[dlg.DownloadName] = dlg.DownloadParameter;
				}
				else
				{
					var item = new ListViewItem(new[] { dlg.DownloadName, dlg.DownloadParameter.Path }) { Tag = dlg.DownloadParameter };
					lv.Items.Add(item);
					Parameters.Add(dlg.DownloadName, dlg.DownloadParameter);
				}
			}
		}

		private void DownloadParameterConfig_Load(object sender, EventArgs e)
		{
			lv.Items.AddRange(Parameters.Select(s => new ListViewItem(new[] { s.Key, s.Value.Path }) { Tag = s.Value }).ToArray());
		}
	}
}
