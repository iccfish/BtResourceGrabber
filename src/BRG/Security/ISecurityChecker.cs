namespace BRG.Security
{
	using BRG.Entities;

	/// <summary>
	/// 校验接口
	/// </summary>
	public interface ISecurityChecker
	{
		/// <summary>
		/// 校验特定资源
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		VerifyState Check(IResourceInfo info);
	}
}
