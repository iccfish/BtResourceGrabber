using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRG.Engines.BuildIn.SearchProviders
{
	using System.ComponentModel.Composition;
	using System.Drawing;
	using System.Text.RegularExpressions;

	using BRG.Entities;

	using Entities;

	using FSLib.Network.Http;

	using HtmlAgilityPack;

	using Service;

	[Export(typeof(IResourceProvider))]
	class BtSceneSearchProvider : AbstractBuildInSearchServiceProviderWithSiteData
	{
		public BtSceneSearchProvider()
			: base("BTScene", Properties.Resources.favicon_btscene, "搜索BTScene", "1.0.0.0")
		{
			RequireBypassGfw = false;
			SupportSortType = null;
			SupportUnicode = false;
			PageCheckKey = "135283409842916";
			ReferUrlPage = "http://www.btscene.cc/";
			SiteID = SiteData.SiteID_BtScene;
			SupportLookupTorrentContents = true;
			SupportResourceInitialMark = false;
		}

		/// <summary>
		/// 连接
		/// </summary>
		public override void Connect()
		{
			base.Connect();

			DefaultHost = "www.btscene.cc";
			ReferUrlPage = $"http://{Host}/";
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			var order = 1;
			if (sortDirection == 4 && sortType == SortType.FileSize)
			{
				order = 4;
			}
			return $"http://{Host}/results.php?q={UE(key)}&order={order}&page={pageindex}";
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrlCore(IResourceInfo resource, SiteInfo data)
		{
			//http://www.btscene.cc/0sec-presents-game-of-thrones-s05e05-720p-hdtv-x264-tf4649378.html
			return $"http://{Host}/{data.PageName}-tf{data.SiteID}.html";
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
			var document = CreateHtmlDocument(htmlContent);
			var nodes = document.DocumentNode.SelectNodes("//table[@class='tor'][last()]//tr[position()>1]");

			foreach (var node in nodes)
			{
				var titlelink = node.SelectSingleNode("td[1]/a");
				var href = titlelink.GetAttributeValue("href", "");
				var hrefm = Regex.Match(href, @"/(.*?)\-tf(\d+)\.html", RegexOptions.IgnoreCase);
				if (!hrefm.Success)
					continue;

				var title = titlelink.InnerText;
				var sid = hrefm.GetGroupValue(2).ToInt64();
				var pagename = hrefm.GetGroupValue(1);
				var size = node.SelectSingleNode("td[2]").InnerText;
				var update = node.SelectSingleNode("td[5]").InnerText;

				var resinfo = CreateResourceInfo(null, title);
				resinfo.DownloadSize = size;
				resinfo.UpdateTimeDesc = update;
				resinfo.SiteData = new SiteInfo()
				{
					SiteID = sid,
					PageName = pagename
				};

				result.Add(resinfo);
			}

			result.HasPrevious = result.PageIndex > 1;
			result.HasMore = document.GetElementbyId("pgright") != null;

			return base.LoadCore(context, url, htmlContent, result);
		}

		protected override void LoadFullDetailCore(IResourceInfo info)
		{
			var ctx = NetworkClient.Create<HtmlDocument>(HttpMethod.Get, GetDetailUrl(info), ReferUrlPage).Send();
			if (!ctx.IsValid())
				return;

			//hash
			var mlink = ctx.Result.DocumentNode.SelectSingleNode("//h3[contains(text(), 'Hash')]/following::p");
			if (mlink == null)
				return;

			var hash = Regex.Match(mlink.InnerText, @"[a-f\d]{40}").Value;
			info.Hash = hash;

			//files
			var fileContainer = ctx.Result.DocumentNode.SelectSingleNode("//h3[contains(text(), 'Files')]/following::div/following::div");
			ProcessFileList(fileContainer, info);

			base.LoadFullDetailCore(info);
		}

		void ProcessFileList(HtmlNode container, ICollection<IFileNode> parent)
		{
			var children = container.SelectNodes("div[@class='int_href'] | div[@class='int_href_array']");
			if (children == null)
				return;

			foreach (var node in children)
			{
				//Ratatouille.German.2007.AC3.BluRay.720p.x264-x264Crew.mkv   4.31 GB
				var folderNode = node.SelectSingleNode("a");
				if (folderNode == null)
				{
					//not folder
					var name = Regex.Match(node.InnerText.Trim(), @"^(.*?)\s+([\d\.]+\s+.*)$");
					parent.Add(new FileNode() { Name = name.GetGroupValue(1), SizeString = name.GetGroupValue(2) });
				}
				else
				{
					var fn = new FileNode() { Name = folderNode.InnerText };
					ProcessFileList(folderNode.NextSibling, fn.Children);
					parent.Add(fn);
				}
			}
		}
	}
}
