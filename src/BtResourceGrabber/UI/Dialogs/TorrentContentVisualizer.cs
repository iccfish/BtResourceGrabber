using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BtResourceGrabber.Entities;
using BtResourceGrabber.Service;

namespace BtResourceGrabber.UI.Dialogs
{
	using BRG;
	using BRG.Entities;
	using BRG.Service;

	partial class TorrentContentVisualizer : FunctionalForm
	{
		IResourceProvider _provider;
		IResourceInfo _resource;

		public TorrentContentVisualizer(IResourceProvider provider, IResourceInfo resource)
		{
			_provider = provider;
			_resource = resource;

			InitializeComponent();

			//初始化文件图标
			il.ImageSize = new Size(20, 20);
			il.Images.Add("video", Utility.Get20PxImageFrom16PxImg(Properties.Resources.video));
			il.Images.Add("image", Utility.Get20PxImageFrom16PxImg(Properties.Resources.image));
			il.Images.Add("file", Utility.Get20PxImageFrom16PxImg(Properties.Resources.file));
			il.Images.Add("folder", Utility.Get20PxImageFrom16PxImg(Properties.Resources.clear_folder));
			il.Images.Add("folderopen", Utility.Get20PxImageFrom16PxImg(Properties.Resources.clear_folder_open));
			il.Images.Add("zip", Utility.Get20PxImageFrom16PxImg(Properties.Resources.zip));
			il.Images.Add("html", Utility.Get20PxImageFrom16PxImg(Properties.Resources.badge_html));
			il.Images.Add("doc", Utility.Get20PxImageFrom16PxImg(Properties.Resources.document_word));


			Load += TorrentContentVisualizer_Load;
			files.AfterExpand += (s, e) =>
			{
				e.Node.ImageKey = e.Node.SelectedImageKey = "folderopen";
			};
			files.AfterCollapse += (s, e) =>
			{
				e.Node.ImageKey = e.Node.SelectedImageKey = "folder";
			};
			tsCollapse.Click += (s, e) => files.CollapseAll();
			tsExpand.Click += (s, e) => files.ExpandAll();
			tsDownload.Click += (s, e) => AppContext.Instance.ResourceOperation.AccquireDownloadTorrent(_resource);
			tsCopy.Click += (s, e) => AppContext.Instance.ResourceOperation.CopyMagnetLink(_resource);

			//标记
			var markItems = AppContext.Instance.Options.HashMarks.Select(s => (ToolStripItem)new ToolStripMenuItem(s.Key) { ForeColor = s.Value.Color, BackColor = s.Value.BackColor }).ToArray();
			var currentMaskType = AppContext.Instance.GetResourceMarkName(_resource);
			tsMarkNone.Checked = string.IsNullOrEmpty(currentMaskType);
			tsMark.DropDownItems.AddRange(markItems);
			foreach (var toolStripItem in markItems)
			{
				toolStripItem.Click += (s, e) =>
				{
					tsMark.DropDownItems.OfType<ToolStripMenuItem>().Skip(1).ForEach(_ => _.Checked = _ == s);
					AppContext.Instance.ResourceOperation.SetTorrentMask((s as ToolStripMenuItem).Text, _resource);
					tsMarkNone.Checked = false;
				};
				if (!string.IsNullOrEmpty(currentMaskType) && currentMaskType == toolStripItem.Text)
					((ToolStripMenuItem)toolStripItem).Checked = true;
			}
			tsMarkNone.Click += tsMarkNone_Click;
		}

		void tsMarkNone_Click(object sender, EventArgs e)
		{
			tsMark.DropDownItems.OfType<ToolStripMenuItem>().Skip(1).ForEach(_ => _.Checked = false);
			AppContext.Instance.ResourceOperation.SetTorrentMask(null, _resource);
			tsMarkNone.Checked = true;
		}

		void TorrentContentVisualizer_Load(object sender, EventArgs e)
		{
			stProgress.Visible = true;
			stProgress.Style = ProgressBarStyle.Marquee;
			stStatus.Text = "正在加载文件内容...";

			Task.Factory.StartNew(() =>
			{
				try
				{
					if (_resource.Count == 0)
						_provider.LookupTorrentContents(_resource);
					this.Invoke(LoadTorrentContent);
					if (_resource.Count == 0)
						throw new Exception("未能加载文件内容");
				}
				catch (Exception ex)
				{
					this.Invoke(() =>
					{
						stStatus.Image = Properties.Resources.block_16;
						stStatus.Text = "加载文件内容出错：" + ex.Message;
					});
				}
				finally
				{
					this.Invoke(() =>
					{
						stProgress.Visible = false;
					});
				}
			});
		}

		void LoadTorrentContent()
		{
			if (_resource.Count == 0)
				return;

			stStatus.Image = Properties.Resources.tick_16;
			stStatus.Text = "加载成功";
			using (files.CreateBatchOperationDispatcher())
			{
				LoadTorrentContentToNode(files.Nodes, _resource);
				files.ExpandAll();
			}

			if (!_resource.ContentLoadErrMessage.IsNullOrEmpty())
			{
				stStatus.Image = Properties.Resources.warning_16;
				stStatus.Text = _resource.ContentLoadErrMessage;
			}
		}

		static readonly HashSet<string> _imageMask = new HashSet<string>(new[] { "png", "jpg", "jpeg", "tiff", "bmp", "gif", "webp", "ico" }, StringComparer.OrdinalIgnoreCase);
		static readonly HashSet<string> _zipMask = new HashSet<string>(new[] { "rar", "zip", "z01", "z02", "z03", "z04", "z05", "z06", "z07", "rar", "7z", "gz", "bz", "arc" }, StringComparer.OrdinalIgnoreCase);
		static readonly HashSet<string> _htmlMask = new HashSet<string>(new[] { "htm", "html", "mht", "url", "site", "chm" }, StringComparer.OrdinalIgnoreCase);
		static readonly HashSet<string> _videoMask = new HashSet<string>(new[] { "wmv", "rm", "rmvb", "mkv", "mp4", "3gp", "flv", "avi", "mpg", "mpeg" }, StringComparer.OrdinalIgnoreCase);
		static readonly HashSet<string> _docMask = new HashSet<string>(new[] { "doc", "docx", "ppt", "pptx", "rtf", "txt", "xls", "xlsx", "wps", "pdf" }, StringComparer.OrdinalIgnoreCase);

		void LoadTorrentContentToNode(TreeNodeCollection parent, IEnumerable<IFileNode> nodes)
		{
			foreach (var node in nodes)
			{
				if (node.Name.IndexOf("_____padding_file_") != -1)
					continue;

				var n = new TreeNode(node.Name + (node.IsDirectory ? "" : "  (" + (node.Size == null ? node.SizeString.DefaultForEmpty("未知大小") : node.Size.Value.ToSizeDescription()) + ")"));
				if (node.IsDirectory)
				{
					n.ImageKey = n.SelectedImageKey = "folder";
					LoadTorrentContentToNode(n.Nodes, node.Children);
				}
				else
				{
					string imagekey = "file";
					var ext = Path.GetExtension(node.Name).Trim('.');

					if (_imageMask.Contains(ext))
						imagekey = "image";
					else if (_zipMask.Contains(ext))
						imagekey = "zip";
					else if (_htmlMask.Contains(ext))
						imagekey = "html";
					else if (_videoMask.Contains(ext))
						imagekey = "video";
					else if (_docMask.Contains(ext))
						imagekey = "doc";
					n.ImageKey = n.SelectedImageKey = imagekey;
				}

				parent.Add(n);
			}
		}
	}
}
