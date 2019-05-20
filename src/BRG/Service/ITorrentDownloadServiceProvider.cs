namespace BRG.Service
{
	using BRG.Entities;

	public interface ITorrentDownloadServiceProvider : IServiceBase
	{
		/// <summary>
		/// 执行下载
		/// </summary>
		/// <param name="torrent"></param>
		/// <returns></returns>
		byte[] Download(IResourceInfo torrent);


	}
}
