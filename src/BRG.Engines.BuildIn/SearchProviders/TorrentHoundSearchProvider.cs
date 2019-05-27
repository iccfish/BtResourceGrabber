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
	class TorrentHoundSearchProvider : AbstractSearchServiceProvider<BuildinServerInfo>, IResourceProvider
	{
		public TorrentHoundSearchProvider() 
			: base(new BuildinServerInfo("TorrntHound", Properties.Resources.favicon_torrenthound_16,"提供对TorrentHound的搜索支持"))
		{
			SupportResourceInitialMark = true;
			SupportLookupTorrentContents = true;
			SupportSortType = SortType.FileSize | SortType.PubDate | SortType.Default | SortType.Title;
			RequireBypassGfw = false;
			SupportUnicode = false;

			ReferUrlPage = "http://www.torrenthound.com/";
			PageCheckKey = @"<title>torrentHound.com";
			SetPreferDownloader<TorrentHoundDownloadProvider>();
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrl(IResourceInfo resource)
		{
			return $"http://www.torrenthound.com/hash/{resource.Hash.ToLower()}/torrent-info/";
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			var sortExp = "";
			var sortDirectionExp = (sortDirection == 0 ? "asc" : "desc");
			if (sortType == SortType.PubDate)
			{
				sortExp = "added:" + sortDirectionExp;
			}
			else if (sortType == SortType.FileSize)
			{
				sortExp = "totalsize:" + sortDirectionExp;
			}

			return $"http://www.torrenthound.com/search/{pageindex}/{HttpUtility.UrlEncode(key)}/{sortExp}";
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

			var list = doc.DocumentNode.SelectSingleNode("//table[@class='searchtable'][last()]");
			if (list == null)
				return false;

			var rows = list.SelectNodes(".//tr[position()>1]");
			foreach (var row in rows)
			{
				var titleLink = row.SelectSingleNode("td[1]/a[1]");
				//移除类别节点
				titleLink.SelectSingleNode("span[@class='cat']")?.Remove();

				var item = CreateResourceInfo(Regex.Match(titleLink.Attributes["href"].Value, "hash/([a-z\\d]{40})", RegexOptions.IgnoreCase | RegexOptions.Singleline).GetGroupValue(1), titleLink.InnerText);

				item.UpdateTimeDesc = row.SelectSingleNode("td/span[contains(@class,'added')]/text()").InnerText;
				item.DownloadSize = row.SelectSingleNode("td/span[contains(@class,'size')]").InnerText;

				result.Add(item);
			}
			var pageDiv = doc.DocumentNode.SelectSingleNode("//div[@class='pagediv']/ul");
			if (pageDiv != null)
			{
				result.HasPrevious = pageDiv.SelectSingleNode("li[1]/a") != null;
				result.HasMore = pageDiv.SelectSingleNode("li[3]/a") != null;
			}

			return base.LoadCore(context, url, htmlContent, result);
		}

		/// <summary>
		/// 加载内容
		/// </summary>
		/// <param name="url"></param>
		public override void LookupTorrentContentsCore(string url, IResourceInfo torrent, string htmlContent)
		{
			base.LookupTorrentContentsCore(url, torrent, htmlContent);

			var doc = new HtmlDocument();
			doc.LoadHtml(htmlContent);

			AddNodeToTorrent(doc.DocumentNode.SelectSingleNode("//td[@class='flist']/ul"), torrent, null);

		}

		void AddNodeToTorrent(HtmlNode node, IResourceInfo torrent, IFileNode parentNode)
		{
			if (node == null)
				return;

			var nodes = node.SelectNodes("li");
			foreach (var li in nodes)
			{
				var cls = li.Attributes["class"].Value;

				if (cls == "branch")
				{
					var bnode = new FileNode()
					{
						IsDirectory = true,
						Name = li.SelectSingleNode("span/text()").InnerText
					};
					if (parentNode == null)
						torrent.Add(bnode);
					else
						parentNode.Children.Add(bnode);
					AddNodeToTorrent(li.SelectSingleNode("ul"), torrent, bnode);
				}
				else
				{
					var lnode = new FileNode()
					{
						IsDirectory = false,
						Name = li.SelectSingleNode("span[1]/text()").InnerText,
						SizeString = li.SelectSingleNode("span[2]/text()").InnerText
					};
					if (parentNode == null)
						torrent.Add(lnode);
					else
						parentNode.Children.Add(lnode);
				}
			}
		}
	}
}
