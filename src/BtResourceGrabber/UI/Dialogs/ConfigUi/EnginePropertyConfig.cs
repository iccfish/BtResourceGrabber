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
	using BRG;
	using BRG.Service;
	using BtResourceGrabber.UI.Dialogs.Engines;

	partial class EnginePropertyConfig : ConfigControl
	{
		public EnginePropertyConfig()
		{
			InitializeComponent();

			Text = "引擎特性配置";
			Image = Properties.Resources.gear_16;

			InitEngineConfig();

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

		void InitEngineConfig()
		{
			var gps = new ListViewGroup("搜索引擎");
			var gpd = new ListViewGroup("下载引擎");
			lvConfigEngine.Groups.AddRange(new[]
											{
												gps,gpd

											});

			using (lvConfigEngine.CreateBatchOperationDispatcher())
			{
				foreach (var engine in AppContext.Instance.ResourceProviders.Where(s => s.SupportOption))
				{
					ilEngineConfig.Images.Add(Utility.Get20PxImage(engine));
					var item = new ListViewItem(engine.Info.Name) { ImageIndex = ilEngineConfig.Images.Count - 1, Tag = engine, Group = gps };

					lvConfigEngine.Items.Add(item);
				}
				foreach (var engine in AppContext.Instance.DownloadServiceProviders.Where(s => s.SupportOption))
				{
					ilEngineConfig.Images.Add(Utility.Get20PxImage(engine));
					var item = new ListViewItem(engine.Info.Name) { ImageIndex = ilEngineConfig.Images.Count - 1, Tag = engine, Group = gpd };

					lvConfigEngine.Items.Add(item);
				}
			}

			var showOption = new Action(() =>
			{
				var item = lvConfigEngine.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
				if (item == null)
					return;

				var engine = item.Tag as IServiceBase;

				if (engine?.SupportOption == true)
					engine.ShowOption();
			});

			lvConfigEngine.MouseDoubleClick += (s, e) =>
			{
				showOption();
			};
			btnEngineConfig.Click += (s, e) =>
			{
				showOption();
			};
			btnEngineConfig.Enabled = false;
			lvConfigEngine.ItemSelectionChanged += (s, e) =>
			{
				btnEngineConfig.Enabled = lvConfigEngine.SelectedItems.Count > 0;
			};
		}

		/// <summary>
		/// 请求保存
		/// </summary>
		/// <returns></returns>
		public override bool Save()
		{
			AppContext.Instance.Options.EngineStandalone = lstEngine.CheckedItems.Cast<ListViewItem>().Select(s => s.Text).Distinct(StringComparer.OrdinalIgnoreCase).MapToHashSet(StringComparer.OrdinalIgnoreCase);

			return base.Save();
		}

	}
}
