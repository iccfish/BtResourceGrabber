using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BRG.Entities
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.FishExtension;
	using System.Reflection;
	using BRG.Service;
	using Newtonsoft.Json;
	using SmartAssembly.Attributes;

	[DoNotObfuscate]
	public class Options : INotifyPropertyChanged
	{
		bool _strictFilter;
		SortType _sortType;
		int _loadPages;
		string _activeSearchProvider;
		List<string> _searchTermsHistory;
		ProxyItem _commonProxy;
		ProxyItem _gfwProxy;
		bool _useSameProxy;
		bool _enableProxy;
		string _lastSearchKey;
		int _pageSize;
		int _sortDirection;
		string _autoMarkDownloadedTorrent;
		Dictionary<string, HashMark> _hashMarks;
		bool _enableAutoMark;
		bool _firstRun;
		bool _useGfwProxyForAll;
		bool _proxyWarningShown;
		bool _dismissSecurityWarning;
		bool _multilineEngineTab = true;
		bool _usingFloatTip;
		HashSet<string> _disabledSearchProviders;
		HashSet<string> _disableDownloadProviders;
		int _maxRetryCount = 3;
		int _networkTimeout;
		bool _enableSearchDirective;
		string _newFeatureVersion;
		bool _logSearchHistory;
		bool _logDownloadHistory;
		HashSet<string> _engineStandalone;
		bool _enablePreviewIfPossible = true;

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}

		public Options()
		{
			UseSameProxy = true;
			PageSize = 50;
			FirstRun = true;
			_disableDownloadProviders = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			_disabledSearchProviders = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			_networkTimeout = 30;
			EnableSearchDirective = true;
			LogSearchHistory = true;
			LogDownloadHistory = true;
			EngineProperties = new Dictionary<string, EnginePropertyCollection>(StringComparer.OrdinalIgnoreCase);
		}

		/// <summary>
		/// 引擎设置
		/// </summary>
		public Dictionary<string, EnginePropertyCollection> EngineProperties { get; set; }

		public EnginePropertyCollection GetEnginePropertyCollection<T>(T engine) where T : IServiceBase
		{
			var type = typeof(T);
			var name = (type.HasInterface<IResourceProvider>() ? "s" : "d") + "_" + engine.Info.Name;

			lock (EngineProperties)
			{
				if (EngineProperties.ContainsKey(name))
					return EngineProperties[name];

				var value = new EnginePropertyCollection();
				EngineProperties.Add(name, value);
				return value;
			}
		}

		public bool FirstRun
		{
			get { return _firstRun; }
			set
			{
				if (value.Equals(_firstRun))
					return;
				_firstRun = value;
				OnPropertyChanged(nameof(FirstRun));
			}
		}

		public string LastSearchKey
		{
			get { return _lastSearchKey; }
			set
			{
				if (value == _lastSearchKey) return;
				_lastSearchKey = value;
				OnPropertyChanged(nameof(LastSearchKey));
			}
		}

		public List<string> SearchTermsHistory
		{
			get { return _searchTermsHistory; }
			set
			{
				if (Equals(value, _searchTermsHistory)) return;
				_searchTermsHistory = value;
				OnPropertyChanged(nameof(SearchTermsHistory));
			}
		}

		public string ActiveSearchProvider
		{
			get { return _activeSearchProvider; }
			set
			{
				if (value == _activeSearchProvider) return;
				_activeSearchProvider = value;
				OnPropertyChanged(nameof(ActiveSearchProvider));
			}
		}

		public int LoadPages
		{
			get { return _loadPages; }
			set
			{
				if (value == _loadPages) return;
				_loadPages = value;
				OnPropertyChanged(nameof(LoadPages));
			}
		}

		public SortType SortType
		{
			get { return _sortType; }
			set
			{
				if (value == _sortType) return;
				_sortType = value;
				OnPropertyChanged(nameof(SortType));
			}
		}

		public bool StrictFilter
		{
			get { return _strictFilter; }
			set
			{
				if (value.Equals(_strictFilter)) return;
				_strictFilter = value;
				OnPropertyChanged(nameof(StrictFilter));
			}
		}

		public ProxyItem CommonProxy
		{
			get { return _commonProxy; }
			set
			{
				if (Equals(value, _commonProxy)) return;
				_commonProxy = value;
				OnPropertyChanged(nameof(CommonProxy));
			}
		}

		public ProxyItem GfwProxy
		{
			get { return _gfwProxy; }
			set
			{
				if (Equals(value, _gfwProxy)) return;
				_gfwProxy = value;
				OnPropertyChanged(nameof(GfwProxy));
			}
		}

		/// <summary>
		/// 使用相同的代琿
		/// </summary>
		public bool UseSameProxy
		{
			get { return _useSameProxy; }
			set
			{
				if (value.Equals(_useSameProxy)) return;
				_useSameProxy = value;
				OnPropertyChanged(nameof(UseSameProxy));
			}
		}

		public bool ProxyWarningShown
		{
			get { return _proxyWarningShown; }
			set
			{
				if (value.Equals(_proxyWarningShown))
					return;
				_proxyWarningShown = value;
				OnPropertyChanged(nameof(ProxyWarningShown));
			}
		}

		/// <summary>
		/// 启用代理服务噿
		/// </summary>
		public bool EnableProxy
		{
			get { return _enableProxy; }
			set
			{
				if (value.Equals(_enableProxy)) return;
				_enableProxy = value;
				OnPropertyChanged(nameof(EnableProxy));
			}
		}

		/// <summary>
		/// 是否使所有的引擎都使用GFW代理
		/// </summary>
		public bool UseGfwProxyForAll
		{
			get { return _useGfwProxyForAll; }
			set
			{
				if (value.Equals(_useGfwProxyForAll))
					return;
				_useGfwProxyForAll = value;
				OnPropertyChanged(nameof(UseGfwProxyForAll));
			}
		}

		/// <summary>
		/// 每页加载记录敿
		/// </summary>
		[JsonIgnore]
		public int PageSize
		{
			get { return _pageSize; }
			set
			{
				if (value == _pageSize) return;
				_pageSize = value;
				OnPropertyChanged(nameof(PageSize));
			}
		}

		public int SortDirection
		{
			get { return _sortDirection; }
			set
			{
				if (value == _sortDirection) return;
				_sortDirection = value;
				OnPropertyChanged(nameof(SortDirection));
			}
		}

		public bool EnableAutoMark
		{
			get { return _enableAutoMark; }
			set
			{
				if (value.Equals(_enableAutoMark)) return;
				_enableAutoMark = value;
				OnPropertyChanged(nameof(EnableAutoMark));
			}
		}

		/// <summary>
		/// 获得或设置自动标注已经成功下载的资源
		/// </summary>
		public string AutoMarkDownloadedTorrent
		{
			get { return _autoMarkDownloadedTorrent; }
			set
			{
				if (Equals(value, _autoMarkDownloadedTorrent)) return;
				_autoMarkDownloadedTorrent = value;
				OnPropertyChanged(nameof(AutoMarkDownloadedTorrent));
			}
		}

		/// <summary>
		/// 标注
		/// </summary>
		public Dictionary<string, HashMark> HashMarks
		{
			get { return _hashMarks ?? (_hashMarks = new Dictionary<string, HashMark>()); }
			set
			{
				if (Equals(value, _hashMarks)) return;
				_hashMarks = value;
				OnPropertyChanged(nameof(HashMarks));
			}
		}

		/// <summary>
		/// 是否禁止安全警告
		/// </summary>
		public bool DismissSecurityWarning
		{
			get { return _dismissSecurityWarning; }
			set
			{
				if (value.Equals(_dismissSecurityWarning)) return;
				_dismissSecurityWarning = value;
				OnPropertyChanged(nameof(DismissSecurityWarning));
			}

		}

		/// <summary>
		/// 是否允许多行标签
		/// </summary>
		public bool MultilineEngineTab
		{
			get { return _multilineEngineTab; }
			set
			{
				if (value.Equals(_multilineEngineTab))
					return;
				_multilineEngineTab = value;
				OnPropertyChanged(nameof(MultilineEngineTab));
			}
		}

		/// <summary>
		/// 使用浮窗通知而不是气泡通知
		/// </summary>
		public bool UsingFloatTip
		{
			get { return _usingFloatTip; }
			set
			{
				if (value.Equals(_usingFloatTip))
					return;
				_usingFloatTip = value;
				OnPropertyChanged(nameof(UsingFloatTip));
			}
		}

		public HashSet<string> DisabledSearchProviders
		{
			get { return _disabledSearchProviders; }
			set
			{
				if (Equals(value, _disabledSearchProviders)) return;
				_disabledSearchProviders = value;
				OnPropertyChanged(nameof(DisabledSearchProviders));
			}
		}

		public HashSet<string> DisableDownloadProviders
		{
			get { return _disableDownloadProviders; }
			set
			{
				if (Equals(value, _disableDownloadProviders)) return;
				_disableDownloadProviders = value;
				OnPropertyChanged(nameof(DisableDownloadProviders));
			}
		}

		public int MaxRetryCount
		{
			get { return _maxRetryCount; }
			set
			{
				if (value == _maxRetryCount) return;
				_maxRetryCount = value;
				OnPropertyChanged(nameof(MaxRetryCount));
			}
		}

		/// <summary>
		/// 获得或设置网络超旿
		/// </summary>
		public int NetworkTimeout
		{
			get { return _networkTimeout; }
			set
			{
				if (value == _networkTimeout) return;
				_networkTimeout = Math.Max(5, value);
				OnPropertyChanged(nameof(NetworkTimeout));
			}
		}

		/// <summary>
		/// 显示新版本功能的版本
		/// </summary>
		public string NewFeatureVersion
		{
			get { return _newFeatureVersion; }
			set
			{
				if (value == _newFeatureVersion)
					return;
				_newFeatureVersion = value;
				OnPropertyChanged(nameof(NewFeatureVersion));
			}
		}

		/// <summary>
		/// 获得或设置是否允许高级过滤语泿
		/// </summary>
		public bool EnableSearchDirective
		{
			get { return _enableSearchDirective; }
			set
			{
				if (value == _enableSearchDirective) return;
				_enableSearchDirective = value;
				OnPropertyChanged(nameof(EnableSearchDirective));
			}
		}

		/// <summary>
		/// 是否记录搜索历史
		/// </summary>
		public bool LogSearchHistory
		{
			get { return _logSearchHistory; }
			set
			{
				if (value == _logSearchHistory) return;
				_logSearchHistory = value;
				OnPropertyChanged(nameof(LogSearchHistory));
			}
		}

		/// <summary>
		/// 是否记录下载历史
		/// </summary>
		public bool LogDownloadHistory
		{
			get { return _logDownloadHistory; }
			set
			{
				if (value == _logDownloadHistory) return;
				_logDownloadHistory = value;
				OnPropertyChanged(nameof(LogDownloadHistory));
			}
		}

		public HashSet<string> EngineStandalone
		{
			get { return _engineStandalone; }
			set
			{
				if (Equals(value, _engineStandalone)) return;
				_engineStandalone = value;
				OnPropertyChanged(nameof(EngineStandalone));
			}
		}

		/// <summary>
		/// 如果可能的话启用预览
		/// </summary>
		public bool EnablePreviewIfPossible
		{
			get { return _enablePreviewIfPossible; }
			set
			{
				if (value == _enablePreviewIfPossible)
					return;
				_enablePreviewIfPossible = value;
				OnPropertyChanged(nameof(EnablePreviewIfPossible));
			}
		}

		string _appVersion;

		/// <summary>
		/// 获得或设置当前运行的程序版本
		/// </summary>
		public string AppVersion
		{
			get { return _appVersion; }
			set
			{
				if (value == _appVersion)
					return;
				_appVersion = value;
				OnPropertyChanged(nameof(AppVersion));
			}
		}

		FilterRuleCollection _ruleCollection = new FilterRuleCollection();

		/// <summary>
		/// 过滤规则列表
		/// </summary>
		public FilterRuleCollection RuleCollection
		{
			get { return _ruleCollection ?? (_ruleCollection = new FilterRuleCollection()); }
			set
			{
				if (Equals(value, _ruleCollection)) return;
				_ruleCollection = value;
				OnPropertyChanged(nameof(RuleCollection));
			}
		}

		RssRuleCollection _rssRuleCollection = new RssRuleCollection();
		/// <summary>
		/// 订阅的规则列表
		/// </summary>
		public RssRuleCollection RssRuleCollection
		{
			get { return _rssRuleCollection; }
			set
			{
				if (Equals(value, _rssRuleCollection)) return;
				_rssRuleCollection = value;
				OnPropertyChanged(nameof(RssRuleCollection));
			}
		}
	}
}
