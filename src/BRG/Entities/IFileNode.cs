namespace BRG.Entities
{
	using System.Collections.Generic;

	/// <summary>
	/// Torrent文件节点
	/// </summary>
	public interface IFileNode
	{
		/// <summary>
		/// 名称
		/// </summary>
		string Name { get; }

		/// <summary>
		/// 大小
		/// </summary>
		long? Size { get; }

		/// <summary>
		/// 大小描述
		/// </summary>
		string SizeString { get; }

		/// <summary>
		/// 是否是目录
		/// </summary>
		bool IsDirectory { get; }

		/// <summary>
		/// 获得子节点信息
		/// </summary>
		ICollection<IFileNode> Children { get; }
	}
}
