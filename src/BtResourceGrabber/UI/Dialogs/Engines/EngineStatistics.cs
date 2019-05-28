namespace BtResourceGrabber.UI.Dialogs.Engines
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Runtime.Remoting.Contexts;
	using System.Windows.Forms;
	using BRG;
	using BRG.Service;
	using BtResourceGrabber.Service;

	public partial class EngineStatistics : Form
	{
		public EngineStatistics()
		{
			InitializeComponent();
		}

		private void EngineStatistics_Load(object sender, EventArgs e)
		{
			var mgr = ServiceManager.Instance;
			var services = mgr.DownloadServiceProviders.Cast<IServiceBase>().Union(mgr.ResourceProviders).ToArray();
			var list = new List<ListViewItem>(services.Length);

			foreach (var service in services)
			{
				var item = new ListViewItem(new[] { service.Info.Name, service.RequireBypassGfw ? "需要翻墙" : "", service.Disabled ? "已禁用" : "正常" }) { Checked = !service.Disabled };
				if (service.Info.Icon != null)
				{
					il.Images.Add(service.GetType().FullName, Utility.Get24PxImageFrom16PxImg(service.Info.Icon));
					item.ImageKey = service.GetType().FullName;
				}

				var stat = ((service is IResourceProvider) ? AppContext.Instance.Statistics.SearchService : AppContext.Instance.Statistics.DownloadService)?.GetValue(service.Info.Name);

				item.SubItems.AddRange(new[]
				{
					stat==null?"--" :stat.UsageCount.ToString("N0"),
					stat==null?"--" :stat.SuccessCount.ToString("N0"),
					//stat==null||(service is IResourceProvider)?"--" :stat.FailedCount.ToString("N0"),
					stat==null?"--" :stat.ErrorCount.ToString("N0")
				});
				item.Group = (service is IResourceProvider) ? lv.Groups[0] : lv.Groups[1];
				item.Tag = service;

				list.Add(item);
			}

			lv.Items.AddRange(list.ToArray());
			lv.ItemChecked += Lv_ItemChecked;
			lv.SelectedIndexChanged += Lv_SelectedIndexChanged;
			lnkSupport.Click += LnkSupport_Click;
			lnkSupport.Enabled = false;
		}

		private void LnkSupport_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(lnkSupport.Tag.ToString());
			}
			catch (Exception)
			{
			}
		}

		private void Lv_SelectedIndexChanged(object sender, EventArgs e)
		{
			var item = lv.FocusedItem;
			if (item == null)
			{
				lblAuthor.Text = lblVersion.Text = "";
				lnkSupport.Enabled = false;
				txtDesc.Text = "选定引擎查看详情";

				return;
			}

			var service = (IServiceBase)item.Tag;
			var info = service.Info;

			txtDesc.Text = info.Descrption;
			lblVersion.Text = info.Version.ToString();
			lblAuthor.Text = info.Author;
			lnkSupport.Tag = info.Contract;
			lnkSupport.Enabled = !info.Contract.IsNullOrEmpty();
		}

		private void Lv_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			var item = e.Item;
			var provider = item.Tag as IServiceBase;

			provider.Disabled = !item.Checked;
			item.SubItems[2].Text = provider.Disabled ? "已禁用" : "正常";

			if (provider.Disabled)
			{
				if (provider is IResourceProvider)
				{
					AppContext.Instance.Options.DisabledSearchProviders.SafeAdd(provider.Info.Name);
				}
				else
				{
					AppContext.Instance.Options.DisableDownloadProviders.SafeAdd(provider.Info.Name);
				}
			}
			else
			{
				if (provider is IResourceProvider)
				{
					AppContext.Instance.Options.DisabledSearchProviders.Remove(provider.Info.Name);
				}
				else
				{
					AppContext.Instance.Options.DisableDownloadProviders.Remove(provider.Info.Name);
				}
			}
		}
	}
}
