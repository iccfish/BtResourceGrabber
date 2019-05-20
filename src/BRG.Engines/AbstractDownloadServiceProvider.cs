namespace BRG.Engines
{
	using BRG.Entities;
	using FSLib.Network.Http;

	public abstract class AbstractDownloadServiceProvider<T> : AbstractServerBase<T> where T : IServiceInfo
	{
		protected AbstractDownloadServiceProvider(T info)
			: base(info)
		{
		}

		/// <summary>
		/// 验证buffer是否是种子内容，如果不是，则返回null
		/// </summary>
		/// <param name="buffer"></param>
		/// <returns></returns>
		protected virtual byte[] ValidateTorrentContent(byte[] buffer)
		{
			return buffer != null && buffer.Length > 100 && buffer[0] == 'd' ? buffer : null;
		}

		/// <summary>
		/// 验证请求响应是否是种子内容，如果不是，则返回null
		/// </summary>
		/// <returns></returns>
		protected virtual byte[] ValidateTorrentContent(HttpContext<byte[]> context)
		{
			if (!context.IsValid())
				return null;

			if (context.IsRedirection || context.Response.ContentType.IndexOf("html") != -1)
				return null;

			return ValidateTorrentContent(context.Result);
		}

		/// <summary>
		/// 执行下载
		/// </summary>
		/// <returns></returns>
		protected virtual byte[] DownloadCore(string url, string refer)
		{
			return ValidateTorrentContent(NetworkClient.Create<byte[]>(HttpMethod.Get, url, refer, allowAutoRedirect:true).Send());
		}

	}
}
