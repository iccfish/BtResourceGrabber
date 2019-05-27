using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BtResourceGrabber.Entities;
using BtResourceGrabber.Service;
using BtResourceGrabber.UI.Dialogs;

namespace BtResourceGrabber
{
	using System;
	using System.ComponentModel.Composition;
	using System.Diagnostics;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Windows.Forms;
	using BRG;
	using BRG.Entities;
	using BRG.Service;
	using BtResourceGrabber.UI.Dialogs;
	using BtResourceGrabber.UI.Dialogs.Messages;

	using UI;

	[Export(typeof(IResourceOperation))]
	class ResourceOperation : IResourceOperation
	{
		public static DownloadQueue DownloadQueue { get; private set; }
		public static MainForm MainForm { get; private set; }

		internal static void AttachForm(MainForm instance)
		{
			MainForm = instance;
		}

		public static void Shutdown()
		{
			DownloadQueue.Close();
		}

		/// <summary>
		/// 请求下载资源
		/// </summary>
		/// <param name="torrents"></param>
		public void AccquireDownloadTorrent(params IResourceInfo[] torrents)
		{
			var ts = torrents.Where(s => s.ResourceType == ResourceType.BitTorrent).ToArray();
			if (ts.Length == 0)
				return;



			if (DownloadQueue == null)
			{
				DownloadQueue = new DownloadQueue();
				DownloadQueue.Show();
			}
			else
			{
				if (DownloadQueue.WindowState == FormWindowState.Minimized)
				{
					DownloadQueue.WindowState = FormWindowState.Normal;
				}
				Utility.ShowWindowAsync(DownloadQueue.Handle, Utility.SW_SHOWNOACTIVATE);
			}

			DownloadQueue.AddResourceToQueue(ts);
			MainForm.ShowFloatTip(string.Format("已加入 {1} 等 {0} 个种子到下载队列。", ts.Length, ts[0].Title));

			//显示安全警告
			SecurityWarning.CheckShowSecurityWarning();
		}

		public void PrepareFullDetail(params IResourceInfo[] torrents)
		{
			if (torrents.Any(s => !s.IsHashLoaded))
			{
				using (var dlg = new WaitingDialog())
				{
					var queue = torrents.Where(s => !s.IsHashLoaded).ToArray();

					dlg.Task = Task.Factory.StartNew(() =>
					{
						foreach (var item in queue)
						{
							try
							{
								item.Provider.LoadFullDetail(item);
							}
							catch (Exception)
							{
							}
						}
					});
					dlg.ShowDialog();
				}
			}
		}

		public void CopyHash(params IResourceInfo[] torrents)
		{
			if (torrents == null || torrents.Length == 0)
				return;

			torrents = torrents.Where(s => s.ResourceType == ResourceType.BitTorrent || s.ResourceType == ResourceType.Ed2K).ToArray();

			PrepareFullDetail(torrents);

			var successItems = torrents.Where(s => s.IsHashLoaded).ToArray();
			var failedItemsCount = torrents.Length - successItems.Length;
			var successItemsCount = successItems.Length;

			try
			{
				Clipboard.SetText(successItems.Select(s => s.Hash).JoinAsString(Environment.NewLine));

				MainForm.ShowFloatTip(string.Format("复制 {0} 条特征码成功！{1}", successItemsCount, failedItemsCount > 0 ? string.Format("{0} 条资源因为无法获得信息没有复制成功。", failedItemsCount) : ""));
			}
			catch (Exception)
			{
				MainForm.Error("无法复制特征码到剪贴板。");
			}

		}

