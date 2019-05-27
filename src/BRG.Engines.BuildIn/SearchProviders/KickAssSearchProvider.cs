namespace BRG.Engines.BuildIn.SearchProviders
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.Net;
	using System.Text.RegularExpressions;
	using System.Web;
	using BRG.Engines.Entities;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;

	[Export(typeof(IResourceProvider))]
	class KickAssSearchProvider : AbstractSearchServiceProvider<BuildinServerInfo, SiteInfo>, IResourceProvider
	{
		public KickAssSearchProvider() : base(new BuildinServerInfo("KickAss", Properties.Resources.favicon_kickass, "提供对KickAss的搜索支持"))
		{
			SupportCustomizePageSize = false;
			SupportLookupTorrentContents = true;
			SupportResourceInitialMark = true;
			SupportSortType = SortType.Default | SortType.FileSize | SortType.PubDate;
			SupportUnicode = false;
			RequireBypassGfw = false;
			PageCheckKey = "KickassTorrents</title>";
			//SiteID = SiteData.SiteID_KickAss;
		}

		/// <summary>
		/// 连接
		/// </summary>
		public override void Connect()
		{
			base.Connect();

			DefaultHost = "kat.cr";
			ReferUrlPage = $"https://{Host}/";
		}


		/// <summary>
		/// 核心加载
		/// </summary>
		/// <param name="context"></param>
		/// <param name="url"></param>
		/// <param name="htmlContent"></param>
		/// <returns></returns>
		protected override bool LoadCore(HttpContext<string> context, string url, string htmlContent, ResourceSearchInfo result)
		{
			if (context.Response.Status == HttpStatusCode.MovedPermanently)
			{
				//Host切换了？
				var host = context.Response.Redirection.Current.Host;
				if (host != Host)
				{
					Host = host;

					result.RequestResearch = true;
				}

				return false;
			}

			var doc = CreateHtmlDocument(context.Result);
			var rows = doc.DocumentNode.SelectNodes("//table[@class='data']//tr[position()>1]");
			if (rows == null)
				return false;

			foreach (var row1 in rows)
			{
				var magnet = row1.SelectSingleNode(".//a[contains(@title, 'magnet')]")?.GetAttributeValue("href", "");
				var hash = Regex.Match(magnet, @"btih:([a-z\d]{40})", RegexOptions.IgnoreCase).GetGroupValue(1);

				var titlelink = row1.SelectSingleNode(".//a[contains(@class,'cellMainLink')]");
				var title = titlelink.InnerText;
				var size = row1.SelectSingleNode("td[2]").InnerText.Trim();
				var files = row1.SelectSingleNode("td[3]").InnerText.Trim().ToInt32();
				var age = row1.SelectSingleNode("td[4]").InnerText.Trim();

				var res = CreateResourceInfo(hash, title);
				res.FileCount = files;
				res.DownloadSize = size;
				res.UpdateTimeDesc = age;

				//sitedata
				//kingsman-the-secret-service-2014-hc-hdrip-xvid-ac3-etrg-por-talamasca32-srt-t10610377.html
				var href = titlelink.GetAttributeValue("href", "");
				var info = Regex.Match(href, @"/(.+?)-t(\d+)\.html", RegexOptions.Singleline | RegexOptions.IgnoreCase);
				res.SiteData = new SiteInfo()
				{
					PageName = info.GetGroupValue(1),
					SiteID = info.GetGroupValue(2).ToInt64()
				};

				result.Add(res);
			}
			result.HasPrevious = result.PageIndex > 1;
			result.HasMore = !doc.DocumentNode.SelectSingleNode("//div[contains(@class,'pages')]/a[last()]").GetAttributeValue("href", "").IsNullOrEmpty();

			return base.LoadCore(context, url, htmlContent, result);
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrlCore(IResourceInfo resource, SiteInfo data)
		{
			return $"https://{Host}/{HttpUtility.UrlEncode(data.PageName)}-t{data.SiteID}.html";
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			var field = "";
			switch (sortType)
			{
				case SortType.Default:
					break;
				case SortType.Title:
					break;
				case SortType.PubDate:
					field = "time_add";
					break;
				case SortType.FileSize:
					field = "size";
					break;
				default:
					break;
			}
			return $"https://{Host}/usearch/{HttpUtility.UrlEncode(key)}/{pageindex}/?field={field}&sorder={(sortDirection == 0 ? "asc" : "desc")}";
		}

		/// <summary>
		/// 加载内容
		/// </summary>
		public override void LookupTorrentContents(IResourceInfo torrent)
		{
			var url = $"https://{Host}/torrents/getfiles/{torrent.Hash}/";

			var ctx = NetworkClient
				.Create(HttpMethod.Post, url, ReferUrlPage, new { ajax = 1, all = 1 }, new { html = "" })
				.Send();

			if (!ctx.IsValid())
				return;

			var html = ctx.Result.html;
			var reg = new Regex(@"<(tr|/tr|table|/table)\s*(class=""innerFolder"")?.*?>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			IFileNode parentNode = null;
			Stack<IFileNode> nodesChain = new Stack<IFileNode>();

			//文件夹名称
			var folderNameReg = new Regex(@"<span\s*class=""folder"">.*?"">([^><]+)</a></span>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			//文件
			var fileReg = new Regex(@"<td\s*class=""torFileName""\s*title=""(.*?)"">.*?<td\s*class=""torFileSize"">(.*?)</td>", RegexOptions.Singleline | RegexOptions.IgnoreCase);


			var match = reg.Match(html);
			//跳过第一个开始
			while ((match = match.NextMatch()).Success)
			{
				var tag = match.Groups[1].Value;

				if (tag == "/table")
				{
					//文件夹结束
					if (nodesChain.Count > 0)
					{
						parentNode = nodesChain.Pop();
					}
					else
					{
						parentNode = null;
					}
				}
				else if (tag == "tr" && match.Groups[2].Success)
				{
					//文件夹节点
					var item = new FileNode();
					item.IsDirectory = true;
					item.Name = folderNameReg.Match(html, match.Index).Groups[1].Value;

					if (parentNode != null)
					{
						nodesChain.Push(parentNode);
						parentNode.Children.Add(item);
					}
					else
					{
						torrent.Add(item);
					}
					parentNode = item;
				}
				else if (tag == "tr")
				{
					//文件节点
					var item = new FileNode();
					item.IsDirectory = false;

					var info = fileReg.Match(html, match.Index);
					item.Name = info.Groups[1].Value;
					item.SizeString = info.Groups[2].Value;

					if (parentNode != null)
					{
						parentNode.Children.Add(item);
					}
					else
					{
						torrent.Add(item);
					}
				}

			}

		}
	}
}
