namespace BRG.Engines
{
	using BRG;

	/// <summary>
	/// 种子在内部存储的信息
	/// </summary>
	public class SiteInfo
	{
		string _pageName;

		public string PageName
		{
			get { return _pageName ?? ""; }
			set { _pageName = BrtUtility.DecodePath(value ?? ""); }
		}

		/// <summary>
		/// 站点下载的ID
		/// </summary>
		public long SiteID { get; set; }

		/// <summary>
		/// 获得或设置是否提供站内下载
		/// </summary>
		public bool? ProvideSiteDownload { get; set; }

		/// <summary>
		/// 站点下载链接
		/// </summary>
		public string SiteDownloadLink { get; set; }
	}
}
