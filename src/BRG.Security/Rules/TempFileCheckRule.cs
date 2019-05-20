namespace BRG.Security.Rules
{
	using System.ComponentModel.Composition;
	using System.Text.RegularExpressions;
	using BRG.Entities;

	/// <summary>
	/// 临时文件校验
	/// </summary>
	[Export(typeof(ISecurityChecker))]
	class TempFileCheckRule : ISecurityChecker
	{
		/// <summary>
		/// 校验特定资源
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public VerifyState Check(IResourceInfo info)
		{
			if (Regex.IsMatch(info.Title, @"\.(bc!|td|cfg|part)$", RegexOptions.IgnoreCase))
				return VerifyState.AutoFake;

			return VerifyState.Unknown;
		}
	}
}
