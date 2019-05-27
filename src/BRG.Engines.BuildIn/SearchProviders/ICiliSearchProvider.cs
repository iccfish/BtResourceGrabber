using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRG.Engines.BuildIn.SearchProviders
{
	using System.ComponentModel.Composition;
	using System.Drawing;
	using System.Text.RegularExpressions;
	using BRG.Engines.Entities;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;
	using HtmlAgilityPack;

	[Export(typeof(IResourceProvider))]
	class ICiliSearchProvider : AbstractBuildInSearchServiceProviderWithSiteData
	{
		public ICiliSearchProvider() : base("iCili电驴站", Properties.Resources.favicon_emule_16, "提供对iClili电驴站的搜索支持", "1.0.0.0")
		{
			RequireBypassGfw = false;
			SupportResourceInitialMark = true;
			SupportLookupTorrentContents = false;
			SupportSortType = null;
			SupportUnicode = true;
			PageCheckKey = "7VvlheciQJsFNW5JM_fgfJsT";
		}

		/// <summary>
		/// 连接
		/// </summary>
		public override void Connect()
		{
			base.Connect();
			DefaultHost = "www.icili.com";
			ReferUrlPage = $"http://{Host}/";

			if (!Property.ContainsKey("first_run_done"))
			{
				AppContext.Instance.Options.EngineStandalone.Add(Info.Name);
				Property.Add("first_run_done", "1");
			}
		}

		/// <summary>
		/// 从指定版本升级
		/// </summary>
		/// <param name="oldVersion"></param>
		public override void UpgradeFrom(string oldVersion, string currentVersion)
		{
			base.UpgradeFrom(oldVersion, currentVersion);

			Disabled = true;
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			//http://www.icili.com/emule/search/%E6%9D%83%E5%8A%9B%E7%9A%84%E6%B8%B8%E6%88%8F/2
			return $"http://{Host}/emule/search/{UE(key)}/{pageindex}";
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrlCore(IResourceInfo resource, SiteInfo data)
		{
			return $"http://{Host}/emule/download/{data.SiteID}";
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
			var nodes = document.GetElementbyId("main")?.SelectNodes("ul/li");
			if (nodes == null)
				return false;

			foreach (var node in nodes)
			{
				var thumb = node.SelectSingleNode("div[@class='thumb']/a");
				var titlelink = node.SelectSingleNode("div[@class='list_info']//h4/a");
				if (titlelink == null)
				{
					continue;
				}

				var item = CreateResourceInfo(null, titlelink.GetAttributeValue("title", ""), ResourceType.MultiResource);
				//<a href="/emule/download/2148521356" title="《权力的游戏 第五季》(Game of Thrones Season 5)[HDTVrip,WEBRip,720P,1080P]更新第9集">《<em class="highlight">权力的游戏</em> 第五季》(Game of Thrones Season 5)[HDTVrip,WEBRip,720P,1080P]更新第9集</a>
				item.SiteData = new SiteInfo()
				{
					SiteID = Regex.Match(titlelink.GetAttributeValue("href", ""), @"\d+").Value.ToInt64()
				};
				//预览信息
				if (thumb != null)
				{
					var style = thumb.GetAttributeValue("style", "");
					if (style != null)
					{
						//background-image: url(http://thumb.icili.com/2148521356.jpg)
						var pm = Regex.Match(style, @"url\(['""]?([^\)]+)['""]?\)");
						if (pm.Success)
						{
							item.SupportPreivewType = PreviewType.Image;
							item.PreviewInfo = new PreviewInfo()
							{
								ImageUrl = pm.GetGroupValue(1)
							};
						}
					}
				}
				var introText = node.SelectSingleNode("div[@class='list_info']").InnerText;
				//发布时间：2015-4-12 19:18 更新时间：2015-6-9 0:19
				item.UpdateTimeDesc = Regex.Match(introText, @"发布时间.*?([\d-_]+\s*[\d-_:])").GetGroupValue(1);
				// 文件数：44 | 点击数：2409
				item.FileCount = Regex.Match(introText, @"文件数[\s：]*?([\d]+)").GetGroupValue(1).ToInt32Nullable();

				item.HasSubResources = true;
				item.ResourceType = ResourceType.MultiResource;
				result.Add(item);
			}
			result.HasPrevious = result.PageIndex > 1;

			var nextAnchor = document.DocumentNode.SelectSingleNode("//div[@class='pager']//a[last()]");
			result.HasMore = nextAnchor != null && nextAnchor.GetAttributeValue("disabled", "") != "disabled";

			return base.LoadCore(context, url, htmlContent, result);
		}

		protected override void LoadSubResourcesCore(ResourceInfo resource)
		{
			if (resource.SubResources != null)
				return;

			var url = GetDetailUrl(resource);
			var ctx = NetworkClient.Create<HtmlDocument>(HttpMethod.Get, url).Send();
			if (!ctx.IsValid())
				return;

			var nodes = ctx.Result.DocumentNode.SelectNodes("//table[@id='emuleFile']//tr[position()>1 and position()<last()]");
			if (nodes == null)
				return;

			var subRes = new List<IResourceInfo>();
			foreach (var node in nodes)
			{
				var link = node.SelectSingleNode("td[2]/a");
				if (link == null)
					continue;

				var title = link.GetAttributeValue("title", "");
				var linkvalue = link.GetAttributeValue("href", "");

				if (linkvalue.StartsWith("ed2k://"))
				{
					//电驴资源
					//ed2k://|file|%5BPBS.%E8%87%AA%E7%84%B6S27E01.%E7%99%BD%E9%9B%AA%E9%B9%B0%E7%8B%BC%5DPBS.Nature.S27E01.White.Falcon.White.Wolf.2008.720p.HDTV.AC3-SoS.avi|1772847104|d645fc5169c7646d7f13c958704f1f54|h=cidyzfagvpgfpunxdlxvhyfxyrwe3t3s|/
					var reg = Regex.Match(linkvalue, "\\|file\\|([^\\|]+)\\|(\\d+)\\|([a-z\\d]+)", RegexOptions.IgnoreCase);
					if (!reg.Success)
						continue;

					var filename = BrtUtility.ClearString(UD(reg.GetGroupValue(1)));
					var hash = reg.GetGroupValue(3);
					var filesize = reg.GetGroupValue(2).ToInt64();

					var res = CreateResourceInfo(hash, filename, ResourceType.Ed2K);
					res.DownloadSizeValue = filesize;

					subRes.Add(res);
				}
				else if (linkvalue.StartsWith("magnet:"))
				{
					//磁力链
					//magnet:?xt=urn:btih:47fc15e7d5f3ad834f6f2152d0983cd07bafad9d&dn=菲洛梅娜.Philomena.2013.BD1080P.X264.AAC.english.CHS-ENG.Mp4Ba[ICILI.COM]

					var reg = Regex.Match(linkvalue, "btih:([a-f\\d]{40})", RegexOptions.IgnoreCase);
					if (!reg.Success)
						continue;

					var filename = BrtUtility.ClearString(UD(title));
					var hash = reg.GetGroupValue(1);

					var res = CreateResourceInfo(hash, filename, ResourceType.BitTorrent);
					res.DownloadSizeValue = null;

					subRes.Add(res);
				}
			}
			resource.SubResources = subRes.ToArray();

			base.LoadSubResourcesCore(resource);
		}
	}
}
