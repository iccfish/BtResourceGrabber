namespace BRG.Engines
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Linq;
	using System.Runtime.Remoting.Contexts;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;
	using BRG;
	using BRG.Engines.Entities;
	using BRG.Entities;
	using BRG.Service;
	using FSLib.Network.Http;

	public abstract class AbstractBuildInSearchServiceProvider : AbstractSearchServiceProvider<BuildinServerInfo>
	{
		protected AbstractBuildInSearchServiceProvider(string name, Image icon, string desc, string version = null)
			: base(new BuildinServerInfo(name, icon, desc))
		{
			if (version != null)
				(Info as BuildinServerInfo).Version = new Version(version);
		}
	}

	public abstract class AbstractSearchServiceProvider<T> : AbstractServerBase<T>, IResourceProvider where T : IServiceInfo
	{
		protected AbstractSearchServiceProvider(T info)
			: base(info)
		{
		}

		static readonly char[] PathSpliter = new[] { '/', '\\' };
		ITorrentDownloadServiceProvider _preferDownloader;

		/// <summary>
		/// 是否允许自动重定向请求
		/// </summary>
		public bool EnableAutoRedirect { get; protected set; }

		/// <summary>
		/// 获得或设置优选的下载器
		/// </summary>
		public ITorrentDownloadServiceProvider PreferDownloader
		{
			get
			{
				if (_preferDownloadServiceType == null)
					return null;

				return _preferDownloader ?? (AppContext.Instance.DownloadServiceProviders.FirstOrDefault(s => _preferDownloadServiceType.IsInstanceOfType(s)));
			}
			set { _preferDownloader = value; }
		}

		Type _preferDownloadServiceType;

		/// <summary>
		/// 设置默认的下载器
		/// </summary>
		/// <typeparam name="TService"></typeparam>
		public void SetPreferDownloader<TService>() where TService : ITorrentDownloadServiceProvider
		{
			_preferDownloadServiceType = typeof(TService);
		}

		/// <summary>
		/// 创建资源项
		/// </summary>
		/// <param name="hash"></param>
		/// <param name="title"></param>
		/// <returns></returns>
		protected ResourceInfo CreateResourceInfo(string hash, string title, ResourceType type = ResourceType.BitTorrent)
		{
			return new ResourceInfo()
			{
				Hash = hash,
				PreferDownloadProvider = PreferDownloader,
				Provider = this,
				Title = title,
				ResourceType = type
			};
		}

		/// <summary>
		/// 将指定的文件节点加到树中
		/// </summary>
		/// <param name="resource"></param>
		/// <param name="path"></param>
		/// <param name="size"></param>
		/// <param name="sizeValue"></param>
		protected virtual void AddFileNode(IResourceInfo resource, string path, long? size, string sizeValue)
		{
			var pathlist = path.Split(PathSpliter, StringSplitOptions.RemoveEmptyEntries);
			var container = (ICollection<IFileNode>)resource;

			for (var i = 0; i < pathlist.Length - 1; i++)
			{
				var subcontainer = container.FirstOrDefault(s => s.Name.IsIgnoreCaseEqualTo(pathlist[i]));
				if (subcontainer == null)
				{
					subcontainer = new FileNode()
					{
						Name = pathlist[i],
						IsDirectory = true
					};
					container.Add(subcontainer);
				}
				container = subcontainer.Children;
			}

			container.Add(new FileNode()
			{
				IsDirectory = false,
				Name = pathlist.Last(),
				Size = size,
				SizeString = sizeValue
			});
		}

		protected string RemoveHtmlStrings(string str)
		{
			if (String.IsNullOrEmpty(str))
				return String.Empty;

			return Regex.Replace(str, "<[^>]+>", "").Trim();
		}

		protected string RemoveSpaceChars(string str)
		{
			if (String.IsNullOrEmpty(str))
				return String.Empty;

			return Regex.Replace(str, @"\s", "");
		}


		/// <summary>
		/// 加载相信信息
		/// </summary>
		public virtual void LoadFullDetail(IResourceInfo info)
		{
			if (info.IsHashLoaded || info.SiteData == null)
				return;

			LoadFullDetailCore(info);
		}

		protected virtual void LoadFullDetailCore(IResourceInfo info)
		{
			if (SiteID == 0 || !info.IsHashLoaded || info.SiteData == null)
				return;

			var sid = 0L;
			var pn = "";
			if (info.SiteData is SiteInfo)
			{
				sid = (info.SiteData as SiteInfo).SiteID;
				pn = (info.SiteData as SiteInfo).PageName;
			}

			if (sid > 0L)
				AppContext.Instance.HashSiteMap.UpdateHash(SiteID, sid, info.Hash);
			else if (!pn.IsNullOrEmpty())
				AppContext.Instance.HashSiteMapStr.UpdateHash(SiteID, pn, info.Hash);
		}

		/// <summary>
		/// 准备下载
		/// </summary>
		/// <param name="resource"></param>
		public virtual void PrepareDownload(IResourceInfo resource)
		{

		}

		/// <summary>
		/// 核心加载
		/// </summary>
		/// <param name="context"></param>
		/// <param name="url"></param>
		/// <param name="htmlContent">HTML内容</param>
		/// <param name="result">目标结果</param>
		/// <returns></returns>
		protected virtual bool LoadCore(HttpContext<string> context, string url, string htmlContent, ResourceSearchInfo result)
		{
			if (SiteID > 0)
			{
				var hashMap = AppContext.Instance.HashSiteMap.GetValue(SiteID);
				var hashMapStr = AppContext.Instance.HashSiteMapStr.GetValue(SiteID);

				if (hashMap != null)
				{
					//预过滤结果
					foreach (var item in result)
					{
						if (item.IsHashLoaded || item.SiteData == null)
							continue;

						var sinfo = item as ResourceInfo;
						if (item.SiteData is SiteInfo)
						{
							var siteinfo = sinfo.SiteData as SiteInfo;

							if (siteinfo.SiteID > 0L)
								sinfo.Hash = hashMap?.GetValue((item.SiteData as SiteInfo).SiteID) ?? "";

							if (sinfo.Hash.IsNullOrEmpty() && !siteinfo.PageName.IsNullOrEmpty())
							{
								sinfo.Hash = hashMapStr?.GetValue(siteinfo.PageName) ?? "";
							}
						}
					}
				}
			}

			result.HasMore &= result.Count > 0;

			//同步资源信息
			var ctx = AppContext.Instance.DataContext;

			ResourceProcessorWrapper.Instance.ResourcesLoaded(result);
			ctx.PreviewInfo_Sync(result);
			ctx.VerifyState_Sync(result);
			ctx.DownloadHistory_Sync(result);
			ResourceProcessorWrapper.Instance.ResourcesFetched(result);

			return true;
		}

		/// <summary>
		/// 加载数据
		/// </summary>
		/// <param name="key"></param>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <returns></returns>
		public virtual IResourceSearchInfo Load(string key, SortType sortType, int sortDirection, int pagesize, int pageindex = 1)
		{
			return Load(key, sortType, sortDirection, pagesize, pageindex, 0);
		}

		/// <summary>
		/// 加载数据
		/// </summary>
		/// <param name="key"></param>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <returns></returns>
		public virtual IResourceSearchInfo Load(string key, SortType sortType, int sortDirection, int pagesize, int pageindex = 1, int loadStack = 0)
		{
			var result = new ResourceSearchInfo(this)
			{
				Key = key,
				PageIndex = pageindex,
				PageSize = null
			};

			var url = GetSearchUrl(key, sortType, sortDirection, pagesize, pageindex);
			var html = NetworkClient.Create<string>(HttpMethod.Get, url, ReferUrlPage, allowAutoRedirect: EnableAutoRedirect).Send();
			if (!html.IsValid())
				return null;

			if (html.IsRedirection && !DefaultHost.IsNullOrEmpty() && html.Redirection.Current.Host != Host && loadStack < 5)
			{
				Host = html.Redirection.Current.Host;
				return Load(key, sortType, sortDirection, pagesize, pageindex, loadStack + 1);
			}

			if (!LoadCore(html, url, html.Result, result))
			{
				if (result.RequestResearch && loadStack < 5)
					return Load(key, sortType, sortDirection, pagesize, pageindex, loadStack + 1);

				return null;
			}
			return result;
		}

		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public abstract string GetDetailUrl(IResourceInfo resource);

		/// <summary>
		/// 获得搜索页地址
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public abstract string GetSearchUrl(string key, SortType sortType, int sortDirection, int pagesize, int pageindex);

		/// <summary>
		/// 是否支持Unicode搜索
		/// </summary>
		public virtual bool SupportUnicode { get; protected set; }

		/// <summary>
		/// 支持的排序类型
		/// </summary>
		public virtual SortType? SupportSortType { get; protected set; }

		/// <summary>
		/// 是否支持查看种子内容
		/// </summary>
		public virtual bool SupportLookupTorrentContents { get; protected set; }

		/// <summary>
		/// 是否支持自定义每页条数
		/// </summary>
		public virtual bool SupportCustomizePageSize { get; protected set; }

		/// <summary>
		/// 加载内容
		/// </summary>
		public virtual void LookupTorrentContents(IResourceInfo torrent)
		{
			if (!torrent.IsHashLoaded)
				LoadFullDetail(torrent);

			//如果已经加载，则跳过
			if (torrent.Count > 0)
				return;

			var url = GetDetailUrl(torrent);
			var htmlContext = NetworkClient.Create<string>(HttpMethod.Get, url, ReferUrlPage).Send();
			if (!htmlContext.IsValid())
				return;

			LookupTorrentContentsCore(url, torrent, htmlContext.Result);
		}

		/// <summary>
		/// 加载内容
		/// </summary>
		/// <param name="url"></param>
		public virtual void LookupTorrentContentsCore(string url, IResourceInfo torrent, string htmlContent)
		{

		}

		/// <summary>
		/// 是否支持预加载标记
		/// </summary>
		public virtual bool SupportResourceInitialMark { get; protected set; } = true;


		public virtual void LoadPreviewInfo(IResourceInfo resource)
		{
			if (resource == null || resource.SupportPreivewType == PreviewType.None || resource.PreviewInfo != null)
				return;

			new TaskFactory().StartNew(() => LoadPreviewInfoCore(resource));
		}


		/// <summary>
		/// 加载子资源
		/// </summary>
		/// <param name="resource"></param>
		public void LoadSubResources(IResourceInfo resource)
		{
			var res = resource as ResourceInfo;
			if (res == null || !res.HasSubResources || res.SubResources != null)
				return;

			LoadSubResourcesCore(res);
		}

		protected virtual void LoadSubResourcesCore(ResourceInfo resource)
		{

		}

		/// <summary>
		/// 加载预览信息
		/// </summary>
		/// <param name="resource"></param>
		protected virtual void LoadPreviewInfoCore(IResourceInfo resource)
		{

		}


		/// <summary>
		/// 默认的主机头
		/// </summary>
		protected string DefaultHost { get; set; }

		string _host;

		/// <summary>
		/// 当前的主机头
		/// </summary>
		public virtual string Host
		{
			get { return Property.GetValue("host") ?? DefaultHost; }
			set
			{
				if (value == Property.GetValue("host"))
					return;

				_host = DefaultHost == value ? null : value;
				if (_host == null)
				{
					if (Property.ContainsKey("host"))
						Property.Remove("host");
				}
				else
				{
					Property["host"] = value;
				}

				//变更引用页
				if (ReferUrlPage != null)
				{
					var refer = new Uri(ReferUrlPage);
					var ub = new UriBuilder(refer.Scheme, Host, refer.Port, refer.AbsolutePath, refer.Query);
					ReferUrlPage = ub.Uri.ToString();
				}
			}
		}

		/// <summary>
		/// 主页
		/// </summary>
		public virtual string HomePage => ReferUrlPage;

		protected static string DecodePage(string html)
		{
			html = Regex.Replace(html, @"<script[^>]*>\s*document\.write\s*\(decode.*?\((.*?)\)\).*?</script>", _ =>
			{
				var arg = _.GetGroupValue(1);
				arg = Regex.Replace(arg, @"[""'+]", "");
				arg = System.Web.HttpUtility.UrlDecode(arg);
				return arg;
			}, RegexOptions.IgnoreCase | RegexOptions.Singleline);

			return html;
		}
	}

	public abstract class AbstractBuildInSearchServiceProvider<TExtraData> : AbstractSearchServiceProvider<BuildinServerInfo, TExtraData>
	{
		protected AbstractBuildInSearchServiceProvider(string name, Image icon, string desc, string version = null)
			: base(new BuildinServerInfo(name, icon, desc))
		{
			if (version != null)
				(Info as BuildinServerInfo).Version = new Version(version);
		}
	}

	public abstract class AbstractBuildInSearchServiceProviderWithSiteData : AbstractSearchServiceProvider<BuildinServerInfo, SiteInfo>
	{
		protected AbstractBuildInSearchServiceProviderWithSiteData(string name, Image icon, string desc, string version = null)
			: base(new BuildinServerInfo(name, icon, desc))
		{
			if (version != null)
				(Info as BuildinServerInfo).Version = new Version(version);
		}
	}



	public abstract class AbstractSearchServiceProvider<T, TExtraData> : AbstractSearchServiceProvider<T> where T : IServiceInfo
	{
		protected AbstractSearchServiceProvider(T info)
			: base(info)
		{

		}

		/// <summary>
		/// 获得站点相关的数据
		/// </summary>
		public TExtraData GetExtraData(IResourceInfo torrent)
		{
			return (TExtraData)torrent.SiteData;
		}


		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public override string GetDetailUrl(IResourceInfo resource)
		{
			var data = GetExtraData(resource);
			return GetDetailUrlCore(resource, data);
		}


		/// <summary>
		/// 获得详细页地址
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public abstract string GetDetailUrlCore(IResourceInfo resource, TExtraData data);
	}
}
