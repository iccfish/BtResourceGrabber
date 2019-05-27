namespace BRG.Engines.BuildIn.DownloadProviders
{
	using System.ComponentModel.Composition;
	using BRG.Engines.BuildIn.SearchProviders;
	using BRG.Service;

	[Export(typeof(ITorrentDownloadServiceProvider))]
	class SobtDownloadProvider : SiteSelfDownloadProvider<BuildinServerInfo>
	{
		public SobtDownloadProvider() : base(new BuildinServerInfo("SoBT", Properties.Resources.favicon_diggbt_16, "提供DiggBT的资源下载支持"))
		{
			SetSourceFilter<DiggBtSearchProvider>();
			RequireBypassGfw = false;
			ReferUrlPage = "http://diggbt.com/";
			PageCheckKey = "搜索神器";
		}
	}
}
