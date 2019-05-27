namespace BRG.Engines.BuildIn.SearchProviders
{
	using System;
	using System.ComponentModel.Composition;
	using System.Linq;
	using System.Text.RegularExpressions;
	using System.Web;
	using BRG.Engines.BuildIn.DownloadProviders;
	using BRG.Engines.Entities;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;
	using HtmlAgilityPack;

	[Export(typeof(IResourceProvider))]
	class IsoHuntSearchProvider : AbstractSearchServiceProvider<BuildinServerInfo, SiteInfo>, IResourceProvider
	{
		public IsoHuntSearchProvider() : base(new BuildinServerInfo("IsoHunt", Properties.Resources.favicon_isohunt, "提供对Isohunt的搜索支持"))
		{
			RequireBypassGfw = false;
			SupportUnicode = false;
			SupportResourceInitialMark = false;
			SupportSortType = SortType.FileSize | SortType.Default | SortType.PubDate;
			SupportCustomizePageSize = true;
			SupportLookupTorrentContents = true;

			ReferUrlPage = "https://isohunt.to/";
			PageCheckKey = "torrent search engine";
			SiteID = SiteData.SiteID_IsoHunt;
		}


		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrlCore(IResourceInfo resource, SiteInfo data)
		{
			return $"https://isohunt.to/torrent_details/{data.SiteID}/{HttpUtility.UrlEncode(data.PageName)}";
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			var sort = "";
			var sortTypeValue = sortDirection == 0 ? "asc" : "desc";

			switch (sortType)
			{
				case SortType.Default:
					break;
				case SortType.Title:
					break;
				case SortType.PubDate:
					sort = "created_at";
					break;
				case SortType.FileSize:
					sort = "size";
					break;
				default:
					break;
			}

			return $"https://isohunt.to/torrents/?ihq={HttpUtility.UrlEncode(key)}&Torrent_sort={sort}.{sortTypeValue}&Torrent_page={((pageindex - 1) * pagesize)}";
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

			var div = doc.GetElementbyId("serps");
			if (div == null)
				return false;

			var rows = div.SelectNodes("//tr[@data-key!='']");
			var downloader = AppContext.Instance.DownloadServiceProviders.FirstOrDefault(s => s is IsoHuntDownloadProvider);
			foreach (var row in rows)
			{
				var titleLink = row.SelectSingleNode("td[2]/a[1]");
				var item = new ResourceInfo()
				{
					Title = titleLink.InnerText,
					Provider = this,
					PreferDownloadProvider = downloader,
					UpdateTimeDesc = row.ChildNodes[4].InnerText,
					DownloadSize = row.ChildNodes[5].InnerText
				};

				var href = titleLink.Attributes["href"].Value;
				var hrefInfo = Regex.Match(href, @"torrent_details/(\d+)/([^'""]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
				item.SiteData = new SiteInfo()
				{
					SiteID = hrefInfo.Groups[1].Value.ToInt64(),
					PageName = hrefInfo.Groups[2].Value
				};

				result.Add(item);
			}

			return base.LoadCore(context, url, htmlContent, result);
		}

		/// <summary>
		/// 加载相信信息
		/// </summary>
		protected override void LoadFullDetailCore(IResourceInfo info)
		{
			if (info.IsHashLoaded && (info.SiteData == null || (info.SiteData as SiteInfo).ProvideSiteDownload != null))
				return;

			var detailurl = GetDetailUrl(info);
			var ctx = NetworkClient.Create<string>(HttpMethod.Get, detailurl, ReferUrlPage, allowAutoRedirect: true).Send();
			if (!ctx.IsValid())
				return;

			//Hash
			((ResourceInfo)info).Hash = (Regex.Match(ctx.Result, @"Hash=([a-z\d]{40})", RegexOptions.IgnoreCase | RegexOptions.Singleline).GetGroupValue(1) ?? "").ToUpper();
			//download url
			var downloadLink = Regex.Match(ctx.Result, @"""([^""]*?isohunt[^""]*?/download\.php\?id[^""]*?)""", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			var data = GetExtraData(info);
			if (downloadLink.Success)
			{
				data.SiteDownloadLink = downloadLink.Groups[1].Value;
				data.ProvideSiteDownload = true;
			}
			else
			{
				data.ProvideSiteDownload = false;
			}

			base.LoadFullDetailCore(info);
		}

		/// <summary>
		/// 加载内容
		/// </summary>
		/// <param name="url"></param>
		public override void LookupTorrentContentsCore(string url, IResourceInfo torrent, string htmlContent)
		{
			var files = Regex.Matches(htmlContent, @"<tr>.*?<td.*?fileRows"">(.*?)</td>\s*<td.*?fileRowsRight"">(.*?)</td>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			foreach (Match match in files)
			{
				AddFileNode(torrent, match.Groups[1].Value, null, match.Groups[2].Value);
			}
			base.LookupTorrentContentsCore(url, torrent, htmlContent);
		}
	}
}
