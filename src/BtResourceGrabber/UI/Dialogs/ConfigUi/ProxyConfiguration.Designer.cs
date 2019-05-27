namespace BtResourceGrabber.UI.Dialogs.ConfigUi
{
	partial class ProxyConfiguration
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.gpCommon = new System.Windows.Forms.GroupBox();
			this.nudCommonPort = new System.Windows.Forms.NumericUpDown();
			this.txtCommonPwd = new System.Windows.Forms.TextBox();
			this.txtCommonUser = new System.Windows.Forms.TextBox();
			this.txtCommonHost = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.gpGfw = new System.Windows.Forms.GroupBox();
			this.nudGfwPort = new System.Windows.Forms.NumericUpDown();
			this.txtGfwPwd = new System.Windows.Forms.TextBox();
			this.txtGfwUser = new System.Windows.Forms.TextBox();
			this.txtGfwHost = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.chkEnableProxy = new System.Windows.Forms.CheckBox();
			this.chkSame = new System.Windows.Forms.CheckBox();
			this.chkUseAllGfw = new System.Windows.Forms.CheckBox();
			this.label9 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.gpCommon.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudCommonPort)).BeginInit();
			this.gpGfw.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudGfwPort)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.btnOk, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnCancel, 1, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(182, 376);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(190, 35);
			this.tableLayoutPanel1.TabIndex = 4;
			// 
			// btnOk
			// 
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
			// gpCommon
			// 
			this.gpCommon.Controls.Add(this.nudCommonPort);
			this.gpCommon.Controls.Add(this.txtCommonPwd);
			this.gpCommon.Controls.Add(this.txtCommonUser);
			this.gpCommon.Controls.Add(this.txtCommonHost);
			this.gpCommon.Controls.Add(this.label4);
			this.gpCommon.Controls.Add(this.label3);
			this.gpCommon.Controls.Add(this.label2);
			this.gpCommon.Controls.Add(this.label1);
			this.gpCommon.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.gpCommon.Location = new System.Drawing.Point(12, 24);
			this.gpCommon.Name = "gpCommon";
			this.gpCommon.Size = new System.Drawing.Size(360, 87);
			this.gpCommon.TabIndex = 1;
			this.gpCommon.TabStop = false;
			this.gpCommon.Text = "常规联网代理";
			// 
			// nudCommonPort
			// 
			this.nudCommonPort.Location = new System.Drawing.Point(223, 20);
			this.nudCommonPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.nudCommonPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudCommonPort.Name = "nudCommonPort";
			this.nudCommonPort.Size = new System.Drawing.Size(129, 23);
			this.nudCommonPort.TabIndex = 1;
			this.nudCommonPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nudCommonPort.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
			// 
			// txtCommonPwd
			// 
			this.txtCommonPwd.Location = new System.Drawing.Point(223, 52);
			this.txtCommonPwd.Name = "txtCommonPwd";
			this.txtCommonPwd.Size = new System.Drawing.Size(134, 23);
			this.txtCommonPwd.TabIndex = 3;
			this.txtCommonPwd.UseSystemPasswordChar = true;
			// 
			// txtCommonUser
			// 
			this.txtCommonUser.Location = new System.Drawing.Point(63, 52);
			this.txtCommonUser.Name = "txtCommonUser";
			this.txtCommonUser.Size = new System.Drawing.Size(121, 23);
			this.txtCommonUser.TabIndex = 2;
			// 
			// txtCommonHost
			// 
			this.txtCommonHost.Location = new System.Drawing.Point(63, 20);
			this.txtCommonHost.Name = "txtCommonHost";
			this.txtCommonHost.Size = new System.Drawing.Size(121, 23);
			this.txtCommonHost.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(190, 56);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(32, 17);
			this.label4.TabIndex = 3;
			this.label4.Text = "密码";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(16, 55);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 17);
			this.label3.TabIndex = 2;
			this.label3.Text = "用户名";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(190, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 17);
			this.label2.TabIndex = 1;
			this.label2.Text = "端口";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "服务器";
			// 
			// gpGfw
			// 
			this.gpGfw.Controls.Add(this.nudGfwPort);
			this.gpGfw.Controls.Add(this.txtGfwPwd);
			this.gpGfw.Controls.Add(this.txtGfwUser);
			this.gpGfw.Controls.Add(this.txtGfwHost);
			this.gpGfw.Controls.Add(this.label5);
			this.gpGfw.Controls.Add(this.label6);
			this.gpGfw.Controls.Add(this.label7);
			this.gpGfw.Controls.Add(this.label8);
			this.gpGfw.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.gpGfw.Location = new System.Drawing.Point(10, 140);
			this.gpGfw.Name = "gpGfw";
			this.gpGfw.Size = new System.Drawing.Size(360, 87);
			this.gpGfw.TabIndex = 3;
			this.gpGfw.TabStop = false;
			this.gpGfw.Text = "科学上网代理服务器";
			// 
			// nudGfwPort
			// 
			this.nudGfwPort.Location = new System.Drawing.Point(223, 21);
			this.nudGfwPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.nudGfwPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudGfwPort.Name = "nudGfwPort";
			this.nudGfwPort.Size = new System.Drawing.Size(129, 23);
			this.nudGfwPort.TabIndex = 1;
			this.nudGfwPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nudGfwPort.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
			// 
			// txtGfwPwd
			// 
			this.txtGfwPwd.Location = new System.Drawing.Point(223, 52);
			this.txtGfwPwd.Name = "txtGfwPwd";
			this.txtGfwPwd.Size = new System.Drawing.Size(134, 23);
			this.txtGfwPwd.TabIndex = 3;
			this.txtGfwPwd.UseSystemPasswordChar = true;
			// 
			// txtGfwUser
			// 
			this.txtGfwUser.Location = new System.Drawing.Point(63, 52);
			this.txtGfwUser.Name = "txtGfwUser";
			this.txtGfwUser.Size = new System.Drawing.Size(121, 23);
			this.txtGfwUser.TabIndex = 2;
			// 
			// txtGfwHost
			// 
			this.txtGfwHost.Location = new System.Drawing.Point(63, 20);
			this.txtGfwHost.Name = "txtGfwHost";
			this.txtGfwHost.Size = new System.Drawing.Size(121, 23);
			this.txtGfwHost.TabIndex = 0;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(190, 56);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(32, 17);
			this.label5.TabIndex = 3;
			this.label5.Text = "密码";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(16, 55);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(44, 17);
			this.label6.TabIndex = 2;
			this.label6.Text = "用户名";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(190, 24);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(32, 17);
			this.label7.TabIndex = 1;
			this.label7.Text = "端口";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(16, 24);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(44, 17);
			this.label8.TabIndex = 0;
			this.label8.Text = "服务器";
			// 
			// chkEnableProxy
			// 
			this.chkEnableProxy.AutoSize = true;
			this.chkEnableProxy.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkEnableProxy.Location = new System.Drawing.Point(196, 22);
			this.chkEnableProxy.Name = "chkEnableProxy";
			this.chkEnableProxy.Size = new System.Drawing.Size(111, 21);
			this.chkEnableProxy.TabIndex = 0;
			this.chkEnableProxy.Text = "启用代理服务器";
			this.chkEnableProxy.UseVisualStyleBackColor = true;
			// 
			// chkSame
			// 
			this.chkSame.AutoSize = true;
			this.chkSame.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkSame.Location = new System.Drawing.Point(196, 138);
			this.chkSame.Name = "chkSame";
			this.chkSame.Size = new System.Drawing.Size(171, 21);
			this.chkSame.TabIndex = 2;
			this.chkSame.Text = "和常规联网代理服务器相同";
			this.chkSame.UseVisualStyleBackColor = true;
			// 
			// chkUseAllGfw
			// 
			this.chkUseAllGfw.AutoSize = true;
			this.chkUseAllGfw.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkUseAllGfw.Location = new System.Drawing.Point(12, 233);
			this.chkUseAllGfw.Name = "chkUseAllGfw";
			this.chkUseAllGfw.Size = new System.Drawing.Size(219, 21);
			this.chkUseAllGfw.TabIndex = 5;
			this.chkUseAllGfw.Text = "所有引擎均使用科学上网代理服务器";
			this.chkUseAllGfw.UseVisualStyleBackColor = true;
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label9.Location = new System.Drawing.Point(34, 260);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(338, 74);
			this.label9.TabIndex = 6;
			this.label9.Text = "特殊的关键字将可能导致您到搜索引擎之间的网络连接同样被GFW和谐。\r\n选择此选项将会强制所有搜索引擎链接均通过科学上网代理，而无视引擎自己报告的特性。";
			// 
			// ProxyConfiguration
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(388, 423);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.chkUseAllGfw);
			this.Controls.Add(this.chkEnableProxy);
			this.Controls.Add(this.chkSame);
			this.Controls.Add(this.gpGfw);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.gpCommon);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProxyConfiguration";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "代理设置";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.gpCommon.ResumeLayout(false);
			this.gpCommon.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudCommonPort)).EndInit();
			this.gpGfw.ResumeLayout(false);
			this.gpGfw.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudGfwPort)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox gpCommon;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown nudCommonPort;
		private System.Windows.Forms.TextBox txtCommonPwd;
		private System.Windows.Forms.TextBox txtCommonUser;
		private System.Windows.Forms.TextBox txtCommonHost;
		private System.Windows.Forms.GroupBox gpGfw;
		private System.Windows.Forms.NumericUpDown nudGfwPort;
		private System.Windows.Forms.TextBox txtGfwPwd;
		private System.Windows.Forms.TextBox txtGfwUser;
		private System.Windows.Forms.TextBox txtGfwHost;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.CheckBox chkEnableProxy;
		private System.Windows.Forms.CheckBox chkSame;
		private System.Windows.Forms.CheckBox chkUseAllGfw;
		private System.Windows.Forms.Label label9;
	}
}