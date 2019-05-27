namespace BRG.Engines.BuildIn.DownloadProviders
{
	using System.ComponentModel.Composition;
	using BRG.Engines.BuildIn.SearchProviders;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;

	[Export(typeof(ITorrentDownloadServiceProvider))]
	class RarbgDownloadProvider : SiteSelfDownloadProvider<BuildinServerInfo>, ITorrentDownloadServiceProvider
	{
		public RarbgDownloadProvider() : base(new BuildinServerInfo("Rarbg", Properties.Resources.favicon_rarbg, "提供Rarbg的资源下载支持"))
		{
			RequireBypassGfw = false;
			ReferUrlPage = "https://rarbg.com/torrents.php";
			PageCheckKey = @"<title>Rarbg.com";
			SetSourceFilter<RarbgSearchProvider>();
		}

		/// <summary>
		/// 执行下载
		/// </summary>
		/// <param name="torrent"></param>
		/// <returns></returns>
		public override byte[] Download(IResourceInfo torrent)
		{
			if (torrent == null || torrent.Provider == null || torrent.Provider.GetType() != typeof(RarbgSearchProvider))
			{
				return null;
			}
			var provider = (torrent.Provider as RarbgSearchProvider);
			var client = provider.NetworkClient;
			var siteinfo = torrent.SiteData as SiteInfo;

			var ctx = client.Create<byte[]>(HttpMethod.Get, siteinfo.SiteDownloadLink, ReferUrlPage, allowAutoRedirect: false).Send();
			if (!ctx.IsValid())
				return null;

			if (provider.IsHostChanged(ctx))
			{
				return Download(torrent);
			}
			if (provider.IsBotCheckNeeded(ctx))
			{
				if (provider.RequireBotCheck())
				{
					return Download(torrent);
				}

				return null;
			}

			return ValidateTorrentContent(ctx);
		}
	}
}
