namespace BRG.Engines.BuildIn.DownloadProviders
{
	using System.ComponentModel.Composition;
	using System.Web;
	using BRG.Engines;
	using BRG.Engines.BuildIn.SearchProviders;
	using BRG.Entities;
	using BRG.Service;

	[Export(typeof(ITorrentDownloadServiceProvider))]
	class ExtraTorrentDownloadProvider : AbstractDownloadServiceProvider<BuildinServerInfo>, ITorrentDownloadServiceProvider
	{
		public ExtraTorrentDownloadProvider() : base(new BuildinServerInfo("ExtraTorrent", Properties.Resources.favicon_extratorrent, "提供对 ExtraTorrent 的下载支持"))
		{
			RequireBypassGfw = true;
			PageCheckKey = "<title>ExtraTorrent";
			ReferUrlPage = "http://extratorrent.cc/";
		}


		/// <summary>
		/// 执行下载
		/// </summary>
		/// <param name="torrent"></param>
		/// <returns></returns>
		public byte[] Download(IResourceInfo torrent)
		{
			if (!torrent.IsHashLoaded || torrent.Provider == null || torrent.Provider.GetType() != typeof(ExtraTorrentSearchProvider))
				return null;

			var data = (SiteInfo)torrent.SiteData;
			var url = $"http://extratorrent.cc/download/{data.SiteID}/{HttpUtility.UrlEncode((string)data.PageName)}.torrent";

			return DownloadCore(url, ReferUrlPage);
		}
	}
}
