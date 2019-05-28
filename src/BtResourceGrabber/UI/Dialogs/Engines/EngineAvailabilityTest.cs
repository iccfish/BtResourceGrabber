namespace BtResourceGrabber.UI.Dialogs.Engines
{
	using System;
	using System.Drawing;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Windows.Forms;
	using BRG.Entities;
	using BRG.Service;
	using BtResourceGrabber.Entities;
	using BtResourceGrabber.Service;
	using BtResourceGrabber.UI.Dialogs.ConfigUi;

	public partial class EngineAvailabilityTest : Form
	{
		public EngineAvailabilityTest()
		{
			InitializeComponent();

			Load += EngineAvailabilityTest_Load;
			//FormClosing += (s, e) =>
			//{
			//	e.Cancel = !btnRecheck.Enabled;
			//};
		}

		void EngineAvailabilityTest_Load(object sender, EventArgs e)
		{
			using (lv.CreateBatchOperationDispatcher())
			{
				lv.Items.AddRange(ServiceManager.Instance.ResourceProviders.Select(s => CreateItem(s, s.Info, s))
					.Concat(ServiceManager.Instance.DownloadServiceProviders.Select(s => CreateItem(s, s.Info, s)))
					.ToArray()
					);
			}
			btnSetProxy.Click += (s, x) =>
			{
				var dlg = new ConfigCenter();
				dlg.SelectConfigUI<NetworkOption>();
				dlg.ShowDialog(this);
			};
			btnRecheck.Click += (x, y) => RunTest();
			RunTest();
		}


		ListViewItem CreateItem<T>(object provider, IServiceInfo info, T target)
		{
			bool needProxy = false;
			if (target is ITorrentDownloadServiceProvider)
			{
				var dp = target as ITorrentDownloadServiceProvider;
				needProxy = dp.RequireBypassGfw;
			}
			else
			{
				var dp = target as IResourceProvider;
				needProxy = dp.RequireBypassGfw;
			}

			var item = new ListViewItem(new[] { info.Name, info.Version.ToString(), needProxy ? "需要" : "不需要", "等待测试..." })
			{
				Tag = target,
				UseItemStyleForSubItems = true
			};
			if (info.Icon != null)
			{
				var key = provider.GetType().FullName;
				il.Images.Add(key, Utility.Get20PxImageFrom16PxImg(info.Icon));
				item.ImageKey = key;
			}
			else
			{
				item.ImageKey = "_";
			}
			item.Group = provider is IResourceProvider ? lv.Groups[0] : lv.Groups[1];

			return item;
		}


		void RunTest()
		{
			btnOk.Enabled = btnRecheck.Enabled = btnSetProxy.Enabled = false;

			var queue = lv.Items.Cast<ListViewItem>().ToQueue();
			Task.Factory.StartNew(() =>
			{
				while (queue.Count > 0)
				{
					var nvi = queue.Dequeue();
					this.Invoke(() =>
					{
						nvi.SubItems[3].Text = "测试中...";
						nvi.ForeColor = SystemColors.ControlText;
						nvi.BackColor = SystemColors.Window;
						nvi.EnsureVisible();
					});
					var result = (nvi.Tag as IServiceBase).Test();
					if (IsDisposed)
						return;

					this.Invoke(() =>
					{
						switch (result)
						{
							case TestStatus.NotTested:
								nvi.ForeColor = Color.DarkGray;
								nvi.BackColor = Color.WhiteSmoke;
								nvi.SubItems[3].Text = "不支持测试";
								break;
							case TestStatus.Ok:
								nvi.ForeColor = Color.Green;
								nvi.BackColor = Color.FromArgb(0xD0, 0xFD, 0xD0);
								nvi.SubItems[3].Text = "可以正常访问";
								break;
							case TestStatus.Failed:
								nvi.ForeColor = Color.Red;
								nvi.BackColor = Color.FromArgb(0xFD, 0xD0, 0xD0);
								nvi.SubItems[3].Text = "无法正常访问";
								break;
						}
					});
				}
				this.Invoke(() =>
				{
					btnOk.Enabled = btnRecheck.Enabled = btnSetProxy.Enabled = true;
				});
			});
		}

	}
}
