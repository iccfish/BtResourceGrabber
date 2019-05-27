namespace BRG.Engines.BuildIn.DownloadProviders
{
	using System.ComponentModel.Composition;
	using BRG.Entities;
	using BRG.Service;

	[Export(typeof(ITorrentDownloadServiceProvider))]
	class TorrentHoundDownloadProvider : AbstractDownloadServiceProvider<BuildinServerInfo>, ITorrentDownloadServiceProvider
	{
		public TorrentHoundDownloadProvider()
			: base(new BuildinServerInfo("TorrentHound", Properties.Resources.favicon_torrenthound_16, "提供对 torrenthound 的下载支持"))
		{
			RequireBypassGfw = false;
			ReferUrlPage = "http://www.torrenthound.com/";
			PageCheckKey = @"<title>torrentHound.com";
		}

		/// <summary>
		/// 执行下载
		/// </summary>
		/// <param name="torrent"></param>
		/// <returns></returns>
		public byte[] Download(IResourceInfo torrent)
		{
			if (torrent == null || !torrent.IsHashLoaded)
				return null;

			var link = $"http://www.torrenthound.com/torrent/{torrent.Hash.ToLower()}";

			return DownloadCore(link, link);
		}
	}
}
