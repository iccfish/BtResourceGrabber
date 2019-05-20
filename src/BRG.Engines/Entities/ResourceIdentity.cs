namespace BRG.Engines.Entities
{
	using System.Collections.Generic;
	using System.Linq;
	using BRG.Entities;

	public class ResourceIdentity
	{
		public IResourceInfo MainTorrentInfo { get; private set; }

		public IResourceInfo[] SubTorrentInfos { get; private set; }


		/// <summary>
		/// 创建 <see cref="ResourceIdentity" />  的新实例(ResourceIdentity)
		/// </summary>
		/// <param name="mainTorrentInfo"></param>
		/// <param name="subTorrentInfos"></param>
		public ResourceIdentity(IResourceInfo mainTorrentInfo, IResourceInfo[] subTorrentInfos)
		{
			MainTorrentInfo = mainTorrentInfo;
			SubTorrentInfos = subTorrentInfos;
		}

		/// <summary>
		/// 查找能够查看内容的资源子项
		/// </summary>
		/// <returns></returns>
		public IResourceInfo FindResourceToViewDetail()
		{
			if (MainTorrentInfo != null && MainTorrentInfo.Provider?.SupportLookupTorrentContents == true)
				return MainTorrentInfo;

			return SubTorrentInfos?.FirstOrDefault(s => s.Provider?.SupportLookupTorrentContents == true);
		}

		/// <summary>
		/// 查找适合复制的资源子项
		/// </summary>
		/// <returns></returns>
		public IResourceInfo FindResourceToCopyLink()
		{
			return MainTorrentInfo ?? SubTorrentInfos.FirstOrDefault();
		}

		/// <summary>
		/// 获得基础信息项
		/// </summary>
		public IResourceInfo BasicInfo
		{
			get { return MainTorrentInfo ?? SubTorrentInfos?.FirstOrDefault(); }
		}

		/// <summary>
		/// 获得适合用来下载的项
		/// </summary>
		/// <returns></returns>
		public IEnumerable<IResourceInfo> FindResourceDownloadInfo()
		{
			var found = false;

			if (MainTorrentInfo != null && MainTorrentInfo.PreferDownloadProvider != null)
			{
				found = true;

				yield return MainTorrentInfo;
			}

			if (SubTorrentInfos != null)
			{
				foreach (var torrentResourceInfo in SubTorrentInfos)
				{
					if (torrentResourceInfo.PreferDownloadProvider != null)
					{
						found = true;

						yield return torrentResourceInfo;
					}
				}
			}

			if (!found)
				yield return BasicInfo;
		}
	}
}
