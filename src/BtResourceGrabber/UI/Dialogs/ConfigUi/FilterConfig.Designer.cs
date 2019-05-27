namespace BtResourceGrabber.UI.Dialogs.ConfigUi
{
	partial class FilterConfig
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterConfig));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.lvRss = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ilLv = new System.Windows.Forms.ImageList(this.components);
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.lvCustomize = new System.Windows.Forms.ListView();
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsAdd = new System.Windows.Forms.ToolStripButton();
			this.tsEdit = new System.Windows.Forms.ToolStripButton();
			this.tsRemove = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsExport = new System.Windows.Forms.ToolStripButton();
			this.ilTab = new System.Windows.Forms.ImageList(this.components);
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.ImageList = this.ilTab;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(535, 366);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.lvRss);
			this.tabPage1.ImageIndex = 1;
			this.tabPage1.Location = new System.Drawing.Point(4, 23);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(527, 339);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "过滤规则订阅";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// lvRss
			// 
			this.lvRss.CheckBoxes = true;
			this.lvRss.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader5});
			this.lvRss.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvRss.FullRowSelect = true;
			this.lvRss.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvRss.HideSelection = false;
			this.lvRss.Location = new System.Drawing.Point(3, 3);
			this.lvRss.Name = "lvRss";
			this.lvRss.Size = new System.Drawing.Size(521, 333);
			this.lvRss.SmallImageList = this.ilLv;
			this.lvRss.TabIndex = 0;
			this.lvRss.UseCompatibleStateImageBehavior = false;
			this.lvRss.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "规则名";
			this.columnHeader1.Width = 300;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "更新时间";
			this.columnHeader4.Width = 120;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "下载次数";
			// 
			// ilLv
			// 
			this.ilLv.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.ilLv.ImageSize = new System.Drawing.Size(24, 24);
			this.ilLv.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.lvCustomize);
			this.tabPage2.Controls.Add(this.toolStrip1);
			this.tabPage2.ImageIndex = 0;
			this.tabPage2.Location = new System.Drawing.Point(4, 23);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(527, 339);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "自定义规则";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// lvCustomize
			// 
			this.lvCustomize.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3});
			this.lvCustomize.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvCustomize.FullRowSelect = true;
			this.lvCustomize.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvCustomize.HideSelection = false;
			this.lvCustomize.Location = new System.Drawing.Point(3, 28);
			this.lvCustomize.Name = "lvCustomize";
			this.lvCustomize.Size = new System.Drawing.Size(521, 308);
			this.lvCustomize.SmallImageList = this.imageList1;
			this.lvCustomize.TabIndex = 0;
			this.lvCustomize.UseCompatibleStateImageBehavior = false;
			this.lvCustomize.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "过滤规则";
			this.columnHeader2.Width = 439;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "正则？";
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(1, 24);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAdd,
            this.tsEdit,
            this.tsRemove,
            this.toolStripSeparator1,
            this.tsExport});
			this.toolStrip1.Location = new System.Drawing.Point(3, 3);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(521, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsAdd
			// 
			this.tsAdd.Image = global::BtResourceGrabber.Properties.Resources.plus_16;
			this.tsAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAdd.Name = "tsAdd";
			this.tsAdd.Size = new System.Drawing.Size(76, 22);
			this.tsAdd.Text = "添加规则";
			// 
			// tsEdit
			// 
			this.tsEdit.Image = global::BtResourceGrabber.Properties.Resources.edit;
			this.tsEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsEdit.Name = "tsEdit";
			this.tsEdit.Size = new System.Drawing.Size(76, 22);
			this.tsEdit.Text = "编辑规则";
			// 
			// tsRemove
			// 
			this.tsRemove.Image = global::BtResourceGrabber.Properties.Resources.block_16;
			this.tsRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRemove.Name = "tsRemove";
			this.tsRemove.Size = new System.Drawing.Size(76, 22);
			this.tsRemove.Text = "删除规则";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsExport
			// 
			this.tsExport.Image = global::BtResourceGrabber.Properties.Resources.letter_16;
			this.tsExport.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsExport.Name = "tsExport";
			this.tsExport.Size = new System.Drawing.Size(76, 22);
			this.tsExport.Text = "导出规则";
			// 
			// ilTab
			// 
			this.ilTab.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTab.ImageStream")));
			this.ilTab.TransparentColor = System.Drawing.Color.Transparent;
			this.ilTab.Images.SetKeyName(0, "user_16.png");
			this.ilTab.Images.SetKeyName(1, "rss.png");
			// 
			// FilterConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl1);
			this.Name = "FilterConfig";
			this.Load += new System.EventHandler(this.FilterConfig_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.ListView lvRss;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ListView lvCustomize;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsAdd;
		private System.Windows.Forms.ToolStripButton tsRemove;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsExport;
		private System.Windows.Forms.ImageList ilLv;
		private System.Windows.Forms.ImageList ilTab;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ToolStripButton tsEdit;
		private System.Windows.Forms.ImageList imageList1;
	}
}
