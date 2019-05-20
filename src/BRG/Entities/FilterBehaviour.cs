namespace BRG.Entities
{
	using System.ComponentModel;

	/// <summary>
	/// 过滤器匹配时候的行为
	/// </summary>
	public enum FilterBehaviour
	{
		[Description("隐藏")]
		Hide = 0,
		[Description("标记")]
		Mark = 1
	}
}