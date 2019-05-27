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

	[Export(typeof(IResourceProvider))]
	class SeedPeerSearchProvider : AbstractSearchServiceProvider<BuildinServerInfo, SiteInfo>, IResourceProvider
	{
		public SeedPeerSearchProvider() : base(new BuildinServerInfo("SeedPeer", Properties.Resources.favicon_seedpeer, "提供对 SeedPeer 的搜索支持"))
		{
			RequireBypassGfw = false;
			SupportCustomizePageSize = false;
			SupportLookupTorrentContents = true;
			SupportResourceInitialMark = false;
			SupportSortType = null;
			SupportUnicode = false;
			RequireBypassGfw = false;
			ReferUrlPage = "http://www.seedpeer.eu/";
			PageCheckKey = "/rss.xml";
			SiteID = SiteData.SiteID_SeedPeer;
			SetPreferDownloader<SeedPeerDownloadProvider>();
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			return $"http://www.seedpeer.eu/search/{HttpUtility.UrlEncode(key.Replace(' ', '-'))}/7/{pageindex}.html";
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrlCore(IResourceInfo resource, SiteInfo data)
		{
			return $"http://www.seedpeer.eu/details/{data.SiteID}/{HttpUtility.UrlEncode(data.PageName)}.html";
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
			var table = doc.DocumentNode.SelectNodes("//div[@id='headerbox']/following::table")?.LastOrDefault(s => s.SelectSingleNode(".//tr[1]/th[6]") != null);
			var rows = table?.SelectNodes(".//tr[position()>1]");
			if (rows != null)
			{
				foreach (var row in rows)
				{
					var titlelink = row.SelectSingleNode("td[1]/a");
					var title = titlelink.InnerText;
					var pinfo = Regex.Match(titlelink.GetAttributeValue("href", ""), @"details/(\d+)/(.*?)\.html", RegexOptions.IgnoreCase);
					if (!pinfo.Success)
						continue;

					var item = CreateResourceInfo(null, title);
					item.SiteData = new SiteInfo()
					{
						SiteID = pinfo.GetGroupValue(1).ToInt64(),
						PageName = pinfo.GetGroupValue(2)
					};
					item.UpdateTimeDesc = row.SelectSingleNode("td[2]").InnerText;
					item.DownloadSize = row.SelectSingleNode("td[3]").InnerText;

					result.Add(item);
				}

				var pagination = doc.GetElementbyId("pagination");
				if (pagination != null)
				{
					var cp = pagination.SelectSingleNode("a[@class='selected']");
					result.HasMore = cp.NextSibling != null;
					result.HasPrevious = cp.PreviousSibling != null;
				}
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

			var tinfo = (ResourceInfo)info;
			tinfo.Hash = Regex.Match(ctx.Result, @"download/[^/]+/([a-z\d]{40})", RegexOptions.IgnoreCase).GetGroupValue(1);
			LookupTorrentContentsCore(url, info, ctx.Result);

			base.LoadFullDetailCore(info);
		}

		/// <summary>
		/// 加载内容
		/// </summary>
		/// <param name="url"></param>
		public override void LookupTorrentContentsCore(string url, IResourceInfo torrent, string htmlContent)
		{
			var doc = CreateHtmlDocument(htmlContent);
			var tinfo = (ResourceInfo)torrent;

			//files
			var files = doc.GetElementbyId("body").SelectNodes("table//tr[position()>1]");
			if (files != null)
			{
				foreach (var file in files)
				{
					if (file.SelectSingleNode("td[2]") == null)
						continue;

					AddFileNode(tinfo, file.SelectSingleNode("td[1]").InnerText.Trim(), null, file.SelectSingleNode("td[2]").InnerText.Trim());
				}
			}

			base.LookupTorrentContentsCore(url, torrent, htmlContent);
		}
	}
}
