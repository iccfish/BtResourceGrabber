using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;

namespace BtResourceGrabber.Service
{
	using System.Diagnostics;
	using System.IO;
	using System.Reflection;
	using System.Windows.Forms;
	using BRG;
	using BRG.Entities;
	using BRG.Security;
	using BRG.Service;

	using FSLib.Extension.FishLib;

	public class ServiceManager
	{
		#region 单例模式

		static ServiceManager _instance;
		static readonly object _lockObject = new object();
		IResourceProvider _activeSearchService;

		public static ServiceManager Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = new ServiceManager();
						}
					}
				}

				return _instance;
			}
		}

		#endregion

		private ServiceManager()
		{

		}


		public IEnumerable<IResourceProvider> ResourceProviders { get { return AppContext.Instance.ResourceProviders; } }

		/// <summary>
		/// 下载提供器
		/// </summary>
		public IEnumerable<ITorrentDownloadServiceProvider> DownloadServiceProviders { get { return AppContext.Instance.DownloadServiceProviders; } }

		public IResourceProcessor[] ResourceProcessors { get { return AppContext.Instance.ResourceProcessors; } }

		public IResourceContextMenuAddin[] ResourceContextMenuAddins { get { return AppContext.Instance.ResourceContextMenuAddins; } }

		Dictionary<object, Dictionary<IResourceContextMenuAddin, IResourceContextMenuAddin>> _contextMenuAddinsCache = new Dictionary<object, Dictionary<IResourceContextMenuAddin, IResourceContextMenuAddin>>();

		/// <summary>
		/// 触发连接
		/// </summary>
		/// <param name="ctx"></param>
		/// <param name="providers"></param>
		internal void InvokeRegisterContextMenuAddin(ContextMenuStrip ctx, IResourceProvider[] providers)
		{
			if (ResourceContextMenuAddins == null || _contextMenuAddinsCache.ContainsKey(ctx))
				return;

			var dic = new Dictionary<IResourceContextMenuAddin, IResourceContextMenuAddin>();
			_contextMenuAddinsCache.Add(ctx, dic);

			foreach (var addin in ResourceContextMenuAddins)
			{
				try
				{
					var obj = Activator.CreateInstance(addin.GetType()) as IResourceContextMenuAddin;

					Trace.TraceInformation("::call register contextmenu addin on :: " + addin.Info.Name);
					obj.Register(ctx, providers);
					dic.Add(addin, obj);
				}
				catch (Exception ex)
				{
					Trace.TraceError(ex.ToString());
				}
			}
		}

		internal void InvokeCallOnOpeningContextMenu(ContextMenuStrip ctx, params IResourceInfo[] resourceInfos)
		{
			if (ResourceContextMenuAddins == null || !_contextMenuAddinsCache.ContainsKey(ctx))
				return;

			var map = _contextMenuAddinsCache[ctx];
			foreach (var addin in ResourceContextMenuAddins)
			{
				var addinClone = map.GetValue(addin);
				if (addinClone == null)
					continue;

				try
				{
					addinClone.OnOpening(resourceInfos);
				}
				catch (Exception ex)
				{
					Trace.TraceError(ex.ToString());
				}
			}
		}

		/// <summary>
		/// 当前活动的搜索引擎
		/// </summary>
		public IResourceProvider ActiveSearchService
		{
			get { return _activeSearchService; }
			set
			{
				if (object.Equals(_activeSearchService, value))
					return;

				_activeSearchService = value;
				OnActiveSearchServiceChanged();
				AppContext.Instance.Options.ActiveSearchProvider = value?.Info.Name;
			}
		}

		public event EventHandler ActiveSearchServiceChanged;

		/// <summary>
		/// 引发 <see cref="ActiveSearchServiceChanged" /> 事件
		/// </summary>
		protected virtual void OnActiveSearchServiceChanged()
		{
			var handler = ActiveSearchServiceChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		CompositionContainer _container;

		internal void Init()
		{
			var root = ApplicationRunTimeContext.GetProcessMainModuleDirectory();

			var cat = new AggregateCatalog();
			cat.Catalogs.Add(new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly()));
			cat.Catalogs.Add(new DirectoryCatalog(root, "BR*.dll"));

			var pluginDir = Path.Combine(root, "Plugins");
			if (Directory.Exists(pluginDir))
				cat.Catalogs.Add(new DirectoryCatalog(pluginDir, "*.dll"));

			_container = new CompositionContainer(cat);

			try
			{
				_container.ComposeParts(AppContext.Instance);
			}
			catch (ReflectionTypeLoadException loaderEx)
			{
				throw new Exception(loaderEx.LoaderExceptions.Select(s => s.ToString()).JoinAsString(";"), loaderEx);
			}
			catch (Exception)
			{

				throw;
			}
		}

		/// <summary>
		/// 连接
		/// </summary>
		internal void Connect()
		{
			var opt = AppContext.Instance.Options;
			//标记禁用的处理器
			opt.DisableDownloadProviders.ForEach(s =>
			{
				var service = DownloadServiceProviders.FirstOrDefault(x => x.Info.Name == s);
				if (service != null) service.Disabled = true;
			});
			opt.DisabledSearchProviders.ForEach(s =>
			{
				var service = ResourceProviders.FirstOrDefault(x => x.Info.Name == s);
				if (service != null) service.Disabled = true;
			});

			//处理默认的引擎
			if (ResourceProviders.Any())
			{
				if (!string.IsNullOrEmpty(opt.ActiveSearchProvider))
				{
					ActiveSearchService = ResourceProviders.FirstOrDefault(s => s.Info.Name == opt.ActiveSearchProvider);
				}
			}
			foreach (var torrentResourceProvider in ResourceProviders)
			{
				torrentResourceProvider.Init(_container);
			}
			foreach (var torrentDownloadServiceProvider in DownloadServiceProviders)
			{
				torrentDownloadServiceProvider.Init(_container);
			}
			Array.ForEach(ResourceProcessors, s => s.Init(_container));
			Array.ForEach(ResourceContextMenuAddins, s => s.Init(_container));

			foreach (var torrentResourceProvider in ResourceProviders)
			{
				torrentResourceProvider.Connect();
				if (opt.AppVersion != Program.CurrentVersion)
					torrentResourceProvider.UpgradeFrom(opt.AppVersion, Program.CurrentVersion);
			}
			foreach (var torrentDownloadServiceProvider in DownloadServiceProviders)
			{
				torrentDownloadServiceProvider.Connect();
				if (opt.AppVersion != Program.CurrentVersion)
					torrentDownloadServiceProvider.UpgradeFrom(opt.AppVersion, Program.CurrentVersion);
			}
			Array.ForEach(ResourceProcessors, s =>
			{
				s.Connect();
				if (opt.AppVersion != Program.CurrentVersion)
					s.UpgradeFrom(opt.AppVersion, Program.CurrentVersion);
			});
			Array.ForEach(ResourceContextMenuAddins, s =>
			{
				s.Connect();
				if (opt.AppVersion != Program.CurrentVersion)
					s.UpgradeFrom(opt.AppVersion, Program.CurrentVersion);

			});

			opt.AppVersion = Program.CurrentVersion;
		}

		internal void Disconnect()
		{
			foreach (var torrentResourceProvider in ResourceProviders)
			{
				torrentResourceProvider.Disconnect();
			}
			foreach (var torrentDownloadServiceProvider in DownloadServiceProviders)
			{
				torrentDownloadServiceProvider.Disconnect();
			}
		}
	}
}
