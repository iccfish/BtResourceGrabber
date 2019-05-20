namespace BRG.Entities
{
	using System.Collections.Generic;
	using BRG.Service;

	/// <summary>
	/// 搜索信息
	/// </summary>
	public interface IResourceSearchInfo : ICollection<IResourceInfo>
	{
		/// <summary>
		/// 提供此资源的提供类
		/// </summary>
		IResourceProvider Provider { get; }

		/// <summary>
		/// 关键字
		/// </summary>
		string Key { get; }

		/// <summary>
		/// 搜索到的记录数
		/// </summary>
		int? ResultCount { get; }

		/// <summary>
		/// 当前页记录数
		/// </summary>
		int? PageSize { get; }

		/// <summary>
		/// 总页数
		/// </summary>
		int? TotalPages { get; }

		/// <summary>
		/// 当前页码
		/// </summary>
		int? PageIndex { get; }

		/// <summary>
		/// 是否有更多
		/// </summary>
		bool HasMore { get; }

		/// <summary>
		/// 是否有之前的记录
		/// </summary>
		bool HasPrevious { get; }

		/// <summary>
		/// 获得结果的排序类型
		/// </summary>
		SortType? SortType { get; }
	}
}
