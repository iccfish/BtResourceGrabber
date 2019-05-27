namespace BRG.Engines.BuildIn.SearchProviders
{
	using System;
	using System.ComponentModel.Composition;
	using System.Text.RegularExpressions;
	using System.Web;
	using BRG;
	using BRG.Engines.Entities;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;

	//[Export(typeof(IResourceProvider))]
	internal class BtCheerySearchProvider : AbstractSearchServiceProvider<BuildinServerInfo>, IResourceProvider
	{
		public BtCheerySearchProvider()
			: base(new BuildinServerInfo("BT樱桃", Properties.Resources.favicon_btcherry, "提供对BT樱桃的搜索支持"))
		{
			((BuildinServerInfo)Info).Version = new Version("1.1.1.0");
			RequireBypassGfw = false;
			SupportLookupTorrentContents = true;
			SupportUnicode = true;
			SupportCustomizePageSize = false;
		}

		/// <summary>
		/// 连接
		/// </summary>
		public override void Connect()
		{
			DefaultHost = "www.btcherry.net";
			ReferUrlPage = $"https://{Host}/";

			base.Connect();
		}

		/// <summary>
		/// 测试服务状态
		/// </summary>
		/// <returns></returns>
		public override TestStatus Test()
		{
			return TestCore(ReferUrlPage, "BT樱桃");
		}

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
			if (context.IsRedirection && context.Redirection.Orginal.Host != context.Redirection.Current.Host)
			{
				Host = context.Redirection.Current.Host;
				result.RequestResearch = true;
				return false;
			}

			//分析
			var strpos = 0;
			string row;
			//var nameMap = Regex.Matches(html.Result, @"map\[['""]([a-z\d]{40})[""']\].*?['""](.*?)[""'];", RegexOptions.IgnoreCase)
			//					.Cast<Match>().Where(s => s.Success).ToDictionary(s => s.Groups[1].Value, s => s.Groups[2].Value);

			while (!string.IsNullOrEmpty((row = context.Result.SearchStringTag("<div class=\"r\">", "磁力链接</span>", ref strpos))))
			{
				row = System.Web.HttpUtility.HtmlDecode(row);

				var hash = Regex.Match(row, @"/t/([a-f\d]{40})", RegexOptions.IgnoreCase).GetGroupValue(1).ToUpper();
				if (string.IsNullOrEmpty(hash))
					continue;

				var item = new ResourceInfo();
				item.Hash = hash;
				item.Provider = this;

				//标题
				item.Title = Regex.Match(row, "<h5.*?>(.*?)</h5>", RegexOptions.IgnoreCase | RegexOptions.Singleline).GetGroupValue(1);
				if (string.IsNullOrEmpty(item.Title))
					continue;

				item.DownloadSize = Regex.Match(row, @"大小.*?>([\d\.]+\s*[^<\s]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline).GetGroupValue(1);
				item.FileCount = Regex.Match(row, @"文件数：.*?>([\d\.\,]+\s*)", RegexOptions.IgnoreCase | RegexOptions.Singleline).GetGroupValue(1).ToInt32();
				item.UpdateTime = Regex.Match(row, @"收录时间.*?>([\d\-]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline).GetGroupValue(1).ToDateTimeNullable();

				result.Add(item);
			}
			result.TotalPages = Regex.Match(context.Result, @"totalPages\s*:\s*(\d+)", RegexOptions.IgnoreCase | RegexOptions.Singleline).GetGroupValue(1).ToInt32();
			result.HasPrevious = result.PageIndex > 1;
			result.HasMore = result.PageIndex < result.TotalPages.Value;
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
			return $"http://{Host}/hash/" + resource.Hash;
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			return $"http://{Host}/search?keyword={HttpUtility.UrlPathEncode(key)}&p={pageindex}";
		}


		/// <summary>
		/// 加载内容
		/// </summary>
		public override void LookupTorrentContents(IResourceInfo torrent)
		{
			var url = GetDetailUrl(torrent);
			var htmlContext = NetworkClient.Create<string>(HttpMethod.Get, url, ReferUrlPage).Send();
			if (!htmlContext.IsValid())
				return;

			var html = htmlContext.Result.SearchStringTag("id=\"filelist\">", "</ul>");
			if (string.IsNullOrEmpty(html))
				return;

			var charPos = 0;
			var row = "";
			while (!string.IsNullOrEmpty((row = html.SearchStringTag("<li", "</li>", ref charPos))))
			{
				var m = Regex.Match(row, "<span.*?>([^<]+)</span><span.*?>([^<]+)</span>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
				if (!m.Success)
					continue;

				var path = m.GetGroupValue(1);
				var size = m.GetGroupValue(2);

				AddFileNode(torrent, BrtUtility.ClearString(path), null, BrtUtility.ClearString(size));
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
