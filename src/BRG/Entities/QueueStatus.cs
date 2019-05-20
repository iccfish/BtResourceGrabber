namespace BRG.Entities
{
	public enum QueueStatus
	{
		Wait = 0,
		Running = 1,
		Succeed = 2,
		Failed = 3,
		Prepare = 4,
		/// <summary>
		/// 已跳过
		/// </summary>
		Skipped = 5
	}
}
