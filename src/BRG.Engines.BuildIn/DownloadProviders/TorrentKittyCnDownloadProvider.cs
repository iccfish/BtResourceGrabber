namespace BRG.Engines.BuildIn.DownloadProviders
{
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;

	//[Export(typeof(ITorrentDownloadServiceProvider))]
	class TorrentKittyCnDownloadProvider : AbstractDownloadServiceProvider<BuildinServerInfo>, ITorrentDownloadServiceProvider
	{
		public TorrentKittyCnDownloadProvider()
			: base(new BuildinServerInfo("TKCN", null, "提供对TKCN的下载支持"))
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
			var url = string.Format("http://d1.torrentkittycn.com/?infohash={0}", torrent.Hash);
			var refer = "http://d1.torrentkittycn.com/";

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
			return TestCore("http://d1.torrentkittycn.com/", null);
		}
	}
}