		public void CopyEd2k(params IResourceInfo[] torrents)
		{
			if (torrents == null || torrents.Length == 0)
				return;
			torrents = torrents.Where(s => s.ResourceType == ResourceType.Ed2K).ToArray();

			PrepareFullDetail(torrents);

			var successItems = torrents.Where(s => s.IsHashLoaded).ToArray();
			var failedItemsCount = torrents.Length - successItems.Length;
			var successItemsCount = successItems.Length;

			try
			{
				Clipboard.SetText(successItems.Select(BrtUtility.CreateEd2kUrl).JoinAsString(Environment.NewLine));

				MainForm.ShowFloatTip(string.Format("复制 {0} 条电驴链接成功！{1}", successItemsCount, failedItemsCount > 0 ? string.Format("{0} 条资源因为无法获得信息没有复制成功。", failedItemsCount) : ""));
			}
			catch (Exception)
			{
				MainForm.Error("无法复制电驴链接到剪贴板。");
			}

		}

		/// <summary>
		/// 复制下载链接
		/// </summary>
		/// <param name="resources"></param>
		public void CopyDownloadLink(params IResourceInfo[] resources)
		{
			if (resources == null || resources.Length == 0)
				return;
			PrepareFullDetail(resources.Where(s => !s.IsHashLoaded).ToArray());

			var text = resources.Where(s => s.IsHashLoaded).Select(BrtUtility.BuildDetailLink).Where(s => !s.IsNullOrEmpty()).JoinAsString(Environment.NewLine);

			if (string.IsNullOrEmpty(text))
			{
				MainForm.Error("未能生成下载链接，可能是搜索引擎已更改或存在意外情况。\n如果总是出现此错误，请在知识社区中向作者反馈此问题。");
				return;
			}

			try
			{
				Clipboard.SetText(text);

				MainForm.ShowFloatTip("复制下载链接成功！");
			}
			catch (Exception)
			{
				MainForm.Error("无法复制到剪贴板，请检查是否有软件正在监控剪贴板。");
			}
		}

		/// <summary>
		/// 打开下载页面
		/// </summary>
		/// <param name="resources"></param>
		public void OpenDownloadPage(params IResourceInfo[] resources)
		{
			if (resources == null || resources.Length == 0)
				return;

			var items = resources.Where(s => s.ResourceType == ResourceType.NetDisk).ToArray();

			PrepareFullDetail(items.Where(s => !s.IsHashLoaded).ToArray());
			items = items.Where(s => s.IsHashLoaded && s.NetDiskData != null).ToArray();

			if (items.Length > 5)
			{
				if (!MainForm.Question($"一次打开 {items.Length} 个下载页可能会导致您的计算机变慢，确定无论如何都要坚持不懈地一如反顾地打开咩？"))
					return;
			}

			try
			{
				foreach (var item in items)
				{
					Process.Start(item.NetDiskData.Url);

					if (!item.NetDiskData.Pwd.IsNullOrEmpty())
					{
						Clipboard.SetText(item.NetDiskData.Pwd);
						MainForm.Question($"刚才打开的下载页密码是 {item.NetDiskData.Pwd}，已经复制到剪贴板，请进入后再点击确定继续。");
					}
				}
			}
			catch (Exception)
			{
				MainForm.Error("矮油，没能打开。。。。");
			}
		}

		public void CopyTitle(params IResourceInfo[] torrents)
		{
			if (torrents == null || torrents.Length == 0)
				return;

			try
			{
				Clipboard.SetText(torrents.Select(s => s.Title).JoinAsString(Environment.NewLine));

				MainForm.ShowFloatTip(string.Format("复制 {0} 条标题成功！", torrents.Length));
			}
			catch (Exception)
			{
				MainForm.Error("无法复制标题到剪贴板。");
			}

		}

		/// <summary>
		/// 将资源标记为已下载
		/// </summary>
		/// <param name="torrents"></param>
		public void MarkDone(params IResourceInfo[] torrents)
		{
			if (torrents == null || torrents.Length == 0)
				return;
			PrepareFullDetail(torrents);

			foreach (var info in torrents)
			{
				info.ChangeDownloadedStatus(true);
			}
		}

