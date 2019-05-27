namespace BRG.Engines.BuildIn.UI.Engines.Rarbg
{
	partial class BotCheck
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.pbCap = new System.Windows.Forms.PictureBox();
			this.txtCode = new System.Windows.Forms.TextBox();
			this.chkSkip = new System.Windows.Forms.CheckBox();
			this.lblStatus = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbCap)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(325, 57);
			this.panel1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(50, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(202, 22);
			this.label1.TabIndex = 3;
			this.label1.Text = "请完成验证以便于继续搜索";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::BRG.Engines.BuildIn.Properties.Resources.warning_32;
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.Silver;
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 52);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(325, 5);
			this.panel2.TabIndex = 1;
			// 
			// pbCap
			// 
			this.pbCap.Location = new System.Drawing.Point(12, 74);
			this.pbCap.Name = "pbCap";
			this.pbCap.Size = new System.Drawing.Size(160, 75);
			this.pbCap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pbCap.TabIndex = 1;
			this.pbCap.TabStop = false;
			// 
			// txtCode
			// 
			this.txtCode.Enabled = false;
			this.txtCode.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtCode.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.txtCode.Location = new System.Drawing.Point(178, 114);
			this.txtCode.Name = "txtCode";
			this.txtCode.Size = new System.Drawing.Size(141, 35);
			this.txtCode.TabIndex = 2;
			this.txtCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// chkSkip
			// 
			this.chkSkip.AutoSize = true;
			this.chkSkip.Location = new System.Drawing.Point(12, 167);
			this.chkSkip.Name = "chkSkip";
			this.chkSkip.Size = new System.Drawing.Size(204, 16);
			this.chkSkip.TabIndex = 3;
			this.chkSkip.Text = "下次再遇到认证时，直接跳过搜索";
			this.chkSkip.UseVisualStyleBackColor = true;
			// 
			// lblStatus
			// 
			this.lblStatus.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lblStatus.Location = new System.Drawing.Point(178, 74);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(135, 37);
			this.lblStatus.TabIndex = 4;
			this.lblStatus.Text = "正在加载验证码...";
			// 
			// BotCheck
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(325, 193);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.chkSkip);
			this.Controls.Add(this.txtCode);
			this.Controls.Add(this.pbCap);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BotCheck";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "RARBG网站验证";
			this.Load += new System.EventHandler(this.BotCheck_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbCap)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.PictureBox pbCap;
		private System.Windows.Forms.TextBox txtCode;
		private System.Windows.Forms.CheckBox chkSkip;
		private System.Windows.Forms.Label lblStatus;
	}
}