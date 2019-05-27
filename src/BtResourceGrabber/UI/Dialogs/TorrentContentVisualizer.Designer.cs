namespace BtResourceGrabber.UI.Dialogs
{
	partial class TorrentContentVisualizer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TorrentContentVisualizer));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsExpand = new System.Windows.Forms.ToolStripButton();
			this.tsCollapse = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsCopy = new System.Windows.Forms.ToolStripButton();
			this.tsDownload = new System.Windows.Forms.ToolStripButton();
			this.tsMark = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsMarkNone = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.stStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.stProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.files = new System.Windows.Forms.TreeView();
			this.il = new System.Windows.Forms.ImageList(this.components);
			this.toolStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsExpand,
            this.tsCollapse,
            this.toolStripSeparator1,
            this.tsCopy,
            this.tsDownload,
            this.tsMark});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(812, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsExpand
			// 
			this.tsExpand.Image = global::BtResourceGrabber.Properties.Resources.clear_folder_open;
			this.tsExpand.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsExpand.Name = "tsExpand";
			this.tsExpand.Size = new System.Drawing.Size(76, 22);
			this.tsExpand.Text = "全部展开";
			// 
			// tsCollapse
			// 
			this.tsCollapse.Image = global::BtResourceGrabber.Properties.Resources.clear_folder;
			this.tsCollapse.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsCollapse.Name = "tsCollapse";
			this.tsCollapse.Size = new System.Drawing.Size(76, 22);
			this.tsCollapse.Text = "全部折叠";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsCopy
			// 
			this.tsCopy.Image = global::BtResourceGrabber.Properties.Resources.paste_plain;
			this.tsCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsCopy.Name = "tsCopy";
			this.tsCopy.Size = new System.Drawing.Size(88, 22);
			this.tsCopy.Text = "复制磁力链";
			// 
			// tsDownload
			// 
			this.tsDownload.Image = global::BtResourceGrabber.Properties.Resources.down_16;
			this.tsDownload.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsDownload.Name = "tsDownload";
			this.tsDownload.Size = new System.Drawing.Size(76, 22);
			this.tsDownload.Text = "下载种子";
			// 
			// tsMark
			// 
			this.tsMark.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMarkNone,
            this.toolStripMenuItem1});
			this.tsMark.Image = global::BtResourceGrabber.Properties.Resources.label_16;
			this.tsMark.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsMark.Name = "tsMark";
			this.tsMark.Size = new System.Drawing.Size(61, 22);
			this.tsMark.Text = "标记";
			// 
			// tsMarkNone
			// 
			this.tsMarkNone.Name = "tsMarkNone";
			this.tsMarkNone.Size = new System.Drawing.Size(88, 22);
			this.tsMarkNone.Text = "无";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(85, 6);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stStatus,
            this.toolStripStatusLabel2,
            this.stProgress});
			this.statusStrip1.Location = new System.Drawing.Point(0, 449);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(812, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// stStatus
			// 
			this.stStatus.Image = global::BtResourceGrabber.Properties.Resources._16px_loading_1;
			this.stStatus.Name = "stStatus";
			this.stStatus.Size = new System.Drawing.Size(16, 17);
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(781, 17);
			this.toolStripStatusLabel2.Spring = true;
			// 
			// stProgress
			// 
			this.stProgress.Name = "stProgress";
			this.stProgress.Size = new System.Drawing.Size(150, 16);
			this.stProgress.Visible = false;
			// 
			// files
			// 
			this.files.Dock = System.Windows.Forms.DockStyle.Fill;
			this.files.ImageIndex = 0;
			this.files.ImageList = this.il;
			this.files.Location = new System.Drawing.Point(0, 25);
			this.files.Name = "files";
			this.files.SelectedImageIndex = 0;
			this.files.Size = new System.Drawing.Size(812, 424);
			this.files.TabIndex = 2;
			// 
			// il
			// 
			this.il.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.il.ImageSize = new System.Drawing.Size(16, 16);
			this.il.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// TorrentContentVisualizer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(812, 471);
			this.Controls.Add(this.files);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.toolStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "TorrentContentVisualizer";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "种子内容检索";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsCopy;
		private System.Windows.Forms.ToolStripButton tsDownload;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel stStatus;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.ToolStripProgressBar stProgress;
		private System.Windows.Forms.TreeView files;
		private System.Windows.Forms.ImageList il;
		private System.Windows.Forms.ToolStripDropDownButton tsMark;
		private System.Windows.Forms.ToolStripButton tsExpand;
		private System.Windows.Forms.ToolStripButton tsCollapse;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem tsMarkNone;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
	}
}