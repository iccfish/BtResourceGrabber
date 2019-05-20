using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRG.Service
{
	using System.Drawing;
	using BRG.Entities;

	/// <summary>
	/// 常规服务信息
	/// </summary>
	public class CommonServiceInfo : IServiceInfo
	{
		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// 作者
		/// </summary>
		public string Author { get; }

		/// <summary>
		/// 联系方式
		/// </summary>
		public string Contract { get; }

		/// <summary>
		/// 图标
		/// </summary>
		public Image Icon { get; }

		/// <summary>
		/// 描述
		/// </summary>
		public string Descrption { get; }

		/// <summary>
		/// 版本
		/// </summary>
		public Version Version { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CommonServiceInfo"/> class.
		/// </summary>
		public CommonServiceInfo(string name, Image icon, Version version, string author = "", string contract = "", string description = "")
		{
			Name = name;
			Icon = icon;
			Version = version;
			Author = author;
			Contract = contract;
			Descrption = description;
		}
	}
}
