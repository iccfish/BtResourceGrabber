namespace BRG.Engines.BuildIn.SearchProviders
{
	using System;
	using System.ComponentModel.Composition;
	using System.Linq;
	using System.Text.RegularExpressions;
	using System.Web;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;

	using Ivony.Html;
	using Ivony.Html.ExpandedAPI;
	using Ivony.Html.Parser;

	[Export(typeof(IResourceProvider))]
	class TorrentKittySearchProvider : AbstractSearchServiceProvider<BuildinServerInfo>, IResourceProvider
	{
		bool _loaded = false;

		public TorrentKittySearchProvider()
			: base(new BuildinServerInfo("TorrentKitty", Properties.Resources.favicon_torrentkitty, "提供对TorrentKitty的搜索支持"))
		{
			SupportResourceInitialMark = true;
			SupportLookupTorrentContents = true;
			SupportSortType = null;
			RequireBypassGfw = false;
			SupportUnicode = true;

			PageCheckKey = @"MP2pq68wfcGc6cm6gQ2YtSeBN9XJwIbQSdrINW1KO9Y";
		}

		/// <summary>
		/// 连接
		/// </summary>
		public override void Connect()
		{
			base.Connect();

			DefaultHost = "www.torrentkitty.org";
			ReferUrlPage = $"http://{Host}/";
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrl(IResourceInfo resource)
		{
			return $"http://{Host}/information/{resource.Hash}";
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			return $"http://{Host}/search/{HttpUtility.UrlEncode(key)}/{pageindex}";
		}

		/// <summary>
		/// 加载数据
		/// </summary>
		/// <param name="key"></param>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <returns></returns>
		public override IResourceSearchInfo Load(string key, SortType sortType, int sortDirection, int pagesize, int pageindex = 1, int loadStack = 0)
		{
			if (!_loaded)
			{
				NetworkClient.Create<string>(HttpMethod.Get, ReferUrlPage).Send();
				_loaded = true;
			}
			return base.Load(key, sortType, sortDirection, pagesize, pageindex, loadStack);
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
			var parser = new JumonyParser();
			var doc = parser.Parse(htmlContent, new Uri(ReferUrlPage));

			var node = doc.Find("#archiveResult tr").Skip(1);
			foreach (var row in node)
			{
				var title = row.FindFirstOrDefault("td.name")?.InnerText();
				//var size = row.FindFirstOrDefault("td.size")?.InnerText();
				var date = row.FindFirstOrDefault("td.date")?.InnerText()?.ToDateTimeNullable();
				var has = Regex.Match(row.FindFirstOrDefault("td.action a:nth-child(1)").Attribute("href").AttributeValue, @"/([a-z\d]{40})", RegexOptions.IgnoreCase).GetGroupValue(1);

				var item = CreateResourceInfo(has, title);
				//item.DownloadSize = size;
				item.UpdateTime = date;

				result.Add(item);
			}

			var pager = doc.FindFirstOrDefault("div.pagination");

			result.HasPrevious = pager?.FindFirstOrDefault("*:first-child")?.Name == "a";
			result.HasMore = pager?.FindFirstOrDefault("*:last-child")?.Name == "a";

			return base.LoadCore(context, url, htmlContent, result);
		}

		/// <summary>
		/// 加载内容
		/// </summary>
		/// <param name="url"></param>
		public override void LookupTorrentContentsCore(string url, IResourceInfo torrent, string htmlContent)
		{
			var doc = CreateHtmlDocument(htmlContent);
			var nodes = doc.DocumentNode.SelectNodes("//table[@id='torrentDetail']//tr[position()>1]");

			if (nodes != null)
			{
				foreach (var node in nodes)
				{
					AddFileNode(torrent, node.SelectSingleNode("td[2]").InnerText,null, node.SelectSingleNode("td[3]").InnerText);
				}
			}

			base.LookupTorrentContentsCore(url, torrent, htmlContent);
		}
	}
}
