namespace BRG.Entities
{
	using System.Collections.Generic;
	using BRG.Service;

	public class ResourceSearchInfo : List<IResourceInfo>, IResourceSearchInfo
	{
		/// <summary>
		/// 创建 <see cref="ResourceSearchInfo" />  的新实例(ResourceSearchInfo)
		/// </summary>
		/// <param name="provider"></param>
		public ResourceSearchInfo(IResourceProvider provider)
		{
			Provider = provider;
		}

		#region Implementation of IResourceSearchInfo
		/// <summary>
		/// 提供此资源的提供类
		/// </summary>
		public IResourceProvider Provider { get; set; }

		/// <summary>
		/// 关键字
		/// </summary>
		public string Key { get; set; }

		/// <summary>
		/// 搜索到的记录数
		/// </summary>
		public int? ResultCount { get; set; }

		/// <summary>
		/// 当前页记录数
		/// </summary>
		public int? PageSize { get; set; }

		/// <summary>
		/// 总页数
		/// </summary>
		public int? TotalPages { get; set; }

		/// <summary>
		/// 当前页码
		/// </summary>
		public int? PageIndex { get; set; }

		/// <summary>
		/// 是否有更多
		/// </summary>
		public bool HasMore { get; set; }

		/// <summary>
		/// 是否有之前的记录
		/// </summary>
		public bool HasPrevious { get; set; }

		/// <summary>
		/// 获得结果的排序类型
		/// </summary>
		public SortType? SortType { get; set; }

		#endregion

		/// <summary>
		/// 是否要求重新搜索
		/// </summary>
		public bool RequestResearch { get; set; }
	}
}
