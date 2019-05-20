namespace BRG.Entities
{
	public class DownloadTask
	{
		public IResourceInfo Resource { get; set; }

		public bool AlterDownload { get; set; }

		public string GroupKey { get; set; }

		/// <summary>
		/// 创建 <see cref="DownloadTask" />  的新实例(DownloadTask)
		/// </summary>
		/// <param name="resource"></param>
		/// <param name="alterDownload"></param>
		public DownloadTask(IResourceInfo resource, bool alterDownload)
		{
			Resource = resource;
			AlterDownload = alterDownload;
			GroupKey = resource.Hash;

			resource.DetailLoaded += (s, e) =>
			{
				GroupKey = resource.Hash;
			};
		}
	}
}
