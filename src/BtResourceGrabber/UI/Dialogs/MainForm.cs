using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BtResourceGrabber.Entities;
using BtResourceGrabber.Service;
using BtResourceGrabber.UI.Controls;

namespace BtResourceGrabber.UI.Dialogs
{
	using System.Diagnostics;
	using System.Reflection;

	using BRG;
	using BRG.Entities;
	using BtResourceGrabber.UI.Dialogs.ConfigUi;
	using BtResourceGrabber.UI.Dialogs.Engines;
	using BtResourceGrabber.UI.Dialogs.Messages;
	using FSLib;
	using FSLib.Extension.FishLib;

	partial class MainForm : FunctionalForm
	{
		public MainForm()
		{
			AppContext.Instance.MainForm = this;
			InitializeComponent();

			KeyPreview = true;
			ResourceOperation.AttachForm(this);
			KeyUp += MainForm_KeyUp;
			Load += MainForm_Load;
			Shown += MainForm_Shown;

		}

		public void ShowFloatTip(string msg)
		{
			if (InvokeRequired)
			{
				this.Invoke(ShowFloatTip, msg);
			}
			else
			{
				tip.Show(msg, this, 0, 0, 3000);
			}
		}

		void MainForm_KeyUp(object sender, KeyEventArgs e)
		{
			if (!e.Control || e.KeyCode != Keys.C)
				return;

			//复制
			var engine = el.SelectedEngineUI.CurrentSelection;
			if (engine == null || engine.Count == 0)
				return;

			AppContext.Instance.ResourceOperation.CopyMagnetLink(engine.Select(s => s.FindResourceToCopyLink()).ExceptNull().ToArray());
		}

		void MainForm_Shown(object sender, EventArgs e)
		{
			//代理提示
			if (!AppContext.Instance.Options.ProxyWarningShown)
			{
				if (Question("您需要变更网络设置吗？"))
				{
					var dlg = new ConfigCenter() { StartPosition = FormStartPosition.CenterParent };
					dlg.SelectConfigUI<NetworkOption>();
					dlg.ShowDialog(this);
				}
				AppContext.Instance.Options.ProxyWarningShown = true;
			}

			if (!string.IsNullOrEmpty(Program.NewFeatureVersion) && AppContext.Instance.Options.NewFeatureVersion != Program.NewFeatureVersion)
			{
				new NewFeature().ShowDialog(this);
			}
		}

		void MainForm_Load(object sender, EventArgs e)
		{
			InitUI();
			InitSearch();

			//更新规则
			if (AppContext.Instance.Options.RssRuleCollection != null)
				AppContext.Instance.Options.RssRuleCollection.Keys.ForEach(s => AppContext.Instance.Options.RssRuleCollection.CheckUpdate(s));
		}

		#region UI处理

		void InitUI()
		{
			tsInfos.Click += (s, e) => new EngineStatistics().ShowDialog(this);
			tsOption.Click += (s, e) => new ConfigCenter().ShowDialog(this);
			tsDownloadManually.Click += (s, e) => new PromptTorrentInfo().ShowDialog(this);

			var opt = AppContext.Instance.Options;
			stStrictFilter.Checked = opt.StrictFilter;
			stShowMultilineTab.Checked = opt.MultilineEngineTab;
			tsUsingFloat.Checked = opt.UsingFloatTip;
			opt.PropertyChanged += (s, e) =>
			{
				if (e.IsPropertyOf(opt, x => x.StrictFilter))
					stStrictFilter.Checked = opt.StrictFilter;
				else if (e.IsPropertyOf(opt, x => x.MultilineEngineTab))
					stShowMultilineTab.Checked = opt.MultilineEngineTab;
				else if (e.IsPropertyOf(opt, x => x.UsingFloatTip))
					tsUsingFloat.Checked = opt.UsingFloatTip;
			};
			stStrictFilter.CheckedChanged += (s, e) => opt.StrictFilter = stStrictFilter.Checked;
			stShowMultilineTab.CheckedChanged += (s, e) => opt.MultilineEngineTab = stShowMultilineTab.Checked;
			tsUsingFloat.CheckedChanged += (s, e) => opt.UsingFloatTip = tsUsingFloat.Checked;
			stStatistics.Click += (s, e) => new EngineStatistics().ShowDialog(this);

			cbKey.AddDataBinding(opt, s => s.Text, s => s.LastSearchKey);
			cbKey.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			cbKey.DataSource = (opt.SearchTermsHistory ?? (opt.SearchTermsHistory = new List<string>()));
			cbKey.AutoCompleteSource = AutoCompleteSource.ListItems;
			opt.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == "SearchTermsHistory")
				{
					cbKey.DataSource = opt.SearchTermsHistory;
				}
			};

