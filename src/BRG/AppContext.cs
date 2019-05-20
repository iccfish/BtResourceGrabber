using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRG
{
	using System.ComponentModel.Composition;
	using System.Drawing;
	using System.Windows.Forms;

	using BRG.Entities;
	using BRG.Service;

	public class AppContext
	{
		#region 单例模式

		static AppContext _instance;
		static readonly object _lockObject = new object();

		public static AppContext Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = new AppContext();
						}
					}
				}

				return _instance;
			}
		}

		private AppContext()
		{

		}

		#endregion

		/// <summary>
		/// 当前的主窗口
		/// </summary>
		public Form MainForm { get; set; }

		/// <summary>
		/// UI回调
		/// </summary>
		/// <param name="action"></param>
		public static void UiInvoke(Action action)
		{
			if (action == null)
				return;

			Instance.MainForm.Invoke(action);
		}


		/// <summary>
		/// 配置加载类
		/// </summary>
		[Import]
		public IFileConfigLoader ConfigLoader { get; private set; }

		/// <summary>
		/// 当前使用的数据库环境
		/// </summary>
		[Import]
		protected IDataContext DataContextMaster { get; private set; }

		/// <summary>
		/// 资源操作
		/// </summary>
		[Import]
		public IResourceOperation ResourceOperation { get; private set; }

		/// <summary>
		/// 当前使用的数据库环境
		/// </summary>
		public IDataContext DataContext { get { return (IDataContext)Activator.CreateInstance(DataContextMaster.GetType()); } }

		/// <summary>
		/// 安全检测
		/// </summary>
		[Import]
		public ISecurityCheck SecurityCheck { get; private set; }

		[ImportMany]
		public IEnumerable<IResourceProvider> ResourceProviders { get; set; }

		/// <summary>
		/// 下载提供器
		/// </summary>
		[ImportMany]
		public IEnumerable<ITorrentDownloadServiceProvider> DownloadServiceProviders { get; set; }

		[ImportMany]
		public IResourceProcessor[] ResourceProcessors { get; set; }

		[ImportMany]
		public IResourceContextMenuAddin[] ResourceContextMenuAddins { get; set; }


		public HashSiteMap HashSiteMap { get; private set; }
		public HashSiteMapStr HashSiteMapStr { get; private set; }

		public HashMarkCollection HashMarkCollection { get; private set; }

		public Statistics Statistics { get; private set; }




		/// <summary>
		/// 下载历史
		/// </summary>
		public DownloadHistory DownloadHistory { get; private set; }




		public Options Options { get; private set; }

		/// <summary>
		/// 初始化
		/// </summary>
		public void Init()
		{
			Options = ConfigLoader.Load<Options>();
			DownloadHistory = ConfigLoader.Load<DownloadHistory>();
			HashMarkCollection = ConfigLoader.Load<HashMarkCollection>();
			HashSiteMap = ConfigLoader.Load<HashSiteMap>();
			HashSiteMapStr = ConfigLoader.Load<HashSiteMapStr>();
			Statistics = ConfigLoader.Load<Statistics>();

			if (Options.HashMarks.Count == 0)
			{
				Options.HashMarks.Add("已下载", new HashMark(Color.Gray) { BackColor = Color.LightGray });
				Options.EnableAutoMark = true;
				Options.AutoMarkDownloadedTorrent = "已下载";
			}
			if (Options.EngineStandalone == null)
			{
				Options.EngineStandalone = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
											{
												"TUS"
											};
			}
		}


		/// <summary>
		/// 关闭服务
		/// </summary>
		public void Shutdown()
		{
			if (!Options.LogSearchHistory)
			{
				Options.LastSearchKey = null;
			}

			ConfigLoader.Save(Options);
			ConfigLoader.Save(DownloadHistory);
			ConfigLoader.Save(HashMarkCollection);
			ConfigLoader.Save(HashSiteMap);
			ConfigLoader.Save(HashSiteMapStr);
			ConfigLoader.Save(Statistics);
		}


		/// <summary>
		/// 获得指定资源的标注名
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public string GetResourceMarkName(IResourceInfo resource)
		{
			if (resource == null || resource.Hash.IsNullOrEmpty())
				return null;
			return HashMarkCollection.GetValue(resource.Hash);
		}


		/// <summary>
		/// 获得指定资源的标注
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public HashMark GetResourceMark(IResourceInfo resource, out string name)
		{
			name = null;

			if (resource == null || !resource.IsHashLoaded)
				return null;

			name = GetResourceMarkName(resource);
			if (string.IsNullOrEmpty(name))
				return null;

			return AppContext.Instance.Options.HashMarks.GetValue(name);
		}



		/// <summary>
		/// 资源已下载
		/// </summary>
		public event EventHandler<ResourceEventArgs> ResouceDownloaded;

		/// <summary>
		/// 资源标记为未下载
		/// </summary>
		public event EventHandler<ResourceEventArgs> ResouceUndownload;

		/// <summary>
		/// 引发 <see cref="ResouceDownloaded" /> 事件
		/// </summary>
		/// <param name="sender">引发此事件的源对象</param>
		/// <param name="ea">包含此事件的参数</param>
		public void OnResouceDownloaded(object sender, ResourceEventArgs ea)
		{
			var handler = ResouceDownloaded;
			if (handler != null)
				handler(sender, ea);
		}

		/// <summary>
		/// 引发 <see cref="ResouceUndownload" /> 事件
		/// </summary>
		/// <param name="sender">引发此事件的源对象</param>
		/// <param name="ea">包含此事件的参数</param>
		public void OnResouceUndownload(object sender, ResourceEventArgs ea)
		{
			var handler = ResouceUndownload;
			if (handler != null)
				handler(sender, ea);
		}


		public event EventHandler<TorrentMarkEventArgs> TorrentMarked;

		/// <summary>
		/// 引发 <see cref="TorrentMarked" /> 事件
		/// </summary>
		/// <param name="sender">引发此事件的源对象</param>
		/// <param name="ea">包含此事件的参数</param>
		public void OnTorrentMarked(object sender, TorrentMarkEventArgs ea)
		{
			var handler = TorrentMarked;
			if (handler != null)
				handler(sender, ea);
		}


		public event EventHandler RequestRefreshMarkCollection;

		/// <summary>
		/// 引发 <see cref="RequestRefreshMarkCollection" /> 事件
		/// </summary>
		/// <param name="sender">引发此事件的源对象</param>
		public void OnRequestRefreshMarkCollection(object sender)
		{
			var handler = RequestRefreshMarkCollection;
			if (handler != null)
				handler(sender, EventArgs.Empty);
		}
	}
}
