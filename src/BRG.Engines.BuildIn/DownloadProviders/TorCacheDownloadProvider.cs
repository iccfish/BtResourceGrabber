namespace BRG.Engines.BuildIn.DownloadProviders
{
	using System;
	using System.ComponentModel.Composition;
	using System.Net;
	using System.Threading;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;

	[Export(typeof(ITorrentDownloadServiceProvider))]
	class TorCacheDownloadProvider : AbstractDownloadServiceProvider<BuildinServerInfo>, ITorrentDownloadServiceProvider
	{
		public TorCacheDownloadProvider()
			: base(new BuildinServerInfo("TC", null, "提供对TorCache的资源下载支持"))
		{
			RequireBypassGfw = false;
		}

		#region Implementation of ITorrentDownloadServiceProvider

		/// <summary>
		/// 执行下载
		/// </summary>
		/// <param name="torrent"></param>
		/// <returns></returns>
		public byte[] Download(IResourceInfo torrent, int loopCount = 0)
		{
			var downloadUrl = "https://torcache.net/torrent/" + torrent.Hash + ".torrent";
			var referUrl = "https://torcache.net/";

			var ctx = NetworkClient.Create<byte[]>(HttpMethod.Get, downloadUrl, referUrl).Send();
			if (!ctx.IsValid())
				return null;

			if (ctx.Response.Headers[HttpResponseHeader.ContentType].IndexOf("html", StringComparison.OrdinalIgnoreCase) != -1 && loopCount < 5)
			{
				//等待
				Thread.Sleep(5000);
				return Download(torrent, loopCount + 1);
			}

			var buffer = ctx.Result;

			return ValidateTorrentContent(buffer);
		}

		/// <summary>
		/// 执行下载
		/// </summary>
		/// <param name="torrent"></param>
		/// <returns></returns>
		public byte[] Download(IResourceInfo torrent)
		{
			return Download(torrent, 0);
		}

		#endregion

		/// <summary>
		/// 测试服务状态
		/// </summary>
		/// <returns></returns>
		public override TestStatus Test()
		{
			return TestCore("https://torcache.net/", "Torcache");
		}
	}
}
