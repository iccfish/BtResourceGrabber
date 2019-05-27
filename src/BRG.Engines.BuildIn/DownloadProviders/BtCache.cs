using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRG.Engines.BuildIn.DownloadProviders
{
	using System.ComponentModel.Composition;
	using System.Net;
	using System.Threading;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;

	// TODO: 这个引擎有验证码校验，需要实现么？意义好像不是太大。

	//[Export(typeof(ITorrentDownloadServiceProvider))]
	class BtCache : AbstractDownloadServiceProvider<BuildinServerInfo>, ITorrentDownloadServiceProvider
	{
		public BtCache() : base(new BuildinServerInfo("BTCACHE", null, "提供对BTCACHE的资源下载支持"))
		{
			RequireBypassGfw = false;
		}



		/// <summary>
		/// 执行下载
		/// </summary>
		/// <param name="torrent"></param>
		/// <returns></returns>
		public byte[] Download(IResourceInfo torrent, int loopCount = 0)
		{
			var downloadUrl = "http://www.btcache.me/torrent/" + torrent.Hash + ".torrent";
			var referUrl = "http://www.btcache.me/";

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

		/// <summary>
		/// 测试服务状态
		/// </summary>
		/// <returns></returns>
		public override TestStatus Test()
		{
			return TestCore("http://www.btcache.me/", "Torcache");
		}
	}
}