			if (opt.LoadPages == 0)
				opt.LoadPages = 5;
			cbLoadCount.DataSource = new[] { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50 };
			cbLoadCount.DataBindings.Add("SelectedItem", opt, "LoadPages");

			var sortKeys = typeof(SortType).GetEnumDescription().Select(s => new KeyValuePair<SortType, string>((SortType)s.Value, s.DescriptionText)).ToArray();
			cbSort.DataSource = sortKeys;
			cbSort.DisplayMember = "Value";
			cbSort.SelectedItem = cbSort.Items.Cast<KeyValuePair<SortType, string>?>().FirstOrDefault(s => s.Value.Key == opt.SortType) ?? cbSort.Items[0];
			opt.PropertyChanged += (x, e) =>
			{
				cbSort.SelectedItem = cbSort.Items.Cast<KeyValuePair<SortType, string>?>().FirstOrDefault(s => s.Value.Key == opt.SortType) ?? cbSort.Items[0];
			};
			cbSort.SelectedIndexChanged += (s, e) =>
			{
				if (cbSort.SelectedItem == null)
					return;

				opt.SortType = ((KeyValuePair<SortType, string>)cbSort.SelectedItem).Key;
			};
			cbSortDirection.AddDataBinding(opt, s => s.SelectedIndex, s => s.SortDirection);

			//标题
			Text = string.Format(Text, ApplicationRunTimeContext.GetProcessMainModule().FileVersionInfo.FileVersion, ApplicationRunTimeContext.GetProcessMainModule().FileVersionInfo.FileMajorPart);
			foreach (var label in ss.Items.OfType<ToolStripStatusLabel>().Where(s => s.IsLink && s.Tag != null))
			{
				label.Click += (s, e) =>
				{
					Process.Start((s as ToolStripStatusLabel).Tag.ToString());
				};
			}
			tsEngineTest.Click += (s, e) =>
			{
				new EngineAvailabilityTest().ShowDialog();
			};

			pTip.KeepCenter();

			//TODO 干掉不支持的方法
			tsTools.Enabled = false;
		}

		#endregion

		#region 搜索动作

		void InitSearch()
		{
			cbKey.TextChanged += (s, e) =>
			{
			};
			cbKey.KeyUp += (s, e) =>
			{
				if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(cbKey.Text))
					RunSearch();
			};
			btnGo.Click += (s, e) => RunSearch();
		}


		/// <summary>
		/// 开始搜索
		/// </summary>
		void RunSearch()
		{
			pTip.Visible = false;

			var key = cbKey.Text;
			if (string.IsNullOrEmpty(key))
			{
				Information("请输入搜索关键字 ￣□￣｜｜");
				return;
			}

			//加到历史记录？
			AppendToHistory(key);

			//搜索
			el.SearchKey = key;
		}

		void AppendToHistory(string key)
		{
			if (!AppContext.Instance.Options.LogSearchHistory)
				return;

			var cnt = AppContext.Instance.Options.SearchTermsHistory;
			if (cnt.Contains(key, StringComparer.OrdinalIgnoreCase))
				return;

			cnt.Add(key);
			if (cnt.Count > 200)
				cnt.RemoveAt(0);
			cbKey.ResetBindings();
		}

		#endregion
	}
}
