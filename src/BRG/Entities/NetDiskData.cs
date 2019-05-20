namespace BRG.Entities
{
	public class NetDiskData : INetDiskData
	{
		/// <summary>
		/// 地址
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// 密码
		/// </summary>
		public string Pwd { get; set; }

		/// <summary>
		/// 获得网盘类型
		/// </summary>
		public NetDiskType NetDiskType { get; set; }
	}
}