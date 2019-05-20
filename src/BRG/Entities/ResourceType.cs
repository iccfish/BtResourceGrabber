namespace BRG.Entities
{
	using System;

	/// <summary>
	/// 资源类型
	/// </summary>
	public enum ResourceType
	{
		/// <summary>
		/// 多个资源项组合
		/// </summary>
		MultiResource = 0,
		/// <summary>
		/// BT
		/// </summary>
		BitTorrent = 1,
		/// <summary>
		/// 电驴
		/// </summary>
		Ed2K = 2,
		/// <summary>
		/// 网盘资源
		/// </summary>
		NetDisk = 100
	}
}
