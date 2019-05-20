namespace BRG.Service
{
	using BRG.Entities;

	public interface IResourceOperation
	{
		void PrepareFullDetail(params IResourceInfo[] torrents);
		/// <summary>
		/// 请求下载资源
		/// </summary>
		/// <param name="torrents"></param>
		void AccquireDownloadTorrent(params IResourceInfo[] torrents);
		void CopyHash(params IResourceInfo[] torrents);
		void CopyEd2k(params IResourceInfo[] torrents);

		/// <summary>
		/// 复制下载链接
		/// </summary>
		/// <param name="resources"></param>
		void CopyDownloadLink(params IResourceInfo[] resources);


		/// <summary>
		/// 打开下载页面
		/// </summary>
		/// <param name="resources"></param>
		void OpenDownloadPage(params IResourceInfo[] resources);

		void CopyTitle(params IResourceInfo[] torrents);
		/// <summary>
		/// 将资源标记为已下载
		/// </summary>
		/// <param name="torrents"></param>
		void MarkDone(params IResourceInfo[] torrents);
		/// <summary>
		/// 将资源标记为已下载
		/// </summary>
		/// <param name="torrents"></param>
		void MarkUndone(params IResourceInfo[] torrents);
		void BaiduSearch(IResourceInfo resource);
		void GoogleSearch(IResourceInfo resource);
		void CopyMagnetLink(params IResourceInfo[] torrents);
		/// <summary>
		/// 查看详细信息
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="resource"></param>
		void ViewDetail(IResourceProvider provider, IResourceInfo resource);

		/// <summary>
		/// 查看种子内容
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="resource"></param>
		void ViewTorrentContents(IResourceProvider provider, IResourceInfo resource);
		/// <summary>
		/// 设置标注
		/// </summary>
		/// <param name="maskName"></param>
		/// <param name="resource"></param>
		void SetTorrentMask(string maskName, params IResourceInfo[] resource);

		/// <summary>
		/// 显示浮动通知
		/// </summary>
		/// <param name="msg"></param>
		void ShowFloatTip(string msg);
	}
}