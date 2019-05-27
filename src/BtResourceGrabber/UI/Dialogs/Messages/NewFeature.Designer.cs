namespace BtResourceGrabber.UI.Dialogs.Messages
{
	partial class NewFeature
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
			this.pnf = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pnf)).BeginInit();
			this.SuspendLayout();
			// 
			// pnf
			// 
			this.pnf.Location = new System.Drawing.Point(1, 1);
			this.pnf.Name = "pnf";
			this.pnf.Size = new System.Drawing.Size(56, 98);
			this.pnf.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pnf.TabIndex = 0;
			this.pnf.TabStop = false;
			// 
			// NewFeature
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(588, 356);
			this.Controls.Add(this.pnf);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NewFeature";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "新功能介绍";
			this.Load += new System.EventHandler(this.NewFeature_Load);
			((System.ComponentModel.ISupportInitialize)(this.pnf)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pnf;
	}
}