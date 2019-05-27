namespace BRG.Engines.BuildIn.DownloadProviders
{
	using System.ComponentModel.Composition;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;

	[Export(typeof(ITorrentDownloadServiceProvider))]
	class I_TorrentsDownloadProvder : AbstractDownloadServiceProvider<BuildinServerInfo>, ITorrentDownloadServiceProvider
	{
		public I_TorrentsDownloadProvder() : base(new BuildinServerInfo("ITorrents.org", Properties.Resources.favicon_torrage, "提供对itorrents.org缓存服务的下载支持"))
		{
			RequireBypassGfw = false;
			ReferUrlPage = "http://itorrents.org/";
			PageCheckKey = "iTorrents.org";
		}

		/// <summary>
		/// 执行下载
		/// </summary>
		/// <param name="torrent"></param>
		/// <returns></returns>
		public byte[] Download(IResourceInfo torrent)
		{
			var url = $"http://itorrents.org/torrent/{torrent.Hash}.torrent";
			var ctx = NetworkClient.Create<byte[]>(HttpMethod.Get, url, ReferUrlPage).Send();
			if (!ctx.IsValid())
				return null;

			if (ctx.IsRedirection || ctx.Response.ContentType.IndexOf("application/x-bittorrent") == -1)
				return null;

			return ValidateTorrentContent(ctx);
		}
	}
}
