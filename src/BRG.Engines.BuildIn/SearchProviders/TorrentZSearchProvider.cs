namespace BRG.Engines.BuildIn.SearchProviders
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.Text.RegularExpressions;
	using System.Web;
	using BRG.Engines.Entities;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;

	[Export(typeof(IResourceProvider))]
	class TorrentZSearchProvider : AbstractSearchServiceProvider<BuildinServerInfo>, IResourceProvider
	{
		static string _referUrl = "https://torrentz.eu/";


		public TorrentZSearchProvider() : base(new BuildinServerInfo("TorrentZ", Properties.Resources.favicon_tzeu, "提供对TorrentZ的搜索支持"))
		{
		}

		/// <summary>
		/// 测试服务状态
		/// </summary>
		/// <returns></returns>
		public override TestStatus Test()
		{
			return TestCore(_referUrl, "Torrentz Search Engine");
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
			//分析
			var content = context.Result.SearchStringTag("<div class=\"SimpleAcceptebleTextAds\">", "Web search results");
			if (!content.IsNullOrEmpty())
			{
				var matches = Regex.Matches(content, @"<dl><dt><a.*?/([a-z\d]{40})"".*?>(.*?)</a>.*?<span\sclass=""a"">.*?title=""([^""]+)"".*?class=""s"">([^<>]+)<.*?</dl>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
				foreach (Match match in matches)
				{
					var item = new ResourceInfo();
					item.Hash = match.GetGroupValue(1).ToUpper();
					item.Provider = this;
					item.Title = match.GetGroupValue(2);
					item.DownloadSize = match.GetGroupValue(4);
					item.UpdateTime = match.GetGroupValue(3).ToDateTimeNullable();

					result.Add(item);
				}
				result.HasPrevious = context.Result.IndexOf("\">&laquo; Previous") != -1;
				result.HasMore = context.Result.IndexOf("\">Next &raquo;") != -1;
			}
			return base.LoadCore(context, url, htmlContent, result);
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrl(IResourceInfo resource)
		{
			return $"https://torrentz.eu/{resource.Hash.ToLower()}";
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			var url = "https://torrentz.eu/search";
			if (sortType == SortType.FileSize)
				url += "S";
			else if (sortType == SortType.PubDate)
				url += "A";

			return $"{url}?f={HttpUtility.UrlPathEncode(key)}&p={pageindex - 1}";
		}

		/// <summary>
		/// 是否支持Unicode搜索
		/// </summary>
		public override bool SupportUnicode
		{
			get { return false; }
		}

		/// <summary>
		/// 是否需要科学上网
		/// </summary>
		public override bool RequireBypassGfw
		{
			get { return false; }
		}

		/// <summary>
		/// 支持的排序类型
		/// </summary>
		public override SortType? SupportSortType
		{
			get { return SortType.Default | SortType.FileSize | SortType.PubDate; }
		}

		/// <summary>
		/// 是否支持查看种子内容
		/// </summary>
		public override bool SupportLookupTorrentContents
		{
			get { return true; }
		}

		/// <summary>
		/// 是否支持自定义每页条数
		/// </summary>
		public override bool SupportCustomizePageSize
		{
			get { return false; }
		}

		/// <summary>
		/// 加载内容
		/// </summary>
		public override void LookupTorrentContents(IResourceInfo torrent)
		{
			var url = GetDetailUrl(torrent);
			var htmlContext = NetworkClient.Create<string>(HttpMethod.Get, url, _referUrl).Send();
			if (!htmlContext.IsValid())
				return;

			var html = htmlContext.Result.SearchStringTag("<div class=\"files\">", "<p>Please note");
			if (string.IsNullOrEmpty(html))
				return;

			var m = Regex.Match(html, @"<(ul|li|/ul)(\s*class=""t"")?>(([^<]+?)(\s*<span>([^<]+)</span>\s*)?)?\s*(?=<)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			var isTorrentSelfSkiped = false;	//是否已经跳过种子本身
			var nodesChain = new Stack<IFileNode>();
			IFileNode parentNode = null;	//当前父节点
			IFileNode lastNode = null;	//最后的节点

			while (m.Success)
			{
				if (!isTorrentSelfSkiped)
				{
					isTorrentSelfSkiped = m.Groups[1].Value == "li";
				}
				else if (m.Groups[1].Value == "/ul")
				{
					parentNode = nodesChain.Count > 0 ? nodesChain.Pop() : null;
				}
				else if (m.Groups[1].Value == "ul")
				{
					//开始文件夹列表
					nodesChain.Push(parentNode);
					parentNode = lastNode;
				}
				else if (m.Groups[1].Value == "li" && !m.Groups[3].Success)
				{
					//文件夹的开始节点，忽略
				}
				else if (m.Groups[2].Success)
				{
					//文件夹节点
					var node = new FileNode()
					{
						IsDirectory = true,
						Name = m.Groups[4].Value
					};
					lastNode = node;
					if (parentNode == null)
						torrent.Add(node);
					else parentNode.Children.Add(node);
				}
				else
				{
					//文件节点
					var node = new FileNode()
					{
						IsDirectory = false,
						Name = m.Groups[4].Value,
						SizeString = m.Groups[6].Value ?? ""
					};
					lastNode = node;
					if (parentNode == null)
						torrent.Add(node);
					else parentNode.Children.Add(node);
				}

				m = m.NextMatch();
			}

		}

		/// <summary>
		/// 是否支持预加载标记
		/// </summary>
		public override bool SupportResourceInitialMark
		{
			get { return true; }
		}
	}
}
