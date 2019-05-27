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
	using System.IO;
	using BRG;
	using BRG.Entities;

	using FSLib.Extension.FishLib;
	using FSLib.Network.Http;
	using Newtonsoft.Json;

	partial class FilterConfig : ConfigControl
	{
		public FilterConfig()
		{
			InitializeComponent();

			Text = "搜索结果过滤";
			Image = Properties.Resources.trash_16;

			ilLv.Images.Add(Utility.Get24PxImageFrom16PxImg(Properties.Resources.rss));
			tsAdd.Click += (s, e) =>
			{
				var dlg = new RuleEditor();
				if (dlg.ShowDialog(this) != DialogResult.OK)
					return;

				AppContext.Instance.Options.RuleCollection.Add(dlg.Rule);
				var item = new ListViewItem(new[] {dlg.Rule.Rules.JoinAsString(";"), dlg.Rule.IsRegex ? "是" : ""})
							{
								Tag = dlg.Rule,
								ImageIndex = dlg.Rule.Behaviour == FilterBehaviour.Hide ? 1 : 2
							};
				lvCustomize.Items.Add(item);
				item.EnsureVisible();
			};
			tsEdit.Enabled = false;
			lvCustomize.SelectedIndexChanged += (s, e) => tsEdit.Enabled = lvCustomize.SelectedIndices.Count > 0;
			tsEdit.Click += (s, e) =>
			{
				var oldItem = lvCustomize.SelectedItems.OfType<ListViewItem>().FirstOrDefault();
				if (oldItem == null)
					return;

				var dlg = new RuleEditor() { Rule = oldItem.Tag as FilterRule };
				if (dlg.ShowDialog(this) != DialogResult.OK)
					return;

				AppContext.Instance.Options.RuleCollection.Remove(oldItem.Tag as FilterRule);
				oldItem.Remove();


				AppContext.Instance.Options.RuleCollection.Add(dlg.Rule);
				var item = new ListViewItem(new[] {dlg.Rule.Rules.JoinAsString(";"), dlg.Rule.IsRegex ? "是" : ""})
							{
								Tag = dlg.Rule,
								ImageIndex = dlg.Rule.Behaviour == FilterBehaviour.Hide ? 1 : 2
							};
				lvCustomize.Items.Add(item);
				item.EnsureVisible();
			};
			tsRemove.Click += (s, e) =>
			{
				var items = lvCustomize.SelectedItems.OfType<ListViewItem>().ToArray();
				lvCustomize.BeginUpdate();

				foreach (var item in items)
				{
					var rule = item.Tag as FilterRule;
					AppContext.Instance.Options.RuleCollection.Remove(rule);
					lvCustomize.Items.Remove(item);
				}

				lvCustomize.EndUpdate();
			};
			tsExport.Click += (s, e) =>
			{
				var items = lvCustomize.SelectedItems.OfType<ListViewItem>().ToArray();
				if (!items.Any())
					items = lvCustomize.Items.OfType<ListViewItem>().ToArray();

				if (!items.Any())
				{
					this.Information("没有可以导出的规则...");
					return;
				}

				var rules = items.Select(x => x.Tag as FilterRule).ToArray();

				var saveDlg = new SaveFileDialog()
				{
					Title = "导出规则...",
					Filter = "过滤规则文件(*.btflt)|*.btflt",
					FileName = "自定义规则.btflt"
				};
				if (saveDlg.ShowDialog() != DialogResult.OK)
					return;

				File.WriteAllBytes(saveDlg.FileName, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(rules)).Compress());
			};
		}

		private async void FilterConfig_Load(object sender, EventArgs e)
		{
			lvCustomize.Items.AddRange(AppContext.Instance.Options.RuleCollection.Select(s =>
			{
				var item = new ListViewItem(new[] { s.Rules.JoinAsString(";"), s.IsRegex ? "是" : "" }) { Tag = s };
				item.ImageIndex = s.Behaviour == FilterBehaviour.Hide ? 1 : 2;
				return item;
			}).ToArray());

			//在线列表
			var client = new HttpClient();
			var ctx = client.Create(HttpMethod.Post, "https://www.fishlee.net/app/getfiles/605", result: new
			{
				response = CollectionUtility.CreateAnymousTypeList(new { id = 0, name = "", downloadCount = 0, updateTime = DateTime.Now }, 4)
			});
			await ctx.SendAsync().ConfigureAwait(true);

			var list = ctx.Result?.response;
			if (list == null || list.Count == 0)
			{
				return;
			}

			lvRss.Items.AddRange(list.Select(s =>
			{
				var item = new ListViewItem(s.name.DefaultForEmpty("未命名订阅")) { ImageIndex = 0, Checked = AppContext.Instance.Options.RssRuleCollection.ContainsKey(s.id), Tag = s.id };
				item.SubItems.AddRange(new[] { s.updateTime.MakeDateTimeFriendly(), s.downloadCount.ToString("N0") });
				return item;
			}).ToArray());
			lvRss.ItemChecked += (s, ex) =>
			{
				var item = ex.Item;
				var id = (int)item.Tag;
				if (item.Checked)
				{
					AppContext.Instance.Options.RssRuleCollection.Add(id, null);
					AppContext.Instance.Options.RssRuleCollection.CheckUpdate(id);

				}
				else
				{
					AppContext.Instance.Options.RssRuleCollection.Remove(id);
				}
			};
		}
	}
}
