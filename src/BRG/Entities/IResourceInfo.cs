namespace BRG.Entities
{
	using System;
	using System.Collections.Generic;
	using BRG.Service;

	public interface IResourceInfo : ICollection<IFileNode>, IComparable<IResourceInfo>
	{

		/// <summary>
		/// 详情已加载
		/// </summary>
		event EventHandler DetailLoaded;

		/// <summary>
		/// <see cref="Downloaded"/> 属性发生变化
		/// </summary>
		event EventHandler DownloadedChanged;

		/// <summary>
		/// 预览信息已加载
		/// </summary>
		event EventHandler PreviewInfoLoaded;

		/// <summary>
		/// 预览状态发生变化
		/// </summary>
		event EventHandler PreviewTypeChanged;

		/// <summary>
		/// 验证状态变化
		/// </summary>
		event EventHandler VerifyStateChanged;

		/// <summary>
		/// 标记已下载状态
		/// </summary>
		/// <param name="downloaded"></param>
		void ChangeDownloadedStatus(bool downloaded);

		/// <summary>
		/// 变更举报状态
		/// </summary>
		/// <param name="state"></param>
		/// <param name="reportNum"></param>
		void ChangeVerifyState(VerifyState state, int reportNum);

		/// <summary>
		/// 生成磁力链
		/// </summary>
		/// <param name="includeDn">是否包含下载名</param>
		/// <returns></returns>
		string CreateMagnetLink(bool includeDn);

		/// <summary>
		/// 内容加载中的错误信息
		/// </summary>
		string ContentLoadErrMessage { get; set; }

		/// <summary>
		/// 附加数据
		/// </summary>
		object Data { get; set; }

		/// <summary>
		/// 描述
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// 是否已经下载
		/// </summary>
		bool Downloaded { get; }

		/// <summary>
		/// 下载的文件大小
		/// </summary>
		string DownloadSize { get; set; }

		/// <summary>
		/// 计算出来的估计大小
		/// </summary>
		long DownloadSizeCalcauted { get; set; }

		/// <summary>
		/// 下载大小的数值
		/// </summary>
		long? DownloadSizeValue { get; set; }

		/// <summary>
		/// 文件数
		/// </summary>
		int? FileCount { get; set; }
		/// <summary>
		/// Hash
		/// </summary>
		string Hash { get; set; }

		/// <summary>
		/// 是否已经加载完全
		/// </summary>
		bool IsHashLoaded { get; }

		/// <summary>
		/// 获得匹配关键字的度量值
		/// </summary>
		int MatchWeight { get; }
		/// <summary>
		/// 获得网盘数据
		/// </summary>
		INetDiskData NetDiskData { get; }

		/// <summary>
		/// 比较偏爱的资源下载类
		/// </summary>
		ITorrentDownloadServiceProvider PreferDownloadProvider { get; set; }

		/// <summary>
		/// 预览信息
		/// </summary>
		PreviewInfo PreviewInfo { get; set; }

		/// <summary>
		/// 数据源
		/// </summary>
		IResourceProvider Provider { get; set; }

		/// <summary>
		/// 举报人数
		/// </summary>
		int ReportNum { get; }

		/// <summary>
		/// 资源类型
		/// </summary>
		ResourceType ResourceType { get; set; }

		/// <summary>
		/// 相关数据
		/// </summary>
		object SiteData { get; set; }

		/// <summary>
		/// 支持的预览类型
		/// </summary>
		PreviewType SupportPreivewType { get; set; }

		/// <summary>
		/// 标题
		/// </summary>
		string Title { get; set; }

		/// <summary>
		/// 种子大小
		/// </summary>
		string TorrentSize { get; set; }

		/// <summary>
		/// 种子文件大小的数值
		/// </summary>
		long? TorrentSizeValue { get; set; }

		/// <summary>
		/// 更新时间
		/// </summary>
		DateTime? UpdateTime { get; set; }

		/// <summary>
		/// 更新时间
		/// </summary>
		string UpdateTimeDesc { get; set; }

		/// <summary>
		/// 验证状态
		/// </summary>
		VerifyState VerifyState { get; }

		#region 子资源

		/// <summary>
		/// 获得是否还有子资源
		/// </summary>
		bool HasSubResources { get; }

		/// <summary>
		/// 获得子资源
		/// </summary>
		IResourceInfo[] SubResources { get; }

		/// <summary>
		/// 子资源已加载
		/// </summary>
		event EventHandler SubResourceLoaded;

		#endregion

	}
}
