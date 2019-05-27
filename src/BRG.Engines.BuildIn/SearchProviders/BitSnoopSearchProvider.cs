namespace BRG.Engines.BuildIn.SearchProviders
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.Text.RegularExpressions;
	using System.Web;
	using BRG.Engines.Entities;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;
	using HtmlAgilityPack;

	[Export(typeof(IResourceProvider))]
	class BitSnoopSearchProvider : AbstractSearchServiceProvider<BuildinServerInfo, SiteInfo>, IResourceProvider
	{
		public BitSnoopSearchProvider()
			: base(new BuildinServerInfo("BitSnoop", Properties.Resources.favicon_bitsnoop, "提供对BitSnoop的搜索支持"))
		{
			SupportCustomizePageSize = false;
			SupportLookupTorrentContents = true;
			SupportResourceInitialMark = false;
			SupportSortType = SortType.Default | SortType.FileSize | SortType.PubDate;
			SupportUnicode = true;
			RequireBypassGfw = false;

			ReferUrlPage = "http://bitsnoop.com/";
			PageCheckKey = "<title>Bitsnoop";
			SiteID = SiteData.SiteID_BitSnoop;
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrlCore(IResourceInfo resource, SiteInfo data)
		{
			return $"http://bitsnoop.com/{HttpUtility.UrlEncode(data.PageName)}-q{data.SiteID}.html";
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			var sortkey = "c";
			if (sortType == SortType.FileSize)
				sortkey = "z";
			else if (sortType == SortType.PubDate)
				sortkey = "a";
			var sortDir = sortDirection == 0 ? "a" : "d";


			return $"http://bitsnoop.com/search/all/{HttpUtility.UrlEncode(key)}+fakes%3Ayes+safe%3Ano/{sortkey}/{sortDir}/{pageindex}/";
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

			var rootNode = doc.GetElementbyId("torrents");
			if (rootNode == null)
				return true;

			var items = rootNode.SelectNodes("li");
			foreach (var item in items)
			{
				var titlelink = item.SelectSingleNode("a[1]");
				var href = Regex.Match(titlelink.Attributes["href"].Value, "/([^\\s\'\"]+?)\\-q(\\d+)\\.html", RegexOptions.IgnoreCase | RegexOptions.Singleline);
				var descNode = item.SelectSingleNode(".//table//td[1]");
				var size = descNode.SelectSingleNode("text()")?.InnerText ?? "";
				var filecount = Regex.Match(descNode.SelectSingleNode("div").InnerText ?? "", @"(\d+)\s*files").GetGroupValue(1).ToInt32();

				var res = CreateResourceInfo(null, titlelink.InnerText);
				res.DownloadSize = size;
				res.FileCount = filecount;
				res.SiteData = new SiteInfo()
				{
					PageName = href.GetGroupValue(1),
					SiteID = href.GetGroupValue(2).ToInt64()
				};

				result.Add(res);
			}


			return base.LoadCore(context, url, htmlContent, result);
		}

		/// <summary>
		/// 加载相信信息
		/// </summary>
		protected override void LoadFullDetailCore(IResourceInfo info)
		{
			var url = GetDetailUrl(info);
			var ctx = NetworkClient.Create<string>(HttpMethod.Get, url, ReferUrlPage).Send();
			if (!ctx.IsValid())
				return;

			var res = (ResourceInfo)info;
			res.Hash = Regex.Match(ctx.Result, @"btih:([a-z\d]{40})", RegexOptions.Singleline | RegexOptions.IgnoreCase).GetGroupValue(1);

			//加载文件
			var doc = new HtmlDocument();
			doc.LoadHtml(ctx.Result);
			var files = doc.GetElementbyId("files").SelectNodes("./table//tr");
			var nodesChain = new Stack<FileNode>();
			FileNode currentNode = null;
			var previousLevel = 0;

			foreach (var file in files)
			{
				var indent = (int)Math.Round(Regex.Match(file.SelectSingleNode("td[1]").GetAttributeValue("style", "") ?? "", @"([\d\.]+)\s*em", RegexOptions.IgnoreCase).GetGroupValue(1).ToDouble() / 0.5);
				var isFolder = file.SelectSingleNode("./td[1]/span[contains(@class,'dir')]") != null;
				var name = file.SelectSingleNode("./td[1]/text()").InnerText;

				//检测缩进
				if (previousLevel == 0 || previousLevel < indent)
					previousLevel = indent;
				else
				{
					//当前缩进小于之前的缩进，文件夹弹出。
					while (indent < previousLevel--)
					{
						currentNode = nodesChain.Count > 0 ? nodesChain.Pop() : null;
					}
					previousLevel = indent;
				}

				if (isFolder)
				{
					var folderNode = new FileNode()
					{
						IsDirectory = true,
						Name = name
					};
					previousLevel++;

					if (currentNode != null)
						nodesChain.Push(currentNode);
					(currentNode?.Children ?? info).Add(folderNode);
					currentNode = folderNode;
				}
				else
				{

					var fileNode = new FileNode()
					{
						IsDirectory = false,
						Name = name,
						SizeString = file.SelectSingleNode("./td[2]").InnerText
					};
					(currentNode?.Children ?? info).Add(fileNode);
				}

			}

			base.LoadFullDetailCore(info);
		}
	}
}
