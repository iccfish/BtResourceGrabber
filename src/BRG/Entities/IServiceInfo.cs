namespace BRG.Entities
{
	using System;
	using System.Drawing;

	/// <summary>
	/// 服务的基本信息
	/// </summary>
	public interface IServiceInfo
	{

		/// <summary>
		/// 名称
		/// </summary>
		string Name { get; }

		/// <summary>
		/// 作者
		/// </summary>
		string Author { get; }

		/// <summary>
		/// 联系方式
		/// </summary>
		string Contract { get; }

		/// <summary>
		/// 图标
		/// </summary>
		Image Icon { get; }

		/// <summary>
		/// 描述
		/// </summary>
		string Descrption { get; }

		/// <summary>
		/// 版本
		/// </summary>
		Version Version { get; }
	}
}
