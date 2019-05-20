namespace BRG.Security.Rules
{
	using System.ComponentModel.Composition;
	using System.Text.RegularExpressions;
	using BRG.Entities;

	/// <summary>
	/// 视频 & 可执行混合
	/// </summary>
	[Export(typeof(ISecurityChecker))]
	class MixedExeAndVideoChecker : ISecurityChecker
	{
		/// <summary>
		/// 校验特定资源
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public VerifyState Check(IResourceInfo info)
		{
			var isExe = Regex.IsMatch(info.Title, @"[-\.]exe($|\.(zip|rar|7z))", RegexOptions.IgnoreCase);
			var isVideo = Regex.IsMatch(info.Title, @"[^a-z\d](rmvb|rm|mkv|wmv|flv|mp4|asf)($|[^a-z]?)", RegexOptions.IgnoreCase);

			if (!isExe || !isVideo)
				return VerifyState.Unknown;

			//白名单，过滤软件类的东西，比如转换器什么的
			if (Regex.IsMatch(info.Title, @"((\d{1,2}\.){2,}|converter|burner|ripper)", RegexOptions.IgnoreCase))
			{
				return VerifyState.Unknown;
			}

			return isExe && isVideo ? VerifyState.AutoIllegal : VerifyState.Unknown;
		}
	}
}
