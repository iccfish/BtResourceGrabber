using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRG.Engines.BuildIn.SearchProviders
{
	using System.ComponentModel.Composition;
	using System.Drawing;
	using System.Text.RegularExpressions;
	using System.Web;
	using BRG.Engines.Entities;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;

	[Export(typeof(IResourceProvider))]
	class TtmjSearchProvider : AbstractBuildInSearchServiceProviderWithSiteData
	{
		public TtmjSearchProvider() : base("天天美剧", Properties.Resources.favicon_ttmj, "天天美剧搜索", "1.0.0.0")
		{
			RequireBypassGfw = false;
			SupportLookupTorrentContents = false;
			SupportResourceInitialMark = false;
			SupportUnicode = true;
			ReferUrlPage = "http://www.ttmeiju.com/";
			PageCheckKey = "美剧下载";
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			return $"http://www.ttmeiju.com/search.php?keyword={HttpUtility.UrlEncode(key, Encoding.GetEncoding(936))}&range=1&page={pageindex}";
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrlCore(IResourceInfo resource, SiteInfo data)
		{
			return $"http://www.ttmeiju.com/seed/{data.SiteID}.html";
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
			var doc = CreateHtmlDocument(context.Result);
			var seedTable= doc.DocumentNode.SelectSingleNode("//table[contains(@class,'seedtable')]");
			if (seedTable == null)
				return false;

			var nodes = seedTable.SelectNodes(".//tr[position()>2 and position()<last()]");
			if (nodes == null)
				return true;


			foreach (var node in nodes)
			{
				var a = node.SelectSingleNode("td[2]/a");
				var title = a.InnerText;
				var sid = Regex.Match(a.GetAttributeValue("href", ""), @"(?<=/)\d+(?=\.html)", RegexOptions.IgnoreCase).Value.ToInt32();
				var sizestr = node.SelectSingleNode("td[4]").InnerText.Trim();

				var downloads = node.SelectNodes("td[3]/a");
				var hasmulti = downloads.Count > 1;

				//parent?
				ResourceInfo parent = null;
				List<ResourceInfo> subRes = null;
				if (hasmulti)
				{
					parent = new ResourceInfo()
					{
						Title = title,
						HasSubResources = true,
						SiteData = new SiteInfo()
						{
							SiteID = sid
						},
						DownloadSize = sizestr,
						Provider = this,
						ResourceType = ResourceType.MultiResource
					};
					subRes = new List<ResourceInfo>();
				}

				long? size = null;
				foreach (var subnode in downloads)
				{
					var href = subnode.GetAttributeValue("href", "");

					if (href.IndexOf("ed2k://") == 0)
					{
						var reg = Regex.Match(href, "\\|file\\|([^\\|]+)\\|(\\d+)\\|([a-z\\d]+)", RegexOptions.IgnoreCase);
						if (!reg.Success)
							continue;

						var filename = UD(reg.GetGroupValue(1)).Trim();
						var hash = reg.GetGroupValue(3);
						size = reg.GetGroupValue(2).ToInt64();

						var res = CreateResourceInfo(hash, filename, ResourceType.Ed2K);
						res.DownloadSizeValue = size;
						//重置其它的Resource
						if (parent != null) parent.DownloadSizeValue = size;
						subRes?.ForEach(s => s.DownloadSizeValue = size);

						if (subRes != null)
							subRes.Add(res);
						else result.Add(res);
					}
					else if (href.IndexOf("magnet:") == 0)
					{
						var reg = Regex.Match(href, "btih:([a-f\\d]{40})", RegexOptions.IgnoreCase);
						if (!reg.Success)
							continue;

						var filename = UD(title).Trim();
						var hash = reg.GetGroupValue(1);

						var res = CreateResourceInfo(hash, filename, ResourceType.BitTorrent);
						res.DownloadSize = sizestr;
						res.DownloadSizeValue = size;

						if (subRes != null)
							subRes.Add(res);
						else result.Add(res);
					}
					else if (href.IndexOf("pan.baidu.com") != -1 || href.IndexOf("urlxf.qq.com") != -1 || href.IndexOf("kuai.xunlei.com") != -1 || href.IndexOf("yunpan.cn") != -1)
					{
						var type = href.IndexOf("pan.baidu.com") != -1 ? NetDiskType.BaiduDesk :
										href.IndexOf("urlxf.qq.com") != -1 ? NetDiskType.QqXf :
											href.IndexOf("kuai.xunlei.com") != -1 ? NetDiskType.XlShare : NetDiskType.QihuShare;
						//百度网盘, 旋风分享， 迅雷快传
						var res = CreateResourceInfo(null, title, ResourceType.NetDisk);
						res.DownloadSize = sizestr;
						res.DownloadSizeValue = size;
						res.NetDiskData = new NetDiskData()
						{
							NetDiskType = type,
							Url = href
						};

						if (subRes != null)
							subRes.Add(res);
						else result.Add(res);
					}
				}

				if (parent != null)
				{
					if (subRes.Count > 1)
					{
						parent.SubResources = subRes.Cast<IResourceInfo>().ToArray();
						result.Add(parent);
					}
					else if (subRes.Count > 0)
					{
						result.Add(subRes[0]);
					}
				}
			}

			result.HasMore = context.Result.IndexOf(">下一页") != -1;
			result.HasPrevious = result.PageIndex > 1;


			return base.LoadCore(context, url, htmlContent, result);
		}

	}
}
