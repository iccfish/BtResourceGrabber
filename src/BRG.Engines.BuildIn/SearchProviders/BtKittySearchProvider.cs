using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRG.Engines.BuildIn.SearchProviders
{
	using System.ComponentModel.Composition;
	using System.IO;
	using System.Text.RegularExpressions;
	using BRG.Engines.BuildIn.DownloadProviders;
	using BRG.Engines.Entities;
	using BRG.Entities;
	using BRG.Service;

	using FSLib.FileFormats.Torrent;
	using FSLib.Network.Http;
	using HtmlAgilityPack;

	[Export(typeof(IResourceProvider))]
	class BtKittySearchProvider : AbstractBuildInSearchServiceProviderWithSiteData
	{
		static readonly Dictionary<string, string> _keyMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		public BtKittySearchProvider() : base("BtKitty", Properties.Resources.favicon_btkitty_16, "提供对BtKitty的搜索支持", "1.1.1.0")
		{
			SetPreferDownloader<StoreBtDownloadProvider>();
			SupportCustomizePageSize = false;
			RequireBypassGfw = false;
			SupportUnicode = true;
			SupportLookupTorrentContents = true;
			SupportOption = false;
			SupportResourceInitialMark = false;
			SupportSortType = SortType.Default | SortType.PubDate | SortType.FileSize;

			PageCheckKey = "<title>BT Kitty";
			SiteID = SiteData.SiteID_BtKitty;
		}

		/// <summary>
		/// 从指定版本升级
		/// </summary>
		/// <param name="oldVersion"></param>
		public override void UpgradeFrom(string oldVersion, string currentVersion)
		{
			base.UpgradeFrom(oldVersion, currentVersion);

			if (Property.ContainsKey("host"))
				Property.Remove("host");
		}

		/// <summary>
		/// 连接
		/// </summary>
		public override void Connect()
		{
			DefaultHost = "btkitty.so";
			ReferUrlPage = $"https://{Host}/";
			base.Connect();
		}

		protected virtual string PrepareSearchKey(string key)
		{
			var ek = _keyMap.GetValue(key);
			if (!ek.IsNullOrEmpty())
				return ek;

			var task = NetworkClient.Create<string>(HttpMethod.Post, ReferUrlPage, ReferUrlPage, new { keyword = key }).Send();
			if (!task.IsRedirection)
				return null;

			//http://diggbt.com/search/Ky5JzEtRSKpUyE0FAA.html
			var path = task.Redirection.Current.PathAndQuery;

			var result = Regex.Match(path, "/([^/]+)/([^/]+)[/\\.].*?html", RegexOptions.IgnoreCase);
			ek = result.GetGroupValue(2);
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
			if (context.IsRedirection)
			{
				var nHost = context.Redirection.Current.Host;
				if (nHost != Host)
				{
					Host = nHost;
					result.RequestResearch = true;
				}
				return false;
			}
			htmlContent = DecodePage(htmlContent);

			var doc = new HtmlDocument();
			doc.LoadHtml(htmlContent);

			//节点
			var nodes = doc.DocumentNode.SelectNodes(@"//div[@class='list-con']/dl");
			if (nodes != null)
			{
				foreach (HtmlNode node in nodes)
				{
					var titleLink = node.SelectSingleNode("dt/a");
					if (titleLink == null)
						continue;

					//pageid;http://btkitty.org/item/BcGBAQAgBATAlco_MY7Q_iN0t40SBXlwtA7jRqUsZ_no4YHR0qQ_.html
					var pageinfo = Regex.Match(titleLink.GetAttributeValue("href", ""), @"/([^/]+?)/([^/]+?)\.html", RegexOptions.IgnoreCase);
                    var pageid = pageinfo.GetGroupValue(2);
					_itemPageKey = pageinfo.GetGroupValue(1).DefaultForEmpty(_itemPageKey);
					var title = titleLink.InnerText;

					//detail spans.
					var detailSpans = node.SelectNodes("dd[@class='option']/span");
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
						SiteID = 0L,
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

		protected override void LoadFullDetailCore(IResourceInfo info)
		{
			var url = GetDetailUrl(info);
			var ctx = NetworkClient.Create<string>(HttpMethod.Get, url, ReferUrlPage).Send();
			if (!ctx.IsValid())
				return;

			var html = DecodePage(ctx.Result);

			//btih:16429c32f383d5e49b9ca2084c8e57473646a62d
			var hash = Regex.Match(html, @"btih:([a-z\d]{40})", RegexOptions.IgnoreCase).GetGroupValue(1);
			if (hash.IsNullOrEmpty())
			{
				//找不到hash，可能是之前被清理了。所以先直接下载种子看看
				FetchOriginalTorrent(info);
			}
			else
			{
				info.Hash = hash;
				LookupTorrentContentsCore(url, info, html);
			}

			base.LoadFullDetailCore(info);
		}

		void FetchOriginalTorrent(IResourceInfo info)
		{
			var data = info.SiteData as SiteInfo;
			if (data == null || data.SiteDownloadLink.IsNullOrEmpty())
				return;

			var torrent = NetworkClient.Create<byte[]>(HttpMethod.Get, data.SiteDownloadLink, ReferUrlPage).Send();
			if (!torrent.IsValid())
				return;

			try
			{
				//update hash
				var contentStream = new MemoryStream(torrent.Result);
				var tf = new TorrentFile();
				try
				{
					tf.Load(contentStream, LoadFlag.ComputeMetaInfoHash);
				}
				catch (Exception)
				{
					return;
				}

				//总下载大小
				var totalsize = tf.Files.Sum(s => s.Length);
				if (tf.Files.Count == 0)
				{
					totalsize = tf.MetaInfo.Length;
				}
				((ResourceInfo)info).DownloadSizeValue = totalsize;
				((ResourceInfo)info).Hash = tf.MetaInfoHashString;
				((ResourceInfo)info).Data = torrent.Result;

				//report
				//AppContext.Instance.CloudService.ReportDownload(info.Hash, info.Title, GetDetailUrl(info), Info.Name, info.DownloadSize, torrent.Result);
			}
			catch (Exception)
			{
			}
		}

		/// <summary>
		/// 加载内容
		/// </summary>
		/// <param name="url"></param>
		public override void LookupTorrentContentsCore(string url, IResourceInfo torrent, string htmlContent)
		{
			if (htmlContent.IndexOf("<h2>404") != -1)
			{
				throw new Exception("此种子已经从网站上移除，无法查看内容，请尝试直接下载。");
			}

			htmlContent = DecodePage(htmlContent);

			var doc = new HtmlDocument();
			doc.LoadHtml(htmlContent);

			var nodes = doc.DocumentNode.SelectNodes("//dd[@class='filelist']/p");
			if (nodes != null)
			{
				foreach (var node in nodes)
				{
					var sizeNode = node.SelectSingleNode("span");
					var size = sizeNode.InnerText;
					var filepath = node.SelectSingleNode("text()").InnerText;

					AddFileNode(torrent, filepath, null, size);
				}
			}

			base.LookupTorrentContentsCore(url, torrent, htmlContent);
		}
	}
}
