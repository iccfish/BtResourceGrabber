namespace BRG.Engines.BuildIn.UI.Engines.Rarbg
{
	partial class RarbgConfig
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
			this.chkSkipVerify = new System.Windows.Forms.CheckBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// chkSkipVerify
			// 
			this.chkSkipVerify.AutoSize = true;
			this.chkSkipVerify.Location = new System.Drawing.Point(25, 23);
			this.chkSkipVerify.Name = "chkSkipVerify";
			this.chkSkipVerify.Size = new System.Drawing.Size(252, 16);
			this.chkSkipVerify.TabIndex = 0;
			this.chkSkipVerify.Text = "如果网站要求输入验证码，则直接跳过搜索";
			this.chkSkipVerify.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.Image = global::BRG.Engines.BuildIn.Properties.Resources.tick_16;
			this.btnOK.Location = new System.Drawing.Point(322, 131);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(115, 31);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "确定(&O)";
			this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// RarbgConfig
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(461, 174);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.chkSkipVerify);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RarbgConfig";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "RARBG引擎设置";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox chkSkipVerify;
		private System.Windows.Forms.Button btnOK;
	}
}