namespace BRG.Engines.BuildIn.SearchProviders
{
	using System;
	using System.ComponentModel.Composition;
	using System.Net;
	using System.Text.RegularExpressions;
	using System.Web;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;
	using HtmlAgilityPack;

	[Export(typeof(IResourceProvider))]
	class ThePirateBaySearchProvider : AbstractSearchServiceProvider<BuildinServerInfo, SiteInfo>, IResourceProvider
	{
		public ThePirateBaySearchProvider() : base(new BuildinServerInfo("海盗湾", Properties.Resources.favicon_tpb, "提供对海盗湾的搜索支持"))
		{
			RequireBypassGfw = false;
			SupportCustomizePageSize = false;
			SupportResourceInitialMark = false;
			SupportLookupTorrentContents = true;
			SupportSortType = SortType.Default | SortType.FileSize | SortType.PubDate;
			SupportUnicode = false;

			PageCheckKey = @"BitTorrent site";
			SiteID = SiteData.SiteID_ThePirateBay;
		}

		/// <summary>
		/// 连接
		/// </summary>
		public override void Connect()
		{
			base.Connect();
			DefaultHost = "thepiratebay.mn";
			ReferUrlPage = $"https://{Host}/";
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrlCore(IResourceInfo resource, SiteInfo data)
		{
			return $"https://{Host}/torrent/{data.SiteID}/{HttpUtility.UrlEncode(data.PageName)}";
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			var sort = 99;

			if (sortType == SortType.FileSize)
			{
				sort = sortDirection == 0 ? 6 : 5;
			}
			else if (sortType == SortType.PubDate)
			{
				sort = sortDirection == 0 ? 4 : 3;
			}
			else
			{
				sort = sortDirection == 0 ? 0 : 99;
			}

			return $"https://{Host}/search/{HttpUtility.UrlEncode(key)}/{pageindex}/{sort}/0";
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
			if (context.Response.Status == HttpStatusCode.MovedPermanently)
			{
				var host = context.Redirection.Current?.Host;
				if (host != Host)
				{
					Property["host"] = host;
					ReferUrlPage = $"https://{Host}/";
					Host = host;
					result.RequestResearch = true;
				}
				return false;
			}

			var doc = new HtmlDocument();
			doc.LoadHtml(htmlContent);

			var table = doc.GetElementbyId("searchResult");
			if (table != null)
			{
				var rows = table.SelectNodes("//tr");

				foreach (var row in rows)
				{
					var link = row.SelectSingleNode(".//a[@class='detLink']");
					if (link == null)
						continue;

					var pageinfo = Regex.Match(link.GetAttributeValue("href", ""), @"torrent/(\d+)/(.*)", RegexOptions.IgnoreCase);
					var name = link.InnerText;

					//hash
					var hash = Regex.Match((row.SelectSingleNode("td[2]/a")).GetAttributeValue("href", ""), @"btih:([a-z\d]{40})", RegexOptions.IgnoreCase).GetGroupValue(1);

					var item = CreateResourceInfo(hash, name);
					item.SiteData = new SiteInfo()
					{
						SiteID = pageinfo.GetGroupValue(1).ToInt64(),
						PageName = pageinfo.GetGroupValue(2)
					};

					//desc
					var desc = HttpUtility.HtmlDecode(row.SelectSingleNode(".//font[@class='detDesc']").InnerText);
					//已上传 04-30 12:13, 大小 3.37 GiB, 上传者 condors369
					item.UpdateTimeDesc = Regex.Match(desc, @"((\d{2}\-\d{2})[\s\d:]+)", RegexOptions.IgnoreCase).GetGroupValue(1);
					item.DownloadSize = Regex.Match(desc, @"([\d\.]+)\s*([GTMK]*i?B)", RegexOptions.IgnoreCase).Result(@"$1 $2");

					result.Add(item);
				}
			}

			var pager = doc.DocumentNode.SelectSingleNode("//div[@id='main-content']/following::div[1]");
			if (pager != null)
			{
				result.HasMore = pager.SelectSingleNode("a[last()]/img") != null;
				result.HasPrevious = pager.SelectSingleNode("a[1]/img") != null;
			}

			return base.LoadCore(context, url, htmlContent, result);
		}

		/// <summary>
		/// 加载内容
		/// </summary>
		/// <param name="url"></param>
		public override void LookupTorrentContentsCore(string url, IResourceInfo torrent, string htmlContent)
		{
			//TODO 暂时好像服务器不支持查看。

			base.LookupTorrentContentsCore(url, torrent, htmlContent);
		}
	}
}
