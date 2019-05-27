namespace BtResourceGrabber.UI.Dialogs.ConfigUi
{
	partial class Option
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Option));
			this.btnOk = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.lnkHelpOp = new System.Windows.Forms.LinkLabel();
			this.chkEnableFilterDirective = new System.Windows.Forms.CheckBox();
			this.chkEnablePreview = new System.Windows.Forms.CheckBox();
			this.chkUsingFloat = new System.Windows.Forms.CheckBox();
			this.chkTabMultiLine = new System.Windows.Forms.CheckBox();
			this.chkAutoMark = new System.Windows.Forms.CheckBox();
			this.gpAutoMark = new System.Windows.Forms.GroupBox();
			this.cbAutoMarkType = new System.Windows.Forms.ComboBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.btnMaskDelete = new System.Windows.Forms.Button();
			this.btnMaskEdit = new System.Windows.Forms.Button();
			this.btnMaskAdd = new System.Windows.Forms.Button();
			this.lbMask = new FSLib.Windows.Controls.Common.ListBoxEx();
			this.tabPage6 = new System.Windows.Forms.TabPage();
			this.label5 = new System.Windows.Forms.Label();
			this.chkEnableCloudSaftyCheck = new System.Windows.Forms.CheckBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.label2 = new System.Windows.Forms.Label();
			this.nudTimeout = new FSLib.Windows.Controls.Editor.IntNumbericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.label3 = new System.Windows.Forms.Label();
			this.chkLogDownload = new System.Windows.Forms.CheckBox();
			this.btnClearDownloadHistory = new System.Windows.Forms.Button();
			this.btnClearHistory = new System.Windows.Forms.Button();
			this.chkRecSearchHistory = new System.Windows.Forms.CheckBox();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.lvConfigEngine = new System.Windows.Forms.ListView();
			this.colEngineConfigTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ilEngineConfig = new System.Windows.Forms.ImageList(this.components);
			this.btnEngineConfig = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.il = new System.Windows.Forms.ImageList(this.components);
			this.cdAutoMark = new System.Windows.Forms.ColorDialog();
			this.btnEngineStandalone = new System.Windows.Forms.Button();
			this.btnEngineInfo = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.gpAutoMark.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage6.SuspendLayout();
			this.tabPage3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudTimeout)).BeginInit();
			this.tabPage4.SuspendLayout();
			this.tabPage5.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnOk.Image = global::BtResourceGrabber.Properties.Resources.Clear_Green_Button;
			this.btnOk.Location = new System.Drawing.Point(293, 225);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(151, 53);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "确定(&O)";
			this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage6);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage5);
			this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tabControl1.ImageList = this.il;
			this.tabControl1.Location = new System.Drawing.Point(1, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(456, 223);
			this.tabControl1.TabIndex = 6;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.lnkHelpOp);
			this.tabPage1.Controls.Add(this.chkEnableFilterDirective);
			this.tabPage1.Controls.Add(this.chkEnablePreview);
			this.tabPage1.Controls.Add(this.chkUsingFloat);
			this.tabPage1.Controls.Add(this.chkTabMultiLine);
			this.tabPage1.Controls.Add(this.chkAutoMark);
			this.tabPage1.Controls.Add(this.gpAutoMark);
			this.tabPage1.ImageIndex = 0;
			this.tabPage1.Location = new System.Drawing.Point(4, 26);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(448, 193);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "常规";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// lnkHelpOp
			// 
			this.lnkHelpOp.AutoSize = true;
			this.lnkHelpOp.Location = new System.Drawing.Point(137, 60);
			this.lnkHelpOp.Name = "lnkHelpOp";
			this.lnkHelpOp.Size = new System.Drawing.Size(78, 17);
			this.lnkHelpOp.TabIndex = 3;
			this.lnkHelpOp.TabStop = true;
			this.lnkHelpOp.Text = "更多信息 >>";
			// 
			// chkEnableFilterDirective
			// 
			this.chkEnableFilterDirective.AutoSize = true;
			this.chkEnableFilterDirective.Location = new System.Drawing.Point(23, 59);
			this.chkEnableFilterDirective.Name = "chkEnableFilterDirective";
			this.chkEnableFilterDirective.Size = new System.Drawing.Size(111, 21);
			this.chkEnableFilterDirective.TabIndex = 2;
			this.chkEnableFilterDirective.Text = "启用过滤运算符";
			this.chkEnableFilterDirective.UseVisualStyleBackColor = true;
			// 
			// chkEnablePreview
			// 
			this.chkEnablePreview.AutoSize = true;
			this.chkEnablePreview.Location = new System.Drawing.Point(23, 81);
			this.chkEnablePreview.Name = "chkEnablePreview";
			this.chkEnablePreview.Size = new System.Drawing.Size(219, 21);
			this.chkEnablePreview.TabIndex = 2;
			this.chkEnablePreview.Text = "如果可能的话，启用主窗口即时预览";
			this.chkEnablePreview.UseVisualStyleBackColor = true;
			// 
			// chkUsingFloat
			// 
			this.chkUsingFloat.AutoSize = true;
			this.chkUsingFloat.Location = new System.Drawing.Point(23, 37);
			this.chkUsingFloat.Name = "chkUsingFloat";
			this.chkUsingFloat.Size = new System.Drawing.Size(267, 21);
			this.chkUsingFloat.TabIndex = 2;
			this.chkUsingFloat.Text = "下载任务通知使用主窗口浮窗而不是托盘气泡";
			this.chkUsingFloat.UseVisualStyleBackColor = true;
			// 
			// chkTabMultiLine
			// 
			this.chkTabMultiLine.AutoSize = true;
			this.chkTabMultiLine.Location = new System.Drawing.Point(23, 15);
			this.chkTabMultiLine.Name = "chkTabMultiLine";
			this.chkTabMultiLine.Size = new System.Drawing.Size(231, 21);
			this.chkTabMultiLine.TabIndex = 2;
			this.chkTabMultiLine.Text = "引擎列表标签页显示为多行而不是滚动";
			this.chkTabMultiLine.UseVisualStyleBackColor = true;
			// 
			// chkAutoMark
			// 
			this.chkAutoMark.AutoSize = true;
			this.chkAutoMark.Location = new System.Drawing.Point(23, 130);
			this.chkAutoMark.Name = "chkAutoMark";
			this.chkAutoMark.Size = new System.Drawing.Size(135, 21);
			this.chkAutoMark.TabIndex = 1;
			this.chkAutoMark.Text = "自动标记已下载资源";
			this.chkAutoMark.UseVisualStyleBackColor = true;
			// 
			// gpAutoMark
			// 
			this.gpAutoMark.Controls.Add(this.cbAutoMarkType);
			this.gpAutoMark.Location = new System.Drawing.Point(17, 130);
			this.gpAutoMark.Name = "gpAutoMark";
			this.gpAutoMark.Size = new System.Drawing.Size(232, 61);
			this.gpAutoMark.TabIndex = 0;
			this.gpAutoMark.TabStop = false;
			// 
			// cbAutoMarkType
			// 
			this.cbAutoMarkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbAutoMarkType.FormattingEnabled = true;
			this.cbAutoMarkType.Location = new System.Drawing.Point(16, 26);
			this.cbAutoMarkType.Name = "cbAutoMarkType";
			this.cbAutoMarkType.Size = new System.Drawing.Size(192, 25);
			this.cbAutoMarkType.TabIndex = 0;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.btnMaskDelete);
			this.tabPage2.Controls.Add(this.btnMaskEdit);
			this.tabPage2.Controls.Add(this.btnMaskAdd);
			this.tabPage2.Controls.Add(this.lbMask);
			this.tabPage2.ImageIndex = 1;
			this.tabPage2.Location = new System.Drawing.Point(4, 26);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(448, 193);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "标记";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// btnMaskDelete
			// 
			this.btnMaskDelete.Image = global::BtResourceGrabber.Properties.Resources.delete;
			this.btnMaskDelete.Location = new System.Drawing.Point(351, 88);
			this.btnMaskDelete.Name = "btnMaskDelete";
			this.btnMaskDelete.Size = new System.Drawing.Size(88, 30);
			this.btnMaskDelete.TabIndex = 1;
			this.btnMaskDelete.Text = "删除(&D)";
			this.btnMaskDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnMaskDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnMaskDelete.UseVisualStyleBackColor = true;
			// 
			// btnMaskEdit
			// 
			this.btnMaskEdit.Image = global::BtResourceGrabber.Properties.Resources.edit;
			this.btnMaskEdit.Location = new System.Drawing.Point(351, 52);
			this.btnMaskEdit.Name = "btnMaskEdit";
			this.btnMaskEdit.Size = new System.Drawing.Size(88, 30);
			this.btnMaskEdit.TabIndex = 1;
			this.btnMaskEdit.Text = "编辑(&E)";
			this.btnMaskEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnMaskEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnMaskEdit.UseVisualStyleBackColor = true;
			// 
			// btnMaskAdd
			// 
			this.btnMaskAdd.Image = global::BtResourceGrabber.Properties.Resources.add_16;
			this.btnMaskAdd.Location = new System.Drawing.Point(351, 16);
			this.btnMaskAdd.Name = "btnMaskAdd";
			this.btnMaskAdd.Size = new System.Drawing.Size(88, 30);
			this.btnMaskAdd.TabIndex = 1;
			this.btnMaskAdd.Text = "添加(&A)";
			this.btnMaskAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnMaskAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnMaskAdd.UseVisualStyleBackColor = true;
			// 
			// lbMask
			// 
			this.lbMask.DefaultCaptionFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
			this.lbMask.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.lbMask.FormattingEnabled = true;
			this.lbMask.ItemPadding = new System.Windows.Forms.Padding(5);
			this.lbMask.Location = new System.Drawing.Point(5, 4);
			this.lbMask.Name = "lbMask";
			this.lbMask.Size = new System.Drawing.Size(339, 186);
			this.lbMask.TabIndex = 0;
			// 
			// tabPage6
			// 
			this.tabPage6.Controls.Add(this.label5);
			this.tabPage6.Controls.Add(this.chkEnableCloudSaftyCheck);
			this.tabPage6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tabPage6.ImageIndex = 5;
			this.tabPage6.Location = new System.Drawing.Point(4, 26);
			this.tabPage6.Name = "tabPage6";
			this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage6.Size = new System.Drawing.Size(448, 193);
			this.tabPage6.TabIndex = 5;
			this.tabPage6.Text = "安全";
			this.tabPage6.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.Location = new System.Drawing.Point(36, 42);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(403, 20);
			this.label5.TabIndex = 4;
			this.label5.Text = "禁用云安全机制可提升启动以及搜索速度，但会导致部分预览功能缺失。";
			// 
			// chkEnableCloudSaftyCheck
			// 
			this.chkEnableCloudSaftyCheck.AutoSize = true;
			this.chkEnableCloudSaftyCheck.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkEnableCloudSaftyCheck.Location = new System.Drawing.Point(20, 18);
			this.chkEnableCloudSaftyCheck.Name = "chkEnableCloudSaftyCheck";
			this.chkEnableCloudSaftyCheck.Size = new System.Drawing.Size(170, 21);
			this.chkEnableCloudSaftyCheck.TabIndex = 3;
			this.chkEnableCloudSaftyCheck.Text = "启用云安全举报&&反馈机制";
			this.chkEnableCloudSaftyCheck.UseVisualStyleBackColor = true;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.label2);
			this.tabPage3.Controls.Add(this.nudTimeout);
			this.tabPage3.Controls.Add(this.label1);
			this.tabPage3.ImageIndex = 2;
			this.tabPage3.Location = new System.Drawing.Point(4, 26);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(448, 193);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "网络";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(187, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 17);
			this.label2.TabIndex = 2;
			this.label2.Text = "（秒）";
			// 
			// nudTimeout
			// 
			this.nudTimeout.IntValue = 0;
			this.nudTimeout.Location = new System.Drawing.Point(101, 14);
			this.nudTimeout.Name = "nudTimeout";
			this.nudTimeout.Size = new System.Drawing.Size(68, 23);
			this.nudTimeout.TabIndex = 1;
			this.nudTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(24, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(57, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "网络超时";
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.label3);
			this.tabPage4.Controls.Add(this.chkLogDownload);
			this.tabPage4.Controls.Add(this.btnClearDownloadHistory);
			this.tabPage4.Controls.Add(this.btnClearHistory);
			this.tabPage4.Controls.Add(this.chkRecSearchHistory);
			this.tabPage4.ImageIndex = 3;
			this.tabPage4.Location = new System.Drawing.Point(4, 26);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(448, 193);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "历史";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(21, 71);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(342, 17);
			this.label3.TabIndex = 1;
			this.label3.Text = "(以上历史数据均记录在程序目录下的data目录中，仅本机记录)";
			// 
			// chkLogDownload
			// 
			this.chkLogDownload.AutoSize = true;
			this.chkLogDownload.Location = new System.Drawing.Point(23, 40);
			this.chkLogDownload.Name = "chkLogDownload";
			this.chkLogDownload.Size = new System.Drawing.Size(219, 21);
			this.chkLogDownload.TabIndex = 0;
			this.chkLogDownload.Text = "记录下载历史 (下载提示依赖此选项)";
			this.chkLogDownload.UseVisualStyleBackColor = true;
			// 
			// btnClearDownloadHistory
			// 
			this.btnClearDownloadHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnClearDownloadHistory.Image = global::BtResourceGrabber.Properties.Resources.clear16;
			this.btnClearDownloadHistory.Location = new System.Drawing.Point(156, 165);
			this.btnClearDownloadHistory.Name = "btnClearDownloadHistory";
			this.btnClearDownloadHistory.Size = new System.Drawing.Size(126, 25);
			this.btnClearDownloadHistory.TabIndex = 7;
			this.btnClearDownloadHistory.Text = "清除下载历史(&D)";
			this.btnClearDownloadHistory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnClearDownloadHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnClearDownloadHistory.UseVisualStyleBackColor = true;
			this.btnClearDownloadHistory.Click += new System.EventHandler(this.btnClearDownloadHistory_Click);
			// 
			// btnClearHistory
			// 
			this.btnClearHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnClearHistory.Image = global::BtResourceGrabber.Properties.Resources.clear16;
			this.btnClearHistory.Location = new System.Drawing.Point(24, 165);
			this.btnClearHistory.Name = "btnClearHistory";
			this.btnClearHistory.Size = new System.Drawing.Size(126, 25);
			this.btnClearHistory.TabIndex = 7;
			this.btnClearHistory.Text = "清除搜索历史(&L)";
			this.btnClearHistory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnClearHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnClearHistory.UseVisualStyleBackColor = true;
			this.btnClearHistory.Click += new System.EventHandler(this.btnClearHistory_Click);
			// 
			// chkRecSearchHistory
			// 
			this.chkRecSearchHistory.AutoSize = true;
			this.chkRecSearchHistory.Location = new System.Drawing.Point(23, 18);
			this.chkRecSearchHistory.Name = "chkRecSearchHistory";
			this.chkRecSearchHistory.Size = new System.Drawing.Size(99, 21);
			this.chkRecSearchHistory.TabIndex = 0;
			this.chkRecSearchHistory.Text = "记录搜索历史";
			this.chkRecSearchHistory.UseVisualStyleBackColor = true;
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this.lvConfigEngine);
			this.tabPage5.Controls.Add(this.btnEngineConfig);
			this.tabPage5.Controls.Add(this.label4);
			this.tabPage5.ImageIndex = 4;
			this.tabPage5.Location = new System.Drawing.Point(4, 26);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage5.Size = new System.Drawing.Size(448, 193);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "引擎";
			this.tabPage5.UseVisualStyleBackColor = true;
			// 
			// lvConfigEngine
			// 
			this.lvConfigEngine.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colEngineConfigTitle});
			this.lvConfigEngine.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvConfigEngine.FullRowSelect = true;
			this.lvConfigEngine.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvConfigEngine.HideSelection = false;
			this.lvConfigEngine.Location = new System.Drawing.Point(3, 31);
			this.lvConfigEngine.Name = "lvConfigEngine";
			this.lvConfigEngine.Size = new System.Drawing.Size(442, 159);
			this.lvConfigEngine.SmallImageList = this.ilEngineConfig;
			this.lvConfigEngine.TabIndex = 2;
			this.lvConfigEngine.UseCompatibleStateImageBehavior = false;
			this.lvConfigEngine.View = System.Windows.Forms.View.Details;
			// 
			// colEngineConfigTitle
			// 
			this.colEngineConfigTitle.Text = "引擎名";
			this.colEngineConfigTitle.Width = 400;
			// 
			// ilEngineConfig
			// 
			this.ilEngineConfig.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.ilEngineConfig.ImageSize = new System.Drawing.Size(20, 20);
			this.ilEngineConfig.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// btnEngineConfig
			// 
			this.btnEngineConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnEngineConfig.Image = global::BtResourceGrabber.Properties.Resources._16_tool_a;
			this.btnEngineConfig.Location = new System.Drawing.Point(367, 3);
			this.btnEngineConfig.Name = "btnEngineConfig";
			this.btnEngineConfig.Size = new System.Drawing.Size(76, 26);
			this.btnEngineConfig.TabIndex = 0;
			this.btnEngineConfig.Text = "配置";
			this.btnEngineConfig.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnEngineConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnEngineConfig.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Dock = System.Windows.Forms.DockStyle.Top;
			this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label4.Location = new System.Drawing.Point(3, 3);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(442, 28);
			this.label4.TabIndex = 0;
			this.label4.Text = "以下引擎拥有自己的可配置选项";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// il
			// 
			this.il.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il.ImageStream")));
			this.il.TransparentColor = System.Drawing.Color.Transparent;
			this.il.Images.SetKeyName(0, "home_16.png");
			this.il.Images.SetKeyName(1, "label_16.png");
			this.il.Images.SetKeyName(2, "globe_16.png");
			this.il.Images.SetKeyName(3, "history.png");
			this.il.Images.SetKeyName(4, "gear_16.png");
			this.il.Images.SetKeyName(5, "protection.png");
			// 
			// cdAutoMark
			// 
			this.cdAutoMark.AnyColor = true;
			// 
			// btnEngineStandalone
			// 
			this.btnEngineStandalone.Location = new System.Drawing.Point(7, 225);
			this.btnEngineStandalone.Name = "btnEngineStandalone";
			this.btnEngineStandalone.Size = new System.Drawing.Size(126, 25);
			this.btnEngineStandalone.TabIndex = 8;
			this.btnEngineStandalone.Text = "引擎独立标签页选项";
			this.btnEngineStandalone.UseVisualStyleBackColor = true;
			this.btnEngineStandalone.Click += new System.EventHandler(this.btnEngineStandalone_Click);
			// 
			// btnEngineInfo
			// 
			this.btnEngineInfo.Location = new System.Drawing.Point(7, 253);
			this.btnEngineInfo.Name = "btnEngineInfo";
			this.btnEngineInfo.Size = new System.Drawing.Size(126, 25);
			this.btnEngineInfo.TabIndex = 8;
			this.btnEngineInfo.Text = "引擎统计&&启用设置";
			this.btnEngineInfo.UseVisualStyleBackColor = true;
			// 
			// Option
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnOk;
			this.ClientSize = new System.Drawing.Size(456, 281);
			this.Controls.Add(this.btnEngineInfo);
			this.Controls.Add(this.btnEngineStandalone);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.tabControl1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Option";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "选项";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.gpAutoMark.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage6.ResumeLayout(false);
			this.tabPage6.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudTimeout)).EndInit();
			this.tabPage4.ResumeLayout(false);
			this.tabPage4.PerformLayout();
			this.tabPage5.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.CheckBox chkAutoMark;
		private System.Windows.Forms.GroupBox gpAutoMark;
		private System.Windows.Forms.ColorDialog cdAutoMark;
		private System.Windows.Forms.Button btnClearHistory;
		private System.Windows.Forms.TabPage tabPage2;
		private FSLib.Windows.Controls.Common.ListBoxEx lbMask;
		private System.Windows.Forms.Button btnMaskDelete;
		private System.Windows.Forms.Button btnMaskEdit;
		private System.Windows.Forms.Button btnMaskAdd;
		private System.Windows.Forms.ComboBox cbAutoMarkType;
		private System.Windows.Forms.CheckBox chkTabMultiLine;
		private System.Windows.Forms.CheckBox chkUsingFloat;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.ImageList il;
		private System.Windows.Forms.Label label2;
		private FSLib.Windows.Controls.Editor.IntNumbericUpDown nudTimeout;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chkEnableFilterDirective;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox chkLogDownload;
		private System.Windows.Forms.CheckBox chkRecSearchHistory;
		private System.Windows.Forms.Button btnEngineStandalone;
		private System.Windows.Forms.LinkLabel lnkHelpOp;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.ListView lvConfigEngine;
		private System.Windows.Forms.Button btnEngineConfig;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ImageList ilEngineConfig;
		private System.Windows.Forms.ColumnHeader colEngineConfigTitle;
		private System.Windows.Forms.CheckBox chkEnablePreview;
		private System.Windows.Forms.Button btnClearDownloadHistory;
		private System.Windows.Forms.TabPage tabPage6;
		private System.Windows.Forms.CheckBox chkEnableCloudSaftyCheck;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnEngineInfo;
	}
}