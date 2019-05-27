namespace BtResourceGrabber.UI.Dialogs.ConfigUi
{
	partial class EnginePropertyConfig
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.lstEngine = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.il = new System.Windows.Forms.ImageList(this.components);
			this.panel4 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.lvConfigEngine = new System.Windows.Forms.ListView();
			this.colEngineConfigTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ilEngineConfig = new System.Windows.Forms.ImageList(this.components);
			this.btnEngineConfig = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panel3);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(535, 366);
			this.panel1.TabIndex = 0;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.lstEngine);
			this.panel3.Controls.Add(this.panel4);
			this.panel3.Location = new System.Drawing.Point(12, 176);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(511, 180);
			this.panel3.TabIndex = 1;
			// 
			// lstEngine
			// 
			this.lstEngine.CheckBoxes = true;
			this.lstEngine.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.lstEngine.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstEngine.FullRowSelect = true;
			this.lstEngine.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lstEngine.HideSelection = false;
			this.lstEngine.Location = new System.Drawing.Point(0, 39);
			this.lstEngine.Name = "lstEngine";
			this.lstEngine.Size = new System.Drawing.Size(511, 141);
			this.lstEngine.SmallImageList = this.il;
			this.lstEngine.TabIndex = 2;
			this.lstEngine.UseCompatibleStateImageBehavior = false;
			this.lstEngine.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "引擎名";
			this.columnHeader1.Width = 186;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "描述";
			this.columnHeader2.Width = 285;
			// 
			// il
			// 
			this.il.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.il.ImageSize = new System.Drawing.Size(20, 20);
			this.il.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.label1);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(0, 0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(511, 39);
			this.panel4.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(511, 39);
			this.label1.TabIndex = 0;
			this.label1.Text = "助手会将建议合并的搜索引擎尽可能的合并在“综合搜索”中，在此指定强行显示在独立标签页的搜索引擎。（重启生效）";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.lvConfigEngine);
			this.panel2.Controls.Add(this.btnEngineConfig);
			this.panel2.Controls.Add(this.label4);
			this.panel2.Location = new System.Drawing.Point(12, 6);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(512, 164);
			this.panel2.TabIndex = 0;
			// 
			// lvConfigEngine
			// 
			this.lvConfigEngine.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colEngineConfigTitle});
			this.lvConfigEngine.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvConfigEngine.FullRowSelect = true;
			this.lvConfigEngine.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvConfigEngine.HideSelection = false;
			this.lvConfigEngine.Location = new System.Drawing.Point(0, 28);
			this.lvConfigEngine.Name = "lvConfigEngine";
			this.lvConfigEngine.Size = new System.Drawing.Size(512, 136);
			this.lvConfigEngine.SmallImageList = this.ilEngineConfig;
			this.lvConfigEngine.TabIndex = 5;
			this.lvConfigEngine.UseCompatibleStateImageBehavior = false;
			this.lvConfigEngine.View = System.Windows.Forms.View.Details;
			// 
			// colEngineConfigTitle
			// 
			this.colEngineConfigTitle.Text = "引擎名";
			this.colEngineConfigTitle.Width = 400;
			// 
			// ilEngineConfig
			// 
			this.ilEngineConfig.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.ilEngineConfig.ImageSize = new System.Drawing.Size(20, 20);
			this.ilEngineConfig.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// btnEngineConfig
			// 
			this.btnEngineConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnEngineConfig.Image = global::BtResourceGrabber.Properties.Resources._16_tool_a;
			this.btnEngineConfig.Location = new System.Drawing.Point(433, 0);
			this.btnEngineConfig.Name = "btnEngineConfig";
			this.btnEngineConfig.Size = new System.Drawing.Size(76, 26);
			this.btnEngineConfig.TabIndex = 3;
			this.btnEngineConfig.Text = "配置";
			this.btnEngineConfig.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnEngineConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnEngineConfig.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Dock = System.Windows.Forms.DockStyle.Top;
			this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label4.Location = new System.Drawing.Point(0, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(512, 28);
			this.label4.TabIndex = 4;
			this.label4.Text = "以下引擎拥有自己的可配置选项";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// EnginePropertyConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Name = "EnginePropertyConfig";
			this.panel1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ListView lvConfigEngine;
		private System.Windows.Forms.ColumnHeader colEngineConfigTitle;
		private System.Windows.Forms.Button btnEngineConfig;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ImageList ilEngineConfig;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListView lstEngine;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ImageList il;
	}
}
