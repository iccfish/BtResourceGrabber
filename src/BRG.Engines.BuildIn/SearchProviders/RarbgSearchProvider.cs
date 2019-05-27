namespace BRG.Engines.BuildIn.SearchProviders
{
	using System;
	using System.ComponentModel.Composition;
	using System.Linq;
	using System.Text.RegularExpressions;
	using System.Windows.Forms;
	using BRG.Engines.BuildIn.DownloadProviders;
	using BRG.Engines.BuildIn.UI.Engines.Rarbg;
	using BRG.Engines.Entities;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;

	using Ivony.Html;
	using Ivony.Html.ExpandedAPI;

	[Export(typeof(IResourceProvider))]
	class RarbgSearchProvider : AbstractSearchServiceProvider<BuildinServerInfo, SiteInfo>, IResourceProvider
	{
		bool _inited = false;
		public RarbgSearchProvider() : base(new BuildinServerInfo("Rarbg", Properties.Resources.favicon_rarbg, "提供对RARBG的搜索支持"))
		{
			RequireBypassGfw = false;
			SupportUnicode = false;
			SupportCustomizePageSize = false;
			SupportLookupTorrentContents = true;
			SupportResourceInitialMark = false;
			SupportSortType = SortType.Default | SortType.PubDate | SortType.FileSize;
			SupportOption = true;

			PageCheckKey = @"opensearchdescription+xml";

			SetPreferDownloader<RarbgDownloadProvider>();
			SiteID = SiteData.SiteID_Rarbg;
		}

		/// <summary>
		/// 连接
		/// </summary>
		public override void Connect()
		{
			DefaultHost = "rarbg.to";
			ReferUrlPage = $"https://{Host}/torrents.php";

			NetworkClient.RequestSuccess += NetworkClient_RequestSuccess;
			base.Connect();
		}


		private void NetworkClient_RequestSuccess(object sender, WebEventArgs e)
		{
			if (e.Response.Content is ResponseStringContent)
			{
				var resp = e.Response.Content as ResponseStringContent;
				if (resp.StringResult.IndexOf("") != -1)
				{
					var code = Regex.Match(resp.StringResult, @"mcpslar\(['""]([^""']+)['""]\s*,\s*['""]([^""']+)['""]");
					if (code.Success)
					{
						NetworkClient.ImportCookies($"{code.Groups[1].Value}={code.Groups[2].Value}", new Uri(ReferUrlPage));
					}
				}
			}
		}

		void CheckInitLoad()
		{
			if (_inited)
				return;

			var i = 0;
			while (i++ < 3)
			{
				var ctx = NetworkClient.Create<string>(HttpMethod.Get, ReferUrlPage).Send();
				if (!ctx.IsValid())
					continue;

				IsHostChanged(ctx);
				break;
			}

			_inited = true;
		}

		/// <summary>
		/// 显示选项
		/// </summary>
		public override void ShowOption()
		{
			new RarbgConfig(Property).ShowDialog(AppContext.Instance.MainForm);
		}

		///// <summary>
		///// 连接
		///// </summary>
		//public override void Connect()
		//{
		//	var currentConfigVersion = Property.GetValue("init_level").ToInt32();
		//	if (currentConfigVersion < 1)
		//	{
		//		//默认独立
		//		AppContext.Instance.Options.EngineStandalone.SafeAdd(Info.Name);
		//	}

		//	Property.AddOrUpdate("init_level", "1");

		//	base.Connect();
		//}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			//https://rarbg.com/torrents.php?search=1pondo&order=data&by=ASC&page=2
			key = UE(key);
			var order = sortType == SortType.FileSize ? "size" : sortType == SortType.PubDate ? "data" : "";
			var by = sortDirection == 1 ? "DESC" : "ASC";

			return $"https://{Host}/torrents.php?search={key}&order={order}&by={by}&page={pageindex}";
		}

		/// <summary>
		/// 是否需要BOT检查
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public bool IsBotCheckNeeded(HttpContext context)
		{
			return context.IsRedirection && context.Redirection.Current.PathAndQuery.IndexOf("bot", StringComparison.OrdinalIgnoreCase) != -1;
		}

		public bool IsHostChanged(HttpContext context)
		{
			if (context == null || !context.IsRedirection || context.Redirection.Current.PathAndQuery.IndexOf("torrent") == -1)
				return false;

			Host = context.Redirection.Current.Host;
			_inited = false;
			CheckInitLoad();

			return true;
		}

		public bool RequireBotCheck()
		{
			if (Property.ContainsKey("skip_bot_check"))
				return false;

			bool? result = BotCheckMain();

			if (result == null)
			{
				if (!Property.ContainsKey("skip_bot_check"))
					Property.Add("skip_bot_check", "");
			}

			return result == true;
		}

		protected virtual bool? BotCheckMain()
		{
			bool? result = null;

			AppContext.Instance.MainForm.Invoke(() =>
			{
				using (var dlg = new BotCheck(this))
				{
					var ret = dlg.ShowDialog(AppContext.Instance.MainForm);
					if (ret == DialogResult.Cancel)
						result = null;
					result = ret == DialogResult.Yes;
				}
			});

			return result;
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
			CheckInitLoad();
			return base.Load(key, sortType, sortDirection, pagesize, pageindex);
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
			if (IsHostChanged(context))
			{
				result.RequestResearch = true;
				return false;
			}
			if (IsBotCheckNeeded(context))
			{
				if (RequireBotCheck())
				{
					result.RequestResearch = true;
				}

				return false;
			}

			var doc = CreateHtmlDom(url, htmlContent);

			var rows = doc.Find("table.lista2t tr").Skip(1);
			foreach (var row in rows)
			{
				var titlelink = row.FindFirstOrDefault("td:nth-child(2) a");
				var titleHref = titlelink.GetAttributeValue("href");
				//如果是提供的直接下载，则忽略。这个是广告。
				if (titleHref.IsNullOrEmpty() || titleHref.IndexOf("torrent") == -1)
					continue;

				//基本信息
				///torrent/zxv627d
				var sidstr = Regex.Match(titlelink.GetAttributeValue("href"), @"torrent/([\da-z]+)", RegexOptions.IgnoreCase).GetGroupValue(1);
				var hash = Regex.Match(titlelink.GetAttributeValue("onmouseover") ?? "", @"/([a-z\d]{40})", RegexOptions.IgnoreCase).GetGroupValue(1);
				if (hash.IsNullOrEmpty())
					continue;

				var title = titlelink.InnerText();

				var size = row.FindFirstOrDefault("td:nth-child(4)").InnerText();
				var date = row.FindFirstOrDefault("td:nth-child(3)").InnerText().Trim().ToDateTimeNullable();

				var item = CreateResourceInfo(hash, title);
				item.DownloadSize = size;
				item.UpdateTime = date;
				item.SiteData = new SiteInfo()
				{
					SiteID = 0L,
					PageName = sidstr,
					ProvideSiteDownload = true,
					SiteDownloadLink = $"https://{Host}/download.php?id={sidstr}&f={title}.torrent"
				};

				//预览信息
				var mouseout = titlelink.GetAttributeValue("onmouseover");
				if (!mouseout.IsNullOrEmpty())
				{
					var imgsrc = Regex.Match(mouseout, @"<img.*?src=.*?//([^'""\\]+)").GetGroupValue(1);
					if (!imgsrc.IsNullOrEmpty())
					{
						item.SupportPreivewType = PreviewType.Image;
						item.PreviewInfo = new PreviewInfo() { ImageUrl = $"http://dyncdn.me/posters2/{hash[0]}/{hash}.jpg" };
					}
				}

				result.Add(item);
			}

			var pager = doc.FindFirstOrDefault("div#pager_links");
			if (pager != null)
			{
				result.HasPrevious = pager.FindFirstOrDefault("*:first-child")?.Name == "a";
				result.HasMore = pager.FindFirstOrDefault("*:last-child")?.Name == "a";
			}

			return base.LoadCore(context, url, htmlContent, result);
		}


		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrlCore(IResourceInfo resource, SiteInfo data)
		{
			return $"https://{Host}/torrent/{data.PageName}";
		}

		/// <summary>
		/// 加载相信信息
		/// </summary>
		protected override void LoadFullDetailCore(IResourceInfo info)
		{
			var url = GetDetailUrl(info);
			var html = NetworkClient.Create<string>(HttpMethod.Get, url, ReferUrlPage).Send();
			var sinfo = (ResourceInfo)info;

			if (html.IsValid())
			{
				if (IsHostChanged(html))
				{
					base.LoadFullDetail(info);
					return;
				}
				if (IsBotCheckNeeded(html))
				{
					if (RequireBotCheck())
					{
						base.LoadFullDetail(info);
					}

					return;
				}

				sinfo.Hash = Regex.Match(html.Result, @"btih:([a-z\d]{40})", RegexOptions.IgnoreCase).GetGroupValue(1);

				LookupTorrentContentsCore(url, info, html.Result);
			}

			base.LoadFullDetailCore(info);
		}

		/// <summary>
		/// 加载内容
		/// </summary>
		/// <param name="url"></param>
		public override void LookupTorrentContentsCore(string url, IResourceInfo torrent, string htmlContent)
		{
			var doc = CreateHtmlDom(url, htmlContent);
			var nodes = doc.Find("div#files tr");
			if (nodes != null)
			{
				foreach (var node in nodes.Skip(1))
				{
					var path = node.FindSingleOrDefault("td:first-child")?.InnerText().Trim();
					var size = node.FindSingleOrDefault("td:last-child")?.InnerText().Trim();

					AddFileNode(torrent, path, null, size);
				}
			}

			base.LookupTorrentContentsCore(url, torrent, htmlContent);
		}
	}
}
