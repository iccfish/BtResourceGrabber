namespace BtResourceGrabber.UI.Dialogs.Messages
{
	partial class License
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
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.lic = new System.Windows.Forms.RichTextBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.t = new System.Windows.Forms.Timer(this.components);
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Window;
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(653, 62);
			this.panel1.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(123, 29);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(509, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "请阅读以下用户协议，需要您同意才可继续运行。如果您不同意，请关闭此窗口并删除本软件。";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(25, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 27);
			this.label1.TabIndex = 1;
			this.label1.Text = "用户协议";
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.SystemColors.WindowFrame;
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 61);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(653, 1);
			this.panel2.TabIndex = 0;
			// 
			// lic
			// 
			this.lic.BackColor = System.Drawing.SystemColors.Window;
			this.lic.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lic.Location = new System.Drawing.Point(15, 74);
			this.lic.Name = "lic";
			this.lic.ReadOnly = true;
			this.lic.Size = new System.Drawing.Size(617, 364);
			this.lic.TabIndex = 1;
			this.lic.Text = "";
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnOk.Location = new System.Drawing.Point(237, 444);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(188, 41);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "我同意";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// t
			// 
			this.t.Interval = 1000;
			// 
			// License
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(653, 490);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.lic);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "License";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TopMost = true;
			this.Load += new System.EventHandler(this.License_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.RichTextBox lic;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Timer t;
	}
}