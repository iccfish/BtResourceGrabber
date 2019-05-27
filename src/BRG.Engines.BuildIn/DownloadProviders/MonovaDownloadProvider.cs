namespace BRG.Engines.BuildIn.DownloadProviders
{
	using System;
	using System.ComponentModel.Composition;
	using BRG.Engines.BuildIn.SearchProviders;
	using BRG.Entities;
	using BRG.Service;

	[Export(typeof(ITorrentDownloadServiceProvider))]
	class MonovaDownloadProvider : AbstractDownloadServiceProvider<BuildinServerInfo>, ITorrentDownloadServiceProvider
	{
		public MonovaDownloadProvider()
			: base(new BuildinServerInfo("Monova", Properties.Resources.favicon_monova, "提供对 monova 的下载支持"))
		{
			RequireBypassGfw = false;
			ReferUrlPage = "https://www.monova.org/";
			PageCheckKey = @"content=""monova.org""";
		}

		/// <summary>
		/// 执行下载
		/// </summary>
		/// <param name="torrent"></param>
		/// <returns></returns>
		public byte[] Download(IResourceInfo torrent)
		{
			if (torrent == null || !torrent.IsHashLoaded || torrent.SiteData == null || !(torrent.Provider is MonovaSearchProvider) || !(torrent.SiteData is SiteInfo))
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
