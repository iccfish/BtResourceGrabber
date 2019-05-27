namespace BtResourceGrabber.UI.Controls
{
	using ComponentOwl.BetterListView;

	partial class EngineTabContent
	{
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.lv = new ComponentOwl.BetterListView.BetterListView();
			this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colFileCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colTag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colUpdateDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ctxMarkType = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.mCopyTitle = new System.Windows.Forms.ToolStripMenuItem();
			this.mCopyHash = new System.Windows.Forms.ToolStripMenuItem();
			this.ilLv = new System.Windows.Forms.ImageList(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblCancelSearch = new System.Windows.Forms.LinkLabel();
			this.lblSearchProgress = new System.Windows.Forms.Label();
			this.pgSearchProgress = new System.Windows.Forms.ProgressBar();
			this.pWarning = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.mCopyMagnet = new System.Windows.Forms.ToolStripMenuItem();
			this.mDownloadTorrent = new System.Windows.Forms.ToolStripMenuItem();
			this.mViewDetail = new System.Windows.Forms.ToolStripMenuItem();
			this.mViewContent = new System.Windows.Forms.ToolStripMenuItem();
			this.mMarkColor = new System.Windows.Forms.ToolStripMenuItem();
			this.mMarkNone = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.mMarkOption = new System.Windows.Forms.ToolStripMenuItem();
			this.mSearchBaiDu = new System.Windows.Forms.ToolStripMenuItem();
			this.mSearchGoogle = new System.Windows.Forms.ToolStripMenuItem();
			this.btnLoadMore = new System.Windows.Forms.Button();
			this.ctxMarkType.SuspendLayout();
			this.panel1.SuspendLayout();
			this.pWarning.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// lv
			// 
			this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colSize,
            this.colFileCount,
            this.colTag,
            this.colUpdateDate});
			this.lv.ContextMenuStrip = this.ctxMarkType;
			this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lv.FullRowSelect = true;
			this.lv.HideSelection = false;
			this.lv.Location = new System.Drawing.Point(0, 0);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(546, 348);
			this.lv.ImageList = this.ilLv;
			this.lv.TabIndex = 0;
			this.lv.View = BetterListViewView.Details;
			// 
			// colName
			// 
			this.colName.Text = "资源名";
			this.colName.Width = 181;
			// 
			// colSize
			// 
			this.colSize.Text = "大小";
			this.colSize.Width = 95;
			// 
			// colFileCount
			// 
			this.colFileCount.Text = "文件数";
			// 
			// colTag
			// 
			this.colTag.Text = "标记";
			// 
			// colUpdateDate
			// 
			this.colUpdateDate.Text = "更新日期";
			// 
			// ctxMarkType
			// 
			this.ctxMarkType.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mCopyMagnet,
            this.mDownloadTorrent,
            this.mViewDetail,
            this.mViewContent,
            this.toolStripMenuItem1,
            this.mMarkColor,
            this.toolStripMenuItem4,
            this.mCopyTitle,
            this.mCopyHash,
            this.mSearchBaiDu,
            this.mSearchGoogle});
			this.ctxMarkType.Name = "ctxMarkType";
			this.ctxMarkType.Size = new System.Drawing.Size(166, 214);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(162, 6);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(162, 6);
			// 
			// mCopyTitle
			// 
			this.mCopyTitle.Name = "mCopyTitle";
			this.mCopyTitle.Size = new System.Drawing.Size(165, 22);
			this.mCopyTitle.Text = "复制标题(&T)";
			// 
			// mCopyHash
			// 
			this.mCopyHash.Name = "mCopyHash";
			this.mCopyHash.Size = new System.Drawing.Size(165, 22);
			this.mCopyHash.Text = "复制特征码(&T)";
			// 
			// ilLv
			// 
			this.ilLv.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.ilLv.ImageSize = new System.Drawing.Size(20, 20);
			this.ilLv.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lblCancelSearch);
			this.panel1.Controls.Add(this.btnLoadMore);
			this.panel1.Controls.Add(this.lblSearchProgress);
			this.panel1.Controls.Add(this.pgSearchProgress);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 348);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(546, 42);
			this.panel1.TabIndex = 1;
			// 
			// lblCancelSearch
			// 
			this.lblCancelSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCancelSearch.Location = new System.Drawing.Point(250, 4);
			this.lblCancelSearch.Name = "lblCancelSearch";
			this.lblCancelSearch.Size = new System.Drawing.Size(99, 14);
			this.lblCancelSearch.TabIndex = 9;
			this.lblCancelSearch.TabStop = true;
			this.lblCancelSearch.Text = "取消搜索";
			this.lblCancelSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblCancelSearch.Visible = false;
			// 
			// lblSearchProgress
			// 
			this.lblSearchProgress.AutoSize = true;
			this.lblSearchProgress.Location = new System.Drawing.Point(3, 5);
			this.lblSearchProgress.Name = "lblSearchProgress";
			this.lblSearchProgress.Size = new System.Drawing.Size(53, 12);
			this.lblSearchProgress.TabIndex = 6;
			this.lblSearchProgress.Text = "搜索进度";
			this.lblSearchProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pgSearchProgress
			// 
			this.pgSearchProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pgSearchProgress.Location = new System.Drawing.Point(3, 20);
			this.pgSearchProgress.Name = "pgSearchProgress";
			this.pgSearchProgress.Size = new System.Drawing.Size(346, 19);
			this.pgSearchProgress.TabIndex = 7;
			this.pgSearchProgress.Visible = false;
			// 
			// pWarning
			// 
			this.pWarning.BackColor = System.Drawing.SystemColors.Window;
			this.pWarning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pWarning.Controls.Add(this.label1);
			this.pWarning.Controls.Add(this.pictureBox1);
			this.pWarning.Location = new System.Drawing.Point(119, 140);
			this.pWarning.Name = "pWarning";
			this.pWarning.Size = new System.Drawing.Size(274, 59);
			this.pWarning.TabIndex = 2;
			this.pWarning.Visible = false;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(44, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(234, 44);
			this.label1.TabIndex = 1;
			this.label1.Text = "当前搜索引擎对非英文支持不好，无法搜索。请选择其它搜索引擎！";
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
			// mCopyMagnet
			// 
			this.mCopyMagnet.Image = global::BtResourceGrabber.Properties.Resources.paste_plain;
			this.mCopyMagnet.Name = "mCopyMagnet";
			this.mCopyMagnet.Size = new System.Drawing.Size(165, 22);
			this.mCopyMagnet.Text = "复制磁力链(&C)";
			// 
			// mDownloadTorrent
			// 
			this.mDownloadTorrent.Image = global::BtResourceGrabber.Properties.Resources.down_16;
			this.mDownloadTorrent.Name = "mDownloadTorrent";
			this.mDownloadTorrent.Size = new System.Drawing.Size(165, 22);
			this.mDownloadTorrent.Text = "下载种子(&D)";
			// 
			// mViewDetail
			// 
			this.mViewDetail.Image = global::BtResourceGrabber.Properties.Resources.application_view_detail;
			this.mViewDetail.Name = "mViewDetail";
			this.mViewDetail.Size = new System.Drawing.Size(165, 22);
			this.mViewDetail.Text = "查看详细信息(&V)";
			// 
			// mViewContent
			// 
			this.mViewContent.Image = global::BtResourceGrabber.Properties.Resources.folder_16;
			this.mViewContent.Name = "mViewContent";
			this.mViewContent.Size = new System.Drawing.Size(165, 22);
			this.mViewContent.Text = "查看文件内容(&D)";
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
			this.mMarkColor.Size = new System.Drawing.Size(165, 22);
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
			// mSearchBaiDu
			// 
			this.mSearchBaiDu.Image = global::BtResourceGrabber.Properties.Resources.favicon;
			this.mSearchBaiDu.Name = "mSearchBaiDu";
			this.mSearchBaiDu.Size = new System.Drawing.Size(165, 22);
			this.mSearchBaiDu.Text = "使用百度搜索(&B)";
			// 
			// mSearchGoogle
			// 
			this.mSearchGoogle.Image = global::BtResourceGrabber.Properties.Resources.favicon_google;
			this.mSearchGoogle.Name = "mSearchGoogle";
			this.mSearchGoogle.Size = new System.Drawing.Size(165, 22);
			this.mSearchGoogle.Text = "使用谷歌搜索(&G)";
			// 
			// btnLoadMore
			// 
			this.btnLoadMore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLoadMore.Enabled = false;
			this.btnLoadMore.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnLoadMore.Image = global::BtResourceGrabber.Properties.Resources.Magnifier___Plus;
			this.btnLoadMore.Location = new System.Drawing.Point(402, 5);
			this.btnLoadMore.Name = "btnLoadMore";
			this.btnLoadMore.Size = new System.Drawing.Size(141, 34);
			this.btnLoadMore.TabIndex = 8;
			this.btnLoadMore.Text = "搜索更多";
			this.btnLoadMore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnLoadMore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnLoadMore.UseVisualStyleBackColor = true;
			this.btnLoadMore.Click += new System.EventHandler(this.btnLoadMore_Click);
			// 
			// EngineTabContent
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pWarning);
			this.Controls.Add(this.lv);
			this.Controls.Add(this.panel1);
			this.Name = "EngineTabContent";
			this.Size = new System.Drawing.Size(546, 390);
			this.ctxMarkType.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.pWarning.ResumeLayout(false);
			this.pWarning.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private ComponentOwl.BetterListView.BetterListView lv;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colSize;
		private System.Windows.Forms.ColumnHeader colUpdateDate;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnLoadMore;
		private System.Windows.Forms.Label lblSearchProgress;
		private System.Windows.Forms.ProgressBar pgSearchProgress;
		private System.Windows.Forms.ContextMenuStrip ctxMarkType;
		private System.Windows.Forms.ImageList ilLv;
		private System.Windows.Forms.LinkLabel lblCancelSearch;
		private System.Windows.Forms.ToolStripMenuItem mCopyMagnet;
		private System.Windows.Forms.ToolStripMenuItem mDownloadTorrent;
		private System.Windows.Forms.ToolStripMenuItem mViewDetail;
		private System.Windows.Forms.ToolStripMenuItem mViewContent;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem mMarkColor;
		private System.Windows.Forms.ToolStripMenuItem mMarkNone;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem mMarkOption;
		private System.Windows.Forms.Panel pWarning;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem mCopyTitle;
		private System.Windows.Forms.ToolStripMenuItem mCopyHash;
		private System.Windows.Forms.ToolStripMenuItem mSearchBaiDu;
		private System.Windows.Forms.ToolStripMenuItem mSearchGoogle;
		private System.Windows.Forms.ColumnHeader colTag;
		private System.Windows.Forms.ColumnHeader colFileCount;
	}
}
