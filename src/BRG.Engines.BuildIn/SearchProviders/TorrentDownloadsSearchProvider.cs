using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRG.Engines.BuildIn.SearchProviders
{
	using System.ComponentModel.Composition;
	using System.Text.RegularExpressions;
	using BRG.Engines.Entities;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;
	using HtmlAgilityPack;

	[Export(typeof(IResourceProvider))]
	class TorrentDownloadsSearchProvider : AbstractSearchServiceProvider<BuildinServerInfo, SiteInfo>, IResourceProvider
	{
		public TorrentDownloadsSearchProvider() : base(new BuildinServerInfo("TorrentDownloads.me", Properties.Resources.favicon_torrentdownloads, "提供对TorrentDownloads.me的搜索支持"))
		{
			ReferUrlPage = "http://www.torrentdownloads.me/";
			PageCheckKey = "UA-652486-45";
			RequireBypassGfw = false;
			SupportLookupTorrentContents = true;
			SupportOption = false;
			SupportResourceInitialMark = false;
			SupportUnicode = false;
			SupportSortType = null;

			SiteID = SiteData.SiteID_TorrentDownload_Me;
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			return $"http://www.torrentdownloads.me/search/?page={pageindex}&search={UE(key)}";
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrlCore(IResourceInfo resource, SiteInfo data)
		{
			return $"http://www.torrentdownloads.me/torrent/{data.SiteID}/{UE(data.PageName)}";
		}

		/// <summary>
		/// 核心加载
		/// </summary>
		/// <param name="context"></param>
		/// <param name="url"></param>
		/// <param name="htmlContent">HTML内容</param>
		/// <param name="result">目标结果</param>
		/// <returns></returns>
		protected override bool LoadCore(HttpContext<string> context, string url, string htmlContent, ResourceSearchInfo result)
		{
			var doc = new HtmlDocument();
			doc.LoadHtml(htmlContent);

			var nodes = doc.DocumentNode.SelectNodes("//div[@class='inner_container']//div[contains(@class,'grey_bar3')]");
			if (nodes != null)
			{
				foreach (var node in nodes)
				{
					var titlelink = node.SelectSingleNode("p/a");
					if (titlelink == null || titlelink.GetAttributeValue("href", "").IndexOf("torrent/", StringComparison.Ordinal) == -1)
						continue;

					var link = titlelink.GetAttributeValue("href", "");
					//torrent/1653923478/Game+of+Thrones+S02E05+The+Ghost+Of+Harrenhal+HDTV+XviD-2HD+%5Beztv%5D
					var linkreg = Regex.Match(link, @"torrent/(\d+)/(.+)", RegexOptions.IgnoreCase);
					var data = new SiteInfo()
					{
						SiteID = linkreg.GetGroupValue(1).ToInt64(),
						PageName = UD(linkreg.GetGroupValue(2))
					};
					var title = titlelink.InnerText;
					var size = node.SelectSingleNode("span[4]").InnerText;

					var item = CreateResourceInfo(null, title);
					item.DownloadSize = size;
					item.SiteData = data;

					result.Add(item);
				}
			}
			result.HasPrevious = result.PageIndex > 1;
			result.HasMore = htmlContent.IndexOf("rel=\"nofollow\">&gt;&gt;</a></li>") != -1;


			return base.LoadCore(context, url, htmlContent, result);
		}

		protected override void LoadFullDetailCore(IResourceInfo info)
		{
			var url = GetDetailUrl(info);
			var docCtx = NetworkClient.Create<string>(HttpMethod.Get, url, ReferUrlPage).Send();
			if (docCtx.IsValid())
			{
				var html = docCtx.Result;

				//hash
				var hash = Regex.Match(html, @"torrent/([a-f\d]{40})", RegexOptions.IgnoreCase).GetGroupValue(1);
				if (!hash.IsNullOrEmpty())
				{
					info.Hash = hash;

					LookupTorrentContentsCore(url, info, html);
				}
			}

			base.LoadFullDetailCore(info);
		}

		/// <summary>
		/// 加载内容
		/// </summary>
		/// <param name="url"></param>
		public override void LookupTorrentContentsCore(string url, IResourceInfo torrent, string htmlContent)
		{
			var currentindent = 0;
			var doc = new HtmlDocument();
			doc.LoadHtml(htmlContent);
			var nodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'grey_bar2')]");
			var stack = new Stack<FileNode>();

			foreach (var node in nodes)
			{
				var name = node.SelectSingleNode("p/text() | p/b")?.InnerText;
				var size = node.SelectSingleNode("span")?.InnerText;

				if (name.IsNullOrEmpty())
					continue;
				var indent = node.SelectNodes("p/span")?.Count ?? 0;
				var cnode = new FileNode()
				{
					Name = name,
					SizeString = size
				};
				if (indent > currentindent && stack.Count > 0)
				{
					var pNode = stack.Peek();
					//压栈
					pNode.IsDirectory = true;
					pNode.Children.Add(cnode);
					stack.Push(cnode);
					currentindent = indent;
				}
				else if (indent < currentindent)
				{
					//出栈
					while (--currentindent != indent)
					{
						if (stack.Count > 0)
							stack.Pop();
					}
					var pNode = stack.Peek();
					(pNode?.Children ?? torrent).Add(cnode);
					stack.Push(cnode);
				}
				else
				{
					if (stack.Count > 0)
						stack.Pop();

					var pNode = (stack.Count > 0) ? stack.Peek() : null;
					(pNode?.Children ?? torrent).Add(cnode);
					stack.Push(cnode);
				}
			}

			base.LookupTorrentContentsCore(url, torrent, htmlContent);
		}
	}
}
