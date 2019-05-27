namespace BtResourceGrabber.UI.Dialogs.ConfigUi
{
	partial class HistoryOption
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
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.chkLogDownload = new System.Windows.Forms.CheckBox();
			this.btnClearDownloadHistory = new System.Windows.Forms.Button();
			this.btnClearHistory = new System.Windows.Forms.Button();
			this.chkRecSearchHistory = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.chkLogDownload);
			this.panel1.Controls.Add(this.btnClearDownloadHistory);
			this.panel1.Controls.Add(this.btnClearHistory);
			this.panel1.Controls.Add(this.chkRecSearchHistory);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(535, 366);
			this.panel1.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(30, 334);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(342, 17);
			this.label3.TabIndex = 10;
			this.label3.Text = "(以上历史数据均记录在程序目录下的data目录中，仅本机记录)";
			// 
			// chkLogDownload
			// 
			this.chkLogDownload.AutoSize = true;
			this.chkLogDownload.Location = new System.Drawing.Point(33, 47);
			this.chkLogDownload.Name = "chkLogDownload";
			this.chkLogDownload.Size = new System.Drawing.Size(219, 21);
			this.chkLogDownload.TabIndex = 8;
			this.chkLogDownload.Text = "记录下载历史 (下载提示依赖此选项)";
			this.chkLogDownload.UseVisualStyleBackColor = true;
			// 
			// btnClearDownloadHistory
			// 
			this.btnClearDownloadHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnClearDownloadHistory.Image = global::BtResourceGrabber.Properties.Resources.clear16;
			this.btnClearDownloadHistory.Location = new System.Drawing.Point(169, 306);
			this.btnClearDownloadHistory.Name = "btnClearDownloadHistory";
			this.btnClearDownloadHistory.Size = new System.Drawing.Size(126, 25);
			this.btnClearDownloadHistory.TabIndex = 11;
			this.btnClearDownloadHistory.Text = "清除下载历史(&D)";
			this.btnClearDownloadHistory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnClearDownloadHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnClearDownloadHistory.UseVisualStyleBackColor = true;
			// 
			// btnClearHistory
			// 
			this.btnClearHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnClearHistory.Image = global::BtResourceGrabber.Properties.Resources.clear16;
			this.btnClearHistory.Location = new System.Drawing.Point(37, 306);
			this.btnClearHistory.Name = "btnClearHistory";
			this.btnClearHistory.Size = new System.Drawing.Size(126, 25);
			this.btnClearHistory.TabIndex = 12;
			this.btnClearHistory.Text = "清除搜索历史(&L)";
			this.btnClearHistory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnClearHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnClearHistory.UseVisualStyleBackColor = true;
			// 
			// chkRecSearchHistory
			// 
			this.chkRecSearchHistory.AutoSize = true;
			this.chkRecSearchHistory.Location = new System.Drawing.Point(33, 25);
			this.chkRecSearchHistory.Name = "chkRecSearchHistory";
			this.chkRecSearchHistory.Size = new System.Drawing.Size(99, 21);
			this.chkRecSearchHistory.TabIndex = 9;
			this.chkRecSearchHistory.Text = "记录搜索历史";
			this.chkRecSearchHistory.UseVisualStyleBackColor = true;
			// 
			// HistoryOption
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Name = "HistoryOption";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox chkLogDownload;
		private System.Windows.Forms.Button btnClearDownloadHistory;
		private System.Windows.Forms.Button btnClearHistory;
		private System.Windows.Forms.CheckBox chkRecSearchHistory;
	}
}
