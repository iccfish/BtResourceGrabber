namespace BtResourceGrabber.UI.Dialogs.ConfigUi
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Runtime.Remoting.Contexts;
	using System.Windows.Forms;
	using BRG;
	using BRG.Entities;
	using BRG.Service;
	using BtResourceGrabber.UI.Dialogs.Engines;
	using Entities;

	using FSLib.Windows.Controls.Common;
	using FSLib.Windows.Forms;

	using Service;

	public partial class Option : FunctionalForm
	{
		List<string> _maskSrc;

		public Option()
		{
			InitializeComponent();

			Load += Option_Load;
			FormClosing += Option_FormClosing;
		}

		void Option_FormClosing(object sender, FormClosingEventArgs e)
		{
			AppContext.Instance.OnRequestRefreshMarkCollection(this);
		}

		void Option_Load(object sender, EventArgs e)
		{
			InitState();
			InitMaskEdit();

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

		void InitState()
		{
			var opt = AppContext.Instance.Options;

			chkAutoMark.AddDataBinding(opt, s => s.Checked, s => s.EnableAutoMark);
			gpAutoMark.AddDataBinding(chkAutoMark, s => s.Enabled, s => s.Checked);
			cbAutoMarkType.DataSource = _maskSrc = opt.HashMarks.Keys.ToList();
			cbAutoMarkType.DataBindings.Add("SelectedItem", opt, "AutoMarkDownloadedTorrent");
			chkTabMultiLine.AddDataBinding(opt, s => s.Checked, s => s.MultilineEngineTab);
			chkUsingFloat.AddDataBinding(opt, s => s.Checked, s => s.UsingFloatTip);
			nudTimeout.AddDataBinding(opt, s => s.IntValue, s => s.NetworkTimeout);
			chkEnableFilterDirective.AddDataBinding(opt, s => s.Checked, s => s.EnableSearchDirective);
			chkLogDownload.AddDataBinding(opt, s => s.Checked, s => s.LogDownloadHistory);
			chkRecSearchHistory.AddDataBinding(opt, s => s.Checked, s => s.LogSearchHistory);
			chkEnablePreview.AddDataBinding(opt, s => s.Checked, s => s.EnablePreviewIfPossible);
			chkEnableCloudSaftyCheck.AddDataBinding(opt, s => s.Checked, s => s.EnableCloudSaftyCheckOverride);

			//引擎配置
			InitEngineConfig();

			chkLogDownload.Text += $" [{AppContext.Instance.DownloadHistory.Count:N0}记录]";
			chkRecSearchHistory.Text += $" [{AppContext.Instance.Options.SearchTermsHistory.Count:N0}关键字]";
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
			btnEngineInfo.Click += (s, e) => new EngineStatistics().ShowDialog(this);
		}

		private void btnClearHistory_Click(object sender, EventArgs e)
		{
			if (!this.Question("确定要清空所有搜索记录吗？", true))
				return;

			AppContext.Instance.Options.SearchTermsHistory = new List<string>();
			AppContext.Instance.Options.LastSearchKey = "";
			Information("操作成功。");
		}


		#region 标记类型

		class MaskBoxItem : ListBoxExItem
		{
			HashMark _mask;

			public HashMark Mask
			{
				get
				{
					return _mask;
				}
				set
				{
					_mask = value;
					ForeColor = value.Color;
				}
			}

			public string MaskName
			{
				get { return DisplayText; }
				set { DisplayText = value; }
			}

			/// <summary>
			/// 创建 <see cref="MaskBoxItem" />  的新实例(MaskBoxItem)
			/// </summary>
			/// <param name="mask"></param>
			public MaskBoxItem(string name, HashMark mask)
			{
				_mask = mask;

				ForeColor = mask.Color;
				DisplayText = name;
			}
		}

		void InitMaskEdit()
		{
			var opt = AppContext.Instance.Options;
			lbMask.Items.AddRange(opt.HashMarks.Select(s => (object)new MaskBoxItem(s.Key, s.Value)).ToArray());

			btnMaskAdd.Click += (s, e) =>
			{
				using (var dlg = new SelectMark())
				{
					if (dlg.ShowDialog() != DialogResult.OK)
						return;

					opt.HashMarks.Add(dlg.MaskName, dlg.HashMark);
					lbMask.Items.Add(new MaskBoxItem(dlg.MaskName, dlg.HashMark));
					lbMask.SelectedIndex = lbMask.Items.Count - 1;
					_maskSrc.Add(dlg.MaskName);
				}
				cbAutoMarkType.ResetBindings();
			};
			btnMaskEdit.Click += (s, e) =>
			{
				if (lbMask.SelectedItem == null)
					return;

				var item = lbMask.SelectedItem as MaskBoxItem;
				var prevMaskName = item.MaskName;
				using (var dlg = new SelectMark() { MaskName = item.DisplayText, HashMark = item.Mask })
				{
					if (dlg.ShowDialog() != DialogResult.OK)
						return;

					item.Mask = dlg.HashMark;
					item.MaskName = dlg.MaskName;
					lbMask.Invalidate();

					if (item.MaskName == prevMaskName)
					{
						opt.HashMarks[item.MaskName] = item.Mask;
					}
					else
					{
						opt.HashMarks.Remove(prevMaskName);
						opt.HashMarks.Add(item.MaskName, item.Mask);

						//更新
						foreach (var key in AppContext.Instance.HashMarkCollection.Keys)
						{
							if (AppContext.Instance.HashMarkCollection[key] == prevMaskName)
								AppContext.Instance.HashMarkCollection[key] = item.MaskName;
						}
					}
				}
			};
			btnMaskDelete.Click += (s, e) =>
			{
				if (lbMask.SelectedItem == null)
					return;

				var item = lbMask.SelectedItem as MaskBoxItem;
				if (!Question("确定要删除标记 【" + item.MaskName + "】吗？"))
					return;

				opt.HashMarks.Remove(item.MaskName);
				lbMask.Items.Remove(item);

				_maskSrc.Remove(item.MaskName);
				cbAutoMarkType.ResetBindings();
			};
		}

		public int CurrentTabIndex
		{
			get { return tabControl1.SelectedIndex; }
			set { tabControl1.SelectedIndex = value; }
		}

		#endregion

		private void btnEngineStandalone_Click(object sender, EventArgs e)
		{
			new Engines.StandaloneEngineSetting().ShowDialog();
		}

		private void btnClearDownloadHistory_Click(object sender, EventArgs e)
		{
			if (!this.Question("确定要清空所有下载记录吗？", true))
				return;

			AppContext.Instance.DownloadHistory.Clear();
			Information("操作成功。");
		}
	}
}
