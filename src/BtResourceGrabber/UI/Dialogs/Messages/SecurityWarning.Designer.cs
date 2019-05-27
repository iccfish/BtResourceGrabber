namespace BtResourceGrabber.UI.Dialogs.Messages
{
	partial class SecurityWarning
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SecurityWarning));
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnOk = new System.Windows.Forms.Button();
			this.chkDismiss = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnOk);
			this.panel1.Controls.Add(this.chkDismiss);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(766, 259);
			this.panel1.TabIndex = 1;
			// 
			// btnOk
			// 
			this.btnOk.Image = global::BtResourceGrabber.Properties.Resources.tick_16;
			this.btnOk.Location = new System.Drawing.Point(579, 203);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(167, 44);
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "我知道了(&O)";
			this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// chkDismiss
			// 
			this.chkDismiss.AutoSize = true;
			this.chkDismiss.Location = new System.Drawing.Point(17, 222);
			this.chkDismiss.Name = "chkDismiss";
			this.chkDismiss.Size = new System.Drawing.Size(126, 24);
			this.chkDismiss.TabIndex = 3;
			this.chkDismiss.Text = "不再显示此消息";
			this.chkDismiss.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.DarkRed;
			this.label2.Location = new System.Drawing.Point(84, 50);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(662, 120);
			this.label2.TabIndex = 2;
			this.label2.Text = resources.GetString("label2.Text");
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(84, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(247, 19);
			this.label1.TabIndex = 1;
			this.label1.Text = "请在下载之前仔细鉴别资源是否可靠！";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::BtResourceGrabber.Properties.Resources.warning;
			this.pictureBox1.Location = new System.Drawing.Point(17, 22);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// SecurityWarning
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(766, 259);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SecurityWarning";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "安全警告";
			this.Load += new System.EventHandler(this.SecurityWarning_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.CheckBox chkDismiss;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}