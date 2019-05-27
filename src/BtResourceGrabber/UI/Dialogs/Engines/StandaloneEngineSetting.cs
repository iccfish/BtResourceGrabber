using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BtResourceGrabber.UI.Dialogs.Engines
{
	using System.Runtime.Remoting.Contexts;
	using BRG;

	public partial class StandaloneEngineSetting : Form
	{
		public StandaloneEngineSetting()
		{
			InitializeComponent();
		}

		private void StandaloneEngineSetting_Load(object sender, EventArgs e)
		{
			var providers = AppContext.Instance;

			var items = providers.ResourceProviders.Where(s => s.SupportResourceInitialMark)
								.Select(s =>
								{
									var ilindex = -1;
									if (s.Info.Icon != null)
									{
										il.Images.Add(Utility.Get20PxImage(s));
										ilindex = il.Images.Count - 1;
									}
									return new ListViewItem(new[] { s.Info.Name, s.Info.Descrption }) { ImageIndex = ilindex, Checked = AppContext.Instance.Options.EngineStandalone.Contains(s.Info.Name) };
								}).ToArray();
			lstEngine.Items.AddRange(items);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			AppContext.Instance.Options.EngineStandalone = lstEngine.CheckedItems.Cast<ListViewItem>().Select(s => s.Text).Distinct(StringComparer.OrdinalIgnoreCase).ToHashSet(StringComparer.OrdinalIgnoreCase);
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
