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
	class MonovaSearchProvider : AbstractSearchServiceProvider<BuildinServerInfo, SiteInfo>, IResourceProvider
	{
		protected MonovaSearchProvider(BuildinServerInfo info) : base(info)
		{
			SupportResourceInitialMark = false;
			SupportLookupTorrentContents = false;
			SupportSortType = SortType.FileSize | SortType.PubDate | SortType.Default | SortType.Title;
			RequireBypassGfw = false;
			SupportUnicode = false;
			PageCheckKey = @"content=""monova.org""";
			SiteID = SiteData.SiteID_Monova;

		}

		public MonovaSearchProvider()
			: this(new BuildinServerInfo("Monova", Properties.Resources.favicon_monova, "提供对 monova[www] 的搜索支持"))
		{
		}

		/// <summary>
		/// 连接
		/// </summary>
		public override void Connect()
		{
			DefaultHost = "www.monova.org";
			ReferUrlPage = $"https://{Host}/";
			base.Connect();
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrlCore(IResourceInfo resource, SiteInfo data)
		{
			return $"https://{Host}/torrent/{data.SiteID}/{HttpUtility.UrlEncode(data.PageName)}.html";
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
			if (!base.LoadCore(context, url, htmlContent, result))
				return false;

			var doc = new HtmlDocument();
			doc.LoadHtml(htmlContent);

			var listTable = doc.DocumentNode.SelectSingleNode("//table");
			if (listTable == null)
				return true;

			var rows = listTable.SelectNodes("tbody/tr[not(contains(@class, 'success'))]");
			var downloader = AppContext.Instance.DownloadServiceProviders.FirstOrDefault(s => s is MonovaDownloadProvider);

			if (rows != null)
			{
				foreach (HtmlNode row in rows)
				{
					//var cls = row.GetAttributeValue("class", "");
					//if (!cls.IsNullOrEmpty() && cls.IndexOf("success", StringComparison.OrdinalIgnoreCase) != -1)
					//	continue;

					var namelink = row.SelectSingleNode("td[1]/a");
					var link = namelink.Attributes["href"].Value;
					var name = namelink.InnerText;

					var item = new ResourceInfo()
					{
						Title = name,
						Provider = this,
						UpdateTimeDesc = row.SelectSingleNode("td[6]").InnerText,
						DownloadSize = row.SelectSingleNode("td[5]").InnerText,
						PreferDownloadProvider = downloader
					};

					var info = Regex.Match(link, @"torrent/(\d+)/([^""]*?)\.html", RegexOptions.IgnoreCase);
					//可能是广告链接
					if (!info.Success)
						continue;
					item.SiteData = new SiteInfo()
					{
						SiteID = info.Groups[1].Value.ToInt64(),
						PageName = info.Groups[2].Value
					};

					result.Add(item);
				}
			}

			//分页

			result.HasPrevious = result.PageIndex > 1;
			result.HasMore = (doc.DocumentNode.SelectSingleNode("//ul[contains(@class,'pagination')]/li[last()]")?.GetAttributeValue("class", "") ?? "active").IndexOf("active", StringComparison.OrdinalIgnoreCase) == -1;

			return base.LoadCore(context, url, htmlContent, result);
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			var sort = "";
			switch (sortType)
			{
				case SortType.Default:
					break;
				case SortType.Title:
					sort = "3";
					break;
				case SortType.PubDate:
					sort = "1";
					break;
				case SortType.FileSize:
					sort = "4";
					break;
				default:
					break;
			}

			return $"https://{Host}/search?sort={sort}&term={HttpUtility.UrlEncode(key)}&start={pageindex - 1}";
		}

		/// <summary>
		/// 加载内容
		/// </summary>
		public override void LookupTorrentContents(IResourceInfo torrent)
		{
			base.LookupTorrentContents(torrent);

			var data = GetExtraData(torrent);
			var url = $"https://{Host}.monova.org/torrent/files/{data.SiteID}/{HttpUtility.UrlEncode(data.PageName)}.html";

			var ctx = NetworkClient.Create<string>(HttpMethod.Get, url, ReferUrlPage).Send();
			if (!ctx.IsValid())
				return;

			var doc = new HtmlDocument();
			doc.LoadHtml(ctx.Result);

			var filesTable = doc.GetElementbyId($"i_files{data.SiteID}");
			if (filesTable == null)
				return;

			//是否有文件记录？
			if (filesTable.SelectSingleNode(".//tr[1][count(td)=1]") != null)
				return;

			var files = filesTable.SelectNodes(".//tr");
			foreach (HtmlNode htmlNode in files)
			{
				var file = new FileNode()
				{
					Name = htmlNode.ChildNodes[0].InnerText,
					SizeString = htmlNode.ChildNodes[1].InnerText
				};
				torrent.Add(file);
			}
		}


		/// <summary>
		/// 加载相信信息
		/// </summary>
		protected override void LoadFullDetailCore(IResourceInfo info)
		{
			if (info.IsHashLoaded && (info.SiteData == null || (info.SiteData as SiteInfo).ProvideSiteDownload != null))
				return;

			//load detail page
			var data = GetExtraData(info);
			var page = GetDetailUrl(info);

			var ctx = NetworkClient.Create<string>(HttpMethod.Get, page, ReferUrlPage).Send();
			if (!ctx.IsValid())
				return;

			((ResourceInfo)info).Hash = Regex.Match(ctx.Result, @"Hash:</span>([a-z\d]{40})", RegexOptions.Singleline | RegexOptions.IgnoreCase).GetGroupValue(1) ?? "";
			//有直接下载链接？
			var downloadLink = Regex.Match(ctx.Result, "href=\"([^\"]+?/torrent/download/[^\"]+)\"", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			if (downloadLink.Success)
			{
				data.ProvideSiteDownload = true;
				data.SiteDownloadLink = $"https:{downloadLink.Groups[1].Value}";
			}
			else
			{
				data.ProvideSiteDownload = false;
			}

			base.LoadFullDetailCore(info);
		}
	}
}
