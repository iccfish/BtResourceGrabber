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
	class StoreBtDownloadProvider : SiteSelfDownloadProvider<BuildinServerInfo>
	{
		public StoreBtDownloadProvider() : base(new BuildinServerInfo("StoreBT", Properties.Resources.favicon_btkitty, "提供StoreBT的资源下载支持"))
		{
			RequireBypassGfw = false;
			ReferUrlPage = "http://btkitty.org/";
			PageCheckKey = "<title>BT Kitty";
		}
	}
}