		/// <summary>
		/// 将资源标记为已下载
		/// </summary>
		/// <param name="torrents"></param>
		public void MarkUndone(params IResourceInfo[] torrents)
		{
			if (torrents == null || torrents.Length == 0)
				return;
			PrepareFullDetail(torrents);

			foreach (var info in torrents)
			{
				info.ChangeDownloadedStatus(false);
			}
		}


		public void BaiduSearch(IResourceInfo resource)
		{
			var url = "https://www.baidu.com/s?wd=" + System.Web.HttpUtility.UrlEncode(resource.Title);

			try
			{
				Process.Start(url);
			}
			catch (Exception)
			{
			}
		}


		public void GoogleSearch(IResourceInfo resource)
		{
			var url = "https://www.google.com/#q=" + System.Web.HttpUtility.UrlEncode(resource.Title);

			try
			{
				Process.Start(url);
			}
			catch (Exception)
			{
			}
		}


		public void CopyMagnetLink(params IResourceInfo[] torrents)
		{
			if (torrents == null || torrents.Length == 0)
				return;

			torrents = torrents.Where(s => s.ResourceType == ResourceType.BitTorrent).ToArray();
			PrepareFullDetail(torrents);

			var successItems = torrents.Where(s => s.IsHashLoaded).ToArray();
			var failedItemsCount = torrents.Length - successItems.Length;
			var successItemsCount = successItems.Length;

			try
			{
				if (successItemsCount > 0)
				{
					Clipboard.SetText(successItems.Select(s => s.CreateMagnetLink(true)).JoinAsString(Environment.NewLine));


					//显示安全警告
					SecurityWarning.CheckShowSecurityWarning();
				}

				MainForm.ShowFloatTip(string.Format("复制 {0} 条磁力链成功！{1}", successItems.Length, failedItemsCount > 0 ? string.Format("{0} 条资源因为无法获得信息没有复制成功。", failedItemsCount) : ""));
			}
			catch (Exception)
			{
				MainForm.Error("无法复制磁力链到剪贴板。");
			}

		}

		/// <summary>
		/// 查看详细信息
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="resource"></param>
		public void ViewDetail(IResourceProvider provider, IResourceInfo resource)
		{
			if (provider == null || resource == null)
				return;
			Process.Start(provider.GetDetailUrl(resource));
		}

		/// <summary>
		/// 查看种子内容
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="resource"></param>
		public void ViewTorrentContents(IResourceProvider provider, IResourceInfo resource)
		{
			if (provider == null || resource == null || !provider.SupportLookupTorrentContents)
				return;
			new TorrentContentVisualizer(provider, resource).Show();
		}

		/// <summary>
		/// 设置标注
		/// </summary>
		/// <param name="maskName"></param>
		/// <param name="resource"></param>
		public void SetTorrentMask(string maskName, params IResourceInfo[] resource)
		{
			if (resource == null)
			{
				return;
			}

			var mask = string.IsNullOrEmpty(maskName) ? null : AppContext.Instance.Options.HashMarks.GetValue(maskName);
			if (mask == null && !string.IsNullOrEmpty(maskName))
				return;

			PrepareFullDetail(resource);

			foreach (var res in resource.Where(s => s.IsHashLoaded))
			{
				var ea = new TorrentMarkEventArgs(res, maskName, mask);
				if (!string.IsNullOrEmpty(maskName))
				{

					AppContext.Instance.HashMarkCollection.AddOrUpdate(res.Hash, maskName);
				}
				else if (AppContext.Instance.HashMarkCollection.ContainsKey(res.Hash))
				{
					AppContext.Instance.HashMarkCollection.Remove(res.Hash);
				}
				AppContext.Instance.OnTorrentMarked(resource, ea);
			}
		}

		/// <summary>
		/// 显示浮动通知
		/// </summary>
		/// <param name="msg"></param>
		public void ShowFloatTip(string msg)
		{
			MainForm.ShowFloatTip(msg);
		}
	}
}
