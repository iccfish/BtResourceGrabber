namespace BRG.Engines.BuildIn.SearchProviders
{
	using System;
	using System.ComponentModel.Composition;
	using System.Text.RegularExpressions;
	using System.Web;
	using BRG.Engines.Entities;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;
	using HtmlAgilityPack;

	[Export(typeof(IResourceProvider))]
	class LimeTorrentsSearchProvider : AbstractSearchServiceProvider<BuildinServerInfo, SiteInfo>, IResourceProvider
	{
		public LimeTorrentsSearchProvider() : base(new BuildinServerInfo("LimeTorrents", Properties.Resources.favicon_limetorrents, "提供对LimeTorrents的搜索支持"))
		{
			SupportResourceInitialMark = true;
			SupportLookupTorrentContents = true;
			SupportSortType = null;
			RequireBypassGfw = false;
			SupportUnicode = false;

			ReferUrlPage = "https://www.limetorrents.cc/";
			PageCheckKey = @"LimeTorrents.cc</title>";
		}



		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			return $"https://www.limetorrents.cc/search/all/{HttpUtility.UrlEncode(key)}/date/{pageindex}/";
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrlCore(IResourceInfo resource, SiteInfo data)
		{
			//https://www.limetorrents.cc/CWPBD-15%20-CATWALK-POISON-15-LUNA-(Blu-ray)-%20TPG%20-[720p-AVC]-mp4-torrent-5868932.html

			return $"https://www.limetorrents.cc/{HttpUtility.UrlEncode(data.PageName)}-torrent-{data.SiteID}.html";
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

			var items = doc.DocumentNode.SelectNodes("//table[@class='table2'][2]//tr[position()>1]");
			if (items != null)
			{
				foreach (var item in items)
				{
					var dlink = item.SelectSingleNode("td[1]/div/a[1]");
					var tlink = item.SelectSingleNode("td[1]/div/a[2]");
					var dcell = item.SelectSingleNode("td[2]");
					var scell = item.SelectSingleNode("td[3]");

					//hash
					var hash = Regex.Match(dlink.GetAttributeValue("href", ""), @"torrent/([a-z\d]{40})", RegexOptions.IgnoreCase).GetGroupValue(1);
					var pinfo = Regex.Match(tlink.GetAttributeValue("href", ""), @"/([^/]+?)\-(\d+)\.html", RegexOptions.IgnoreCase);

					var resnode = CreateResourceInfo(hash, tlink.InnerText);
					resnode.SiteData = new SiteInfo()
					{
						PageName = pinfo.GetGroupValue(1),
						SiteID = pinfo.GetGroupValue(2).ToInt64()
					};
					resnode.UpdateTimeDesc = dcell.InnerText.Split('-')[0].Trim();
					resnode.DownloadSize = scell.InnerText;

					result.Add(resnode);
				}
			}

			var pager = doc.DocumentNode.SelectSingleNode("//div[@class='search_stat']");
			if (pager != null)
			{
				result.HasMore = pager.SelectSingleNode("./*[last()]")?.Name == "a";
				result.HasPrevious = pager.SelectSingleNode("./*[1]")?.Name == "a";
			}
			return base.LoadCore(context, url, htmlContent, result);
		}

		/// <summary>
		/// 加载内容
		/// </summary>
		/// <param name="url"></param>
		public override void LookupTorrentContentsCore(string url, IResourceInfo torrent, string htmlContent)
		{
			var doc = CreateHtmlDocument(htmlContent);

			var files = doc.DocumentNode.SelectNodes("//div[@class='fileline']");
			if (files != null)
			{
				var currentFolder = "";

				foreach (var file in files)
				{
					var html = HttpUtility.HtmlDecode(file.InnerHtml.Replace("&nbsp;", " "));
					var match = Regex.Match(html, @"(<span[^>]*?csprite_doc_dir""></span>\s*([^<]+)<br>)?(\s*)<span[^>]*?csprite_doc_[^""]+""></span>\s*([^<]+?)\s*\-\s*<div[^>]+?filelinesize"">([^<]+)</div>", RegexOptions.IgnoreCase);
					if (!match.Success)
						continue;

					if (match.Groups[3].Value.Length <= 4)
					{
						//不在文件夹下
						currentFolder = "";
					}

					if (match.Groups[2].Success)
					{
						//文件夹
						currentFolder = match.Groups[2].Value;
					}

					AddFileNode(torrent, currentFolder + "/" + match.Groups[4].Value, null, match.Groups[5].Value);
				}
			}

			if (htmlContent.IndexOf("Show all torrent content") != -1)
			{
				((ResourceInfo)torrent).ContentLoadErrMessage = "文件列表不完整，请下载种子后查看所有文件。当前列表仅供参考。";
			}

			base.LookupTorrentContentsCore(url, torrent, htmlContent);
		}
	}
}
