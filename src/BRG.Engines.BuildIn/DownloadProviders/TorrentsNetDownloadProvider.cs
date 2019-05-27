namespace BRG.Engines.BuildIn.DownloadProviders
{
	using System.ComponentModel.Composition;
	using BRG.Service;

	[Export(typeof(ITorrentDownloadServiceProvider))]
	class TorrentsNetDownloadProvider : SiteSelfDownloadProvider<BuildinServerInfo>
	{
		public TorrentsNetDownloadProvider()
			: base(new BuildinServerInfo("Torrents.net", Properties.Resources.favicon_torrentsnet, "提供对 torrents.net 网站的种子下载支持"))
		{
			RequireBypassGfw = false;
			ReferUrlPage = "http://www.torrents.net/";
			PageCheckKey = @"Download free";
		}
	}
}
