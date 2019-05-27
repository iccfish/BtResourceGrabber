namespace BRG.Engines.BuildIn.SearchProviders
{
	using System;
	using System.ComponentModel.Composition;
	using System.Linq;
	using System.Text.RegularExpressions;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;

	using Ivony.Html;
	using Ivony.Html.ExpandedAPI;

	[Export(typeof(IResourceProvider))]
	class Donkey4YouSearchProvider : AbstractSearchServiceProvider<BuildinServerInfo>, IResourceProvider
	{
		public Donkey4YouSearchProvider() : base(new BuildinServerInfo("Donkey4You", Properties.Resources.favicon_donkey4you, "Donkey4You"))
		{
			SupportResourceInitialMark = false;
			RequireBypassGfw = false;
			SupportLookupTorrentContents = false;
			SupportCustomizePageSize = false;
			SupportSortType = null;
			SupportUnicode = true;

			ReferUrlPage = "http://donkey4u.com/";
			PageCheckKey = "rRKccyOAoBwQbZ6cYa5qxCroh5ZlnbivYNMfj5xWhy4";
		}

		/// <summary>
		/// 从指定版本升级
		/// </summary>
		/// <param name="oldVersion"></param>
		public override void UpgradeFrom(string oldVersion, string currentVersion)
		{
			base.UpgradeFrom(oldVersion, currentVersion);

			//屏蔽
			Disabled = true;
		}

		#region Overrides of AbstractSearchServiceProvider<BuildinServerInfo>

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrl(IResourceInfo resource)
		{
			return $"http://donkey4u.com/detail/{resource.Hash}";
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			return $"http://donkey4u.com/search/{UE(key)}?mode=list&page={pageindex}";
		}

		#endregion

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
			if (!context.IsValid())
				return false;

			var doc = CreateHtmlDom(context.Request.Uri.ToString(), context.Result);
			var nodes = doc.Find(@"table.search_table tr");
			if (nodes != null)
			{
				foreach (var node in nodes.Skip(1))
				{
					var link = node.FindFirstOrDefault("td:last-child a");
					var href = link.GetAttributeValue("href");

					var infoMatch = Regex.Match(href, @"\|file\|([^\|]+)\|(\d+)\|([A-Z\d]+)\|");

					var res = CreateResourceInfo(infoMatch.GetGroupValue(3), infoMatch.GetGroupValue(1), ResourceType.Ed2K);
					res.DownloadSizeValue = infoMatch.GetGroupValue(2).ToInt64();

					//preview
					var preview = node.FindFirstOrDefault("td:nth-child(2) img");
					if (preview?.GetAttributeValue("src")?.IndexOf("hexie") != -1)
					{
						res.SupportPreivewType = PreviewType.Image;
						res.PreviewInfo = new PreviewInfo()
						{
							ImageUrl = $"http://thumb.donkey4u.com/{res.Hash}/thumb.jpg"
						};
					}

					result.Add(res);
				}

				result.HasMore = context.Result.IndexOf("&mode=list\">下一页") != -1;
			}

			return base.LoadCore(context, url, htmlContent, result);
		}

		#endregion
	}
}
