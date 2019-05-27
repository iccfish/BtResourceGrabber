namespace BRG.Engines.BuildIn.SearchProviders
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.Text.RegularExpressions;
	using BRG.Engines.BuildIn.DownloadProviders;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;
	using HtmlAgilityPack;

	[Export(typeof(IResourceProvider))]
	class DiggBtSearchProvider : AbstractSearchServiceProvider<BuildinServerInfo, SiteInfo>
	{
		static readonly Dictionary<string, string> _keyMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		public DiggBtSearchProvider() : base(new BuildinServerInfo("DiggBT", Properties.Resources.favicon_diggbt_16, "提供对DiggBT的搜索支持"))
		{
			SetPreferDownloader<SobtDownloadProvider>();
			SupportCustomizePageSize = false;
			RequireBypassGfw = false;
			SupportUnicode = true;
			SupportLookupTorrentContents = true;
			SupportOption = false;
			SupportResourceInitialMark = true;
			SupportSortType = SortType.Default | SortType.PubDate | SortType.FileSize;
			PageCheckKey = "搜索神器";
			SiteID = SiteData.SiteID_DiggBT;
		}

		/// <summary>
		/// 连接
		/// </summary>
		public override void Connect()
		{
			base.Connect();

			DefaultHost = "diggbt.net";
			ReferUrlPage = $"http://{Host}/";
		}

		protected virtual string PrepareSearchKey(string key)
		{
			var ek = _keyMap.GetValue(key);
			if (!ek.IsNullOrEmpty())
				return ek;

			var task = NetworkClient.Create<string>(HttpMethod.Post, ReferUrlPage, ReferUrlPage, new { s = key }).Send();
			if (!task.IsRedirection)
				return null;

			if (task.Redirection.Current.Host != Host)
			{
				Host = task.Redirection.Current.Host;
				return PrepareSearchKey(key);
			}



			//http://diggbt.com/search/Ky5JzEtRSKpUyE0FAA.html
			ek = Regex.Match(task.Redirection.Current.PathAndQuery, @"/search/(.*?)\.html", RegexOptions.IgnoreCase).GetGroupValue(1);
			if (!ek.IsNullOrEmpty())
			{
				_keyMap.AddOrUpdate(key, ek);
			}

			return ek;
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			var sort = 0;
			if (sortDirection == 1)
			{
				switch (sortType)
				{
					case SortType.Default:
						break;
					case SortType.Title:
						break;
					case SortType.PubDate:
						sort = 1;
						break;
					case SortType.FileSize:
						sort = 2;
						break;
					default:
						break;
				}
			}

			var nk = PrepareSearchKey(key);
			if (nk.IsNullOrEmpty())
				throw new InvalidOperationException();

			return $"http://{Host}/search/{nk}/{pageindex}/{sort}.html";
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrlCore(IResourceInfo resource, SiteInfo data)
		{
			return $"http://{Host}/{_itemPageKey}/{data.PageName}.html";
		}

		string _itemPageKey;

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

			//节点
			var nodes = doc.DocumentNode.SelectNodes(@"//div[@class='item']");
			if (nodes != null)
			{
				foreach (HtmlNode node in nodes)
				{
					var titleLink = node.SelectSingleNode("div[@class='item-title']/a");
					if (titleLink == null)
						continue;

					//pageid;http://diggbt.com/item/BcEBDQAwCAMwSx9h5HbGOP4lvO206MSCOsT1qDCMjiKbAz0Z3g8.html
					var pageinfo = Regex.Match(titleLink.GetAttributeValue("href", ""), @"/([^/]+?)/([^/]+?)\.html", RegexOptions.IgnoreCase);
					var pageid = pageinfo.GetGroupValue(2);
					_itemPageKey = pageinfo.GetGroupValue(1).DefaultForEmpty(_itemPageKey);
					var title = titleLink.InnerText;

					//detail spans.
					var detailSpans = node.SelectNodes("div[@class='item-detail']/span");
					//hash
					//var hash = Regex.Match(detailSpans[0].SelectSingleNode("a").GetAttributeValue("href", ""), @"([A-F\d]{40})").GetGroupValue(1);
					var hash = Regex.Replace(Regex.Match(detailSpans[0].SelectSingleNode("script").InnerText, @":([a-f'""\+\d]+)&", RegexOptions.IgnoreCase).GetGroupValue(1), @"['""+]", "").Trim('&', ':');
					//date
					var date = detailSpans[1].SelectSingleNode("b").InnerText;
					//size
					var size = detailSpans[2].SelectSingleNode("b").InnerText;
					//fc
					var fc = Regex.Match(detailSpans[3].SelectSingleNode("b").InnerText, @"(\d+)").GetGroupValue(1).ToInt32();

					var item = CreateResourceInfo(hash, title);
					item.FileCount = fc;
					item.DownloadSize = size;
					item.UpdateTimeDesc = date;
					item.SiteData = new SiteInfo()
					{
						SiteID = 99999999999L,
						PageName = pageid,
						ProvideSiteDownload = true,
						SiteDownloadLink = $"http://storebt.com/down/{pageid}/torrent.torrent"
					};

					result.Add(item);
				}
			}
			//hasmore?
			result.HasPrevious = result.PageIndex > 1;

			var totalPage = Regex.Match(htmlContent, @"<span>共\s*(\d+)\s*页</span>", RegexOptions.IgnoreCase).GetGroupValue(1).ToInt32();
			result.HasMore = totalPage > result.PageIndex;


			return base.LoadCore(context, url, htmlContent, result);
		}

		/// <summary>
		/// 加载内容
		/// </summary>
		/// <param name="url"></param>
		public override void LookupTorrentContentsCore(string url, IResourceInfo torrent, string htmlContent)
		{
			var doc = new HtmlDocument();
			doc.LoadHtml(htmlContent);

			var nodes = doc.DocumentNode.SelectNodes("//dl[@class='filelist']//table//tr[position()>1]");
			if (nodes != null)
			{
				foreach (var node in nodes)
				{
					var size = node.SelectSingleNode("td[1]").InnerText;
					var filepath = node.SelectSingleNode("td[2]").InnerText;

					AddFileNode(torrent, filepath, null, size);
				}
			}

			base.LookupTorrentContentsCore(url, torrent, htmlContent);
		}
	}
}
