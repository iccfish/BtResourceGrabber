namespace BRG.Engines.BuildIn.SearchProviders
{
	using System;
	using System.Collections.Generic;
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
	class TorrentProjectSearchProvider : AbstractSearchServiceProvider<BuildinServerInfo>, IResourceProvider
	{
		const string _referUrl = "https://torrentproject.se/";

		public TorrentProjectSearchProvider()
			: base(new BuildinServerInfo("TPSE", Properties.Resources.favicon_tps, "提供对TPSE的资源搜索支持"))
		{
			SetPreferDownloader<TorrentProjectDownloadProvider>();
		}

		#region Implementation of IResourceProvider

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
			ParserSearchPageHtml(context.Result, result);
			return base.LoadCore(context, url, htmlContent, result);
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrl(IResourceInfo resource)
		{
			return "https://torrentproject.se/7999e3919e9b0672083495352bf1e047f016a38f/" + resource.Hash.ToLower() + "/";
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			return string.Format("https://torrentproject.se/?t={0}&p={1}", HttpUtility.UrlPathEncode(key), pageindex - 1);
		}

		/// <summary>
		/// 是否支持Unicode搜索
		/// </summary>
		public override bool SupportUnicode
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// 是否需要科学上网
		/// </summary>
		public override bool RequireBypassGfw { get { return false; } }

		/// <summary>
		/// 支持的排序类型
		/// </summary>
		public override SortType? SupportSortType
		{
			get { return null; }
		}

		/// <summary>
		/// 是否支持查看种子内容
		/// </summary>
		public override bool SupportLookupTorrentContents { get { return true; } }

		public override bool SupportResourceInitialMark { get { return true; } }

		/// <summary>
		/// 是否支持自定义每页条数
		/// </summary>
		public override bool SupportCustomizePageSize { get { return false; } }

		/// <summary>
		/// 加载内容
		/// </summary>
		public override void LookupTorrentContents(IResourceInfo torrent)
		{
			var detailPage = GetDetailUrl(torrent);
			var html = NetworkClient.Create<string>(HttpMethod.Get, detailPage, _referUrl).Send();
			if (!html.IsValid())
				return;

			var ajaxAdd = Regex.Match(html.Result, @"hashid=(\d+)").GetGroupValue(1);
			if (string.IsNullOrEmpty(ajaxAdd))
				return;

			html = NetworkClient.Create<string>(HttpMethod.Post, "https://torrentproject.se/ajax_show_all_files.php", detailPage, new
			{
				id = torrent.Hash.ToLower(),
				hashid = ajaxAdd
			}).Send();

			var bodycontent = html.Result;

			var pos = 0;
			var row = "";
			while (!string.IsNullOrEmpty((row = bodycontent.SearchStringTag("<div class=\"torrent_folder\">", "</div>", ref pos))))
			{
				//处理文件夹路径
				var folder = Regex.Matches(row, "folder\"></span>(.*?)<");
				var container = torrent as ICollection<IFileNode>;
				if (folder.Count > 1)
				{
					for (int i = 1; i < folder.Count; i++)
					{
						var fn = RemoveHtmlStrings(folder[i].Groups[1].Value);
						var p = container.FirstOrDefault(s => s.Name.IsIgnoreCaseEqualTo(fn));
						if (p == null)
						{
							p = new FileNode() { Name = fn, IsDirectory = true };
							container.Add(p);
						}
						container = p.Children;
					}
				}

				string line;
				while (!string.IsNullOrEmpty((line = bodycontent.SearchStringTag("<div class=\"torrent_cont\"><", "</div>", ref pos))))
				{

					var m = Regex.Match(line, "</span>(.*?)<span.*?file_size_search_all.*?>(.*?)<");
					if (!m.Success)
						continue;

					var node = new FileNode()
					{
						Name = RemoveHtmlStrings(m.GetGroupValue(1)),
						SizeString = RemoveSpaceChars(m.GetGroupValue(2))
					};
					container.Add(node);

					//如果有下一级文件夹，则直接跳过。
					var nextFIndex = bodycontent.IndexOf("<div class=\"torrent_folder\">", pos);
					if (nextFIndex != -1 && nextFIndex < bodycontent.IndexOf("<div class=\"torrent_cont\">", pos))
						break;

				}
			}
		}

		#endregion

		#region 页面分析

		void ParserSearchPageHtml(string html, ResourceSearchInfo result)
		{
			var doc = new HtmlDocument();
			doc.LoadHtml(html);

			var rows = doc.GetElementbyId("similarfiles").SelectNodes(".//div");
			if (rows != null)
			{
				foreach (var row in rows)
				{
					var title = row.SelectSingleNode("span/a");
					var hash = Regex.Match(title?.GetAttributeValue("href", "") ?? "", @"se/(\w{40})/", RegexOptions.IgnoreCase).GetGroupValue(1);
					if (string.IsNullOrEmpty(hash))
						continue;

					var item = CreateResourceInfo(hash.ToUpper(), title.InnerText);

					item.DownloadSize = row.SelectSingleNode("span[5]").InnerText.Trim();
					item.UpdateTimeDesc = row.SelectSingleNode("span[4]").InnerText.Trim();

					result.Add(item);
				}
			}

			result.HasPrevious = result.PageIndex > 1;
			result.HasMore = doc.DocumentNode.SelectSingleNode("//td[@class='cur']")?.NextSibling != null;
		}

		#endregion

		/// <summary>
		/// 测试服务状态
		/// </summary>
		/// <returns></returns>
		public override TestStatus Test()
		{
			return TestCore("https://torrentproject.se/", "Torrent Meta Search");
		}
	}
}
