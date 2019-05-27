namespace BtResourceGrabber.UI.Dialogs
{
	partial class DownloadQueue
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadQueue));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsRemoveSucc = new System.Windows.Forms.ToolStripButton();
			this.tsRemoveFail = new System.Windows.Forms.ToolStripButton();
			this.tsRemoveAll = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsReloadError = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsCopyMagnet = new System.Windows.Forms.ToolStripButton();
			this.tsMarkRes = new System.Windows.Forms.ToolStripDropDownButton();
			this.mMarkNone = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.mMarkOption = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.stStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.stProgressCurrent = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.stProgressTotal = new System.Windows.Forms.ToolStripProgressBar();
			this.stDownloadProgressTotal = new System.Windows.Forms.ToolStripStatusLabel();
			this.queue = new System.Windows.Forms.ListView();
			this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.il = new System.Windows.Forms.ImageList(this.components);
			this._saveTorrent = new System.Windows.Forms.SaveFileDialog();
			this.ni = new System.Windows.Forms.NotifyIcon(this.components);
			this.ctx = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ctxOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.ctxOpenDirectory = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.ctxCopyLink = new System.Windows.Forms.ToolStripMenuItem();
			this.ctxRedownload = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.ctxDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.ctx.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRemoveSucc,
            this.tsRemoveFail,
            this.tsRemoveAll,
            this.toolStripSeparator3,
            this.tsReloadError,
            this.toolStripSeparator1,
            this.tsCopyMagnet,
            this.tsMarkRes});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(784, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsRemoveSucc
			// 
			this.tsRemoveSucc.Image = ((System.Drawing.Image)(resources.GetObject("tsRemoveSucc.Image")));
			this.tsRemoveSucc.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRemoveSucc.Name = "tsRemoveSucc";
			this.tsRemoveSucc.Size = new System.Drawing.Size(112, 22);
			this.tsRemoveSucc.Text = "删除已完成任务";
			// 
			// tsRemoveFail
			// 
			this.tsRemoveFail.Image = ((System.Drawing.Image)(resources.GetObject("tsRemoveFail.Image")));
			this.tsRemoveFail.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRemoveFail.Name = "tsRemoveFail";
			this.tsRemoveFail.Size = new System.Drawing.Size(100, 22);
			this.tsRemoveFail.Text = "删除错误任务";
			// 
			// tsRemoveAll
			// 
			this.tsRemoveAll.Image = ((System.Drawing.Image)(resources.GetObject("tsRemoveAll.Image")));
			this.tsRemoveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRemoveAll.Name = "tsRemoveAll";
			this.tsRemoveAll.Size = new System.Drawing.Size(76, 22);
			this.tsRemoveAll.Text = "删除全部";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// tsReloadError
			// 
			this.tsReloadError.Image = global::BtResourceGrabber.Properties.Resources.stock_repeat;
			this.tsReloadError.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsReloadError.Name = "tsReloadError";
			this.tsReloadError.Size = new System.Drawing.Size(124, 22);
			this.tsReloadError.Text = "重新下载错误任务";
			this.tsReloadError.Click += new System.EventHandler(this.tsReloadError_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsCopyMagnet
			// 
			this.tsCopyMagnet.Image = global::BtResourceGrabber.Properties.Resources.paste_plain;
			this.tsCopyMagnet.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsCopyMagnet.Name = "tsCopyMagnet";
			this.tsCopyMagnet.Size = new System.Drawing.Size(88, 22);
			this.tsCopyMagnet.Text = "复制磁力链";
			// 
			// tsMarkRes
			// 
			this.tsMarkRes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mMarkNone,
            this.toolStripSeparator2,
            this.toolStripMenuItem3,
            this.mMarkOption});
			this.tsMarkRes.Image = global::BtResourceGrabber.Properties.Resources.label_16;
			this.tsMarkRes.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsMarkRes.Name = "tsMarkRes";
			this.tsMarkRes.Size = new System.Drawing.Size(85, 22);
			this.tsMarkRes.Text = "标记资源";
			// 
			// mMarkNone
			// 
			this.mMarkNone.Name = "mMarkNone";
			this.mMarkNone.Size = new System.Drawing.Size(157, 22);
			this.mMarkNone.Text = "无标记";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(154, 6);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(154, 6);
			// 
			// mMarkOption
			// 
			this.mMarkOption.Name = "mMarkOption";
			this.mMarkOption.Size = new System.Drawing.Size(157, 22);
			this.mMarkOption.Text = "设置标记信息...";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stStatus,
            this.stProgressCurrent,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel1,
            this.stProgressTotal,
            this.stDownloadProgressTotal});
			this.statusStrip1.Location = new System.Drawing.Point(0, 305);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(784, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// stStatus
			// 
			this.stStatus.Name = "stStatus";
			this.stStatus.Size = new System.Drawing.Size(0, 17);
			// 
			// stProgressCurrent
			// 
			this.stProgressCurrent.Name = "stProgressCurrent";
			this.stProgressCurrent.Size = new System.Drawing.Size(150, 16);
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(393, 17);
			this.toolStripStatusLabel2.Spring = true;
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Image = global::BtResourceGrabber.Properties.Resources.down_16;
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(72, 17);
			this.toolStripStatusLabel1.Text = "下载进度";
			// 
			// stProgressTotal
			// 
			this.stProgressTotal.Name = "stProgressTotal";
			this.stProgressTotal.Size = new System.Drawing.Size(150, 16);
			// 
			// stDownloadProgressTotal
			// 
			this.stDownloadProgressTotal.Name = "stDownloadProgressTotal";
			this.stDownloadProgressTotal.Size = new System.Drawing.Size(0, 17);
			// 
			// queue
			// 
			this.queue.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colStatus});
			this.queue.ContextMenuStrip = this.ctx;
			this.queue.Dock = System.Windows.Forms.DockStyle.Fill;
			this.queue.FullRowSelect = true;
			this.queue.HideSelection = false;
			this.queue.Location = new System.Drawing.Point(0, 25);
			this.queue.Name = "queue";
			this.queue.Size = new System.Drawing.Size(784, 280);
			this.queue.SmallImageList = this.il;
			this.queue.TabIndex = 2;
			this.queue.UseCompatibleStateImageBehavior = false;
			this.queue.View = System.Windows.Forms.View.Details;
			// 
			// colName
			// 
			this.colName.Text = "标题";
			this.colName.Width = 567;
			// 
			// colStatus
			// 
			this.colStatus.Text = "状态";
			this.colStatus.Width = 179;
			// 
			// il
			// 
			this.il.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.il.ImageSize = new System.Drawing.Size(24, 24);
			this.il.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// _saveTorrent
			// 
			this._saveTorrent.Filter = "种子文件(*.torrent)|*.torrent";
			this._saveTorrent.Title = "保存种子为...";
			// 
			// ni
			// 
			this.ni.Icon = ((System.Drawing.Icon)(resources.GetObject("ni.Icon")));
			this.ni.Text = "BT资源助手 BY 木魚";
			this.ni.Visible = true;
			// 
			// ctx
			// 
			this.ctx.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxOpen,
            this.ctxOpenDirectory,
            this.toolStripMenuItem1,
            this.ctxCopyLink,
            this.ctxRedownload,
            this.toolStripMenuItem2,
            this.ctxDelete});
			this.ctx.Name = "ctx";
			this.ctx.Size = new System.Drawing.Size(199, 148);
			// 
			// ctxOpen
			// 
			this.ctxOpen.Image = global::BtResourceGrabber.Properties.Resources.tick_16;
			this.ctxOpen.Name = "ctxOpen";
			this.ctxOpen.Size = new System.Drawing.Size(198, 22);
			this.ctxOpen.Text = "打开种子(&O)";
			// 
			// ctxOpenDirectory
			// 
			this.ctxOpenDirectory.Image = global::BtResourceGrabber.Properties.Resources.folder_16;
			this.ctxOpenDirectory.Name = "ctxOpenDirectory";
			this.ctxOpenDirectory.Size = new System.Drawing.Size(198, 22);
			this.ctxOpenDirectory.Text = "打开种子所在文件夹(&F)";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(195, 6);
			// 
			// ctxCopyLink
			// 
			this.ctxCopyLink.Image = global::BtResourceGrabber.Properties.Resources.paste_plain;
			this.ctxCopyLink.Name = "ctxCopyLink";
			this.ctxCopyLink.Size = new System.Drawing.Size(198, 22);
			this.ctxCopyLink.Text = "复制磁力链(&C)";
			// 
			// ctxRedownload
			// 
			this.ctxRedownload.Image = global::BtResourceGrabber.Properties.Resources.stock_repeat;
			this.ctxRedownload.Name = "ctxRedownload";
			this.ctxRedownload.Size = new System.Drawing.Size(198, 22);
			this.ctxRedownload.Text = "重新下载(&R)";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(195, 6);
			// 
			// ctxDelete
			// 
			this.ctxDelete.Image = global::BtResourceGrabber.Properties.Resources.trash_16;
			this.ctxDelete.Name = "ctxDelete";
			this.ctxDelete.Size = new System.Drawing.Size(198, 22);
			this.ctxDelete.Text = "删除任务(&D)";
			// 
			// DownloadQueue
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 327);
			this.Controls.Add(this.queue);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.toolStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DownloadQueue";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "下载管理器";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ctx.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel stStatus;
		private System.Windows.Forms.ToolStripProgressBar stProgressCurrent;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripProgressBar stProgressTotal;
		private System.Windows.Forms.ToolStripStatusLabel stDownloadProgressTotal;
		private System.Windows.Forms.ListView queue;
		private System.Windows.Forms.ImageList il;
		private System.Windows.Forms.ToolStripButton tsRemoveSucc;
		private System.Windows.Forms.ToolStripButton tsRemoveFail;
		private System.Windows.Forms.ToolStripButton tsRemoveAll;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsCopyMagnet;
		private System.Windows.Forms.ToolStripDropDownButton tsMarkRes;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colStatus;
		private System.Windows.Forms.ToolStripMenuItem mMarkNone;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem mMarkOption;
		private System.Windows.Forms.SaveFileDialog _saveTorrent;
		private System.Windows.Forms.NotifyIcon ni;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton tsReloadError;
		private System.Windows.Forms.ContextMenuStrip ctx;
		private System.Windows.Forms.ToolStripMenuItem ctxOpen;
		private System.Windows.Forms.ToolStripMenuItem ctxOpenDirectory;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem ctxCopyLink;
		private System.Windows.Forms.ToolStripMenuItem ctxRedownload;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem ctxDelete;
	}
}