using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BtResourceGrabber.Entities;
using BtResourceGrabber.Service;

namespace BtResourceGrabber.UI.Controls
{
	using System.IO;
	using System.Text.RegularExpressions;
	using BRG;
	using BRG.Entities;

	class DownloadQueueItem : ListViewItem
	{
		QueueStatus _status;
		int _engineIndex;
		public IResourceInfo ResourceItem { get; private set; }

		public string Hash { get { return ResourceItem.Hash; } }

		public event EventHandler StatusChanged;

		/// <summary>
		/// 引发 <see cref="StatusChanged" /> 事件
		/// </summary>
		protected virtual void OnStatusChanged()
		{
			var handler = StatusChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		public QueueStatus Status
		{
			get { return _status; }
			set
			{
				if (_status == value)
					return;

				_status = value;
				OnStatusChanged();
				AppContext.UiInvoke(RefreshStatus);
			}
		}

		public event EventHandler EngineIndexChanged;


		/// <summary>
		/// 引发 <see cref="EngineIndexChanged" /> 事件
		/// </summary>
		protected virtual void OnEngineIndexChanged()
		{
			var handler = EngineIndexChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		public int EngineIndex
		{
			get { return _engineIndex; }
			set
			{
				if (value == _engineIndex)
					return;

				_engineIndex = value;
				OnEngineIndexChanged();
			}
		}

		void RefreshStatus()
		{
			ImageIndex = (int)Status;

			var desc = "";
			switch (Status)
			{
				case QueueStatus.Wait:
					desc = "等待中";
					break;
				case QueueStatus.Prepare:
					desc = "准备中";
					break;
				case QueueStatus.Running:
					desc = "正在寻找下载资源";
					break;
				case QueueStatus.Succeed:
					desc = "下载成功";
					break;
				case QueueStatus.Failed:
					desc = "下载失败，请使用磁力链下载";
					break;
				case QueueStatus.Skipped:
					desc = "下载已跳过";
					break;
				default:
					break;

			}

			SubItems[1].Text = desc;

		}

		/// <summary>
		/// 创建 <see cref="DownloadQueueItem" />  的新实例(DownloadQueueItem)
		/// </summary>
		/// <param name="resourceItem"></param>
		public DownloadQueueItem(IResourceInfo resourceItem)
			: base(new[] { resourceItem.Title.GetSubString(150), "等待中..." })
		{
			Contract.Requires(resourceItem != null);

			ResourceItem = resourceItem;
			ImageIndex = 0;
			_engineIndex = 0;
		}

		public void StartDownload(string path, Func<string, bool> checkDownloadFunc)
		{
			Status = QueueStatus.Running;

			if (!ResourceItem.IsHashLoaded)
			{
				try
				{
					ResourceItem.Provider.LoadFullDetail(ResourceItem);
				}
				catch (Exception)
				{
				}
			}
			if (!ResourceItem.IsHashLoaded)
			{
				Status = QueueStatus.Failed;
				return;
			}
			if (CheckIsDownloaded(ResourceItem.Hash, ResourceItem.Title, checkDownloadFunc))
			{
				Status = QueueStatus.Skipped;
				return;
			}

			byte[] torrentData = null;
			string engineName = null;

			if (ResourceItem.PreferDownloadProvider != null)
			{
				torrentData = DownloadUsingPreferEngine();
				engineName = ResourceItem.PreferDownloadProvider.Info.Name;
			}

			if (torrentData == null)
			{

				torrentData = GetTorrentData(ref engineName);
			}

			if (torrentData != null)
			{
				var filePath = GetLocalPath(path, ResourceItem.Title);
				File.WriteAllBytes(filePath, torrentData);

				DownloadedFilePath = filePath;
				Status = QueueStatus.Succeed;

				AppContext.Instance.OnResouceDownloaded(this, new ResourceEventArgs(ResourceItem));
			}
			else
			{
				Status = QueueStatus.Failed;
			}
		}

		/// <summary>
		/// 已保存的文件路径
		/// </summary>
		public string DownloadedFilePath { get; private set; }

		byte[] DownloadUsingPreferEngine()
		{
			var engine = ResourceItem.PreferDownloadProvider;
			if (engine.Disabled)
				return null;

			try
			{
				var data = engine.Download(ResourceItem);
				if (data != null)
					AppContext.Instance.Statistics.UpdateRunningStatistics(false, engine, 1, 0, 1, 0);
				else
				{
					//AppContext.Instance.Statistics.UpdateRunningStatistics(false, engine, 0, 1, 0, 0);
				}
				return data;
			}
			catch (Exception)
			{
				AppContext.Instance.Statistics.UpdateRunningStatistics(false, engine, 0, 0, 0, 1);
			}
			return null;
		}

		byte[] GetTorrentData(ref string engineName)
		{
			if (!ResourceItem.IsHashLoaded)
				return null;

			var engines = ServiceManager.Instance.DownloadServiceProviders.ToArray();
			for (var i = 0; i < engines.Length; i++)
			{
				EngineIndex = i + 1;

				if ((ResourceItem.PreferDownloadProvider != null && ResourceItem.PreferDownloadProvider == engines[i]) || engines[i].Disabled)
					continue;

				byte[] torrentData = null;
				AppContext.Instance.Statistics.UpdateRunningStatistics(false, engines[i], 1, 0, 0, 0);
				try
				{
					torrentData = engines[i].Download(ResourceItem);

					if (torrentData == null)
					{
						AppContext.Instance.Statistics.UpdateRunningStatistics(false, engines[i], 0, 1, 0, 0);
					}
					else AppContext.Instance.Statistics.UpdateRunningStatistics(false, engines[i], 0, 0, 1, 0);

				}
				catch (Exception)
				{
					AppContext.Instance.Statistics.UpdateRunningStatistics(false, engines[i], 0, 0, 0, 1);
				}

				if (torrentData == null)
					continue;

				engineName = engines[i].Info.Name;
				return torrentData;
			}

			return null;
		}

		static bool _confirmedAll = false;
		static bool? _continueRedownload = null;

		bool CheckIsDownloaded(string hash, string title, Func<string, bool> checkDownloadFunc)
		{
			if (checkDownloadFunc != null && checkDownloadFunc(hash))
			{
				return true;
			}

			if (!AppContext.Instance.DownloadHistory.ContainsKey(hash))
				return false;

			if (_continueRedownload != null)
				return _continueRedownload.Value;

			var cont = false;
			ListView.Invoke(new Action(() =>
			{
				cont = !ResourceOperation.DownloadQueue.Question(string.Format("种子【{0}】已经下载过了，确定要重新下载吗？", title));
				if (!_confirmedAll)
				{
					_confirmedAll = true;
					if (ResourceOperation.DownloadQueue.Question("下次都进行此操作吗？"))
						_continueRedownload = cont;
				}
			}));
			return cont;
		}

		static Regex _fileNameReplaceReg = new Regex("[" + Regex.Escape(Path.GetInvalidFileNameChars().JoinAsString("")) + "]", RegexOptions.IgnoreCase);

		static string GetLocalPath(string folder, string filename)
		{
			Contract.Requires(folder != null);
			Contract.Requires(filename != null);
			filename = _fileNameReplaceReg.Replace(filename, "_");

			string path;
			var index = 0;
			do
			{
				path = Path.Combine(folder, String.Format("{0}{1}.torrent", filename, (index > 0 ? " (" + index + ")" : "")));
				index++;
			} while (File.Exists(path));

			return path;
		}
	}
}
