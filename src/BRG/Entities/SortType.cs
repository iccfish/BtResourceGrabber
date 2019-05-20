namespace BRG.Entities
{
	using System;

	using FSLib;
	using FSLib.Extension.FishLib.Attributes;

	/// <summary>
	/// 排序类型
	/// </summary>
	[Flags]
	public enum SortType
	{
		[SRDescription(typeof(SortTypeName), "Default")]
		Default = 1,
		[SRDescription(typeof(SortTypeName), "Title")]
		Title = 2,
		[SRDescription(typeof(SortTypeName), "PubDate")]
		PubDate = 4,
		//[SRDescription(typeof(SortTypeName), "TorrentSize")]
		//TorrentSize = 8,
		[SRDescription(typeof(SortTypeName), "FileSize")]
		FileSize = 16,
		//[SRDescription(typeof(SortTypeName), "LeechCount")]
		//LeechCount = 32,
		//[SRDescription(typeof(SortTypeName), "SeederCount")]
		//SeederCount = 64
	}
}
