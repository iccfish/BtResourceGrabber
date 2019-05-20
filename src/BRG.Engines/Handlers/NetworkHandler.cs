namespace BRG.Engines.Handlers
{
	using System;
	using System.Net;
	using BRG;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;

	public class NetworkHandler : HttpHandler
	{

		Options _options;

		public NetworkHandler()
		{
			_options = AppContext.Instance.Options;
		}

		#region Overrides of HttpHandler

		/// <summary>
		/// 获得用于发送请求的Request对象
		/// </summary>
		/// <param name="uri">当前请求的目标地址</param>
		/// <param name="method">当前请求的HTTP方法</param>
		/// <param name="context">当前的上下文 <see cref="HttpContext" /></param>
		/// <returns></returns>
		public override HttpWebRequest GetRequest(Uri uri, string method, HttpContext context)
		{
			var req = base.GetRequest(uri, method, context);

			var client = context.Client as NetworkClient;
			if (req != null && client != null && client.RequestSource != null)
			{
				req.Proxy = null;

				var useProxy = false;
				if (client.RequestSource is IResourceProvider)
				{
					useProxy = (client.RequestSource as IResourceProvider).RequireBypassGfw;
				}
				else if (client.RequestSource is ITorrentDownloadServiceProvider)
				{
					useProxy = (client.RequestSource as ITorrentDownloadServiceProvider).RequireBypassGfw;
				}
				if (client.RequestSource is IServiceBase && AppContext.Instance.Options.UseGfwProxyForAll)
					useProxy = true;

				if (useProxy)
				{
					var proxy = _options.UseSameProxy ? _options.CommonProxy : _options.GfwProxy;
					ApplyProxy(req, proxy);
				}
				else if (_options.EnableProxy) ApplyProxy(req, _options.CommonProxy);
			}
			else if (_options.EnableProxy) ApplyProxy(req, _options.CommonProxy);

			return req;
		}

		void ApplyProxy(WebRequest request, ProxyItem proxy)
		{
			if (proxy == null || string.IsNullOrEmpty(proxy.Host))
				return;

			request.Proxy = new WebProxy(proxy.Host, proxy.Port);
			if (!string.IsNullOrEmpty(proxy.UserName))
			{
				var p = request.Proxy as WebProxy;
				p.UseDefaultCredentials = false;
				p.Credentials = new NetworkCredential(proxy.UserName, proxy.Password);
			}
		}

		#endregion
	}
}
