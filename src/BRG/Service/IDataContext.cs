namespace BRG.Service
{
	using System;
	using System.Collections.Generic;
	using BRG.Entities;

	public interface IDataContext : IDisposable
	{
		/// <summary>
		/// 初始化数据库
		/// </summary>
		void Init();
		void DownloadHistory_Update(IResourceInfo info);
		void DownloadHistory_Sync(IResourceSearchInfo infos);
		void VerifyState_Update(IResourceInfo info);
		void VerifyState_Sync(IResourceSearchInfo infos);
		void PreviewInfo_Update(ResourceType stype, string engine, string hash, PreviewType type, PreviewInfo info);
		void PreviewInfo_MarkEmptyPreview(ResourceType stype, string hash);
		/// <summary>
		/// 同步资源信息以及填充资源预览信息。
		/// </summary>
		/// <param name="infos"></param>
		void PreviewInfo_Sync(IResourceSearchInfo infos);
		/// <summary>
		/// 根据Hash加载预览信息
		/// </summary>
		/// <param name="hashes"></param>
		/// <returns></returns>
		PreviewInfoStore[] PreviewInfo_Load(BRG.Entities.ResourceType stype, params string[] hashes);
	}
}
