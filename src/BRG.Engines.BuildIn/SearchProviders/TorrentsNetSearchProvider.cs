namespace BRG.Engines.BuildIn.SearchProviders
{
	using System;
	using System.ComponentModel.Composition;
	using System.Text.RegularExpressions;
	using System.Web;
	using BRG.Engines.BuildIn.DownloadProviders;
	using BRG.Engines.Entities;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;
	using HtmlAgilityPack;

	[Export(typeof(IResourceProvider))]
	class TorrentsNetSearchProvider : AbstractSearchServiceProvider<BuildinServerInfo, SiteInfo>, IResourceProvider
	{
		public TorrentsNetSearchProvider() : base(new BuildinServerInfo("Torrents.net", Properties.Resources.favicon_torrentsnet, "提供对 torrents.net 网站的种子搜索支持"))

		{
			RequireBypassGfw = false;
			SupportUnicode = false;
			SupportResourceInitialMark = false;
			SupportSortType = SortType.FileSize | SortType.Default | SortType.Title;
			SupportCustomizePageSize = false;
			SupportLookupTorrentContents = true;

			ReferUrlPage = "http://www.torrents.net/";
			PageCheckKey = @"Download free";
			SiteID = SiteData.SiteID_TorrentsNet;
			SetPreferDownloader<TorrentsNetDownloadProvider>();
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrlCore(IResourceInfo resource, SiteInfo data)
		{
			return $"http://www.torrents.net/torrent/{data.SiteID}/{HttpUtility.UrlEncode(data.PageName)}/";
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			//http://www.torrents.net/find/CWPBD/&page=1&sort=size&order=desc&perpage=10
			var sort = "";
			if (sortType == SortType.FileSize)
				sort = "size";
			else if (sortType == SortType.Title)
				sort = "name";
			var sortDir = sortDirection == 0 ? "asc" : "desc";


			return $"http://www.torrents.net/find/{HttpUtility.UrlEncode(key)}/&page={pageindex}&sort={sort}&order={sortDir}";
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

			var rows = doc.DocumentNode.SelectNodes("//ul[@class='table-list newly']/li");
			if (rows != null)
			{
				foreach (var row in rows)
				{
					var link = row.SelectSingleNode("./div[@class='name']/span[1]/a");
					var dlink = row.SelectSingleNode("./div[@class='name']/a[contains(@class, 'btn-download')]");

					//torrent/3344360/%5BCWPBD-76%5D.Actress.Kiss.Blow.Job.HD720p.mkv/
					var linkinfo = Regex.Match(link.GetAttributeValue("href", ""), "torrent/(\\d+)/([^/]+)", RegexOptions.IgnoreCase);
					var title = link.InnerText;

					var item = CreateResourceInfo(null, title);
					var downloadlink = dlink == null ? null : $"http://www.torrents.net/{(dlink.GetAttributeValue("href", ""))}";
					item.SiteData = new SiteInfo()
					{
						SiteID = linkinfo.GetGroupValue(1).ToInt64(),
						PageName = linkinfo.GetGroupValue(2),
						ProvideSiteDownload = !string.IsNullOrEmpty(downloadlink),
						SiteDownloadLink = downloadlink
					};
					item.DownloadSize = row.SelectSingleNode("span[@class='size']")?.InnerText;

					result.Add(item);
				}
			}

			var pagebar = doc.DocumentNode.SelectSingleNode("//div[@class='pagination-bar']");
			if (pagebar != null)
			{
				result.HasPrevious = pagebar.SelectSingleNode("a[1]/span/strike") == null;
				result.HasMore = pagebar.SelectSingleNode("a[2]/span/strike") == null;
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

			var ctx = NetworkClient.Create<string>(HttpMethod.Get, GetDetailUrl(info), ReferUrlPage).Send();
			if (!ctx.IsValid())
				return;

			var doc = new HtmlDocument();
			var tinfo = (ResourceInfo)info;
			doc.LoadHtml(ctx.Result);

			//hash
			tinfo.Hash = doc.DocumentNode.SelectSingleNode("//div[@class='info-table']//dl[5]/dd").InnerText;
			ParseDocumentFiles(tinfo, doc);

			base.LoadFullDetailCore(info);
		}

		/// <summary>
		/// 加载内容
		/// </summary>
		/// <param name="url"></param>
		public override void LookupTorrentContentsCore(string url, IResourceInfo torrent, string htmlContent)
		{
			var doc = new HtmlDocument();
			var tinfo = (ResourceInfo)torrent;
			doc.LoadHtml(htmlContent);
			ParseDocumentFiles(tinfo, doc);

			base.LookupTorrentContentsCore(url, torrent, htmlContent);
		}

		void ParseDocumentFiles(ResourceInfo tinfo, HtmlDocument doc)
		{
			//files
			var rows = doc.DocumentNode.SelectNodes("//div[@class='files-table']//ul/li");
			if (rows != null)
			{
				rows.ForEach(s =>
				{
					tinfo.Add(new FileNode()
					{
						Name = s.SelectSingleNode("span[1]").InnerText,
						SizeString = s.SelectSingleNode("span[2]").InnerText
					});
				});
			}

		}
	}
}
