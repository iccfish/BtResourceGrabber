using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRG.Engines.BuildIn.SearchProviders
{
	using System.ComponentModel.Composition;
	using System.Text.RegularExpressions;
	using BRG.Engines.BuildIn.DownloadProviders;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;
	using HtmlAgilityPack;

	[Export(typeof(IResourceProvider))]
	class BtlibrarySearchProvider : AbstractSearchServiceProvider<BuildinServerInfo, SiteInfo>, IResourceProvider
	{
		string _searchPagePath = "b";

		static readonly Dictionary<string, string> _keyMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		public BtlibrarySearchProvider() : base(new BuildinServerInfo("BtLibrary", Properties.Resources.favicon_btlibrary_16, "提供对BtLibrary的搜索支持"))
		{
			RequireBypassGfw = false;
			SupportResourceInitialMark = true;
			SupportLookupTorrentContents = true;
			SupportSortType = SortType.FileSize | SortType.PubDate;
			SupportUnicode = true;
			SetPreferDownloader<StoreBtDownloadProvider>();
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

			var path = task.Redirection.Current.PathAndQuery;

			var result = Regex.Match(path, "/([^/]+)/([^/]+)[/\\.].*?html", RegexOptions.IgnoreCase);
			ek = result.GetGroupValue(2);
			if (_searchPagePath.IsNullOrEmpty())
				_searchPagePath = result.GetGroupValue(1);
			if (!ek.IsNullOrEmpty())
			{
				_keyMap.AddOrUpdate(key, ek);
			}

			return ek;
		}


		/// <summary>
		/// 连接
		/// </summary>
		public override void Connect()
		{
			base.Connect();

			DefaultHost = "btlibrary.net";
			ReferUrlPage = $"http://{Host}/";
		}


		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrlCore(IResourceInfo resource, SiteInfo data)
		{
			//http://btlibrary.org/torrent/18a52fa6a81420b01fa7ca430f8e319a584e2296/-%E7%A0%B4%E7%83%82%E7%86%8A-%E5%8F%B2%E5%89%8D%E6%96%B0%E7%BA%AA%E5%85%83-Terra-Nova-S01E04-rmvb.html
			return $"http://{Host}/torrent/{resource.Hash}/{data.PageName}.html";
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
						sort = 2;
						break;
					case SortType.FileSize:
						sort = 3;
						break;
					default:
						break;
				}
			}

			var nk = PrepareSearchKey(key);
			if (nk.IsNullOrEmpty())
				throw new InvalidOperationException();

			return $"http://{Host}/{_searchPagePath}/{nk}/{pageindex}/{sort}.html";
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
			htmlContent = DecodePage(htmlContent);

			var doc = new HtmlDocument();
			doc.LoadHtml(htmlContent);

			//节点
			var nodes = doc.DocumentNode.SelectNodes(@"//div[@class='list-content']/div");
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
					var title = titleLink.InnerText;

					//detail spans.
					var detailSpans = node.SelectNodes("div[@class='item-detail']/span");
					//hash
					var hash = Regex.Match(detailSpans[0].SelectSingleNode("a").GetAttributeValue("href", ""), @"([A-F\d]{40})").GetGroupValue(1);
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
						ProvideSiteDownload = null
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
		/// 加载相信信息
		/// </summary>
		public override void LoadFullDetail(IResourceInfo info)
		{
			base.LookupTorrentContents(info);
		}

		/// <summary>
		/// 加载内容
		/// </summary>
		/// <param name="url"></param>
		public override void LookupTorrentContentsCore(string url, IResourceInfo torrent, string htmlContent)
		{
			htmlContent = DecodePage(htmlContent);

			var doc = new HtmlDocument();
			doc.LoadHtml(htmlContent);

			//下载链接
			var dlink = Regex.Match(htmlContent, @"<a.*?/torrent/([^/]+)/", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			var sdata = (torrent.SiteData as SiteInfo);
			if (dlink.Success)
			{
				sdata.SiteDownloadLink = $"http://storebt.com/down/{dlink.GetGroupValue(1)}/torrent.torrent";
				sdata.ProvideSiteDownload = true;
			}
			else
			{
				sdata.ProvideSiteDownload = false;
			}

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
