namespace BtResourceGrabber.UI.Dialogs.ConfigUi
{
	partial class SelectMark
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnChangeBg = new System.Windows.Forms.Button();
			this.btnChangeColor = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.cd = new System.Windows.Forms.ColorDialog();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(3, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "标记名";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(3, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 17);
			this.label2.TabIndex = 0;
			this.label2.Text = "标记颜色";
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(90, 13);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(182, 23);
			this.txtName.TabIndex = 1;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnChangeBg);
			this.panel1.Controls.Add(this.btnChangeColor);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.txtName);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(305, 126);
			this.panel1.TabIndex = 2;
			// 
			// btnChangeBg
			// 
			this.btnChangeBg.BackColor = System.Drawing.SystemColors.Window;
			this.btnChangeBg.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnChangeBg.Location = new System.Drawing.Point(90, 77);
			this.btnChangeBg.Name = "btnChangeBg";
			this.btnChangeBg.Size = new System.Drawing.Size(182, 29);
			this.btnChangeBg.TabIndex = 2;
			this.btnChangeBg.Text = "点击更换颜色";
			this.btnChangeBg.UseVisualStyleBackColor = false;
			this.btnChangeBg.Click += new System.EventHandler(this.btnChangeBg_Click);
			// 
			// btnChangeColor
			// 
			this.btnChangeColor.BackColor = System.Drawing.SystemColors.Window;
			this.btnChangeColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnChangeColor.Location = new System.Drawing.Point(90, 42);
			this.btnChangeColor.Name = "btnChangeColor";
			this.btnChangeColor.Size = new System.Drawing.Size(182, 29);
			this.btnChangeColor.TabIndex = 2;
			this.btnChangeColor.Text = "点击更换颜色";
			this.btnChangeColor.UseVisualStyleBackColor = false;
			this.btnChangeColor.Click += new System.EventHandler(this.btnChangeColor_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(3, 83);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 17);
			this.label3.TabIndex = 0;
			this.label3.Text = "标记背景颜色";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.btnOk, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnCancel, 1, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(129, 144);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(190, 35);
			this.tableLayoutPanel1.TabIndex = 6;
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Image = global::BtResourceGrabber.Properties.Resources.Clear_Green_Button;
			this.btnOk.Location = new System.Drawing.Point(3, 3);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(89, 25);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "确定(&O)";
			this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Image = global::BtResourceGrabber.Properties.Resources.Cancel_Red_Button;
			this.btnCancel.Location = new System.Drawing.Point(98, 3);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(89, 25);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "取消(&C)";
			this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// cd
			// 
			this.cd.AnyColor = true;
			this.cd.FullOpen = true;
			// 
			// SelectMark
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(331, 185);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.panel1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SelectMark";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "新建标记";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnChangeColor;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ColorDialog cd;
		private System.Windows.Forms.Button btnChangeBg;
		private System.Windows.Forms.Label label3;
	}
}