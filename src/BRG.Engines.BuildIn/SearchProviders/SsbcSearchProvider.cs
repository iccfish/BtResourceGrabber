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
	class SsbcSearchProvider : AbstractSearchServiceProvider<BuildinServerInfo, SiteInfo>, IResourceProvider
	{

		public SsbcSearchProvider()
			: base(new BuildinServerInfo("SSBC", Properties.Resources.favicon_ssc, "提供对SSBC的资源搜索支持"))
		{

		}

		/// <summary>
		/// 连接
		/// </summary>
		public override void Connect()
		{
			base.Connect();

			DefaultHost = "www.cilizhushou.com";
			ReferUrlPage = $"https://{Host}/";
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
		public override string GetDetailUrlCore(IResourceInfo resource, SiteInfo data)
		{
			return $"https://{Host}/h/{data.SiteID}";
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			return string.Format("https://{2}/search/{0}/{1}", HttpUtility.UrlPathEncode(key), pageindex, Host);
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
			var html = NetworkClient.Create<string>(HttpMethod.Get, detailPage, ReferUrlPage).Send();
			if (!html.IsValid())
				return;

			var bodycontent = html.Result.SearchStringTag("<div class=\"files\">", "</div>");
			if (string.IsNullOrEmpty(bodycontent))
				return;

			var pos = 0;
			var row = "";
			while (!string.IsNullOrEmpty((row = bodycontent.SearchStringTag("<li>", "</li>", ref pos))))
			{
				var mcs = Regex.Match(row, @"<li>(.*)\s([\d\.]+\s+\w+)</li>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
				if (!mcs.Success)
					continue;

				var path = HttpUtility.HtmlDecode(mcs.Groups[1].Value);
				AddFileNode(torrent, path, null, mcs.Groups[2].Value);
			}
		}

		#endregion

		#region 页面分析

		void ParserSearchPageHtml(string html, ResourceSearchInfo result)
		{
			string bodyContent;
			if (string.IsNullOrEmpty(html) || string.IsNullOrEmpty((bodyContent = html.SearchStringTag("<table class=\"table\">", "</table>"))))
				return;

			string rowContent;
			var charPosition = 0;

			while (!string.IsNullOrEmpty((rowContent = bodyContent.SearchStringTag("<tr>", "</tr>", ref charPosition))))
			{
				var hash = Regex.Match(rowContent, @"btih:(\w{40})", RegexOptions.IgnoreCase).GetGroupValue(1);
				if (string.IsNullOrEmpty(hash))
					continue;

				var item = new ResourceInfo();
				item.Hash = hash.ToUpper();
				item.Provider = this;
				item.SiteData = new SiteInfo()
				{
					SiteID = Regex.Match(rowContent, @"/h/(\d+)", RegexOptions.IgnoreCase).GetGroupValue(1).ToInt64()
				};

				//标题
				item.Title = RemoveHtmlStrings(Regex.Match(rowContent, @"<div>[\s]*<a.*?class=['""]title['""].*?>(.*?)</a>", RegexOptions.IgnoreCase | RegexOptions.Singleline).GetGroupValue(1).DefaultForEmpty("....."));
				item.DownloadSize = RemoveSpaceChars(Regex.Match(rowContent, @"大小\s*:\s*([\d\.]+\s*[A-Z]+)", RegexOptions.IgnoreCase).GetGroupValue(1));
				item.UpdateTimeDesc = Regex.Match(rowContent, @"更新时间:\s*<span.*?>(.*?)</span>", RegexOptions.IgnoreCase).GetGroupValue(1) ?? "";
				item.FileCount = Regex.Match(rowContent, @"文件数\s*:\s*([\d\.,]+)", RegexOptions.IgnoreCase).GetGroupValue(1).ToInt32();

				result.Add(item);
			}

			result.HasPrevious = result.PageIndex > 1;
			result.HasMore = result.Count > 0 && html.IndexOf("<li class=\"disabled\"><a href=\"#\"> Next", StringComparison.OrdinalIgnoreCase) == -1;
		}

		#endregion

		/// <summary>
		/// 测试服务状态
		/// </summary>
		/// <returns></returns>
		public override TestStatus Test()
		{
			return TestCore($"https://{Host}/", "磁力搜索");
		}
	}
}
