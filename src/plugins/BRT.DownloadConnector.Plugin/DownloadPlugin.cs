using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRT.DownloadConnector.Plugin
{
	using System.ComponentModel.Composition;
	using System.Diagnostics;
	using System.Drawing;
	using System.IO;
	using System.Text.RegularExpressions;
	using System.Windows.Forms;

	using BRG;
	using BRG.Entities;
	using BRG.Service;

	using UI.Dialogs;

	[Export(typeof(IResourceContextMenuAddin))]
	public class DownloadPlugin : AddinBase, IResourceContextMenuAddin
	{
		public DownloadPlugin()
		{
			Info = new CommonServiceInfo("使用下载工具下载扩展", Properties.Resources.Downloads, new Version(FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion), "木鱼", "iccfish@qq.com", "我好懒啊。")
			{

			};

		}

		ToolStripMenuItem _parentItem;
		static DownloadOption _option;
		static event EventHandler RequireRefreshMenu;

		/// <summary>
		/// 引发 <see cref="RequireRefreshMenu" /> 事件
		/// </summary>
		/// <param name="sender">引发此事件的源对象</param>
		static void OnRequireRefreshMenu(object sender)
		{
			var handler = RequireRefreshMenu;
			if (handler != null)
				handler(sender, EventArgs.Empty);
		}


		/// <summary>
		/// 注册
		/// </summary>
		/// <param name="ctx"></param>
		/// <param name="providers"></param>
		public void Register(ContextMenuStrip ctx, IResourceProvider[] providers)
		{
			_option = AppContext.Instance.ConfigLoader.Load<DownloadOption>();

			_parentItem = new ToolStripMenuItem("使用下载工具下载", Properties.Resources.Downloads);
			//_parentItem.Font = new Font(_parentItem.Font, FontStyle.Bold);
			ctx.Items.Insert(1, _parentItem);

			//迅雷
			CheckThunder();

			//QQ旋风
			CheckQQD();

			//utorrent
			CheckuTorrent();

			//分割线
			var itemsep = new ToolStripSeparator();

			//自定义
			var itemcustomize = new ToolStripMenuItem("自定义下载工具...");
			itemcustomize.Click += Itemcustomize_Click;

			_parentItem.DropDownItems.Add(itemsep);
			_parentItem.DropDownItems.Add(itemcustomize);

			RefreshCustomizeMenu();
			RequireRefreshMenu += (s, e) =>
			{
				RefreshCustomizeMenu();
			};
		}

		void CheckQQD()
		{
			var reg = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey("qqdl");

			if (reg == null)
				return;

			reg.Close();
			var itemq = new ToolStripMenuItem("使用QQ旋风下载", Properties.Resources.qqdownloader_16);
			itemq.Click += Itemq_Click;
			_parentItem.DropDownItems.Add(itemq);
		}

		void CheckThunder()
		{
			var reg = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey("thunder");
			if (reg == null)
				return;

			reg.Close();
			var item = new ToolStripMenuItem("使用迅雷下载", Properties.Resources.thunder_16);
			item.Click += (s, e) =>
			{
				var reses = Current;
				if (reses == null || !reses.Any())
					return;

				foreach (var info in reses)
				{
					var link = BrtUtility.BuildDetailLink(info);
					if (link.IsNullOrEmpty())
						continue;

					var slink = "thunder://" + Convert.ToBase64String(Encoding.UTF8.GetBytes($"AA{link}ZZ"));
					try
					{
						Process.Start(slink);
					}
					catch (Exception)
					{
						return;
					}
				}
			};
			_parentItem.DropDownItems.Add(item);
		}

		void CheckV6Speed()
		{
			var reg = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(@"v6player\shell\open\command");
			if (reg == null)
				return;

			var command = (reg.GetValue("") ?? "").ToString();
			var filepath = DownUtility.GetFilePath(command);
			if (filepath.IsNullOrEmpty())
				return;

			reg.Close();
			var item = new ToolStripMenuItem("使用V6Player下载", Properties.Resources.v6Speed_16);
			item.Click += (s, e) =>
			{
				var reses = Current;
				if (reses == null || !reses.Any())
					return;

				foreach (var info in reses)
				{
					var link = BrtUtility.BuildDetailLink(info);
					if (link.IsNullOrEmpty())
						continue;

					try
					{
						Process.Start(filepath, link);
					}
					catch (Exception)
					{
						return;
					}
				}
			};
			_parentItem.DropDownItems.Add(item);
		}


		void CheckuTorrent()
		{
			var path = Environment.ExpandEnvironmentVariables("%AppData%\\uTorrent\\uTorrent.exe");
			if (!File.Exists(path))
			{
				return;
			}

			var item = new ToolStripMenuItem("用uTorrent下载(仅支持BT资源)", Properties.Resources.uTorrent_16_16_32);
			item.Click += (s, e) =>
			{
				var reses = Current;
				if (reses == null || !reses.Any())
					return;

				foreach (var info in reses.Where(x => x.ResourceType == ResourceType.BitTorrent))
				{
					var link = BrtUtility.BuildDetailLink(info);
					if (link.IsNullOrEmpty())
						continue;

					try
					{
						Process.Start(path, "\"" + link + "\"");
					}
					catch (Exception)
					{
						return;
					}
				}
			};
			_parentItem.DropDownItems.Add(item);
		}

		private void Itemcustomize_Click(object sender, EventArgs e)
		{
			using (var dlg = new DownloadParameterConfig() { Parameters = _option.DownloadParameters })
			{
				dlg.ShowDialog(AppContext.Instance.MainForm);
				OnRequireRefreshMenu(this);
				AppContext.Instance.ConfigLoader.Save(_option);
			}
		}

		private void Itemq_Click(object sender, EventArgs e)
		{
			var reses = Current;
			if (reses == null || !reses.Any())
				return;

			foreach (var info in reses)
			{
				var link = BrtUtility.BuildDetailLink(info);
				if (link.IsNullOrEmpty())
					continue;

				var slink = "qqdl://" + Convert.ToBase64String(Encoding.UTF8.GetBytes(link));
				try
				{
					Process.Start(slink);
				}
				catch (Exception)
				{
					return;
				}
			}
		}


		/// <summary>
		/// 打开的时候回调
		/// </summary>
		/// <param name="resourceInfos"></param>
		public void OnOpening(params IResourceInfo[] resourceInfos)
		{
			_parentItem.Tag = resourceInfos;
			_parentItem.Enabled = resourceInfos.Any();
		}

		IResourceInfo[] Current { get { return _parentItem.Tag as IResourceInfo[]; } }

		void RefreshCustomizeMenu()
		{
			var sepIndex = _parentItem.DropDownItems.Cast<ToolStripItem>().TakeWhile(s => s.GetType() != typeof(ToolStripSeparator)).Count();
			var currentItems = _parentItem.DropDownItems.OfType<ToolStripMenuItem>().Where(s => s.Tag != null).ToArray();

			var itemNames = _option.DownloadParameters.Keys.MapToHashSet(StringComparer.OrdinalIgnoreCase);
			//移除无效的
			foreach (var s1 in currentItems.Where(s => !itemNames.Contains(s.Tag as string)).ToArray())
			{
				_parentItem.DropDownItems.Remove(s1);
			}
			//加入新增的
			foreach (var s1 in itemNames.Except(currentItems.Select(s => s.Tag as string), StringComparer.OrdinalIgnoreCase))
			{
				var item = new ToolStripMenuItem(s1) { Tag = s1 };
				_parentItem.DropDownItems.Insert(sepIndex, item);

				item.Click += LaunchCustomeItem;
			}
		}

		private void LaunchCustomeItem(object sender, EventArgs e)
		{
			var reses = Current;
			if (reses == null || !reses.Any())
				return;

			var dp = _option.DownloadParameters.GetValue(((sender as ToolStripMenuItem).Tag as string) ?? "");
			if (dp == null)
				return;

			foreach (var info in reses)
			{
				var link = BrtUtility.BuildDetailLink(info);
				if (link.IsNullOrEmpty())
					continue;

				var path = BindInfo(dp.Path, info, link);
				var parameter = BindInfo(dp.Parameter, info, link);
				try
				{
					Process.Start(path, parameter);
				}
				catch (Exception)
				{
					return;
				}
			}
		}

		string BindInfo(string template, IResourceInfo resource, string link)
		{
			if (template.IsNullOrEmpty())
				return string.Empty;

			template = Regex.Replace(template, @"\${([a-z]+)}", _ =>
			{
				if (_.Groups[1].Value.IsIgnoreCaseEqualTo("url"))
					return link;
				if (_.Groups[1].Value.IsIgnoreCaseEqualTo("base64(url)"))
					return BitConverter.ToString(Encoding.UTF8.GetBytes(link));
				if (_.Groups[1].Value.IsIgnoreCaseEqualTo("urlencode(url)"))
					return System.Web.HttpUtility.UrlEncode(link);

				return _.Value;
			});

			return template;
		}
	}
}
