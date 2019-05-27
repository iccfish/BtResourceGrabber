namespace BtResourceGrabber.UI.Dialogs.Engines
{
	partial class EngineStatistics
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
			System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("搜索引擎", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("下载引擎", System.Windows.Forms.HorizontalAlignment.Left);
			this.panel1 = new System.Windows.Forms.Panel();
			this.txtDesc = new System.Windows.Forms.TextBox();
			this.lnkSupport = new System.Windows.Forms.LinkLabel();
			this.label5 = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.lblAuthor = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lv = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.il = new System.Windows.Forms.ImageList(this.components);
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.txtDesc);
			this.panel1.Controls.Add(this.lnkSupport);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.lblVersion);
			this.panel1.Controls.Add(this.lblAuthor);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.btnOk);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 480);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(728, 71);
			this.panel1.TabIndex = 0;
			// 
			// txtDesc
			// 
			this.txtDesc.Location = new System.Drawing.Point(223, 7);
			this.txtDesc.Multiline = true;
			this.txtDesc.Name = "txtDesc";
			this.txtDesc.ReadOnly = true;
			this.txtDesc.Size = new System.Drawing.Size(286, 57);
			this.txtDesc.TabIndex = 4;
			this.txtDesc.Text = "选定引擎查看详情";
			// 
			// lnkSupport
			// 
			this.lnkSupport.AutoSize = true;
			this.lnkSupport.Location = new System.Drawing.Point(68, 47);
			this.lnkSupport.Name = "lnkSupport";
			this.lnkSupport.Size = new System.Drawing.Size(80, 17);
			this.lnkSupport.TabIndex = 3;
			this.lnkSupport.TabStop = true;
			this.lnkSupport.Text = "获得更多信息";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label5.Location = new System.Drawing.Point(12, 47);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(56, 17);
			this.label5.TabIndex = 2;
			this.label5.Text = "支持信息";
			// 
			// lblVersion
			// 
			this.lblVersion.AutoSize = true;
			this.lblVersion.Location = new System.Drawing.Point(68, 27);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(13, 17);
			this.lblVersion.TabIndex = 2;
			this.lblVersion.Text = "-";
			// 
			// lblAuthor
			// 
			this.lblAuthor.AutoSize = true;
			this.lblAuthor.Location = new System.Drawing.Point(68, 7);
			this.lblAuthor.Name = "lblAuthor";
			this.lblAuthor.Size = new System.Drawing.Size(13, 17);
			this.lblAuthor.TabIndex = 1;
			this.lblAuthor.Text = "-";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label4.Location = new System.Drawing.Point(12, 27);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 17);
			this.label4.TabIndex = 2;
			this.label4.Text = "引擎版本";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(12, 7);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 17);
			this.label3.TabIndex = 1;
			this.label3.Text = "引擎作者";
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Image = global::BtResourceGrabber.Properties.Resources.tick_16;
			this.btnOk.Location = new System.Drawing.Point(578, 7);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(129, 57);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "确定(&O)";
			this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.White;
			this.panel2.Controls.Add(this.pictureBox1);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(728, 70);
			this.panel2.TabIndex = 1;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::BtResourceGrabber.Properties.Resources.chart_pie;
			this.pictureBox1.Location = new System.Drawing.Point(12, 9);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(97, 37);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(261, 20);
			this.label2.TabIndex = 0;
			this.label2.Text = "可使用复选框来切换是否启用指定的引擎";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(66, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(266, 22);
			this.label1.TabIndex = 0;
			this.label1.Text = "这里显示的是各个引擎运行状态统计";
			// 
			// lv
			// 
			this.lv.CheckBoxes = true;
			this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader7,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader6});
			this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lv.FullRowSelect = true;
			listViewGroup1.Header = "搜索引擎";
			listViewGroup1.Name = "lvgSearch";
			listViewGroup2.Header = "下载引擎";
			listViewGroup2.Name = "lvgDownload";
			this.lv.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
			this.lv.HideSelection = false;
			this.lv.Location = new System.Drawing.Point(0, 70);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(728, 410);
			this.lv.SmallImageList = this.il;
			this.lv.TabIndex = 2;
			this.lv.UseCompatibleStateImageBehavior = false;
			this.lv.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "引擎名";
			this.columnHeader1.Width = 267;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "翻墙?";
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "状态";
			this.columnHeader7.Width = 113;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "使用次数";
			this.columnHeader3.Width = 92;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "成功次数";
			this.columnHeader4.Width = 92;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "错误次数";
			this.columnHeader6.Width = 87;
			// 
			// il
			// 
			this.il.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.il.ImageSize = new System.Drawing.Size(24, 24);
			this.il.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// EngineStatistics
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnOk;
			this.ClientSize = new System.Drawing.Size(728, 551);
			this.Controls.Add(this.lv);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EngineStatistics";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "引擎状态统计";
			this.Load += new System.EventHandler(this.EngineStatistics_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ListView lv;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ImageList il;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.LinkLabel lnkSupport;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label lblAuthor;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtDesc;
		private System.Windows.Forms.ColumnHeader columnHeader2;
	}
}