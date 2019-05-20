namespace BRG.Engines
{
	using System;
	using System.ComponentModel.Composition.Hosting;
	using BRG.Engines.Handlers;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;
	using HtmlAgilityPack;
	using System.Web;

	using Ivony.Html;
	using Ivony.Html.Parser;

	public abstract class AbstractServerBase<T> : IServiceBase where T : IServiceInfo
	{
		bool _disabled;

		/// <summary>
		/// 站点ID
		/// </summary>
		public int SiteID { get; protected set; }

		protected AbstractServerBase(T info)
		{
			Info = info;
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public virtual void Init(CompositionContainer container)
		{

		}


		/// <summary>
		/// 测试服务状态
		/// </summary>
		/// <returns></returns>
		public virtual TestStatus Test()
		{
			if (PageCheckKey.IsNullOrEmpty())
				return TestStatus.Ok;

			return TestCore(ReferUrlPage, PageCheckKey);
		}

		/// <summary>
		/// 获得或设置是否禁用
		/// </summary>
		public bool Disabled
		{
			get { return _disabled; }
			set
			{
				if (_disabled == value)
					return;

				_disabled = value;
				OnDisabledChanged();

				//自动处理事件
				var setting = (this is ITorrentDownloadServiceProvider) ? AppContext.Instance.Options.DisabledSearchProviders : AppContext.Instance.Options.DisableDownloadProviders;
				if (value)
				{
					if (!setting.Contains(Info.Name))
						setting.Add(Info.Name);
				}else if(!setting.Contains(Info.Name)) setting.Remove(Info.Name);
			}
		}

		public event EventHandler DisabledChanged;

		/// <summary>
		/// 引发 <see cref="DisabledChanged" /> 事件
		/// </summary>
		protected virtual void OnDisabledChanged()
		{
			var handler = DisabledChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 测试核心
		/// </summary>
		/// <param name="uri"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		protected virtual TestStatus TestCore(string uri, string keyword)
		{
			var ctx = NetworkClient.Create<string>(HttpMethod.Get, uri, uri).Send();
			if (!ctx.IsValid())
				return TestStatus.Failed;

			if (!string.IsNullOrEmpty(keyword) && ctx.Result.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) == -1)
				return TestStatus.Failed;

			return TestStatus.Ok;
		}

		#region Implementation of IServiceBase

		/// <summary>
		/// 扩展信息
		/// </summary>
		public virtual IServiceInfo Info { get; private set; }

		#endregion

		NetworkClient _networkClient;

		/// <summary>
		/// 网络客户端
		/// </summary>
		public NetworkClient NetworkClient {
			get
			{
				if (_networkClient != null)
					return _networkClient;

				_networkClient=new NetworkClient(this);

				return _networkClient;
			}
		}

		/// <summary>
		/// 获得或设置页面检测的关键字
		/// </summary>
		public string PageCheckKey { get; set; }

		/// <summary>
		/// 获得或设置引用页
		/// </summary>
		public string ReferUrlPage { get; protected set; }

		EnginePropertyCollection _properties;

		/// <summary>
		/// 获得引擎的设置
		/// </summary>
		public EnginePropertyCollection Property
		{
			get
			{
				return _properties ?? (_properties = AppContext.Instance.Options.GetEnginePropertyCollection(this));
			}
		}


		/// <summary>
		/// 是否需要科学上网
		/// </summary>
		public virtual bool RequireBypassGfw { get; protected set; }

		protected HtmlDocument CreateHtmlDocument(string html)
		{
			var doc = new HtmlDocument();
			doc.LoadHtml(html);

			return doc;
		}

		protected IHtmlDocument CreateHtmlDom(string url, string html)
		{
			var parser = new JumonyParser();
			return parser.Parse(html, new Uri(url));
		}

		protected string UE(string str)
		{
			return HttpUtility.UrlEncode(str);
		}

		protected string UD(string str)
		{
			return HttpUtility.UrlDecode(str);
		}


		protected string HE(string str)
		{
			return HttpUtility.HtmlEncode(str);
		}

		protected string HD(string str)
		{
			return HttpUtility.HtmlDecode(str);
		}

		public bool SupportOption { get; protected set; } = false;

		/// <summary>
		/// 显示选项
		/// </summary>
		public virtual void ShowOption()
		{

		}

		/// <summary>
		/// 连接
		/// </summary>
		public virtual void Connect()
		{

		}

		/// <summary>
		/// 断开连接
		/// </summary>
		public virtual void Disconnect()
		{

		}

		/// <summary>
		/// 从指定版本升级
		/// </summary>
		/// <param name="oldVersion"></param>
		public virtual void UpgradeFrom(string oldVersion, string currentVersion)
		{
		}
	}
}
