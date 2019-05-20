namespace BRG.Entities
{
	public enum VerifyState
	{
		/// <summary>
		/// 未知
		/// </summary>
		Unknown = -1,
		/// <summary>
		/// 无
		/// </summary>
		None = 0,
		/// <summary>
		/// 已验证
		/// </summary>
		Verified = 10,
		/// <summary>
		/// 已报告
		/// </summary>
		Reported = 1,
		/// <summary>
		/// 非法
		/// </summary>
		Illegal = 2,
		/// <summary>
		/// 无效资源
		/// </summary>
		Fake = 3,
		/// <summary>
		/// 无效资源
		/// </summary>
		AutoFake = 4,
		/// <summary>
		/// 被自动判定为非法资源
		/// </summary>
		AutoIllegal = 5
	}
}
