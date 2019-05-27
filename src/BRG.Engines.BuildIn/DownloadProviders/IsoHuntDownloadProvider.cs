namespace BRG.Engines.BuildIn.DownloadProviders
{
	using System;
	using System.ComponentModel.Composition;
	using BRG.Engines.BuildIn.SearchProviders;
	using BRG.Entities;
	using BRG.Service;

	[Export(typeof(ITorrentDownloadServiceProvider))]
	class IsoHuntDownloadProvider : AbstractDownloadServiceProvider<BuildinServerInfo>, ITorrentDownloadServiceProvider
	{
		public IsoHuntDownloadProvider()
			: base(new BuildinServerInfo("IsoHunt", Properties.Resources.favicon_isohunt, "提供对 IsoHunt 的下载支持"))
		{
			RequireBypassGfw = false;
			ReferUrlPage = "https://isohunt.to/";
			PageCheckKey = "torrent search engine";
		}

		/// <summary>
		/// 执行下载
		/// </summary>
		/// <param name="torrent"></param>
		/// <returns></returns>
		public byte[] Download(IResourceInfo torrent)
		{
			if (torrent == null || !torrent.IsHashLoaded || torrent.SiteData == null || !(torrent.Provider is IsoHuntSearchProvider) || !(torrent.SiteData is SiteInfo))
				return null;

			var data = torrent.SiteData as SiteInfo;

			if (data.ProvideSiteDownload == null)
			{
				torrent.Provider.LoadFullDetail(torrent);
			}

			var link = data.SiteDownloadLink;
			if (data.ProvideSiteDownload == false || link.IsNullOrEmpty())
				return null;

			return DownloadCore(link, link);
		}
	}
}
