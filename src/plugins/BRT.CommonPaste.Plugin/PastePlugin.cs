using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRT.CommonPaste.Plugin
{
	using System.ComponentModel.Composition;
	using System.Diagnostics;
	using System.Web;
	using System.Windows.Forms;
	using BRG;
	using BRG.Entities;
	using BRG.Service;

	/// <summary>
	/// 常规复制粘贴扩展
	/// </summary>
	[Export(typeof(IResourceContextMenuAddin))]
	public class PastePlugin : AddinBase, IResourceContextMenuAddin
	{
		public PastePlugin()
		{
			Info = new CommonServiceInfo("复制到剪贴板扩展", Properties.Resources.paste_plain, new Version(FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion), "木鱼", "iccfish@qq.com", "我好懒啊。")
			{

			};
		}

		ToolStripMenuItem _parentMenuItem;

		/// <summary>
		/// 注册
		/// </summary>
		/// <param name="ctx"></param>
		/// <param name="providers"></param>
		public void Register(ContextMenuStrip ctx, IResourceProvider[] providers)
		{
			ctx.Items.Add(new ToolStripSeparator());

			_parentMenuItem = new ToolStripMenuItem("复制信息/搜索引擎搜索", Properties.Resources.paste_plain);
			ctx.Items.Add(_parentMenuItem);

			var copyItemInfo = new ToolStripMenuItem("复制标题和下载链接到剪贴板");
			_parentMenuItem.DropDownItems.Add(copyItemInfo);
			copyItemInfo.Click += _copyToClipboardMenuItem_Click;

			var copyTitle = new ToolStripMenuItem("复制标题");
			_parentMenuItem.DropDownItems.Add(copyTitle);
			copyTitle.Click += (s, e) =>
			{
				var res = _parentMenuItem.Tag as IResourceInfo[];
				var text = res.Select(x => x.Title).Distinct(StringComparer.OrdinalIgnoreCase).JoinAsString(Environment.NewLine);
				Clipboard.SetText(text);
			};


			var copyHash = new ToolStripMenuItem("复制特征码");
			_parentMenuItem.DropDownItems.Add(copyHash);
			copyHash.Click += (s, e) =>
			{
				var res = _parentMenuItem.Tag as IResourceInfo[];
				var text = res.Select(x => x.Hash).Where(x => !x.IsNullOrEmpty()).Distinct(StringComparer.OrdinalIgnoreCase).JoinAsString(Environment.NewLine);
				Clipboard.SetText(text);
			};

			var searchBaidu = new ToolStripMenuItem("使用百度搜索", Properties.Resources.favicon);
			_parentMenuItem.DropDownItems.Add(searchBaidu);
			searchBaidu.Click += (s, e) =>
			{
				var res = (_parentMenuItem.Tag as IResourceInfo[]).First();
				var url = "https://www.baidu.com/s?wd=" + System.Web.HttpUtility.UrlEncode(res.Title);
				Process.Start(url);
			};

			var searchGoogle = new ToolStripMenuItem("使用Google搜索", Properties.Resources.favicon_google);
			_parentMenuItem.DropDownItems.Add(searchGoogle);
			searchGoogle.Click += (s, e) =>
			{
				var res = (_parentMenuItem.Tag as IResourceInfo[]).First();
				var url = "https://www.google.com/#q=" + System.Web.HttpUtility.UrlEncode(res.Title);
				Process.Start(url);
			};
		}

		private static void GenerateDownloadInfos(IResourceInfo[] res, StringBuilder sb)
		{
			foreach (var info in res)
			{
				switch (info.ResourceType)
				{
					case ResourceType.BitTorrent:
						sb.Append("磁力链：");
						break;
					case ResourceType.Ed2K:
						sb.Append("电驴下载：");
						break;
					case ResourceType.NetDisk:
						if (info.NetDiskData == null)
							continue;

						switch (info.NetDiskData.NetDiskType)
						{
							case NetDiskType.Unknown:
								sb.Append("网盘下载：");
								break;
							case NetDiskType.BaiduDesk:
								sb.Append("百度网盘：");
								break;
							case NetDiskType.XlShare:
								sb.Append("迅雷快传：");
								break;
							case NetDiskType.QqXf:
								sb.Append("QQ旋风分享：");
								break;
							case NetDiskType.QihuShare:
								sb.Append("360网盘分享：");
								break;
							case NetDiskType.Weiyun:
								sb.Append("微云分享：");
								break;
						}
						break;
					case ResourceType.MultiResource:
						GenerateDownloadInfos(info.SubResources, sb);
						continue;
				}
				sb.Append("【");
				sb.Append(info.Title);
				sb.AppendLine("】");

				sb.AppendLine(BrtUtility.BuildDetailLink(info));
				sb.AppendLine();
			}
		}
		private void _copyToClipboardMenuItem_Click(object sender, EventArgs e)
		{
			var res = _parentMenuItem.Tag as IResourceInfo[];
			var sb = new StringBuilder();

			GenerateDownloadInfos(res, sb);

			Clipboard.SetText(sb.ToString());
		}


		/// <summary>
		/// 打开的时候回调
		/// </summary>
		/// <param name="resourceInfos"></param>
		public void OnOpening(params IResourceInfo[] resourceInfos)
		{
			_parentMenuItem.Tag = resourceInfos;
			_parentMenuItem.Enabled = resourceInfos.Any();
		}
	}
}
