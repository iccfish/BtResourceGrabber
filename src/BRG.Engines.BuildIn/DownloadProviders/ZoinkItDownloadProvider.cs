namespace BRG.Engines.BuildIn.DownloadProviders
{
	using System.ComponentModel.Composition;
	using BRG.Entities;
	using BRG.Service;

	[Export(typeof(ITorrentDownloadServiceProvider))]
	class ZoinkItDownloadProvider : AbstractDownloadServiceProvider<BuildinServerInfo>, ITorrentDownloadServiceProvider
	{
		public ZoinkItDownloadProvider()
			: base(new BuildinServerInfo("ZOINK", Properties.Resources.favicon_zoink, "提供对ZOINK的下载搜索支持"))
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
			return DownloadCore(string.Format("https://zoink.ch/torrent/{0}.torrent", torrent.Hash), "https://zoink.ch/");
		}

		/// <summary>
		/// 测试服务状态
		/// </summary>
		/// <returns></returns>
		public override TestStatus Test()
		{
			return TestCore("https://zoink.ch/", "ZoinkCH");
		}
	}
}
