namespace BtResourceGrabber.UI.Dialogs.Engines
{
	partial class EngineAvailabilityTest
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
			System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("搜索引擎", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("下载引擎", System.Windows.Forms.HorizontalAlignment.Left);
			this.panel1 = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.lv = new System.Windows.Forms.ListView();
			this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colReport = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colResult = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.il = new System.Windows.Forms.ImageList(this.components);
			this.btnOk = new System.Windows.Forms.Button();
			this.btnRecheck = new System.Windows.Forms.Button();
			this.btnSetProxy = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Window;
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(717, 50);
			this.panel1.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(204, 19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(416, 17);
			this.label2.TabIndex = 3;
			this.label2.Text = "测试您的网络连接各引擎情况。如果出现错误，请检查网络设置和代理设置。";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::BtResourceGrabber.Properties.Resources.testtube;
			this.pictureBox1.Location = new System.Drawing.Point(15, 9);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(53, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(145, 26);
			this.label1.TabIndex = 1;
			this.label1.Text = "引擎可用性检测";
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 49);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(717, 1);
			this.panel2.TabIndex = 0;
			// 
			// lv
			// 
			this.lv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colVersion,
            this.colReport,
            this.colResult});
			this.lv.FullRowSelect = true;
			listViewGroup3.Header = "搜索引擎";
			listViewGroup3.Name = "listViewGroup1";
			listViewGroup4.Header = "下载引擎";
			listViewGroup4.Name = "listViewGroup2";
			this.lv.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup3,
            listViewGroup4});
			this.lv.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lv.HideSelection = false;
			this.lv.Location = new System.Drawing.Point(15, 56);
			this.lv.MultiSelect = false;
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(690, 415);
			this.lv.SmallImageList = this.il;
			this.lv.TabIndex = 0;
			this.lv.UseCompatibleStateImageBehavior = false;
			this.lv.View = System.Windows.Forms.View.Details;
			// 
			// colName
			// 
			this.colName.Text = "引擎名";
			this.colName.Width = 224;
			// 
			// colVersion
			// 
			this.colVersion.Text = "版本";
			this.colVersion.Width = 111;
			// 
			// colReport
			// 
			this.colReport.Text = "引擎报告是否需要科学上网";
			this.colReport.Width = 168;
			// 
			// colResult
			// 
			this.colResult.Text = "测试结果";
			this.colResult.Width = 118;
			// 
			// il
			// 
			this.il.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.il.ImageSize = new System.Drawing.Size(20, 20);
			this.il.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnOk.Image = global::BtResourceGrabber.Properties.Resources.Clear_Green_Button;
			this.btnOk.Location = new System.Drawing.Point(581, 477);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(124, 25);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "确定(&O)";
			this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// btnRecheck
			// 
			this.btnRecheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRecheck.Image = global::BtResourceGrabber.Properties.Resources.stock_repeat;
			this.btnRecheck.Location = new System.Drawing.Point(136, 477);
			this.btnRecheck.Name = "btnRecheck";
			this.btnRecheck.Size = new System.Drawing.Size(118, 25);
			this.btnRecheck.TabIndex = 2;
			this.btnRecheck.Text = "重新检测(&R)";
			this.btnRecheck.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnRecheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnRecheck.UseVisualStyleBackColor = true;
			// 
			// btnSetProxy
			// 
			this.btnSetProxy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSetProxy.Image = global::BtResourceGrabber.Properties.Resources.stock_proxy;
			this.btnSetProxy.Location = new System.Drawing.Point(12, 477);
			this.btnSetProxy.Name = "btnSetProxy";
			this.btnSetProxy.Size = new System.Drawing.Size(118, 25);
			this.btnSetProxy.TabIndex = 1;
			this.btnSetProxy.Text = "设置代理(&O)";
			this.btnSetProxy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSetProxy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSetProxy.UseVisualStyleBackColor = true;
			// 
			// EngineAvailabilityTest
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnOk;
			this.ClientSize = new System.Drawing.Size(717, 507);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnRecheck);
			this.Controls.Add(this.btnSetProxy);
			this.Controls.Add(this.lv);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EngineAvailabilityTest";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "引擎可用性检测";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListView lv;
		private System.Windows.Forms.ImageList il;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colVersion;
		private System.Windows.Forms.ColumnHeader colReport;
		private System.Windows.Forms.ColumnHeader colResult;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnSetProxy;
		private System.Windows.Forms.Button btnRecheck;
	}
}