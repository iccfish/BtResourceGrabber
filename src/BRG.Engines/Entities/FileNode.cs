namespace BRG.Engines.Entities
{
	using System.Collections.Generic;
	using BRG;
	using BRG.Entities;

	public class FileNode : IFileNode
	{
		ICollection<IFileNode> _children;
		string _sizeString;
		string _name;

		#region Implementation of ITorrentFileNode

		/// <summary>
		/// 名称
		/// </summary>
		public string Name
		{
			get { return _name; }
			set { _name = BrtUtility.RemoveHtmlChars(value); }
		}

		/// <summary>
		/// 大小
		/// </summary>
		public long? Size { get; set; }

		/// <summary>
		/// 大小描述
		/// </summary>
		public string SizeString
		{
			get { return _sizeString; }
			set { _sizeString = BrtUtility.ClearString(value); }
		}

		/// <summary>
		/// 是否是目录
		/// </summary>
		public bool IsDirectory { get; set; }

		/// <summary>
		/// 获得子节点信息
		/// </summary>
		public ICollection<IFileNode> Children
		{
			get
			{
				if (!IsDirectory)
					return null;
				return _children ?? (_children = new List<IFileNode>());
			}
		}

		#endregion
	}
}
