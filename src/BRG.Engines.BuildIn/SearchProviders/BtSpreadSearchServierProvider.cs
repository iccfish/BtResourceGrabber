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

	[Export(typeof(IResourceProvider))]
	class BtSpreadSearchServierProvider : AbstractSearchServiceProvider<BuildinServerInfo>, IResourceProvider
	{
		const string _referUrl = "http://www.bt2mag.com/search";

		public BtSpreadSearchServierProvider()
			: base(new BuildinServerInfo("BtSpread", Properties.Resources.favicon_btspread, "提供对BtSpread的资源搜索支持"))
		{
			DefaultHost = "www.bt2mag.com";
		}

		/// <summary>
		/// 从指定版本升级
		/// </summary>
		/// <param name="oldVersion"></param>
		public override void UpgradeFrom(string oldVersion, string currentVersion)
		{
			base.UpgradeFrom(oldVersion, currentVersion);

			Host = "www.bt2mag.com";
		}


		#region Implementation of IResourceProvider


		#region Overrides of AbstractSearchServiceProvider<BuildinServerInfo>

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

		#endregion

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrl(IResourceInfo resource)
		{
			return $"http://{Host}/magnet/detail/hash/{resource.Hash}";
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			return $"http://{Host}/search/{HttpUtility.UrlPathEncode(key)}/currentPage/{pageindex}";
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

			var mcs = Regex.Matches(html.Result, "<div.*?file\">(?:.*?<span.*?</span>)?(.*?)</div><div.*?size(.*?)</div>", RegexOptions.IgnoreCase);
			foreach (Match match in mcs)
			{
				AddFileNode(torrent, HttpUtility.HtmlDecode(match.Groups[1].Value), null, match.Groups[1].Value);
			}
		}

		#endregion

		#region 页面分析

		void ParserSearchPageHtml(string html, ResourceSearchInfo result)
		{
			string rowContent;
			var charPosition = 0;

			while (!string.IsNullOrEmpty((rowContent = html.SearchStringTag(@"<div class=""row"">", "    </div>", ref charPosition))))
			{
				var hash = Regex.Match(rowContent, @"hash\/([a-f\d]{40})['""]", RegexOptions.IgnoreCase).GetGroupValue(1);
				if (string.IsNullOrEmpty(hash))
					continue;

				var item = new ResourceInfo();
				item.Hash = hash.ToUpper();
				item.Provider = this;

				//标题
				item.Title = Regex.Match(rowContent, @"title=['""](.*?)['""]", RegexOptions.IgnoreCase).GetGroupValue(1).DefaultForEmpty(".....");
				item.DownloadSize = Regex.Match(rowContent, @"size"">([^<]+?)<", RegexOptions.IgnoreCase).GetGroupValue(1);
				item.UpdateTime = Regex.Match(rowContent, @"date"">([^<]+?)<", RegexOptions.IgnoreCase).GetGroupValue(1).ToDateTimeNullable();

				result.Add(item);
			}

			result.HasPrevious = result.PageIndex > 1;
			result.HasMore = html.IndexOf(">Next <", StringComparison.OrdinalIgnoreCase) != -1;
		}

		#endregion

		/// <summary>
		/// 测试服务状态
		/// </summary>
		/// <returns></returns>
		public override TestStatus Test()
		{
			return TestCore("http://www.btava.com/search/", "Search</title>");
		}
	}
}
