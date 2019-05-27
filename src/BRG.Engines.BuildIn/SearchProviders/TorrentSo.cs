using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRG.Engines.BuildIn.SearchProviders
{
	using System.ComponentModel.Composition;
	using System.Text.RegularExpressions;
	using BRG.Entities;
	using BRG.Service;

	using FSLib.Extension.FishLib;
	using FSLib.Network.Http;
	using HtmlAgilityPack;

	[Export(typeof(IResourceProvider))]
	class TorrentSo : AbstractSearchServiceProvider<BuildinServerInfo>
	{
		public TorrentSo() : base(new BuildinServerInfo("TorrentSo", Properties.Resources.globe_16, "提供对Torrent.so的搜索支持"))
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

			DefaultHost = "www.torrent.so";
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrl(IResourceInfo resource)
		{
			return $"http://{Host}/info/{resource.Hash}";
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			return $"http://www.torrent.so/s/{UE(key).ToUpper()}/{pageindex}/";
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
			var url = $"http://api.xhub.cn/api.php?op=search_list&callback=jQuery{DateTime.Now.Ticks}&key={UE(key)}&page={pageindex}&_={DateTime.Now.ToJsTicks()}";
			var refer = GetSearchUrl(key, sortType, sortDirection, pagesize, pageindex);

			var result = CollectionUtility.CreateAnymousDictionary("", new { day = DateTime.Now, hits = 0, size = "", title = "" });
			var ctx = NetworkClient.Create(HttpMethod.Get, url, refer, result: new
			{
				data = result,
				total = 0,
				status = 0
			}).Send();
			if (!ctx.IsValid())
				return null;

			var items = new ResourceSearchInfo(this)
			{
				PageIndex = pageindex,
				PageSize = 30,
				TotalPages = (int)Math.Ceiling(ctx.Result.total / 30.0)
			};
			items.HasMore = pageindex < (items.TotalPages ?? 1);
			if (ctx.Result.data != null)
			{
				foreach (var item in ctx.Result.data)
				{
					var res = CreateResourceInfo(item.Key, item.Value.title);
					res.UpdateTime = item.Value.day;
					res.DownloadSize = item.Value.size;

					items.Add(res);
				}
			}

			if (base.LoadCore(null, url, null, items))
				return items;

			return null;
		}

		///// <summary>
		///// 核心加载
		///// </summary>
		///// <param name="context"></param>
		///// <param name="url"></param>
		///// <param name="htmlContent">HTML内容</param>
		///// <param name="result">目标结果</param>
		///// <returns></returns>
		//protected override bool LoadCore(HttpContext<string> context, string url, string htmlContent, ResourceSearchInfo result)
		//{
		//var doc = new HtmlDocument();
		//doc.LoadHtml(htmlContent);

		//var nodes = doc.DocumentNode.SelectNodes("//div[@class='listhide']//li");
		//if (nodes != null)
		//{
		//	foreach (var node in nodes)
		//	{
		//		var titlelink = node.SelectSingleNode("./a");
		//		var hash = Regex.Match(titlelink.GetAttributeValue("href", ""), @"info/([a-f\d]{40})", RegexOptions.IgnoreCase).GetGroupValue(1);
		//		var title = titlelink.GetAttributeValue("href", "");

		//		var size = Regex.Match(node.SelectSingleNode(".//span[@class='filesize_']").InnerText.Trim(), @"[:：]\s*([^<>]+)", RegexOptions.IgnoreCase).GetGroupValue(1);
		//		var time = Regex.Match(node.SelectSingleNode(".//span[@class='days_']").InnerText.Trim(), @"[:：]\s*([^<>]+)", RegexOptions.IgnoreCase).GetGroupValue(1).ToDateTimeNullable();

		//		var res = CreateResourceInfo(hash, title);
		//		res.UpdateTime = time;
		//		res.DownloadSize = size;

		//		result.Add(res);
		//	}
		//}

		//result.HasMore = htmlContent.IndexOf(">Next Page</a>") != -1;

		//return base.LoadCore(context, url, htmlContent, result);
		//}

		/// <summary>
		/// 加载内容
		/// </summary>
		/// <param name="url"></param>
		public override void LookupTorrentContentsCore(string url, IResourceInfo torrent, string htmlContent)
		{
			var doc = new HtmlDocument();
			doc.LoadHtml(htmlContent);

			var files = doc.GetElementbyId("file_list")?.SelectNodes(".//div");
			if (files != null)
			{
				foreach (var file in files)
				{
					var name = UD(file.SelectSingleNode("./dt/b").InnerText.Trim());
					var size = file.SelectSingleNode("./dd").InnerText.Trim();

					AddFileNode(torrent, name, null, size);
				}
			}

			base.LookupTorrentContentsCore(url, torrent, htmlContent);
		}
	}
}
