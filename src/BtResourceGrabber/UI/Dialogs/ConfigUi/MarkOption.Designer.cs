namespace BtResourceGrabber.UI.Dialogs.ConfigUi
{
	using Controls;

	partial class MarkOption
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
			this.panel2 = new System.Windows.Forms.Panel();
			this.btnMaskDelete = new System.Windows.Forms.Button();
			this.btnMaskEdit = new System.Windows.Forms.Button();
			this.btnMaskAdd = new System.Windows.Forms.Button();
			this.lbMask = new ListBoxEx();
			this.chkAutoMark = new System.Windows.Forms.CheckBox();
			this.gpAutoMark = new System.Windows.Forms.GroupBox();
			this.cbAutoMarkType = new System.Windows.Forms.ComboBox();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.gpAutoMark.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.chkAutoMark);
			this.panel1.Controls.Add(this.gpAutoMark);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(535, 366);
			this.panel1.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.btnMaskDelete);
			this.panel2.Controls.Add(this.btnMaskEdit);
			this.panel2.Controls.Add(this.btnMaskAdd);
			this.panel2.Controls.Add(this.lbMask);
			this.panel2.Location = new System.Drawing.Point(23, 24);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(479, 122);
			this.panel2.TabIndex = 8;
			// 
			// btnMaskDelete
			// 
			this.btnMaskDelete.Image = global::BtResourceGrabber.Properties.Resources.delete;
			this.btnMaskDelete.Location = new System.Drawing.Point(388, 79);
			this.btnMaskDelete.Name = "btnMaskDelete";
			this.btnMaskDelete.Size = new System.Drawing.Size(88, 30);
			this.btnMaskDelete.TabIndex = 3;
			this.btnMaskDelete.Text = "删除(&D)";
			this.btnMaskDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnMaskDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnMaskDelete.UseVisualStyleBackColor = true;
			// 
			// btnMaskEdit
			// 
			this.btnMaskEdit.Image = global::BtResourceGrabber.Properties.Resources.edit;
			this.btnMaskEdit.Location = new System.Drawing.Point(388, 43);
			this.btnMaskEdit.Name = "btnMaskEdit";
			this.btnMaskEdit.Size = new System.Drawing.Size(88, 30);
			this.btnMaskEdit.TabIndex = 4;
			this.btnMaskEdit.Text = "编辑(&E)";
			this.btnMaskEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnMaskEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnMaskEdit.UseVisualStyleBackColor = true;
			// 
			// btnMaskAdd
			// 
			this.btnMaskAdd.Image = global::BtResourceGrabber.Properties.Resources.add_16;
			this.btnMaskAdd.Location = new System.Drawing.Point(388, 7);
			this.btnMaskAdd.Name = "btnMaskAdd";
			this.btnMaskAdd.Size = new System.Drawing.Size(88, 30);
			this.btnMaskAdd.TabIndex = 5;
			this.btnMaskAdd.Text = "添加(&A)";
			this.btnMaskAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnMaskAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnMaskAdd.UseVisualStyleBackColor = true;
			// 
			// lbMask
			// 
			this.lbMask.DefaultCaptionFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
			this.lbMask.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.lbMask.FormattingEnabled = true;
			this.lbMask.ItemPadding = new System.Windows.Forms.Padding(5);
			this.lbMask.Location = new System.Drawing.Point(3, 7);
			this.lbMask.Name = "lbMask";
			this.lbMask.Size = new System.Drawing.Size(379, 108);
			this.lbMask.TabIndex = 2;
			// 
			// chkAutoMark
			// 
			this.chkAutoMark.AutoSize = true;
			this.chkAutoMark.Location = new System.Drawing.Point(32, 265);
			this.chkAutoMark.Name = "chkAutoMark";
			this.chkAutoMark.Size = new System.Drawing.Size(135, 21);
			this.chkAutoMark.TabIndex = 7;
			this.chkAutoMark.Text = "自动标记已下载资源";
			this.chkAutoMark.UseVisualStyleBackColor = true;
			// 
			// gpAutoMark
			// 
			this.gpAutoMark.Controls.Add(this.cbAutoMarkType);
			this.gpAutoMark.Location = new System.Drawing.Point(26, 265);
			this.gpAutoMark.Name = "gpAutoMark";
			this.gpAutoMark.Size = new System.Drawing.Size(232, 61);
			this.gpAutoMark.TabIndex = 6;
			this.gpAutoMark.TabStop = false;
			// 
			// cbAutoMarkType
			// 
			this.cbAutoMarkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbAutoMarkType.FormattingEnabled = true;
			this.cbAutoMarkType.Location = new System.Drawing.Point(16, 26);
			this.cbAutoMarkType.Name = "cbAutoMarkType";
			this.cbAutoMarkType.Size = new System.Drawing.Size(192, 25);
			this.cbAutoMarkType.TabIndex = 0;
			// 
			// MarkOption
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Name = "MarkOption";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.gpAutoMark.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.CheckBox chkAutoMark;
		private System.Windows.Forms.GroupBox gpAutoMark;
		private System.Windows.Forms.ComboBox cbAutoMarkType;
		private System.Windows.Forms.Button btnMaskDelete;
		private System.Windows.Forms.Button btnMaskEdit;
		private System.Windows.Forms.Button btnMaskAdd;
		private ListBoxEx lbMask;
	}
}
