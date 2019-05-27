namespace BRG.Engines.BuildIn.DownloadProviders
{
	using System;
	using System.ComponentModel.Composition;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;

	[Export(typeof(ITorrentDownloadServiceProvider))]
	class TorrentOrgCnDownloadProvider : AbstractDownloadServiceProvider<BuildinServerInfo>, ITorrentDownloadServiceProvider
	{
		public TorrentOrgCnDownloadProvider()
			: base(new BuildinServerInfo("TOC", null, "提供对TOC的下载搜索支持"))
		{
			((BuildinServerInfo) Info).Version = new Version("1.1.0.0");
			RequireBypassGfw = false;
		}

		/// <summary>
		/// 执行下载
		/// </summary>
		/// <param name="torrent"></param>
		/// <returns></returns>
		public byte[] Download(IResourceInfo torrent)
		{
			var url = "http://www.torrent.org.cn/download.php";
			var refer = "http://www.torrent.org.cn/";

			var ctx = NetworkClient.Create<byte[]>(HttpMethod.Post, url, refer, new { hash = torrent.Hash }).Send();
			if (!ctx.IsValid())
				return null;

			if (ctx.Response.ContentType.IndexOf("html") != -1)
				return null;

			return ValidateTorrentContent(ctx);
		}

		/// <summary>
		/// 测试服务状态
		/// </summary>
		/// <returns></returns>
		public override TestStatus Test()
		{
			return TestCore("http://www.torrent.org.cn/", "种子转磁力链接");
		}
	}
}
