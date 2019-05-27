namespace BRG.Engines.BuildIn.SearchProviders
{
	using System;
	using System.ComponentModel.Composition;
	using System.Text.RegularExpressions;
	using BRG;
	using BRG.Engines.Entities;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;

	[Export(typeof(IResourceProvider))]
	class BtDiggInfoSearchProvider : AbstractSearchServiceProvider<BuildinServerInfo>, IResourceProvider
	{
		static string _referUrl = "https://btdigg.org/";

		public BtDiggInfoSearchProvider()
			: base(new BuildinServerInfo("BtDigg", Properties.Resources.favicon_btdigginfo16, "英气逼人的BtDigg.info...."))
		{

		}

		/// <summary>
		/// 测试服务状态
		/// </summary>
		/// <returns></returns>
		public override TestStatus Test()
		{
			return TestCore(_referUrl, "BTDigg DHT");
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
			var result = new ResourceSearchInfo(this)
			{
				Key = key,
				PageIndex = pageindex,
				PageSize = null
			};
			if (sortType == SortType.PubDate || sortType == SortType.FileSize)
			{
				result.SortType = sortType;
			}

			var url = GetSearchUrl(key, sortType, sortDirection, pagesize, pageindex);
			var html = NetworkClient.Create<string>(HttpMethod.Get, url, _referUrl).Send();
			if (!html.IsValid())
				return null;

			//分析
			var strpos = 0;
			string row;
			while (!string.IsNullOrEmpty((row = html.Result.SearchStringTag("td class=\"idx\"", "</pre>", ref strpos))))
			{
				row = System.Web.HttpUtility.HtmlDecode(row);

				var hash = Regex.Match(row, @"info_hash=([a-f\d]{40})", RegexOptions.IgnoreCase).GetGroupValue(1).ToUpper();
				if (string.IsNullOrEmpty(hash))
					continue;

				var item = new ResourceInfo();
				item.Hash = hash;
				item.Provider = this;

				//标题
				item.Title = Regex.Match(row, "torrent_name\".*?><.*?>(.*?)</a>", RegexOptions.IgnoreCase).GetGroupValue(1).DefaultForEmpty(".....");
				item.DownloadSize = Regex.Match(row, @"大小:</span>.*?attr_val"">([\d\.]+(&nbsp;|\s)+(\w+))", RegexOptions.IgnoreCase).GetGroupValue(1);
				//</span><span class="attr_val">179</span>
				item.FileCount = Regex.Match(row, @"文件数:</span>.*?attr_val"">([\d\.,\s]+)", RegexOptions.IgnoreCase).GetGroupValue(1).ToInt32();
				item.UpdateTimeDesc = Regex.Match(row, @"添加时间:</span>.*?attr_val"">([\d\.]+(&nbsp;|\s)+(\w+))", RegexOptions.IgnoreCase).GetGroupValue(1);

				result.Add(item);
			}

			result.HasPrevious = html.Result.IndexOf("\">← 上一个", StringComparison.OrdinalIgnoreCase) != -1;
			result.HasMore = html.Result.IndexOf("\">下一个", StringComparison.OrdinalIgnoreCase) != -1;



			return result;
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrl(IResourceInfo resource)
		{
			return "https://btdigg.org/search?info_hash={0}".FormatWith(resource.Hash.ToLower());
		}

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex)
		{
			var order = 0;
			if ((sortType & SortType.PubDate) == SortType.PubDate)
				order = 2;
			else if ((sortType & SortType.FileSize) == SortType.FileSize)
				order = 3;


			return string.Format("https://btdigg.org/search?q={0}&p={1}&order={2}", System.Web.HttpUtility.UrlEncode(key), pageindex - 1, order);
		}

		/// <summary>
		/// 是否支持Unicode搜索
		/// </summary>
		public override bool SupportUnicode
		{
			get { return true; }
		}

		/// <summary>
		/// 是否需要科学上网
		/// </summary>
		public override bool RequireBypassGfw
		{
			get { return false; }
		}

		/// <summary>
		/// 支持的排序类型
		/// </summary>
		public override SortType? SupportSortType
		{
			get { return SortType.PubDate | SortType.FileSize; }
		}

		/// <summary>
		/// 是否支持查看种子内容
		/// </summary>
		public override bool SupportLookupTorrentContents
		{
			get { return true; }
		}

		/// <summary>
		/// 是否支持自定义每页条数
		/// </summary>
		public override bool SupportCustomizePageSize
		{
			get { return false; }
		}

		/// <summary>
		/// 加载内容
		/// </summary>
		public override void LookupTorrentContents(IResourceInfo torrent)
		{
			var url = GetDetailUrl(torrent);
			var htmlContext = NetworkClient.Create<string>(HttpMethod.Get, url, _referUrl).Send();
			if (!htmlContext.IsValid())
				return;

			var html = htmlContext.Result.SearchStringTag("<th>文件名</th></tr>", "</table>");
			if (string.IsNullOrEmpty(html))
				return;

			var charPos = 0;
			var row = "";
			while (!string.IsNullOrEmpty((row = html.SearchStringTag("<tr", "</tr>", ref charPos))))
			{
				var m = Regex.Match(row, @"<td>([\.\d]+(\s|&nbsp;)+\w+)</td>.*?<td>(.*?)</td>", RegexOptions.IgnoreCase);
				if (!m.Success)
					continue;

				var path = m.GetGroupValue(3);
				var size = m.GetGroupValue(1);

				AddFileNode(torrent, BrtUtility.ClearString(path), null, BrtUtility.ClearString(size));
			}
		}

		/// <summary>
		/// 是否支持预加载标记
		/// </summary>
		public override bool SupportResourceInitialMark
		{
			get { return false; }
		}
	}
}
