namespace BtResourceGrabber.UI.Dialogs.ConfigUi
{
	partial class RuleEditor
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
			this.cbSource = new System.Windows.Forms.ComboBox();
			this.cbBehaviour = new System.Windows.Forms.ComboBox();
			this.txtTest = new System.Windows.Forms.TextBox();
			this.txtRule = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.chkUseReg = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.lblTestResult = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.cbSource);
			this.panel1.Controls.Add(this.cbBehaviour);
			this.panel1.Controls.Add(this.txtTest);
			this.panel1.Controls.Add(this.txtRule);
			this.panel1.Controls.Add(this.btnCancel);
			this.panel1.Controls.Add(this.btnOk);
			this.panel1.Controls.Add(this.chkUseReg);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.lblTestResult);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(487, 253);
			this.panel1.TabIndex = 0;
			// 
			// cbSource
			// 
			this.cbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSource.FormattingEnabled = true;
			this.cbSource.Items.AddRange(new object[] {
            "从搜索结果中移除",
            "在搜索结果上做标记"});
			this.cbSource.Location = new System.Drawing.Point(82, 9);
			this.cbSource.Name = "cbSource";
			this.cbSource.Size = new System.Drawing.Size(372, 25);
			this.cbSource.TabIndex = 3;
			// 
			// cbBehaviour
			// 
			this.cbBehaviour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbBehaviour.FormattingEnabled = true;
			this.cbBehaviour.Items.AddRange(new object[] {
            "从搜索结果中移除",
            "在搜索结果上做标记"});
			this.cbBehaviour.Location = new System.Drawing.Point(82, 160);
			this.cbBehaviour.Name = "cbBehaviour";
			this.cbBehaviour.Size = new System.Drawing.Size(372, 25);
			this.cbBehaviour.TabIndex = 3;
			// 
			// txtTest
			// 
			this.txtTest.Location = new System.Drawing.Point(82, 126);
			this.txtTest.Name = "txtTest";
			this.txtTest.Size = new System.Drawing.Size(283, 23);
			this.txtTest.TabIndex = 2;
			// 
			// txtRule
			// 
			this.txtRule.Location = new System.Drawing.Point(82, 43);
			this.txtRule.Multiline = true;
			this.txtRule.Name = "txtRule";
			this.txtRule.Size = new System.Drawing.Size(372, 53);
			this.txtRule.TabIndex = 0;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Image = global::BtResourceGrabber.Properties.Resources.block_16;
			this.btnCancel.Location = new System.Drawing.Point(247, 221);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(89, 29);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "取消(&C)";
			this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Image = global::BtResourceGrabber.Properties.Resources.tick_16;
			this.btnOk.Location = new System.Drawing.Point(152, 221);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(89, 29);
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "确定(&O)";
			this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// chkUseReg
			// 
			this.chkUseReg.AutoSize = true;
			this.chkUseReg.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkUseReg.Location = new System.Drawing.Point(82, 96);
			this.chkUseReg.Name = "chkUseReg";
			this.chkUseReg.Size = new System.Drawing.Size(99, 21);
			this.chkUseReg.TabIndex = 1;
			this.chkUseReg.Text = "使用正则语法";
			this.chkUseReg.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label5.Location = new System.Drawing.Point(20, 11);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(56, 17);
			this.label5.TabIndex = 0;
			this.label5.Text = "过滤来源";
			// 
			// lblTestResult
			// 
			this.lblTestResult.AutoSize = true;
			this.lblTestResult.Location = new System.Drawing.Point(371, 130);
			this.lblTestResult.Name = "lblTestResult";
			this.lblTestResult.Size = new System.Drawing.Size(80, 17);
			this.lblTestResult.TabIndex = 0;
			this.lblTestResult.Text = "输入文本测试";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label4.Location = new System.Drawing.Point(20, 163);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 17);
			this.label4.TabIndex = 0;
			this.label4.Text = "过滤行为";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(20, 130);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 17);
			this.label2.TabIndex = 0;
			this.label2.Text = "输入测试";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.ForeColor = System.Drawing.Color.DarkGray;
			this.label3.Location = new System.Drawing.Point(266, 97);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(188, 17);
			this.label3.TabIndex = 0;
			this.label3.Text = "每行一条规则，全部匹配才算成功";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(21, 43);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "过滤规则";
			// 
			// RuleEditor
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(487, 253);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RuleEditor";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "规则编辑器";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.CheckBox chkUseReg;
		private System.Windows.Forms.Label lblTestResult;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtTest;
		private System.Windows.Forms.TextBox txtRule;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox cbBehaviour;
		private System.Windows.Forms.ComboBox cbSource;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label3;
	}
}