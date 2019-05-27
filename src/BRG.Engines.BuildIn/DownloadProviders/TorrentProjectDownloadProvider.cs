namespace BRG.Engines.BuildIn.DownloadProviders
{
	using System.ComponentModel.Composition;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;

	[Export(typeof(ITorrentDownloadServiceProvider))]
	class TorrentProjectDownloadProvider : AbstractDownloadServiceProvider<BuildinServerInfo>, ITorrentDownloadServiceProvider
	{
		public TorrentProjectDownloadProvider()
			: base(new BuildinServerInfo("TPS", Properties.Resources.favicon_tps, "提供对TPS的下载支持"))
		{
			RequireBypassGfw = false;
		}

		/// <summary>
		/// 执行下载
		/// </summary>
		/// <param name="torrent"></param>
		/// <returns></returns>
		public byte[] Download(IResourceInfo torrent)
		{
			var url = string.Format("http://torrentproject.se/torrent/{0}.torrent", torrent.Hash);
			var refer = "http://torrentproject.se/";

			var ctx = NetworkClient.Create<byte[]>(HttpMethod.Get, url, refer).Send();
			if (!ctx.IsValid() || ctx.Response.ContentType.IndexOf("html") != -1)
				return null;

			return ValidateTorrentContent(ctx);
		}

		/// <summary>
		/// 测试服务状态
		/// </summary>
		/// <returns></returns>
		public override TestStatus Test()
		{
			return TestCore("http://torrentproject.se/", "TorrentProject");
		}
	}
}
