using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRG.Engines.BuildIn.SearchProviders
{
	using System.ComponentModel.Composition;
	using System.Drawing;
	using System.Text.RegularExpressions;

	using BRG.Entities;

	using FSLib.Network.Http;

	using HtmlAgilityPack;

	using Service;

	[Export(typeof(IResourceProvider))]
	class TorrentDownloadSearchProvider : AbstractBuildInSearchServiceProviderWithSiteData
	{
		public TorrentDownloadSearchProvider()
			: base("TorrentDownload", Properties.Resources.favicon_torrentdownload_ws, "提供对TorrentDownload的搜索支持", "1.0.0.0")
		{
			SupportCustomizePageSize = false;
			SupportLookupTorrentContents = true;
			SupportResourceInitialMark = true;
			RequireBypassGfw = false;
			SupportUnicode = false;
			SupportSortType = SortType.Default | SortType.PubDate | SortType.FileSize;
		}

		/// <summary>
		/// 连接
		/// </summary>
		public override void Connect()
		{
			base.Connect();

			DefaultHost = "www.torrentdownload.ws";
			ReferUrlPage = $"http://{Host}/";
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrlCore(IResourceInfo resource, SiteInfo data)
		{
			//http://www.torrentdownload.ws/Game-of-Thrones-S05E08-HDTV-x264-KILLERS%5Bettv%5D/2C7FAC7A8B18716E7209FC4AD769642DEAB43AE1
			return $"http://{Host}/{UE(data.PageName)}/{resource.Hash}";
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			var searchType = "searchr";
			if (sortDirection == 1)
			{
				if (sortType == SortType.FileSize)
					searchType = "searchs";
				else if (sortType == SortType.PubDate)
					searchType = "searchd";
			}

			return $"http://{Host}/{searchType}?q={UE(key)}&p={pageindex}";
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
			doc.LoadHtml(context.Result);

			var nodes = doc.DocumentNode.SelectNodes("//table[@class='table2'][last()]//tr[position()>1]");
			if (nodes == null)
				return false;

			foreach (var node in nodes)
			{
				var a = node.SelectSingleNode("td[1]//a");
				if (a == null)
					continue;
				var href = a.GetAttributeValue("href", "");
				//game-of-thrones-season-3/6f185a9f14a6ce7a3794c5dd1ef3dd51f0dc9f71
				var m = Regex.Match(href, @"/([^/]+)/([a-f\d]{40})", RegexOptions.IgnoreCase);
				if (!m.Success)
					continue;

				var item = CreateResourceInfo(m.GetGroupValue(2), a.InnerText);
				item.SiteData = new SiteInfo()
				{
					PageName = m.GetGroupValue(1)
				};
				item.UpdateTimeDesc = node.SelectSingleNode("td[2]").InnerText;
				item.DownloadSize = node.SelectSingleNode("td[3]").InnerText;

				result.Add(item);
			}
			result.HasPrevious = result.PageIndex > 1;
			result.HasMore = doc.GetElementbyId("next") != null;

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

			var nodes = doc.DocumentNode.SelectNodes("//div[@class='fileline']");
			var rootPath = torrent.Title;
			var currentPath = "";
			var trim = new[] { ' ', '-' };
			if (nodes != null)
			{
				foreach (var node in nodes)
				{
					var nodeHtml = node.InnerHtml;
					var nm = Regex.Matches(nodeHtml, @"((?:&nbsp;)+)<img[^>]*>\s*([^<>]+)(?:<div.*?>(.*?)</div>)?", RegexOptions.IgnoreCase);
					if (nm.Count == 0)
						continue;

					if (nm.Count > 1)
					{
						var dirgp = nm[nm.Count - 2];
						if (!dirgp.GetGroupValue(2).IsNullOrEmpty())
							currentPath = dirgp.GetGroupValue(2);
					}
					var filegp = nm[nm.Count - 1];

					var filename = (filegp.GetGroupValue(2) ?? "").TrimEnd(trim);
					var filesize = filegp.GetGroupValue(3);
					if (filename.IsNullOrEmpty())
						continue;

					var hasPath = filegp.GetGroupValue(1).Replace("&nbsp;", " ").Length > 4;
					var path = string.Concat(rootPath, "\\", hasPath ? currentPath : "", "\\", filename);
					AddFileNode(torrent, path, null, filesize);
				}
			}

			base.LookupTorrentContentsCore(url, torrent, htmlContent);
		}
	}
}
