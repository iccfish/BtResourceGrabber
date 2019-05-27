using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRG.Engines.BuildIn.DownloadProviders
{
	using System.ComponentModel.Composition;
	using BRG.Engines.BuildIn.SearchProviders;
	using BRG.Service;

	[Export(typeof(ITorrentDownloadServiceProvider))]
	class Mp4BaDownloadProvider : SiteSelfDownloadProvider<BuildinServerInfo>
	{
		public Mp4BaDownloadProvider()
			: base(
				  new BuildinServerInfo("高清MP4", Properties.Resources.favicon_mp4ba_16, "提供高清MP4网站的种子下载服务")
				  { Version = new Version("1.0.0.0") }
			)
		{
			RequireBypassGfw = false;
			ReferUrlPage = "http://www.mp4ba.com/";
			PageCheckKey = "高清Mp4吧";
			SetSourceFilter<Mp4BaSearchProvider>();
		}
	}
}
