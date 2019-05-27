namespace BRG.Engines.BuildIn.DownloadProviders
{
	using System.ComponentModel.Composition;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;

	[Export(typeof(ITorrentDownloadServiceProvider))]
	class TorRageDownloadProvider : AbstractDownloadServiceProvider<BuildinServerInfo>, ITorrentDownloadServiceProvider
	{

		public TorRageDownloadProvider()
			: base(new BuildinServerInfo("TR", Properties.Resources.favicon_torrage, "提供对TR的下载搜索支持"))
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
			var url = string.Format("http://torrage.net/torrent/{0}.torrent", torrent.Hash);
			var refer = "http://torrage.net/";

			var ctx = NetworkClient.Create<byte[]>(HttpMethod.Get, url, refer).Send();
			if (!ctx.IsValid())
				return null;

			return ValidateTorrentContent(ctx);
		}

		/// <summary>
		/// 测试服务状态
		/// </summary>
		/// <returns></returns>
		public override TestStatus Test()
		{
			return TestCore("http://torrage.net/", "Torrage");
		}
	}
}
