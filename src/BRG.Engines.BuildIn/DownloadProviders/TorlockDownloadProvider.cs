namespace BRG.Engines.BuildIn.DownloadProviders
{
	using System;
	using System.ComponentModel.Composition;
	using System.IO;
	using System.Linq;
	using BRG.Engines.BuildIn.SearchProviders;
	using BRG.Engines.Entities;
	using BRG.Entities;
	using BRG.Service;

	using FSLib.FileFormats.Torrent;

	[Export(typeof(ITorrentDownloadServiceProvider))]
	class TorlockDownloadProvider : AbstractDownloadServiceProvider<BuildinServerInfo>, ITorrentDownloadServiceProvider
	{
		public TorlockDownloadProvider() : base(new BuildinServerInfo("TorLock", Properties.Resources.favicon_torlock, "提供对 TorLock 的下载支持"))
		{
			RequireBypassGfw = false;

			ReferUrlPage = "https://www.torlock.com/";
			PageCheckKey = "TorLock</title>";
		}

		/// <summary>
		/// 执行下载
		/// </summary>
		/// <param name="torrent"></param>
		/// <returns></returns>
		public byte[] Download(IResourceInfo torrent)
		{
			if (torrent.Provider == null || torrent.Provider.GetType() != typeof(TorLockSearchProvider))
				return null;

			if (torrent.Data != null)
				return torrent.Data as byte[];

			((ResourceInfo)torrent).Hash = null;

			var data = (SiteInfo)torrent.SiteData;
			var url = $"https://www.torlock.com/tor/{data.SiteID}.torrent";

			var torData = DownloadCore(url, ReferUrlPage);
			if (data != null)
			{
				//update hash
				var contentStream = new MemoryStream(torData);
				var tf = new TorrentFile();
				try
				{
					tf.Load(contentStream, LoadFlag.ComputeMetaInfoHash);
				}
				catch (Exception)
				{
					return null;
				}

				//总下载大小
				var totalsize = tf.Files.Sum(s => s.Length);
				if (tf.Files.Count == 0)
				{
					totalsize = tf.MetaInfo.Length;
				}
				((ResourceInfo)torrent).DownloadSizeValue = totalsize;
				((ResourceInfo)torrent).Hash = tf.MetaInfoHashString;
			}

			return torData;
		}

	}
}
