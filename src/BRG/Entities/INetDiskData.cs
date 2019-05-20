using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRG.Entities
{
	/// <summary>
	/// 网盘数据
	/// </summary>
	public interface INetDiskData
	{
		/// <summary>
		/// 地址
		/// </summary>
		string Url { get; }

		/// <summary>
		/// 密码
		/// </summary>
		string Pwd { get; }


		/// <summary>
		/// 获得网盘类型
		/// </summary>
		NetDiskType NetDiskType { get; }

	}
}
