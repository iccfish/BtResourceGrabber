namespace BRG.Engines.BuildIn.DownloadProviders
{
	using System.ComponentModel.Composition;
	using BRG.Entities;
	using BRG.Service;

	[Export(typeof(ITorrentDownloadServiceProvider))]
	class SeedPeerDownloadProvider : AbstractDownloadServiceProvider<BuildinServerInfo>, ITorrentDownloadServiceProvider
	{
		public SeedPeerDownloadProvider() : base(new BuildinServerInfo("SeedPeer", Properties.Resources.favicon_seedpeer, "提供对 SeedPeer 的下载支持"))
		{
			RequireBypassGfw = false;

			ReferUrlPage = "http://www.seedpeer.eu/";
			PageCheckKey = "/rss.xml";
		}


		/// <summary>
		/// 执行下载
		/// </summary>
		/// <param name="torrent"></param>
		/// <returns></returns>
		public byte[] Download(IResourceInfo torrent)
		{
			var url = $"http://www.seedpeer.eu/download/{torrent.Hash}/{torrent.Hash.ToLower()}";

			return DownloadCore(url, ReferUrlPage);
		}
	}
}
