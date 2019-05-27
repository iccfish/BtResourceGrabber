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
	class TorLockSearchProvider : AbstractSearchServiceProvider<BuildinServerInfo, SiteInfo>, IResourceProvider
	{
		public TorLockSearchProvider() : base(new BuildinServerInfo("TorLock", Properties.Resources.favicon_torlock, "提供对 TorLock 的搜索支持"))
		{
			RequireBypassGfw = false;
			SupportCustomizePageSize = false;
			SupportLookupTorrentContents = true;
			SupportResourceInitialMark = false;
			SupportSortType = SortType.Default | SortType.PubDate | SortType.FileSize;
			SupportUnicode = false;
			RequireBypassGfw = false;
			ReferUrlPage = "https://www.torlock.com/";
			PageCheckKey = "TorLock</title>";
			EnableAutoRedirect = true;

			SiteID = SiteData.SiteID_TorLock;
			SetPreferDownloader<TorlockDownloadProvider>();
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			var order = sortDirection == 1 ? "desc" : "asc";
			var sort = sortType == SortType.Default ? "" : sortType == SortType.FileSize ? "size" : sortType == SortType.PubDate ? "added" : "";

			key = key.Replace(' ', '-');

			return $"https://www.torlock.com/all/torrents/{UE(key).ToLower()}.html?sort={sort}&page={pageindex}&order={order}";
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrlCore(IResourceInfo resource, SiteInfo data)
		{
			return $"https://www.torlock.com/torrent/{data.SiteID}/{HttpUtility.UrlEncode(data.PageName).ToLower()}.html";
		}

		/// <summary>
		/// 加载相信信息
		/// </summary>
		protected override void LoadFullDetailCore(IResourceInfo info)
		{
			if (!info.IsHashLoaded)
			{
				var data = info.PreferDownloadProvider.Download(info);
				//load hash
				((ResourceInfo)info).Data = data;
				//report
				//if (data != null)
				//	AppContext.Instance.CloudService.ReportDownload(info.Hash, info.Title, GetDetailUrl(info), Info.Name, info.DownloadSize, data);
			}

			base.LoadFullDetailCore(info);
		}

		/// <summary>
		/// 加载内容
		/// </summary>
		/// <param name="url"></param>
		public override void LookupTorrentContentsCore(string url, IResourceInfo torrent, string htmlContent)
		{
			var doc = new HtmlDocument();
			doc.LoadHtml(htmlContent);

			var nodes = doc.DocumentNode.SelectNodes("//article/table[last()]//tr[position()>1]");
			if (nodes != null)
			{
				foreach (var node in nodes)
				{
					AddFileNode(torrent, node.SelectSingleNode("td[2]").InnerText.Replace('/', '\\'), null, node.SelectSingleNode("td[3]").InnerText);
				}
			}

			base.LookupTorrentContentsCore(url, torrent, htmlContent);
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

			var nodes = doc.DocumentNode.SelectNodes("//article/table[last()]//tr[position()>1]");
			if (nodes != null)
			{
				foreach (var node in nodes)
				{
					var titlelink = node.SelectSingleNode("td[1]//a");
					//torrent/2733310/1pondo-011014_734-hd.html
					var siteinfoReg = Regex.Match(titlelink.GetAttributeValue("href", ""), @"torrent/(\d+)/(.*?)\.html");

					var item = CreateResourceInfo(null, titlelink.InnerText);
					item.SiteData = new SiteInfo()
					{
						SiteID = siteinfoReg.GetGroupValue(1).ToInt64(),
						PageName = siteinfoReg.GetGroupValue(2),
						ProvideSiteDownload = true,
						SiteDownloadLink = $"https://www.torlock.com/tor/{siteinfoReg.GetGroupValue(1).ToInt64()}.torrent"
					};

					item.UpdateTimeDesc = node.SelectSingleNode("td[2]")?.InnerText;
					item.UpdateTime = item.UpdateTimeDesc.ToDateTimeNullable();
					item.DownloadSize = node.SelectSingleNode("td[3]")?.InnerText;

					result.Add(item);
				}
			}
			result.HasPrevious = doc.DocumentNode.SelectSingleNode("//div[@id='pag']/a[@class='sel']/preceding-sibling::*") != null;
			result.HasMore = doc.DocumentNode.SelectSingleNode("//div[@id='pag']/a[@class='sel']/following-sibling::*") != null;

			return base.LoadCore(context, url, htmlContent, result);
		}
	}
}
