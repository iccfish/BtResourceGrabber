namespace BRG.Service
{
	using BRG.Entities;

	public interface IResourceProvider : IServiceBase
	{
		/// <summary>
		/// 加载数据
		/// </summary>
		/// <param name="key"></param>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <returns></returns>
		IResourceSearchInfo Load(string key, SortType sortType, int sortDirection, int pagesize, int pageindex = 1);

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		string GetDetailUrl(IResourceInfo resource);

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex);


		/// <summary>
		/// 是否支持Unicode搜索
		/// </summary>
		bool SupportUnicode { get; }

		/// <summary>
		/// 支持的排序类型
		/// </summary>
		SortType? SupportSortType { get; }

		/// <summary>
		/// 是否支持查看种子内容
		/// </summary>
		bool SupportLookupTorrentContents { get; }

		/// <summary>
		/// 是否支持自定义每页条数
		/// </summary>
		bool SupportCustomizePageSize { get; }

		/// <summary>
		/// 加载内容
		/// </summary>
		void LookupTorrentContents(IResourceInfo torrent);

		/// <summary>
		/// 是否支持预加载标记
		/// </summary>
		bool SupportResourceInitialMark { get; }

		/// <summary>
		/// 加载相信信息
		/// </summary>
		void LoadFullDetail(IResourceInfo resource);

		/// <summary>
		/// 准备下载
		/// </summary>
		/// <param name="resource"></param>
		void PrepareDownload(IResourceInfo resource);

		/// <summary>
		/// 加载预览信息
		/// </summary>
		/// <param name="resource"></param>
		void LoadPreviewInfo(IResourceInfo resource);

		/// <summary>
		/// 加载子资源
		/// </summary>
		/// <param name="resource"></param>
		void LoadSubResources(IResourceInfo resource);

		/// <summary>
		/// 主页
		/// </summary>
		string HomePage { get; }
	}
}
