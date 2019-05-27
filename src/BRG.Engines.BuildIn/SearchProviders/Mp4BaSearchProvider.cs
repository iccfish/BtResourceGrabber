using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRG.Engines.BuildIn.SearchProviders
{
	using System.ComponentModel.Composition;
	using System.Drawing;
	using System.Text.RegularExpressions;
	using BRG.Engines.BuildIn.DownloadProviders;
	using BRG.Engines.Entities;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;
	using HtmlAgilityPack;

	[Export(typeof(IResourceProvider))]
	class Mp4BaSearchProvider : AbstractBuildInSearchServiceProviderWithSiteData
	{
		public Mp4BaSearchProvider() : base("高清MP4", Properties.Resources.favicon_mp4ba_16, "提供对高清MP4的搜索支持", "1.0.0.0")
		{
			RequireBypassGfw = false;
			ReferUrlPage = "http://www.mp4ba.com/";
			PageCheckKey = "高清Mp4吧";
			SupportLookupTorrentContents = true;
			SupportResourceInitialMark = true;
			SupportUnicode = true;
			SetPreferDownloader<Mp4BaDownloadProvider>();
		}

		/// <summary>
		/// 连接
		/// </summary>
		public override void Connect()
		{
			base.Connect();

			if (!Property.ContainsKey("first_run_done"))
			{
				AppContext.Instance.Options.EngineStandalone.Add(Info.Name);
				Property.Add("first_run_done", "1");
			}
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrlCore(IResourceInfo resource, SiteInfo data)
		{
			return $"http://www.mp4ba.com/show.php?hash={resource.Hash.ToLower()}";
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			return $"http://www.mp4ba.com/search.php?keyword={UE(key)}&page={pageindex}";
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
			var tbody = document.GetElementbyId("data_list");
			if (tbody == null)
				return false;

			var nodes = tbody.SelectNodes("tr");
			foreach (var node in nodes)
			{
				var tds = node.SelectNodes("td");
				if (!(tds?.Count > 2))
					continue;

				var titleLink = tds[2].SelectSingleNode("a");
				var title = titleLink.InnerText;
				var href = titleLink.GetAttributeValue("href", "");
				//show.php?hash=a86af7382b7f8fe4852cce9f98c29344c9d22993
				var hash = Regex.Match(href, @"hash=([a-f\d]{40})", RegexOptions.IgnoreCase).GetGroupValue(1);
				var size = tds[3].InnerText;
				var time = tds[0].InnerText.Trim();

				var item = CreateResourceInfo(hash, title);
				item.SupportPreivewType = PreviewType.Image;
				item.UpdateTimeDesc = time;
				item.DownloadSize = size;
				item.SiteData = new SiteInfo()
				{
					ProvideSiteDownload = null
				};

				result.Add(item);
			}
			result.HasPrevious = result.PageIndex > 1;
			result.HasMore = document.DocumentNode.SelectSingleNode("//a[@class='nextprev']") != null;

			return base.LoadCore(context, url, htmlContent, result);
		}

		/// <summary>
		/// 加载相信信息
		/// </summary>
		public override void LoadFullDetail(IResourceInfo info)
		{
			if (info.Count == 0)
				FillFullInfo(info);

			base.LoadFullDetail(info);
		}

		/// <summary>
		/// 加载预览信息
		/// </summary>
		/// <param name="resource"></param>
		protected override void LoadPreviewInfoCore(IResourceInfo resource)
		{
			FillFullInfo(resource);
			base.LoadPreviewInfoCore(resource);
		}

		/// <summary>
		/// 加载内容
		/// </summary>
		public override void LookupTorrentContents(IResourceInfo torrent)
		{
			if (torrent.Count == 0)
				FillFullInfo(torrent);

			base.LookupTorrentContents(torrent);
		}

		void FillFullInfo(IResourceInfo resource)
		{
			var url = GetDetailUrl(resource);
			var ctx = NetworkClient.Create<HtmlDocument>(HttpMethod.Get, url, ReferUrlPage).Send();
			if (!ctx.IsValid())
				return;

			//下载地址
			var dlink = ctx.Result.GetElementbyId("download");
			if (dlink != null)
			{
				var data = resource.SiteData as SiteInfo;
				data.ProvideSiteDownload = true;
				data.SiteDownloadLink = "http://www.mp4ba.com/" + dlink.GetAttributeValue("href", "");
			}

			//预览信息
			var node = ctx.Result.DocumentNode.SelectSingleNode("//div[@class='intro']//img");
			if (node != null)
			{
				resource.PreviewInfo = new PreviewInfo()
				{
					ImageUrl = node.GetAttributeValue("src", "")
				};
			}

			//文件
			var fileNode = ctx.Result.DocumentNode.SelectSingleNode("//div[@class='torrent_files']/ul");
			if (fileNode != null)
			{
				AddFileNode(fileNode, resource);
			}
		}

		void AddFileNode(HtmlNode node, ICollection<IFileNode> parent)
		{
			foreach (var childNode in node.ChildNodes)
			{
				var fileNode = new FileNode()
				{
					Name = childNode.SelectSingleNode("text()").InnerText,
					IsDirectory = childNode.GetAttributeValue("class", "")?.IndexOf("folder") > -1
				};
				parent.Add(fileNode);
				if (fileNode.IsDirectory)
				{
					AddFileNode(childNode.SelectSingleNode("ul"), fileNode.Children);
				}
				else
				{
					fileNode.SizeString = childNode.SelectSingleNode("span").InnerText.Trim('(', ')');
				}

			}
		}
	}
}
