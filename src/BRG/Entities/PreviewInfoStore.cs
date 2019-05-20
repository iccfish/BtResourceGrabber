namespace BRG.Entities
{
	public class PreviewInfoStore : PreviewInfo
	{
		public PreviewType PreviewType { get; set; }

		public string PreviewSource { get; set; }

		public string Hash { get; set; }

		public ResourceType SType { get; set; }
	}
}