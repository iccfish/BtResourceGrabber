namespace BtResourceGrabber.UI.Dialogs.ConfigUi
{
	partial class GeneralOption
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
			this.lnkHelpOp = new System.Windows.Forms.LinkLabel();
			this.chkEnableFilterDirective = new System.Windows.Forms.CheckBox();
			this.chkEnablePreview = new System.Windows.Forms.CheckBox();
			this.chkUsingFloat = new System.Windows.Forms.CheckBox();
			this.chkTabMultiLine = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lnkHelpOp);
			this.panel1.Controls.Add(this.chkEnableFilterDirective);
			this.panel1.Controls.Add(this.chkEnablePreview);
			this.panel1.Controls.Add(this.chkUsingFloat);
			this.panel1.Controls.Add(this.chkTabMultiLine);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(535, 366);
			this.panel1.TabIndex = 0;
			// 
			// lnkHelpOp
			// 
			this.lnkHelpOp.AutoSize = true;
			this.lnkHelpOp.Location = new System.Drawing.Point(150, 69);
			this.lnkHelpOp.Name = "lnkHelpOp";
			this.lnkHelpOp.Size = new System.Drawing.Size(78, 17);
			this.lnkHelpOp.TabIndex = 10;
			this.lnkHelpOp.TabStop = true;
			this.lnkHelpOp.Text = "更多信息 >>";
			// 
			// chkEnableFilterDirective
			// 
			this.chkEnableFilterDirective.AutoSize = true;
			this.chkEnableFilterDirective.Location = new System.Drawing.Point(36, 68);
			this.chkEnableFilterDirective.Name = "chkEnableFilterDirective";
			this.chkEnableFilterDirective.Size = new System.Drawing.Size(111, 21);
			this.chkEnableFilterDirective.TabIndex = 6;
			this.chkEnableFilterDirective.Text = "启用过滤运算符";
			this.chkEnableFilterDirective.UseVisualStyleBackColor = true;
			// 
			// chkEnablePreview
			// 
			this.chkEnablePreview.AutoSize = true;
			this.chkEnablePreview.Location = new System.Drawing.Point(36, 90);
			this.chkEnablePreview.Name = "chkEnablePreview";
			this.chkEnablePreview.Size = new System.Drawing.Size(219, 21);
			this.chkEnablePreview.TabIndex = 7;
			this.chkEnablePreview.Text = "如果可能的话，启用主窗口即时预览";
			this.chkEnablePreview.UseVisualStyleBackColor = true;
			// 
			// chkUsingFloat
			// 
			this.chkUsingFloat.AutoSize = true;
			this.chkUsingFloat.Location = new System.Drawing.Point(36, 46);
			this.chkUsingFloat.Name = "chkUsingFloat";
			this.chkUsingFloat.Size = new System.Drawing.Size(267, 21);
			this.chkUsingFloat.TabIndex = 8;
			this.chkUsingFloat.Text = "下载任务通知使用主窗口浮窗而不是托盘气泡";
			this.chkUsingFloat.UseVisualStyleBackColor = true;
			// 
			// chkTabMultiLine
			// 
			this.chkTabMultiLine.AutoSize = true;
			this.chkTabMultiLine.Location = new System.Drawing.Point(36, 24);
			this.chkTabMultiLine.Name = "chkTabMultiLine";
			this.chkTabMultiLine.Size = new System.Drawing.Size(231, 21);
			this.chkTabMultiLine.TabIndex = 9;
			this.chkTabMultiLine.Text = "引擎列表标签页显示为多行而不是滚动";
			this.chkTabMultiLine.UseVisualStyleBackColor = true;
			// 
			// GeneralOption
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Name = "GeneralOption";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.LinkLabel lnkHelpOp;
		private System.Windows.Forms.CheckBox chkEnableFilterDirective;
		private System.Windows.Forms.CheckBox chkEnablePreview;
		private System.Windows.Forms.CheckBox chkUsingFloat;
		private System.Windows.Forms.CheckBox chkTabMultiLine;
	}
}
