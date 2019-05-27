namespace BRG.Engines.BuildIn.SearchProviders
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.Linq;
	using System.Text.RegularExpressions;
	using BRG.Engines.Entities;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;

	[Export(typeof(IResourceProvider))]
	class ExtraTorrentSearchProvider : AbstractSearchServiceProvider<BuildinServerInfo, SiteInfo>
	{
		public ExtraTorrentSearchProvider() : base(new BuildinServerInfo("ExtraTorrent", Properties.Resources.favicon_extratorrent, "提供对 ExtraTorrent 的搜索支持"))
		{
			RequireBypassGfw = false;
			SupportCustomizePageSize = false;
			SupportLookupTorrentContents = true;
			SupportResourceInitialMark = false;
			SupportSortType = null;
			SupportUnicode = false;
			RequireBypassGfw = false;
			RequireBypassGfw = false;
			PageCheckKey = "<title>ExtraTorrent";
			ReferUrlPage = "http://extratorrent.cc/";
			SiteID = SiteData.SiteID_ExtraTorrent;
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			return $"http://extratorrent.cc/search/?page={pageindex}&search={UE(key)}&s_cat=&srt=added&pp=50&order=desc";
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrlCore(IResourceInfo resource, SiteInfo data)
		{
			return $"http://extratorrent.cc/torrent/{data.SiteID}/{UE(data.PageName)}.html";
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
			var doc = CreateHtmlDocument(htmlContent);
			var rows = doc.DocumentNode.SelectNodes("//table[@class='tl']/tr");

			if (rows != null)
			{
				foreach (var row in rows)
				{
					var titlelink = row.SelectSingleNode("td[3]/a");
					var title = titlelink.InnerText;
					var linkinfo = Regex.Match(titlelink.GetAttributeValue("href", ""), @"torrent/(\d+)/(.*?)\.html", RegexOptions.IgnoreCase);
					var size = row.SelectSingleNode("td[4]").InnerText;

					var res = CreateResourceInfo(null, title);
					res.SiteData = new SiteInfo()
					{
						SiteID = linkinfo.GetGroupValue(1).ToInt64(),
						PageName = linkinfo.GetGroupValue(2)
					};
					res.DownloadSize = size;

					result.Add(res);
				}

				var currentPage = doc.DocumentNode.SelectSingleNode("//b[@class='pager_no_link']");
				result.HasPrevious = currentPage?.PreviousSibling != null;
				result.HasMore = currentPage?.NextSibling != null;
			}

			return base.LoadCore(context, url, htmlContent, result);
		}

		/// <summary>
		/// 加载相信信息
		/// </summary>
		protected override void LoadFullDetailCore(IResourceInfo info)
		{
			if (info.IsHashLoaded)
				return;

			var url = GetDetailUrl(info);
			var ctx = NetworkClient.Create<string>(HttpMethod.Get, url, ReferUrlPage).Send();
			if (!ctx.IsValid())
				return;

			((ResourceInfo)info).Hash = Regex.Match(ctx.Result, @"btih:([\da-z]{40})", RegexOptions.Singleline | RegexOptions.IgnoreCase).GetGroupValue(1);

			base.LoadFullDetailCore(info);
		}

		/// <summary>
		/// 加载内容
		/// </summary>
		public override void LookupTorrentContents(IResourceInfo torrent)
		{
			if (!torrent.IsHashLoaded)
				LoadFullDetail(torrent);

			//如果已经加载，则跳过
			if (torrent.Count > 0)
				return;

			var data = GetExtraData(torrent);
			var url = $"http://extratorrent.cc/torrent_files/{data.SiteID}/{UE(data.PageName)}.html";
			var htmlContext = NetworkClient.Create<string>(HttpMethod.Get, url, ReferUrlPage).Send();
			if (!htmlContext.IsValid())
				return;

			var html = htmlContext.Result;
			//修正错误的html结构
			html = Regex.Replace(html, @"(<td[^>]+>\s*<img[^>]+>)<td", "$1</td><td", RegexOptions.Singleline | RegexOptions.IgnoreCase);

			var doc = CreateHtmlDocument(html);
			var rows = doc.DocumentNode.SelectNodes("//form[@name='frm']/preceding::table[1]//tr");
			if (rows == null)
				return;

			var chain = new Stack<FileNode>();
			FileNode currentNode = null;

			foreach (var row in rows)
			{
				var tds = row.SelectNodes("td");
				var level = tds.Count - 3;
				var isFolder = tds[tds.Count - 2].SelectSingleNode("img").GetAttributeValue("alt", "").IndexOf("folder") != -1;

				if (level < chain.Count)
				{
					while (chain.Count > level)
					{
						chain.Pop();
						currentNode = chain.Peek();
					}
				}

				if (isFolder)
				{
					var name = tds.Last().SelectSingleNode("b").InnerText;
					var fnode = new FileNode() { Name = name, IsDirectory = true };
					if (currentNode != null)
					{
						currentNode.Children.Add(fnode);
						currentNode = fnode;
						chain.Push(fnode);
					}
					else
					{
						torrent.Add(fnode);
						chain.Push(fnode);
						currentNode = fnode;
					}
				}
				else
				{
					var name = tds.Last().SelectSingleNode("text()").InnerText;
					var size = tds.Last().SelectSingleNode("font").InnerText.Trim('(', ')');
					var lnode = new FileNode() { Name = name, SizeString = size };
					(currentNode?.Children ?? torrent).Add(lnode);
				}
			}
		}
	}
}
