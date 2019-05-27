namespace BRG.Engines.BuildIn.SearchProviders
{
	using System;
	using System.ComponentModel.Composition;
	using System.Text.RegularExpressions;
	using System.Web;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;
	using HtmlAgilityPack;

	[Export(typeof(IResourceProvider))]
	class BtBookSearchProvider : AbstractSearchServiceProvider<BuildinServerInfo>, IResourceProvider
	{
		public BtBookSearchProvider() : base(new BuildinServerInfo("BtBook", Properties.Resources.favicon_btbook_16, "提供对BtBook的搜索支持"))
		{
			SupportResourceInitialMark = true;
			SupportLookupTorrentContents = true;
			SupportSortType = SortType.Default | SortType.FileSize | SortType.PubDate;
			RequireBypassGfw = false;
			SupportUnicode = true;

			ReferUrlPage = "http://www.btbook.net/";
			PageCheckKey = @"changeLanguage(this.value)";
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrl(IResourceInfo resource)
		{
			return $"http://www.btbook.net/hash/{resource.Hash.ToLower()}.html";
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			var sort = 1;
			if (sortType == SortType.FileSize && sortDirection == 1)
			{
				sort = 2;
			}

			return $"http://www.btbook.net/search/{HttpUtility.UrlEncode(key)}/{pageindex}-{sort}.html";
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

			var nodes = doc.DocumentNode.SelectNodes("//div[@class='search-item']");
			if (nodes != null)
			{
				foreach (var node in nodes)
				{
					var item = CreateResourceInfo(
						Regex.Match(node.SelectSingleNode(".//a[@class='download'][1]").GetAttributeValue("href", ""), @":([A-F\d]{40})").GetGroupValue(1),
						node.SelectSingleNode(".//h3/a").InnerText
						);
					item.DownloadSize = node.SelectSingleNode(".//div[@class='item-bar']/span[3]/b")?.InnerText;
					item.UpdateTimeDesc = node.SelectSingleNode(".//div[@class='item-bar']/span[2]/b")?.InnerText;

					result.Add(item);
				}
			}
			result.HasPrevious = doc.DocumentNode.SelectSingleNode("//div[@class='bottom-pager']/span/preceding-sibling::*") != null;
			result.HasMore = doc.DocumentNode.SelectSingleNode("//div[@class='bottom-pager']/span/following-sibling::*") != null;

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

			var nodes = doc.DocumentNode.SelectNodes("//div[@class='fileDetail']/div[4]//li");
			if (nodes != null)
			{
				foreach (var node in nodes)
				{
					var size = node.SelectSingleNode("span").InnerText;
					var path = node.SelectSingleNode("./text()").InnerText;

					AddFileNode(torrent, path, null, size);
				}
			}

			base.LookupTorrentContentsCore(url, torrent, htmlContent);
		}
	}
}
