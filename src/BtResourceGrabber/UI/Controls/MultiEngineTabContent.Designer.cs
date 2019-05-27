namespace BtResourceGrabber.UI.Controls
{
	partial class MultiEngineTabContent
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiEngineTabContent));
			this.ctxMarkType = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsMultiResNotLoad = new System.Windows.Forms.ToolStripMenuItem();
			this.mDownloadTorrent = new System.Windows.Forms.ToolStripMenuItem();
			this.mOpenDownloadPage = new System.Windows.Forms.ToolStripMenuItem();
			this.mCopyDownloadLink = new System.Windows.Forms.ToolStripMenuItem();
			this.mViewContent = new System.Windows.Forms.ToolStripMenuItem();
			this.mViewDetail = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.mMarkColor = new System.Windows.Forms.ToolStripMenuItem();
			this.mMarkNone = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.mMarkOption = new System.Windows.Forms.ToolStripMenuItem();
			this.mMarkDone = new System.Windows.Forms.ToolStripMenuItem();
			this.mMarkUndone = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.mReportState = new System.Windows.Forms.ToolStripMenuItem();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnCancelLoad = new System.Windows.Forms.Button();
			this.panStatus = new System.Windows.Forms.Panel();
			this.lblSearchProgress = new System.Windows.Forms.Label();
			this.pWarning = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tt = new System.Windows.Forms.ToolTip(this.components);
			this.lv = new BtResourceGrabber.UI.Controls.ResourceListView.ListView();
			this.ctxMarkType.SuspendLayout();
			this.panel1.SuspendLayout();
			this.pWarning.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lv)).BeginInit();
			this.SuspendLayout();
			// 
			// ctxMarkType
			// 
			this.ctxMarkType.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMultiResNotLoad,
            this.mDownloadTorrent,
            this.mOpenDownloadPage,
            this.mCopyDownloadLink,
            this.mViewContent,
            this.mViewDetail,
            this.toolStripMenuItem1,
            this.mMarkColor,
            this.mMarkDone,
            this.mMarkUndone,
            this.toolStripSeparator1,
            this.mReportState});
			this.ctxMarkType.Name = "ctxMarkType";
			this.ctxMarkType.Size = new System.Drawing.Size(365, 328);
			// 
			// tsMultiResNotLoad
			// 
			this.tsMultiResNotLoad.ForeColor = System.Drawing.Color.Red;
			this.tsMultiResNotLoad.Name = "tsMultiResNotLoad";
			this.tsMultiResNotLoad.Size = new System.Drawing.Size(364, 24);
			this.tsMultiResNotLoad.Text = "选择的项中有带有多个子资源的项，展开以加载子资源";
			// 
			// mDownloadTorrent
			// 
			this.mDownloadTorrent.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.mDownloadTorrent.Image = global::BtResourceGrabber.Properties.Resources.down_16;
			this.mDownloadTorrent.Name = "mDownloadTorrent";
			this.mDownloadTorrent.Size = new System.Drawing.Size(364, 24);
			this.mDownloadTorrent.Text = "下载种子(&D)";
			// 
			// mOpenDownloadPage
			// 
			this.mOpenDownloadPage.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.mOpenDownloadPage.Image = global::BtResourceGrabber.Properties.Resources.globe_16;
			this.mOpenDownloadPage.Name = "mOpenDownloadPage";
			this.mOpenDownloadPage.Size = new System.Drawing.Size(364, 24);
			this.mOpenDownloadPage.Text = "打开下载页面(&P)";
			// 
			// mCopyDownloadLink
			// 
			this.mCopyDownloadLink.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.mCopyDownloadLink.Image = global::BtResourceGrabber.Properties.Resources.down_16;
			this.mCopyDownloadLink.Name = "mCopyDownloadLink";
			this.mCopyDownloadLink.Size = new System.Drawing.Size(364, 24);
			this.mCopyDownloadLink.Text = "复制下载链接(&C)";
			// 
			// mViewContent
			// 
			this.mViewContent.Image = global::BtResourceGrabber.Properties.Resources.folder_16;
			this.mViewContent.Name = "mViewContent";
			this.mViewContent.Size = new System.Drawing.Size(364, 24);
			this.mViewContent.Text = "查看文件内容(&D)";
			// 
			// mViewDetail
			// 
			this.mViewDetail.Image = global::BtResourceGrabber.Properties.Resources.application_view_detail;
			this.mViewDetail.Name = "mViewDetail";
			this.mViewDetail.Size = new System.Drawing.Size(364, 24);
			this.mViewDetail.Text = "查看详细信息(&V)";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(361, 6);
			// 
			// mMarkColor
			// 
			this.mMarkColor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mMarkNone,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.mMarkOption});
			this.mMarkColor.Image = global::BtResourceGrabber.Properties.Resources.label_16;
			this.mMarkColor.Name = "mMarkColor";
			this.mMarkColor.Size = new System.Drawing.Size(364, 24);
			this.mMarkColor.Text = "标记资源";
			// 
			// mMarkNone
			// 
			this.mMarkNone.Name = "mMarkNone";
			this.mMarkNone.Size = new System.Drawing.Size(157, 22);
			this.mMarkNone.Text = "无标记";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(154, 6);
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
			// mMarkDone
			// 
			this.mMarkDone.Image = global::BtResourceGrabber.Properties.Resources.accept;
			this.mMarkDone.Name = "mMarkDone";
			this.mMarkDone.Size = new System.Drawing.Size(364, 24);
			this.mMarkDone.Text = "标记为已下载";
			// 
			// mMarkUndone
			// 
			this.mMarkUndone.Image = global::BtResourceGrabber.Properties.Resources.block_16;
			this.mMarkUndone.Name = "mMarkUndone";
			this.mMarkUndone.Size = new System.Drawing.Size(364, 24);
			this.mMarkUndone.Text = "标记为未下载";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(361, 6);
			// 
			// mReportState
			// 
			this.mReportState.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.mReportState.Name = "mReportState";
			this.mReportState.Size = new System.Drawing.Size(364, 24);
			this.mReportState.Text = "此资源安全性未知, 请谨慎下载";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnCancelLoad);
			this.panel1.Controls.Add(this.panStatus);
			this.panel1.Controls.Add(this.lblSearchProgress);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 370);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(566, 42);
			this.panel1.TabIndex = 2;
			// 
			// btnCancelLoad
			// 
			this.btnCancelLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancelLoad.Enabled = false;
			this.btnCancelLoad.Image = global::BtResourceGrabber.Properties.Resources.Cancel_Red_Button;
			this.btnCancelLoad.Location = new System.Drawing.Point(469, 6);
			this.btnCancelLoad.Name = "btnCancelLoad";
			this.btnCancelLoad.Size = new System.Drawing.Size(94, 33);
			this.btnCancelLoad.TabIndex = 8;
			this.btnCancelLoad.Text = "取消搜索";
			this.btnCancelLoad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnCancelLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancelLoad.UseVisualStyleBackColor = true;
			// 
			// panStatus
			// 
			this.panStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panStatus.Location = new System.Drawing.Point(5, 20);
			this.panStatus.Name = "panStatus";
			this.panStatus.Size = new System.Drawing.Size(458, 19);
			this.panStatus.TabIndex = 9;
			// 
			// lblSearchProgress
			// 
			this.lblSearchProgress.AutoSize = true;
			this.lblSearchProgress.Location = new System.Drawing.Point(3, 5);
			this.lblSearchProgress.Name = "lblSearchProgress";
			this.lblSearchProgress.Size = new System.Drawing.Size(365, 12);
			this.lblSearchProgress.TabIndex = 6;
			this.lblSearchProgress.Text = "当前搜索使用下列引擎。综合搜索中特定引擎可以独立，参见设置。";
			this.lblSearchProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pWarning
			// 
			this.pWarning.BackColor = System.Drawing.SystemColors.Window;
			this.pWarning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pWarning.Controls.Add(this.pictureBox1);
			this.pWarning.Controls.Add(this.label1);
			this.pWarning.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pWarning.Location = new System.Drawing.Point(146, 177);
			this.pWarning.Name = "pWarning";
			this.pWarning.Size = new System.Drawing.Size(274, 59);
			this.pWarning.TabIndex = 3;
			this.pWarning.Visible = false;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::BtResourceGrabber.Properties.Resources.warning_32;
			this.pictureBox1.Location = new System.Drawing.Point(6, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(44, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(234, 44);
			this.label1.TabIndex = 1;
			this.label1.Text = "当前搜索引擎对非英文支持不好，无法搜索。请选择其它搜索引擎！";
			// 
			// tt
			// 
			this.tt.AutomaticDelay = 0;
			this.tt.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.tt.ToolTipTitle = "引擎状态";
			// 
			// lv
			// 
			this.lv.AutoSizeItemsInDetailsView = true;
			this.lv.ContextMenuStrip = this.ctxMarkType;
			this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lv.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lv.GridLines = ComponentOwl.BetterListView.BetterListViewGridLines.None;
			this.lv.HideSelectionMode = ComponentOwl.BetterListView.BetterListViewHideSelectionMode.Disable;
			this.lv.Indent = 30;
			this.lv.Location = new System.Drawing.Point(0, 0);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(566, 370);
			this.lv.SortedColumnsRowsHighlight = ComponentOwl.BetterListView.BetterListViewSortedColumnsRowsHighlight.ShowAlways;
			this.lv.SortOnCollectionChange = false;
			this.lv.TabIndex = 0;
			// 
			// MultiEngineTabContent
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pWarning);
			this.Controls.Add(this.lv);
			this.Controls.Add(this.panel1);
			this.Name = "MultiEngineTabContent";
			this.Size = new System.Drawing.Size(566, 412);
			this.ctxMarkType.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.pWarning.ResumeLayout(false);
			this.pWarning.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lv)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private BtResourceGrabber.UI.Controls.ResourceListView.ListView lv;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnCancelLoad;
		private System.Windows.Forms.Label lblSearchProgress;
		private System.Windows.Forms.Panel pWarning;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ContextMenuStrip ctxMarkType;
		private System.Windows.Forms.ToolStripMenuItem mCopyDownloadLink;
		private System.Windows.Forms.ToolStripMenuItem mDownloadTorrent;
		private System.Windows.Forms.ToolStripMenuItem mViewDetail;
		private System.Windows.Forms.ToolStripMenuItem mViewContent;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem mMarkColor;
		private System.Windows.Forms.ToolStripMenuItem mMarkNone;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem mMarkOption;
		private System.Windows.Forms.Panel panStatus;
		private System.Windows.Forms.ToolTip tt;
		private System.Windows.Forms.ToolStripMenuItem mMarkDone;
		private System.Windows.Forms.ToolStripMenuItem mMarkUndone;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem mReportState;
		private System.Windows.Forms.ToolStripMenuItem mOpenDownloadPage;
		private System.Windows.Forms.ToolStripMenuItem tsMultiResNotLoad;
	}
}
