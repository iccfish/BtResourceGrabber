namespace BRG.Entities
{
	using System;

	public class ResourceVerifyInfo
	{
		public int SType { get; set; }

		public string Hash { get; set; }

		public VerifyState State { get; set; }


		public int ReportCount { get; set; }

		public DateTime UpdateTime { get; set; }
	}
}
