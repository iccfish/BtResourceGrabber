using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRG.Engines.BuildIn.SearchProviders
{
	using System.ComponentModel.Composition;
	using System.Text.RegularExpressions;
	using BRG.Engines.Entities;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;
	using HtmlAgilityPack;

	[Export(typeof(IResourceProvider))]
	class BreadSearchProvider : AbstractSearchServiceProvider<BuildinServerInfo, SiteInfo>
	{
		public BreadSearchProvider() : base(new BuildinServerInfo("面包搜索", Properties.Resources.favicon_breadsearch_11, "提供对面包搜索的支持"))
		{
			SupportUnicode = true;
			SupportLookupTorrentContents = true;
			SupportResourceInitialMark = true;
		}

		/// <summary>
		/// 连接
		/// </summary>
		public override void Connect()
		{
			base.Connect();

			DefaultHost = "www.breadsearch.com";   //http:///link/mp6TuAFQC5k8STE1AcySFVKHoHktlsYK6x-nBr79bL7OkZr3Gocms3DIAlHGREdW
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			return $"http://{Host}/search/{UE(key)}/{pageindex}";
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrlCore(IResourceInfo resource, SiteInfo data)
		{
			return $"http://{Host}/link/{data.PageName}";
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
			if (!context.IsValid())
				return false;

			var doc = new HtmlDocument();
			//doc.IgnoreWhiteSpace = true;
			doc.LoadHtml(context.Result);

			var items = doc.DocumentNode.SelectNodes("//div[@class='search-item']");
			if (items != null)
			{
				foreach (var item in items)
				{
					var titlelink = item.SelectSingleNode(".//span[@class='list-title']/a");
					var title = titlelink.InnerText.Trim();
					var link = titlelink.GetAttributeValue("href", "");

					//page name  /link/lId3hWZ2mE6NM2DoScAGFPRa6Mgf1zRTTFz1kLLMR2Tls1PzR_mOMiHf5aCFrPkJ
					var pagename = Regex.Match(link, @"link/([a-z\d-_]+)", RegexOptions.IgnoreCase).GetGroupValue(1);

					var attLabels = item.SelectNodes(".//span[@class='list-label']");
					DateTime? updateTime = null;
					int? fileCount = null;
					string size = null;

					foreach (var label in attLabels)
					{
						var attName = label.InnerText;

						if (attName.Contains("时间"))
							updateTime = label.NextSibling.InnerText.ToDateTimeNullable();
						else if (attName.Contains("大小"))
							size = label.NextSibling.InnerText.Trim();
						else if (attName.Contains("文件数"))
							fileCount = label.NextSibling.InnerText.ToInt32Nullable();
					}

					var magLink = Regex.Match(item.SelectSingleNode(".//a[starts-with(@href,'magnet:')]").GetAttributeValue("href", ""), @"[A-F\d]{40}", RegexOptions.IgnoreCase).GetGroupValue(0);

					var resItem = CreateResourceInfo(magLink, title);
					resItem.SiteData = new SiteInfo() { PageName = pagename };
					resItem.UpdateTime = updateTime;
					resItem.FileCount = fileCount;
					resItem.DownloadSize = size;

					result.Add(resItem);
				}
			}

			var totalPages = Regex.Match(context.Result, @"totalPages\s*:\s*(\d+)").GetGroupValue(1)?.ToInt32() ?? 0;
			result.HasMore = result.PageIndex < totalPages;


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

			var items = doc.DocumentNode.SelectNodes("//ul[@class='file-list']/li");
			if (items != null)
			{
				foreach (var item in items)
				{
					var filename = UD(item.SelectSingleNode("./span[1]").InnerText);
					var size = item.SelectSingleNode("./span[2]").InnerText;

					AddFileNode(torrent, filename, null, size);
				}
			}

			base.LookupTorrentContentsCore(url, torrent, htmlContent);
		}
	}
}
