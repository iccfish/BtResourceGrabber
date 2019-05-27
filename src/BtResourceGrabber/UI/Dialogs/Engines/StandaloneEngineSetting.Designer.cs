namespace BtResourceGrabber.UI.Dialogs.Engines
{
	partial class StandaloneEngineSetting
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.lstEngine = new System.Windows.Forms.ListView();
			this.panel3 = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.il = new System.Windows.Forms.ImageList(this.components);
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lstEngine);
			this.panel1.Controls.Add(this.panel3);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(604, 297);
			this.panel1.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.White;
			this.panel2.Controls.Add(this.label1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(604, 39);
			this.panel2.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(604, 39);
			this.label1.TabIndex = 0;
			this.label1.Text = "默认情况下，助手会将建议合并的搜索引擎尽可能的合并在“综合搜索”中。你可以在此指定强行显示在独立标签页的搜索引擎。";
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
			this.lstEngine.Size = new System.Drawing.Size(604, 220);
			this.lstEngine.SmallImageList = this.il;
			this.lstEngine.TabIndex = 1;
			this.lstEngine.UseCompatibleStateImageBehavior = false;
			this.lstEngine.View = System.Windows.Forms.View.Details;
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.Color.White;
			this.panel3.Controls.Add(this.btnCancel);
			this.panel3.Controls.Add(this.btnOK);
			this.panel3.Controls.Add(this.label2);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel3.Location = new System.Drawing.Point(0, 259);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(604, 38);
			this.panel3.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.Red;
			this.label2.Location = new System.Drawing.Point(12, 10);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(196, 17);
			this.label2.TabIndex = 0;
			this.label2.Text = "(此处的更改需要重启助手才可起效)";
			// 
			// btnOK
			// 
			this.btnOK.Image = global::BtResourceGrabber.Properties.Resources.tick_16;
			this.btnOK.Location = new System.Drawing.Point(428, 4);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(79, 29);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "确定(&O)";
			this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Image = global::BtResourceGrabber.Properties.Resources.block_16;
			this.btnCancel.Location = new System.Drawing.Point(513, 4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(79, 29);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "取消(&C)";
			this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// il
			// 
			this.il.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.il.ImageSize = new System.Drawing.Size(20, 20);
			this.il.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "引擎名";
			this.columnHeader1.Width = 184;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "描述";
			this.columnHeader2.Width = 386;
			// 
			// StandaloneEngineSetting
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(604, 297);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "StandaloneEngineSetting";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "独立引擎控制";
			this.Load += new System.EventHandler(this.StandaloneEngineSetting_Load);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ListView lstEngine;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ImageList il;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
	}
}