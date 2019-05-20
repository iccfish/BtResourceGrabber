namespace BRG.Entities
{
	public class ServiceStatistics
	{
		public int ErrorCount { get; set; }


		/// <summary>
		/// 失败次数
		/// </summary>
		public int FailedCount { get; set; }

		/// <summary>
		/// 成功次数
		/// </summary>
		public int SuccessCount { get; set; }

		/// <summary>
		/// 总使用次数
		/// </summary>
		public int UsageCount { get; set; }

	}
}
