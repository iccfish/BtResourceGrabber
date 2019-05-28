using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BtResourceGrabber.Service;
using BtResourceGrabber.UI.Controls;

namespace BtResourceGrabber.UI.Dialogs
{
	using System.Diagnostics;
	using System.Diagnostics.Contracts;
	using BRG;
	using BRG.Entities;
	using BtResourceGrabber.UI.Dialogs.ConfigUi;

	partial class DownloadQueue : FunctionalForm
	{
		HashSet<string> _queueHash = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
		HashSet<string> _downloadedHashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
		string _downloadTarget;

		public DownloadQueue()
		{
			InitializeComponent();
			InitUI();
			InitContextMenu();

			FormClosing += (s, e) =>
			{
				if (Program.IsShutodown)
					return;

				e.Cancel = true;

				if (queue.Items.Cast<DownloadQueueItem>().Any(x => x.Status == QueueStatus.Wait || x.Status == QueueStatus.Running))
				{
					WindowState = FormWindowState.Minimized;
				}
				else
				{
					Hide();
				}
			};
			InitWorker();
		}

		void InitUI()
		{
			var targetImg = new[]
							{
								Properties.Resources.clock_16,
								Properties.Resources.stock_right,
								Properties.Resources.tick_16,
								Properties.Resources.delete_16,
								Properties.Resources.refresh,
								Properties.Resources.Block
							};
			il.Images.AddRange(targetImg.Select(Utility.Get24PxImageFrom16PxImg).ToArray());
			RefreshMarkMenu();
			AppContext.Instance.RequestRefreshMarkCollection += (s, e) => RefreshMarkMenu();
			tsMarkRes.DropDownOpening += tsMarkRes_DropDownOpening;
			mMarkOption.Click += (s, e) =>
			{
				using (var dlg = new ConfigCenter())
				{
					dlg.SelectedConfig = dlg.FindConfigUI<MarkOption>().First();
					dlg.ShowDialog();
				}
			};
			mMarkNone.Click += SetMarkHandler;
			stProgressCurrent.Maximum = ServiceManager.Instance.DownloadServiceProviders.Count();
			stProgressCurrent.Visible = false;
			stDownloadProgressTotal.Text = "0/0";
			stStatus.Text = "当前没有任务等待下载";
			stStatus.Image = Properties.Resources.tick_16;

			//任务操作
			tsRemoveAll.Click += (s, e) => RemoveTasks(_ => true);
			tsRemoveFail.Click += (s, e) => RemoveTasks(_ => _.Status == QueueStatus.Failed);
			tsRemoveSucc.Click += (s, e) => RemoveTasks(_ => _.Status == QueueStatus.Succeed || _.Status == QueueStatus.Skipped);
			tsCopyMagnet.Click += (s, e) =>
			{
				var items = queue.SelectedItems.Cast<DownloadQueueItem>().Select(x => x.ResourceItem).ToArray();
				AppContext.Instance.ResourceOperation.CopyMagnetLink(items);
			};
			queue.MouseDoubleClick += (s, e) =>
			{
				var item = queue.SelectedItems.Cast<DownloadQueueItem>().FirstOrDefault();
				if (item == null)
					return;
				if (item.Status == QueueStatus.Succeed)
				{
					//尝试打开
					try
					{
						Process.Start(item.DownloadedFilePath);
					}
					catch (Exception)
					{

					}
				}
				else if (item.Status == QueueStatus.Failed)
				{
					lock (_downloadQueue)
					{
						item.Status = QueueStatus.Wait;
						_downloadQueue.Enqueue(item.ResourceItem);
					}
				}
			};
		}

		void RemoveTasks(Func<DownloadQueueItem, bool> predicate)
		{
			Contract.Requires(predicate != null);

			using (queue.CreateBatchOperationDispatcher())
			{
				lock (_downloadQueue)
				{
					var queuebak = _downloadQueue.MapToHashSet();
					_downloadQueue.Clear();
					foreach (var item in queue.Items.Cast<DownloadQueueItem>().ToArray())
					{
						if (item.Status == QueueStatus.Running)
							continue;

						if (predicate(item))
						{
							queue.Items.Remove(item);
							if (queuebak.Contains(item.ResourceItem))
								queuebak.Remove(item.ResourceItem);
						}
					}
					_downloadQueue.EnqueueMany(queuebak);

				}
			}
		}

		public IResourceInfo SelectedResouce
		{
			get
			{
				return queue.SelectedItems.OfType<DownloadQueueItem>().FirstOrDefault().SelectValue(s => s.ResourceItem);
			}
		}

		void tsMarkRes_DropDownOpening(object sender, EventArgs e)
		{
			//仅选择第一个
			var res = SelectedResouce;
			if (res == null)
			{
				return;
			}

			var curType = AppContext.Instance.GetResourceMarkName(SelectedResouce);
			tsMarkRes.DropDownItems.OfType<ToolStripMenuItem>().ForEach(_ => _.Checked = false);
			if (string.IsNullOrEmpty(curType))
			{
				mMarkNone.Checked = true;
			}
			else
			{
				var item = tsMarkRes.DropDownItems.OfType<ToolStripMenuItem>().FirstOrDefault(_ => _.Tag != null && _.Text == curType);
				if (item != null)
					item.Checked = true;
			}
		}

