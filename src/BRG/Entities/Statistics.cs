namespace BRG.Entities
{
	using System;
	using System.Collections.Generic;
	using BRG.Service;

	public class Statistics
	{
		public Statistics()
		{
			SearchService = new Dictionary<string, ServiceStatistics>(StringComparer.OrdinalIgnoreCase);
			DownloadService = new Dictionary<string, ServiceStatistics>(StringComparer.OrdinalIgnoreCase);
		}

		public Dictionary<string, ServiceStatistics> SearchService { get; set; }


		public Dictionary<string, ServiceStatistics> DownloadService { get; set; }

		public void UpdateRunningStatistics(bool isSearchProvider, string name, int usageCount, int failedCount, int successCount, int errorCount)
		{
			var dic = isSearchProvider ? SearchService : DownloadService;
			var data = dic.GetValue(name, _ => new ServiceStatistics());

			data.UsageCount += usageCount;
			data.FailedCount += failedCount;
			data.ErrorCount += errorCount;
			data.SuccessCount += successCount;
		}


		public void UpdateRunningStatistics(bool isSearchProvider, IServiceBase service, int usageCount, int failedCount, int successCount, int errorCount)
		{
			UpdateRunningStatistics(isSearchProvider, service.Info.Name, usageCount, failedCount, successCount, errorCount);
		}

	}
}
