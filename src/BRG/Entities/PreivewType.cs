namespace BRG.Entities
{
	using System;

	[Flags]
	public enum PreviewType
	{
		None = 0,
		Text = 1,
		Image = 2,
		WebPage = 4
	}
}