		void RefreshMarkMenu()
		{
			var keepItems = tsMarkRes.DropDownItems.Cast<ToolStripItem>().Take(2).ToArray();
			var afterItems = tsMarkRes.DropDownItems.Cast<ToolStripItem>().Skip(tsMarkRes.DropDownItems.Count - 2).ToArray();

			tsMarkRes.DropDownItems.Clear();
			tsMarkRes.DropDownItems.AddRange(keepItems);

			var markItems = AppContext.Instance.Options.HashMarks.Select(s => (ToolStripItem)new ToolStripMenuItem(s.Key) { ForeColor = s.Value.Color, BackColor = s.Value.BackColor, Tag = s }).ToArray();
			tsMarkRes.DropDownItems.AddRange(markItems);
			foreach (var toolStripItem in markItems)
			{
				toolStripItem.Click += SetMarkHandler;
			}

			if (!markItems.Any())
			{
				var emptyItem = new ToolStripMenuItem("(没有已添加的标记...)");
				emptyItem.Enabled = false;
				tsMarkRes.DropDownItems.Add(emptyItem);
			}

			tsMarkRes.DropDownItems.AddRange(afterItems);
		}

		void SetMarkHandler(object sender, EventArgs e)
		{
			var menu = sender as ToolStripMenuItem;
			var mark = menu.Tag == null ? null : menu.Text;

			var selectedResources = queue.SelectedItems.OfType<DownloadQueueItem>().Select(s => s.ResourceItem).ToArray();
			foreach (var res in selectedResources)
			{
				AppContext.Instance.ResourceOperation.SetTorrentMask(mark, res);
			}
		}

		public void AddResourceToQueue(params IResourceInfo[] resources)
		{
			if (resources == null || resources.Length == 0)
				return;

			if (string.IsNullOrEmpty(_downloadTarget))
			{
				_saveTorrent.FileName = resources[0].Title + ".torrent";
				if (_saveTorrent.ShowDialog() != DialogResult.OK)
					return;
				_downloadTarget = Path.GetDirectoryName(_saveTorrent.FileName);
			}

			var items = resources.Where(s => !_queueHash.Contains(s.Hash)).Select(s =>
			{
				var item = new DownloadQueueItem(s);
				item.StatusChanged += (x, y) =>
				{
					var si = item;
					if (si.Status == QueueStatus.Succeed || si.Status == QueueStatus.Failed || si.Status == QueueStatus.Skipped)
					{
						//成功
						RefreshTaskQueueStatus(si);
					}
					else if (si.Status == QueueStatus.Running)
					{
						RefreshTaskProgress(si);
					}
				};
				item.EngineIndexChanged += (x, y) =>
				{
					this.Invoke(() =>
					{
						stProgressCurrent.Value = (x as DownloadQueueItem).EngineIndex;
					});
				};
				return (ListViewItem)item;
			}).ToArray();
			queue.Items.AddRange(items);
			lock (_downloadQueue)
			{
				_downloadQueue.EnqueueMany(resources);
			}
		}

		void RefreshTaskProgress(DownloadQueueItem item)
		{
			if (InvokeRequired)
			{
				Invoke(new Action<DownloadQueueItem>(RefreshTaskProgress), item);
				return;
			}

			stStatus.Text = string.Format("开始下载 【{0}】 ...", item.ResourceItem.Title.GetSubString(40));
			stProgressCurrent.Visible = true;
			stStatus.Image = Properties.Resources._16px_loading_1;

			var total = queue.Items.Count;
			var processed = total - _downloadQueue.Count;
			if (stProgressTotal.Value > total)
				stProgressTotal.Value = 0;
			stProgressTotal.Maximum = total;
			stProgressTotal.Value = processed;
			stDownloadProgressTotal.Text = string.Format("{0}/{1}", processed, total);
		}

