namespace BtResourceGrabber.UI.Dialogs.ConfigUi
{
	partial class NetworkOption
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
			this.chkUseAllGfw = new System.Windows.Forms.CheckBox();
			this.chkSame = new System.Windows.Forms.CheckBox();
			this.gpGfw = new System.Windows.Forms.GroupBox();
			this.nudGfwPort = new System.Windows.Forms.NumericUpDown();
			this.txtGfwPwd = new System.Windows.Forms.TextBox();
			this.txtGfwUser = new System.Windows.Forms.TextBox();
			this.txtGfwHost = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.chkEnableProxy = new System.Windows.Forms.CheckBox();
			this.gpCommon = new System.Windows.Forms.GroupBox();
			this.nudCommonPort = new System.Windows.Forms.NumericUpDown();
			this.txtCommonPwd = new System.Windows.Forms.TextBox();
			this.txtCommonUser = new System.Windows.Forms.TextBox();
			this.txtCommonHost = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.nudTimeout = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.gpGfw.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudGfwPort)).BeginInit();
			this.gpCommon.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudCommonPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudTimeout)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.chkUseAllGfw);
			this.panel1.Controls.Add(this.chkSame);
			this.panel1.Controls.Add(this.gpGfw);
			this.panel1.Controls.Add(this.chkEnableProxy);
			this.panel1.Controls.Add(this.gpCommon);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.nudTimeout);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(535, 366);
			this.panel1.TabIndex = 0;
			// 
			// chkUseAllGfw
			// 
			this.chkUseAllGfw.AutoSize = true;
			this.chkUseAllGfw.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkUseAllGfw.Location = new System.Drawing.Point(29, 201);
			this.chkUseAllGfw.Name = "chkUseAllGfw";
			this.chkUseAllGfw.Size = new System.Drawing.Size(195, 21);
			this.chkUseAllGfw.TabIndex = 10;
			this.chkUseAllGfw.Text = "所有引擎均使用特殊代理服务器";
			this.chkUseAllGfw.UseVisualStyleBackColor = true;
			// 
			// chkSame
			// 
			this.chkSame.AutoSize = true;
			this.chkSame.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkSame.Location = new System.Drawing.Point(317, 106);
			this.chkSame.Name = "chkSame";
			this.chkSame.Size = new System.Drawing.Size(171, 21);
			this.chkSame.TabIndex = 8;
			this.chkSame.Text = "和常规联网代理服务器相同";
			this.chkSame.UseVisualStyleBackColor = true;
			// 
			// gpGfw
			// 
			this.gpGfw.Controls.Add(this.nudGfwPort);
			this.gpGfw.Controls.Add(this.txtGfwPwd);
			this.gpGfw.Controls.Add(this.txtGfwUser);
			this.gpGfw.Controls.Add(this.txtGfwHost);
			this.gpGfw.Controls.Add(this.label7);
			this.gpGfw.Controls.Add(this.label8);
			this.gpGfw.Controls.Add(this.label9);
			this.gpGfw.Controls.Add(this.label10);
			this.gpGfw.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.gpGfw.Location = new System.Drawing.Point(26, 108);
			this.gpGfw.Name = "gpGfw";
			this.gpGfw.Size = new System.Drawing.Size(475, 87);
			this.gpGfw.TabIndex = 9;
			this.gpGfw.TabStop = false;
			this.gpGfw.Text = "特殊代理服务器";
			// 
			// nudGfwPort
			// 
			this.nudGfwPort.Location = new System.Drawing.Point(277, 21);
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
			this.nudGfwPort.Size = new System.Drawing.Size(95, 23);
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
			this.txtGfwPwd.Location = new System.Drawing.Point(277, 52);
			this.txtGfwPwd.Name = "txtGfwPwd";
			this.txtGfwPwd.Size = new System.Drawing.Size(177, 23);
			this.txtGfwPwd.TabIndex = 3;
			this.txtGfwPwd.UseSystemPasswordChar = true;
			// 
			// txtGfwUser
			// 
			this.txtGfwUser.Location = new System.Drawing.Point(63, 52);
			this.txtGfwUser.Name = "txtGfwUser";
			this.txtGfwUser.Size = new System.Drawing.Size(170, 23);
			this.txtGfwUser.TabIndex = 2;
			// 
			// txtGfwHost
			// 
			this.txtGfwHost.Location = new System.Drawing.Point(63, 20);
			this.txtGfwHost.Name = "txtGfwHost";
			this.txtGfwHost.Size = new System.Drawing.Size(170, 23);
			this.txtGfwHost.TabIndex = 0;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(239, 55);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(32, 17);
			this.label7.TabIndex = 3;
			this.label7.Text = "密码";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(16, 55);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(44, 17);
			this.label8.TabIndex = 2;
			this.label8.Text = "用户名";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(239, 24);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(32, 17);
			this.label9.TabIndex = 1;
			this.label9.Text = "端口";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(16, 24);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(44, 17);
			this.label10.TabIndex = 0;
			this.label10.Text = "服务器";
			// 
			// chkEnableProxy
			// 
			this.chkEnableProxy.AutoSize = true;
			this.chkEnableProxy.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkEnableProxy.Location = new System.Drawing.Point(319, 12);
			this.chkEnableProxy.Name = "chkEnableProxy";
			this.chkEnableProxy.Size = new System.Drawing.Size(111, 21);
			this.chkEnableProxy.TabIndex = 6;
			this.chkEnableProxy.Text = "启用代理服务器";
			this.chkEnableProxy.UseVisualStyleBackColor = true;
			// 
			// gpCommon
			// 
			this.gpCommon.Controls.Add(this.nudCommonPort);
			this.gpCommon.Controls.Add(this.txtCommonPwd);
			this.gpCommon.Controls.Add(this.txtCommonUser);
			this.gpCommon.Controls.Add(this.txtCommonHost);
			this.gpCommon.Controls.Add(this.label4);
			this.gpCommon.Controls.Add(this.label3);
			this.gpCommon.Controls.Add(this.label5);
			this.gpCommon.Controls.Add(this.label6);
			this.gpCommon.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.gpCommon.Location = new System.Drawing.Point(26, 15);
			this.gpCommon.Name = "gpCommon";
			this.gpCommon.Size = new System.Drawing.Size(475, 87);
			this.gpCommon.TabIndex = 7;
			this.gpCommon.TabStop = false;
			this.gpCommon.Text = "常规联网代理";
			// 
			// nudCommonPort
			// 
			this.nudCommonPort.Location = new System.Drawing.Point(277, 22);
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
			this.nudCommonPort.Size = new System.Drawing.Size(95, 23);
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
			this.txtCommonPwd.Location = new System.Drawing.Point(277, 54);
			this.txtCommonPwd.Name = "txtCommonPwd";
			this.txtCommonPwd.Size = new System.Drawing.Size(177, 23);
			this.txtCommonPwd.TabIndex = 3;
			this.txtCommonPwd.UseSystemPasswordChar = true;
			// 
			// txtCommonUser
			// 
			this.txtCommonUser.Location = new System.Drawing.Point(63, 52);
			this.txtCommonUser.Name = "txtCommonUser";
			this.txtCommonUser.Size = new System.Drawing.Size(170, 23);
			this.txtCommonUser.TabIndex = 2;
			// 
			// txtCommonHost
			// 
			this.txtCommonHost.Location = new System.Drawing.Point(63, 20);
			this.txtCommonHost.Name = "txtCommonHost";
			this.txtCommonHost.Size = new System.Drawing.Size(170, 23);
			this.txtCommonHost.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(239, 55);
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
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(239, 22);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(32, 17);
			this.label5.TabIndex = 1;
			this.label5.Text = "端口";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(16, 24);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(44, 17);
			this.label6.TabIndex = 0;
			this.label6.Text = "服务器";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(175, 332);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 17);
			this.label2.TabIndex = 5;
			this.label2.Text = "（秒）";
			// 
			// nudTimeout
			// 
			this.nudTimeout.Location = new System.Drawing.Point(101, 330);
			this.nudTimeout.Name = "nudTimeout";
			this.nudTimeout.Size = new System.Drawing.Size(68, 23);
			this.nudTimeout.TabIndex = 4;
			this.nudTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(23, 332);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 17);
			this.label1.TabIndex = 3;
			this.label1.Text = "网络超时";
			// 
			// NetworkOption
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Name = "NetworkOption";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.gpGfw.ResumeLayout(false);
			this.gpGfw.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudGfwPort)).EndInit();
			this.gpCommon.ResumeLayout(false);
			this.gpCommon.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudCommonPort)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudTimeout)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown nudTimeout;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chkEnableProxy;
		private System.Windows.Forms.GroupBox gpCommon;
		private System.Windows.Forms.NumericUpDown nudCommonPort;
		private System.Windows.Forms.TextBox txtCommonPwd;
		private System.Windows.Forms.TextBox txtCommonUser;
		private System.Windows.Forms.TextBox txtCommonHost;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox chkSame;
		private System.Windows.Forms.GroupBox gpGfw;
		private System.Windows.Forms.NumericUpDown nudGfwPort;
		private System.Windows.Forms.TextBox txtGfwPwd;
		private System.Windows.Forms.TextBox txtGfwUser;
		private System.Windows.Forms.TextBox txtGfwHost;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.CheckBox chkUseAllGfw;
	}
}
