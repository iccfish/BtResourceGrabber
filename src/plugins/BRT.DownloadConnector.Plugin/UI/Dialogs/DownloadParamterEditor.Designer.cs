namespace BRT.DownloadConnector.Plugin.UI.Dialogs
{
	partial class DownloadParamterEditor
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
			this.txtName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnBrowser = new System.Windows.Forms.Button();
			this.txtArgs = new System.Windows.Forms.TextBox();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.ofd = new System.Windows.Forms.OpenFileDialog();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.txtName);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.btnCancel);
			this.panel1.Controls.Add(this.btnOk);
			this.panel1.Controls.Add(this.btnBrowser);
			this.panel1.Controls.Add(this.txtArgs);
			this.panel1.Controls.Add(this.txtPath);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(577, 225);
			this.panel1.TabIndex = 1;
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(85, 12);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(395, 23);
			this.txtName.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label4.Location = new System.Drawing.Point(23, 15);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(44, 17);
			this.label4.TabIndex = 5;
			this.label4.Text = "工具名";
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Image = global::BRT.DownloadConnector.Plugin.Properties.Resources.block_16;
			this.btnCancel.Location = new System.Drawing.Point(461, 101);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(91, 30);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "取消(&C)";
			this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOk
			// 
			this.btnOk.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnOk.Image = global::BRT.DownloadConnector.Plugin.Properties.Resources.tick_16;
			this.btnOk.Location = new System.Drawing.Point(364, 101);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(91, 30);
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "确定(&O)";
			this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// btnBrowser
			// 
			this.btnBrowser.Location = new System.Drawing.Point(486, 42);
			this.btnBrowser.Name = "btnBrowser";
			this.btnBrowser.Size = new System.Drawing.Size(66, 23);
			this.btnBrowser.TabIndex = 2;
			this.btnBrowser.Text = "浏览(&B)...";
			this.btnBrowser.UseVisualStyleBackColor = true;
			// 
			// txtArgs
			// 
			this.txtArgs.Location = new System.Drawing.Point(85, 72);
			this.txtArgs.Name = "txtArgs";
			this.txtArgs.Size = new System.Drawing.Size(395, 23);
			this.txtArgs.TabIndex = 3;
			// 
			// txtPath
			// 
			this.txtPath.Location = new System.Drawing.Point(85, 42);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(395, 23);
			this.txtPath.TabIndex = 1;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.White;
			this.panel2.Controls.Add(this.label3);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 148);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(577, 77);
			this.panel2.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.ForeColor = System.Drawing.Color.ForestGreen;
			this.label3.Location = new System.Drawing.Point(0, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(577, 77);
			this.label3.TabIndex = 0;
			this.label3.Text = "调用路径将会最终交由系统执行，因此可以为协议/可执行文件以及任何系统可以打开的路径形式。\r\n当调用程序时，可以使用参数来定义要传递给程序的参数。\r\n路径和参数均可" +
    "以使用占位符，“${url}” 表示下载的资源地址，“${base64(url)}”和“${urlencode(url)}”表示经过Base64编码以及UrlE" +
    "ncode编码的资源地址。";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(23, 75);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 17);
			this.label2.TabIndex = 1;
			this.label2.Text = "调用参数";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(23, 42);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 17);
			this.label1.TabIndex = 2;
			this.label1.Text = "调用路径";
			// 
			// ofd
			// 
			this.ofd.Filter = "可执行文件(*.cmd;*.bat;*.com;*.exe)|*.cmd;*.bat;*.com;*.exe";
			this.ofd.Title = "选择可执行文件...";
			// 
			// DownloadParamterEditor
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(577, 225);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DownloadParamterEditor";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "下载工具编辑";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnBrowser;
		private System.Windows.Forms.TextBox txtArgs;
		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.OpenFileDialog ofd;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label4;
	}
}