		void RefreshTaskQueueStatus(DownloadQueueItem item)
		{
			if (InvokeRequired)
			{
				Invoke(new Action<DownloadQueueItem>(RefreshTaskQueueStatus), item);
				return;
			}

			if (item.Status == QueueStatus.Succeed)
			{
				_downloadedHashSet.SafeAdd(item.Hash);

				var mstTitle = "种子下载成功";
				var mstContent = string.Format("种子【{0}】下载成功！", item.ResourceItem.Title);

				if (AppContext.Instance.Options.UsingFloatTip)
					ResourceOperation.MainForm.ShowFloatTip(mstTitle + "\n" + mstContent);
				else
					ni.ShowBalloonTip(3000, mstTitle, mstContent, ToolTipIcon.Info);

				if (AppContext.Instance.Options.EnableAutoMark)
				{
					AppContext.Instance.ResourceOperation.SetTorrentMask(AppContext.Instance.Options.AutoMarkDownloadedTorrent, item.ResourceItem);
				}
				AppContext.Instance.ResourceOperation.MarkDone(item.ResourceItem);
			}
			else if (item.Status == QueueStatus.Skipped)
			{
				//下载跳过
				if (AppContext.Instance.Options.EnableAutoMark)
				{
					AppContext.Instance.ResourceOperation.SetTorrentMask(AppContext.Instance.Options.AutoMarkDownloadedTorrent, item.ResourceItem);
				}
				AppContext.Instance.DownloadHistory.AddOrUpdate(item.ResourceItem.Hash, new HistoryItem()
				{
					Title = item.ResourceItem.Title,
					DownloadTime = DateTime.Now
				});
			}
			else
			{
				var mstTitle = "种子下载失败";
				var mstContent = string.Format("种子【{0}】下载失败！请使用磁力链来获得资源地址！", item.ResourceItem.Title);

				if (AppContext.Instance.Options.UsingFloatTip)
					ResourceOperation.MainForm.ShowFloatTip(mstTitle + "\n" + mstContent);
				else
					ni.ShowBalloonTip(3000, mstTitle, mstContent, ToolTipIcon.Warning);
			}
			stProgressCurrent.Visible = false;
			stStatus.Text = "当前没有任务等待下载";
			stStatus.Image = Properties.Resources.tick_16;
		}


		#region 任務管理

		Queue<IResourceInfo> _downloadQueue;
		IResourceInfo _downloadTask;

		void InitWorker()
		{
			_downloadQueue = new Queue<IResourceInfo>();
			_downloadTask = null;
			Task.Factory.StartNew(TaskDownloadQueue, TaskCreationOptions.LongRunning);
		}

		void TaskDownloadQueue()
		{
			Thread.CurrentThread.Name = "TaskDownloadThread";
			while (!IsDisposed)
			{
				while (_downloadQueue.Count > 0)
				{
					//查找下一个任务
					lock (_downloadQueue)
					{
						_downloadTask = _downloadQueue.Count == 0 ? null : _downloadQueue.Dequeue();
						if (_downloadTask == null)
							continue;
					}
					DownloadQueueItem lvitem = null;
					this.Invoke(() =>
					{
						lvitem = queue.Items.Cast<DownloadQueueItem>().FirstOrDefault(x => x.ResourceItem == _downloadTask && x.Status == QueueStatus.Wait);
						if (lvitem != null)
							lvitem.EnsureVisible();
					});
					if (lvitem == null)
					{
						continue;
					}
					lvitem.StartDownload(_downloadTarget, _ => _downloadedHashSet.Contains(_));
				}
				Thread.Sleep(100);
			}
		}

		#endregion

		private void tsReloadError_Click(object sender, EventArgs e)
		{
			using (queue.CreateBatchOperationDispatcher())
			{
				lock (_downloadQueue)
				{
					foreach (var item in queue.Items.Cast<DownloadQueueItem>().ToArray())
					{
						if (item.Status != QueueStatus.Failed)
							continue;

						item.Status = QueueStatus.Wait;
						_downloadQueue.Enqueue(item.ResourceItem);
					}
				}
			}
		}

		#region 菜单操作

		void InitContextMenu()
		{
			ctx.Opening += (s, e) =>
			{
				var items = queue.SelectedItems.Cast<DownloadQueueItem>().ToArray();
				if (items.Length == 0)
				{
					e.Cancel = true;
					return;
				}
				var item = items[0];
				ctxOpen.Enabled = item.Status == QueueStatus.Succeed;
				ctxOpenDirectory.Enabled = item.Status == QueueStatus.Succeed;
				ctxRedownload.Enabled = item.Status == QueueStatus.Failed;
			};
			ctxCopyLink.Click += (s, e) =>
			{
				AppContext.Instance.ResourceOperation.CopyMagnetLink(queue.SelectedItems.Cast<DownloadQueueItem>().Select(x => x.ResourceItem).ToArray());
			};
			ctxRedownload.Click += (s, e) =>
			{
				var items = queue.SelectedItems.Cast<DownloadQueueItem>().Where(x => x.Status == QueueStatus.Failed).ToArray();
				lock (_downloadQueue)
				{
					items.ForEach(x =>
					{
						x.Status = QueueStatus.Wait;
						_downloadQueue.Enqueue(x.ResourceItem);
					});
				}
			};
			ctxDelete.Click += (s, e) =>
			{
				var items = queue.SelectedItems.Cast<DownloadQueueItem>().ToArray();
				RemoveTasks(x => items.Contains(x));
			};
			ctxOpen.Click += (s, e) =>
			{
				var item = (queue.SelectedItems[0] as DownloadQueueItem).DownloadedFilePath;
				try
				{
					Process.Start(item);
				}
				catch (Exception)
				{
					this.Error("无法打开指定文件.");
				}
			};
			ctxOpenDirectory.Click += (s, e) =>
			{
				var item = (queue.SelectedItems[0] as DownloadQueueItem).DownloadedFilePath;
				try
				{
					System.Diagnostics.Process.Start("explorer.exe", "/select,\"" + item + "\"");
				}
				catch (Exception)
				{
					this.Error("无法打开指定文件.");
				}
			};
		}

		#endregion
	}
}
