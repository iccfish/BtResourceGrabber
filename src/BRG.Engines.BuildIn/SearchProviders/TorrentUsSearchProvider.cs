namespace BRG.Engines.BuildIn.SearchProviders
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.Linq;
	using System.Text.RegularExpressions;
	using System.Web;
	using BRG.Engines.Entities;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;

	//[Export(typeof(IResourceProvider))]
	class TorrentUsSearchProvider : AbstractSearchServiceProvider<BuildinServerInfo, SiteInfo>, IResourceProvider
	{
		const string _referUrl = "http://torrentus.si/";

		protected TorrentUsSearchProvider()
			: base(new BuildinServerInfo("TUS", Properties.Resources.favicon_torrentus, "提供对TUS的资源搜索支持"))
		{
			SiteID = SiteData.SiteID_TorrentUs;
			SupportResourceInitialMark = true;
			SupportUnicode = false;
			RequireBypassGfw = false;
			SupportSortType= SortType.FileSize | SortType.Title | SortType.PubDate;
			SupportLookupTorrentContents = true;
			SupportCustomizePageSize = false;
		}

		/// <summary>
		/// 加载数据
		/// </summary>
		/// <param name="key"></param>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <returns></returns>
		public override IResourceSearchInfo Load(string key, SortType sortType, int sortDirection, int pagesize, int pageindex = 1)
		{
			var result = (ResourceSearchInfo)base.Load(key, sortType, sortDirection, pagesize, pageindex);
			result.SortType = sortType == SortType.FileSize | sortType == SortType.PubDate ? (SortType?)sortType : null;

			return result;
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
			ParserSearchPageHtml(context.Result, result);
			return base.LoadCore(context, url, htmlContent, result);
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrlCore(IResourceInfo resource, SiteInfo data)
		{
			return $"http://torrentus.si/{data.SiteID}/{data.PageName}.html";
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			var url = "http://torrentus.si/?q=" + HttpUtility.UrlPathEncode(key) + "&page=" + pageindex;

			if (sortType == SortType.FileSize)
				url += "&sort=size_" + (sortDirection == 1 ? "desc" : "asc");
			else if (sortType == SortType.PubDate)
				url += "&sort=age_" + (sortDirection == 1 ? "asc" : "desc");

			return url;
		}

		/// <summary>
		/// 加载内容
		/// </summary>
		public override void LookupTorrentContents(IResourceInfo torrent)
		{
			var detailPage = GetDetailUrl(torrent);
			var html = NetworkClient.Create<string>(HttpMethod.Get, detailPage, _referUrl).Send();
			if (!html.IsValid())
				return;

			var bodycontent = html.Result.SearchStringTag("<table class=\"list contents\">", "</table>");
			if (string.IsNullOrEmpty(bodycontent))
				return;

			var pos = 0;
			var row = "";
			var parentStack = new Stack<IFileNode>();
			var previousLevel = 0;
			while (!string.IsNullOrEmpty((row = bodycontent.SearchStringTag("<tr", "</tr>", ref pos))))
			{
				var level = Regex.Match(row, @"child-(\d+)", RegexOptions.IgnoreCase).GetGroupValue(1).ToInt32Nullable();
				if (level == null)
					continue;

				var name = RemoveHtmlStrings(Regex.Match(row, ">([^><]*?)</td>", RegexOptions.IgnoreCase).GetGroupValue(1) ?? "");
				if (string.IsNullOrEmpty(name))
					continue;

				var node = new FileNode()
				{
					Name = name,
					IsDirectory = false,
					SizeString = RemoveSpaceChars(Regex.Match(row, @"right"">\s*([\d\.]+\s*[a-z]+)\s*</td>", RegexOptions.IgnoreCase).GetGroupValue(1) ?? "")
				};
				if (previousLevel == level)
				{
					//子目录节点
					if (parentStack.Count > 0)
					{
						parentStack.Pop();
					}
					if (parentStack.Count > 0)
					{
						parentStack.Peek().Children.Add(node);
					}
					else
					{
						parentStack.Push(node);
						torrent.Add(node);
					}
				}
				else if (previousLevel < level)
				{
					//子目录
					if (parentStack.Count == 0)
						torrent.Add(node);
					else
					{
						var parent = parentStack.Peek();
						((FileNode)parent).IsDirectory = true;
						parent.Children.Add(node);
					}
					parentStack.Push(node);
					previousLevel = level.Value;
				}
				else
				{
					//返回
					while (previousLevel-- > level && parentStack.Count > 0)
					{
						parentStack.Pop();
					}
					if (parentStack.Count == 0)
						torrent.Add(node);
					else parentStack.Peek().Children.Add(node);
					parentStack.Push(node);
					previousLevel = level.Value;

				}
			}
		}

		void ParserSearchPageHtml(string html, ResourceSearchInfo result)
		{
			string bodyContent;
			if (string.IsNullOrEmpty(html) || string.IsNullOrEmpty((bodyContent = html.SearchStringTag("<table class=\"list\" width=\"100%\">", "</table>"))))
				return;

			bodyContent = RemoveEmailProtect(bodyContent);

			string rowContent;
			var charPosition = 0;

			while (!string.IsNullOrEmpty((rowContent = bodyContent.SearchStringTag("<tr", "</tr>", ref charPosition))))
			{
				var hash = Regex.Match(rowContent, @"value=['""]([a-f\d]{40})['""]", RegexOptions.IgnoreCase).GetGroupValue(1);
				if (string.IsNullOrEmpty(hash))
					continue;

				var item = new ResourceInfo();
				item.Hash = hash.ToUpper();
				item.Provider = this;

				//标题
				var title = Regex.Match(rowContent, @"<a.*?href=['""].*?/(\d+)/([^'""]+?)\.html['""]>(.*?)</a>", RegexOptions.IgnoreCase);
				item.Title = title.GetGroupValue(3).DefaultForEmpty(".....");
				item.SiteData = new SiteInfo()
				{
					PageName = title.GetGroupValue(2),
					SiteID = title.GetGroupValue(1).ToInt64()
				};
				item.DownloadSize = RemoveSpaceChars(Regex.Match(rowContent, @"nowrap>\s*(([\d\.]+).*?B)\s*<", RegexOptions.IgnoreCase).GetGroupValue(1) ?? "");
				item.UpdateTimeDesc = Regex.Match(rowContent, "class=\"light\">([^><]+)</td>", RegexOptions.IgnoreCase).GetGroupValue(1);

				result.Add(item);
			}

			result.HasPrevious = html.IndexOf("paging_prev", StringComparison.OrdinalIgnoreCase) != -1;
			result.HasMore = html.IndexOf("paging_next", StringComparison.OrdinalIgnoreCase) != -1;
		}

		string RemoveEmailProtect(string html)
		{
			html = Regex.Replace(html, "<script[^>]*?cf-hash=[^>]*?>.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

			html = Regex.Replace(html, @"<span[^>]*?data\-cfemail=['""]([a-z\d]+)['""][^>]*?>.*?</span>", _ =>
			{
				var data = _.Groups[1].Value.ConvertHexStringToBytes();
				var key = data[0];
				return new string(data.Skip(1).Select(s => (char)(s ^ key)).ToArray());
			}, RegexOptions.Singleline | RegexOptions.IgnoreCase);

			return html;
		}

		/// <summary>
		/// 测试服务状态
		/// </summary>
		/// <returns></returns>
		public override TestStatus Test()
		{
			return TestCore("http://torrentus.si/", "TorrentUs");
		}
	}
}
