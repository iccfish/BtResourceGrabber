namespace BRG.Engines.Entities
{
	using System;
	using System.Collections.Generic;
	using System.Text.RegularExpressions;
	using System.Web;
	using BRG;
	using BRG.Entities;
	using BRG.Service;

	public class ResourceInfo : List<IFileNode>, IResourceInfo
	{
		string _downloadSize;
		long _downloadSizeCalcauted;
		long? _downloadSizeValue;
		string _hash;
		PreviewInfo _previewInfo;
		IResourceInfo[] _subResources;
		PreviewType _supportPreivewType = PreviewType.None;
		string _title;
		string _torrentSize;
		string _updateTimeDesc;

		protected virtual void OnDownloadedChanged()
		{
			DownloadedChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// 引发 <see cref="SubResourceLoaded"/> 事件
		/// </summary>

		protected virtual void OnSubResourceLoaded() => SubResourceLoaded?.Invoke(this, EventArgs.Empty);

		/// <summary>
		/// 比较当前对象和同一类型的另一对象。
		/// </summary>
		/// <returns>
		/// 一个值，指示要比较的对象的相对顺序。返回值的含义如下：值含义小于零此对象小于 <paramref name="other"/> 参数。零此对象等于 <paramref name="other"/>。大于零此对象大于 <paramref name="other"/>。
		/// </returns>
		/// <param name="other">与此对象进行比较的对象。</param>
		public int CompareTo(IResourceInfo other)
		{
			if (PreferDownloadProvider == null ^ other.PreferDownloadProvider == null)
			{
				return PreferDownloadProvider == null ? 1 : -1;
			}
			if (IsHashLoaded ^ other.IsHashLoaded)
				return other.IsHashLoaded ? 1 : -1;
			if (DownloadSizeValue == null ^ other.DownloadSizeValue == null)
				return other.DownloadSizeValue == null ? -1 : 1;

			return 0;
		}

		#region Implementation of ITorrentResourceInfo

		/// <summary>
		/// Hash
		/// </summary>
		public string Hash
		{
			get { return _hash; }
			set
			{
				_hash = value?.Trim().ToUpper();
				if (!_hash.IsNullOrEmpty() && !Regex.IsMatch(_hash, @"^[A-Z\d]{30,}$"))
					throw new ArgumentOutOfRangeException("hash invalid.");

				if (!_hash.IsNullOrEmpty())
				{
					OnDetailLoaded();
				}
			}
		}

		/// <summary>
		/// 标题
		/// </summary>
		public string Title
		{
			get { return _title; }
			set
			{
				if (string.IsNullOrEmpty(value))
					return;
				_title = BrtUtility.RemoveHtmlChars(value);
			}
		}

		/// <summary>
		/// 种子大小
		/// </summary>
		public string TorrentSize
		{
			get { return _torrentSize; }
			set
			{
				if (string.IsNullOrEmpty(value))
					return;
				_torrentSize = BrtUtility.ClearString(value);
			}
		}

		/// <summary>
		/// 下载的文件大小
		/// </summary>
		public string DownloadSize
		{
			get { return _downloadSize; }
			set
			{
				_downloadSize = BrtUtility.ClearString(value);
				_downloadSizeCalcauted = EngineUtility.ToSize(_downloadSize);
			}
		}

		/// <summary>
		/// 计算出来的估计大小
		/// </summary>
		public long DownloadSizeCalcauted
		{
			get { return _downloadSizeCalcauted; }
			set { _downloadSizeCalcauted = value; }
		}

		/// <summary>
		/// 种子文件大小的数值
		/// </summary>
		public long? TorrentSizeValue { get; set; }

		/// <summary>
		/// 下载大小的数值
		/// </summary>
		public long? DownloadSizeValue
		{
			get { return _downloadSizeValue; }
			set
			{
				_downloadSizeValue = value;
				if (value > 0)
					_downloadSizeCalcauted = value ?? 0L;
			}
		}

		/// <summary>
		/// 描述
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime? UpdateTime { get; set; }

		/// <summary>
		/// 更新时间
		/// </summary>
		public string UpdateTimeDesc
		{
			get { return _updateTimeDesc ?? string.Empty; }
			set { _updateTimeDesc = BrtUtility.RemoveHtmlChars(value); }
		}

		/// <summary>
		/// 相关数据
		/// </summary>
		public object SiteData { get; set; }

		/// <summary>
		/// 生成磁力链
		/// </summary>
		/// <param name="includeDn">是否包含下载名</param>
		/// <returns></returns>
		public string CreateMagnetLink(bool includeDn)
		{
			return $"magnet:?xt=urn:btih:{Hash}{(includeDn ? "&dn=" + HttpUtility.UrlPathEncode(Title) : "")}";
		}

		/// <summary>
		/// 数据源
		/// </summary>
		public IResourceProvider Provider
		{
			get; set;
		}

		/// <summary>
		/// 比较偏爱的资源下载类
		/// </summary>
		public ITorrentDownloadServiceProvider PreferDownloadProvider { get; set; }

		/// <summary>
		/// 是否已经加载完全
		/// </summary>
		public bool IsHashLoaded
		{
			get
			{
				//如果不是电驴或BT则不要求装载Hash
				if (ResourceType == ResourceType.Ed2K || ResourceType == ResourceType.BitTorrent)
					return !string.IsNullOrEmpty(Hash);

				if (ResourceType == ResourceType.NetDisk)
					return NetDiskData != null;

				return true;
			}
		}

		/// <summary>
		/// 文件数
		/// </summary>
		public int? FileCount { get; set; }

		/// <summary>
		/// 内容加载中的错误信息
		/// </summary>
		public string ContentLoadErrMessage { get; set; }

		public event EventHandler DetailLoaded;

		/// <summary>
		/// 资源类型
		/// </summary>
		public ResourceType ResourceType { get; set; } = ResourceType.BitTorrent;

		/// <summary>
		/// 附加数据
		/// </summary>
		public object Data { get; set; }

		/// <summary>
		/// 支持的预览类型
		/// </summary>
		public PreviewType SupportPreivewType
		{
			get { return _supportPreivewType; }
			set
			{
				if (_supportPreivewType == value)
					return;

				_supportPreivewType = value;
				OnPreviewTypeChanged();
			}
		}

		/// <summary>
		/// 预览信息
		/// </summary>
		public PreviewInfo PreviewInfo
		{
			get { return _previewInfo; }
			set
			{
				if (_previewInfo == value)
					return;

				_previewInfo = value;
				if (_previewInfo != null)
					OnPreviewInfoLoaded();
			}
		}

		public event EventHandler PreviewInfoLoaded;
		public event EventHandler PreviewTypeChanged;

		/// <summary>
		/// 验证状态
		/// </summary>
		public VerifyState VerifyState { get; private set; } = VerifyState.Unknown;

		/// <summary>
		/// 举报人数
		/// </summary>
		public int ReportNum { get; private set; }

		/// <summary>
		/// 验证状态变化
		/// </summary>
		public event EventHandler VerifyStateChanged;

		/// <summary>
		/// 引发 <see cref="VerifyStateChanged" /> 事件
		/// </summary>
		protected virtual void OnVerifyStateChanged()
		{
			var handler = VerifyStateChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 变更举报状态
		/// </summary>
		/// <param name="state"></param>
		/// <param name="reportNum"></param>
		public void ChangeVerifyState(VerifyState state, int reportNum)
		{
			if (state == VerifyState)
				return;

			ReportNum = reportNum;
			VerifyState = state;
			OnVerifyStateChanged();
		}

		/// <summary>
		/// 是否已经下载
		/// </summary>
		public bool Downloaded { get; private set; } = false;

		/// <summary>
		/// <see cref="ITorrentResourceInfo.Downloaded"/> 属性发生变化
		/// </summary>
		public event EventHandler DownloadedChanged;

		/// <summary>
		/// 标记已下载状态
		/// </summary>
		/// <param name="downloaded"></param>
		public void ChangeDownloadedStatus(bool downloaded)
		{
			if (downloaded == Downloaded)
				return;

			this.Downloaded = downloaded;
			OnDownloadedChanged();
		}

		/// <summary>
		/// 获得是否还有子资源
		/// </summary>
		public bool HasSubResources { get; set; }

		/// <summary>
		/// 获得子资源
		/// </summary>
		public IResourceInfo[] SubResources
		{
			get { return _subResources; }
			set
			{
				if (value == null || _subResources == value)
					return;

				_subResources = value;
				OnSubResourceLoaded();
			}
		}

		/// <summary>
		/// 子资源已加载
		/// </summary>
		public event EventHandler SubResourceLoaded;

		/// <summary>
		/// 获得网盘数据
		/// </summary>
		public INetDiskData NetDiskData { get; set; }

		/// <summary>
		/// 获得匹配关键字的度量值
		/// </summary>
		public int MatchWeight
		{
			get; set;
		}

		/// <summary>
		/// 引发 <see cref="PreviewTypeChanged" /> 事件
		/// </summary>
		protected virtual void OnPreviewTypeChanged()
		{
			var handler = PreviewTypeChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 引发 <see cref="PreviewInfoLoaded" /> 事件
		/// </summary>
		protected virtual void OnPreviewInfoLoaded()
		{
			var handler = PreviewInfoLoaded;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 引发 <see cref="DetailLoaded" /> 事件
		/// </summary>
		protected virtual void OnDetailLoaded()
		{
			var handler = DetailLoaded;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		#endregion

	}
}
