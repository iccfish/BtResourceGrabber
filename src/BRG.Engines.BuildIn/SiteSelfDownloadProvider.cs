namespace BRG.Engines.BuildIn
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using BRG.Entities;
	using BRG.Service;

	abstract class SiteSelfDownloadProvider<T> : AbstractDownloadServiceProvider<T>, ITorrentDownloadServiceProvider where T : IServiceInfo
	{
		public SiteSelfDownloadProvider(T info)
			: base(info)
		{
		}

		HashSet<Type> _filterDownloadType;

		protected void SetSourceFilter<T1>() where T1 : IResourceProvider
		{
			if (_filterDownloadType == null)
				_filterDownloadType = new HashSet<Type>();
			if (!_filterDownloadType.Contains(typeof(T1))) _filterDownloadType.Add(typeof(T1));
		}


		/// <summary>
		/// 执行下载
		/// </summary>
		/// <param name="torrent"></param>
		/// <returns></returns>
		public virtual byte[] Download(IResourceInfo torrent)
		{
			if (torrent.Data is byte[])
			{
				return torrent.Data as byte[];
			}

			if (torrent == null || (_filterDownloadType != null && !_filterDownloadType.Contains(torrent.Provider.GetType())) || !torrent.IsHashLoaded || torrent.SiteData == null || !(torrent.SiteData is SiteInfo))
				return null;

			var data = torrent.SiteData as SiteInfo;

			if (data.ProvideSiteDownload == null)
			{
				torrent.Provider.LoadFullDetail(torrent);
			}

			var link = data.SiteDownloadLink;
			if (data.ProvideSiteDownload == false || link.IsNullOrEmpty())
				return null;

			return DownloadCore(link, link);
		}
	}
}
