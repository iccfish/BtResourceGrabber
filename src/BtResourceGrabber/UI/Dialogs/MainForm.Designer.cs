namespace BtResourceGrabber.UI.Dialogs
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.panel1 = new System.Windows.Forms.Panel();
			this.cbSortDirection = new System.Windows.Forms.ComboBox();
			this.cbKey = new System.Windows.Forms.ComboBox();
			this.btnGo = new System.Windows.Forms.Button();
			this.cbLoadCount = new System.Windows.Forms.ComboBox();
			this.cbSort = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.ts = new System.Windows.Forms.ToolStrip();
			this.tsTools = new System.Windows.Forms.ToolStripButton();
			this.tsDownloadManually = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsEngineTest = new System.Windows.Forms.ToolStripButton();
			this.tsOption = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsInfos = new System.Windows.Forms.ToolStripButton();
			this.tt = new System.Windows.Forms.ToolTip(this.components);
			this.ss = new System.Windows.Forms.StatusStrip();
			this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			this.stStatistics = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.stStrictFilter = new System.Windows.Forms.ToolStripMenuItem();
			this.stShowMultilineTab = new System.Windows.Forms.ToolStripMenuItem();
			this.tsUsingFloat = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
			this.savePath = new System.Windows.Forms.SaveFileDialog();
			this.tip = new System.Windows.Forms.ToolTip(this.components);
			this.pTip = new System.Windows.Forms.Panel();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.panel4 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.label9 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.el = new BtResourceGrabber.UI.Controls.EngineList();
			this.panel1.SuspendLayout();
			this.ts.SuspendLayout();
			this.ss.SuspendLayout();
			this.pTip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.cbSortDirection);
			this.panel1.Controls.Add(this.cbKey);
			this.panel1.Controls.Add(this.btnGo);
			this.panel1.Controls.Add(this.cbLoadCount);
			this.panel1.Controls.Add(this.cbSort);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 25);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1200, 37);
			this.panel1.TabIndex = 1;
			// 
			// cbSortDirection
			// 
			this.cbSortDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSortDirection.FormattingEnabled = true;
			this.cbSortDirection.Items.AddRange(new object[] {
			"顺序",
			"逆序"});
			this.cbSortDirection.Location = new System.Drawing.Point(539, 6);
			this.cbSortDirection.Name = "cbSortDirection";
			this.cbSortDirection.Size = new System.Drawing.Size(51, 20);
			this.cbSortDirection.TabIndex = 4;
			// 
			// cbKey
			// 
			this.cbKey.FormattingEnabled = true;
			this.cbKey.Location = new System.Drawing.Point(86, 6);
			this.cbKey.Name = "cbKey";
			this.cbKey.Size = new System.Drawing.Size(306, 20);
			this.cbKey.TabIndex = 0;
			// 
			// btnGo
			// 
			this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnGo.ForeColor = System.Drawing.Color.MediumSlateBlue;
			this.btnGo.Image = global::BtResourceGrabber.Properties.Resources.search_globe_24;
			this.btnGo.Location = new System.Drawing.Point(1074, 3);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(120, 31);
			this.btnGo.TabIndex = 3;
			this.btnGo.Text = "搜索资源";
			this.btnGo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnGo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnGo.UseVisualStyleBackColor = true;
			// 
			// cbLoadCount
			// 
			this.cbLoadCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbLoadCount.FormatString = "0\'页\'";
			this.cbLoadCount.FormattingEnabled = true;
			this.cbLoadCount.Location = new System.Drawing.Point(652, 6);
			this.cbLoadCount.Name = "cbLoadCount";
			this.cbLoadCount.Size = new System.Drawing.Size(67, 20);
			this.cbLoadCount.TabIndex = 2;
			// 
			// cbSort
			// 
			this.cbSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSort.FormattingEnabled = true;
			this.cbSort.Location = new System.Drawing.Point(450, 6);
			this.cbSort.Name = "cbSort";
			this.cbSort.Size = new System.Drawing.Size(83, 20);
			this.cbSort.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(596, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 17);
			this.label3.TabIndex = 0;
			this.label3.Text = "每次加载";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(395, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 17);
			this.label2.TabIndex = 0;
			this.label2.Text = "排序方式";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "搜索关键字";
			// 
			// ts
			// 
			this.ts.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.ts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsTools,
			this.tsDownloadManually,
			this.toolStripSeparator1,
			this.tsEngineTest,
			this.tsOption,
			this.toolStripSeparator2,
			this.tsInfos});
			this.ts.Location = new System.Drawing.Point(0, 0);
			this.ts.Name = "ts";
			this.ts.Size = new System.Drawing.Size(1200, 25);
			this.ts.TabIndex = 0;
			this.ts.Text = "toolStrip1";
			// 
			// tsTools
			// 
			this.tsTools.Image = global::BtResourceGrabber.Properties.Resources._16_tool_a;
			this.tsTools.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsTools.Name = "tsTools";
			this.tsTools.Size = new System.Drawing.Size(91, 22);
			this.tsTools.Text = "BT种子工具";
			this.tsTools.Visible = false;
			// 
			// tsDownloadManually
			// 
			this.tsDownloadManually.Image = global::BtResourceGrabber.Properties.Resources.down_161;
			this.tsDownloadManually.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsDownloadManually.Name = "tsDownloadManually";
			this.tsDownloadManually.Size = new System.Drawing.Size(96, 22);
			this.tsDownloadManually.Text = "手动下载(&M)";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsEngineTest
			// 
			this.tsEngineTest.Image = global::BtResourceGrabber.Properties.Resources.testtube;
			this.tsEngineTest.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsEngineTest.Name = "tsEngineTest";
			this.tsEngineTest.Size = new System.Drawing.Size(76, 22);
			this.tsEngineTest.Text = "引擎测试";
			// 
			// tsOption
			// 
			this.tsOption.Image = global::BtResourceGrabber.Properties.Resources.gear_16;
			this.tsOption.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsOption.Name = "tsOption";
			this.tsOption.Size = new System.Drawing.Size(52, 22);
			this.tsOption.Text = "选项";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// tsInfos
			// 
			this.tsInfos.Image = global::BtResourceGrabber.Properties.Resources.plugin;
			this.tsInfos.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsInfos.Name = "tsInfos";
			this.tsInfos.Size = new System.Drawing.Size(76, 22);
			this.tsInfos.Text = "插件信息";
			// 
			// tt
			// 
			this.tt.AutomaticDelay = 0;
			this.tt.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.tt.ToolTipTitle = "提示";
			// 
			// ss
			// 
			this.ss.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripDropDownButton1,
			this.toolStripStatusLabel2,
			this.toolStripStatusLabel4,
			this.toolStripStatusLabel3,
			this.toolStripStatusLabel7,
			this.toolStripStatusLabel1,
			this.toolStripStatusLabel6});
			this.ss.Location = new System.Drawing.Point(0, 617);
			this.ss.Name = "ss";
			this.ss.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.ss.Size = new System.Drawing.Size(1200, 23);
			this.ss.SizingGrip = false;
			this.ss.TabIndex = 2;
			this.ss.Text = "statusStrip1";
			// 
			// toolStripDropDownButton1
			// 
			this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.stStatistics,
			this.toolStripMenuItem1,
			this.stStrictFilter,
			this.stShowMultilineTab,
			this.tsUsingFloat});
			this.toolStripDropDownButton1.Image = global::BtResourceGrabber.Properties.Resources._16_tool_a;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new System.Drawing.Size(85, 21);
			this.toolStripDropDownButton1.Text = "界面选项";
			// 
			// stStatistics
			// 
			this.stStatistics.Name = "stStatistics";
			this.stStatistics.Size = new System.Drawing.Size(244, 22);
			this.stStatistics.Text = "引擎统计";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(241, 6);
			// 
			// stStrictFilter
			// 
			this.stStrictFilter.CheckOnClick = true;
			this.stStrictFilter.Name = "stStrictFilter";
			this.stStrictFilter.Size = new System.Drawing.Size(244, 22);
			this.stStrictFilter.Text = "严格按标题过滤";
			// 
			// stShowMultilineTab
			// 
			this.stShowMultilineTab.CheckOnClick = true;
			this.stShowMultilineTab.Name = "stShowMultilineTab";
			this.stShowMultilineTab.Size = new System.Drawing.Size(244, 22);
			this.stShowMultilineTab.Text = "显示多行搜索标签";
			// 
			// tsUsingFloat
			// 
			this.tsUsingFloat.CheckOnClick = true;
			this.tsUsingFloat.Name = "tsUsingFloat";
			this.tsUsingFloat.Size = new System.Drawing.Size(244, 22);
			this.tsUsingFloat.Text = "使用浮窗通知而不是任务栏气泡";
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(1100, 18);
			this.toolStripStatusLabel2.Spring = true;
			// 
			// toolStripStatusLabel4
			// 
			this.toolStripStatusLabel4.Image = global::BtResourceGrabber.Properties.Resources.globe_16;
			this.toolStripStatusLabel4.IsLink = true;
			this.toolStripStatusLabel4.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.toolStripStatusLabel4.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
			this.toolStripStatusLabel4.Size = new System.Drawing.Size(84, 18);
			this.toolStripStatusLabel4.Tag = "http://www.fishlee.net/soft/bt_resouce_grabber/index.html";
			this.toolStripStatusLabel4.Text = "比目鱼官网";
			this.toolStripStatusLabel4.Visible = false;
			// 
			// toolStripStatusLabel3
			// 
			this.toolStripStatusLabel3.Image = global::BtResourceGrabber.Properties.Resources.globe_16;
			this.toolStripStatusLabel3.IsLink = true;
			this.toolStripStatusLabel3.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.toolStripStatusLabel3.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
			this.toolStripStatusLabel3.Size = new System.Drawing.Size(60, 18);
			this.toolStripStatusLabel3.Tag = "http://ask.fishlee.net/category-6";
			this.toolStripStatusLabel3.Text = "知识库";
			this.toolStripStatusLabel3.Visible = false;
			// 
			// toolStripStatusLabel7
			// 
			this.toolStripStatusLabel7.Image = global::BtResourceGrabber.Properties.Resources.globe_16;
			this.toolStripStatusLabel7.IsLink = true;
			this.toolStripStatusLabel7.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.toolStripStatusLabel7.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
			this.toolStripStatusLabel7.Size = new System.Drawing.Size(72, 18);
			this.toolStripStatusLabel7.Tag = "http://bbs.fishlee.net/forum-61-1.html";
			this.toolStripStatusLabel7.Text = "反馈论坛";
			this.toolStripStatusLabel7.Visible = false;
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Image = global::BtResourceGrabber.Properties.Resources.sina_mb_logo_16;
			this.toolStripStatusLabel1.IsLink = true;
			this.toolStripStatusLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.toolStripStatusLabel1.LinkColor = System.Drawing.Color.Green;
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(72, 18);
			this.toolStripStatusLabel1.Tag = "http://weibo.com/imcfish/";
			this.toolStripStatusLabel1.Text = "渣浪微博";
			this.toolStripStatusLabel1.Visible = false;
			// 
			// toolStripStatusLabel6
			// 
			this.toolStripStatusLabel6.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.toolStripStatusLabel6.Image = global::BtResourceGrabber.Properties.Resources._199;
			this.toolStripStatusLabel6.IsLink = true;
			this.toolStripStatusLabel6.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.toolStripStatusLabel6.LinkColor = System.Drawing.Color.DarkViolet;
			this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
			this.toolStripStatusLabel6.Size = new System.Drawing.Size(72, 18);
			this.toolStripStatusLabel6.Tag = "http://www.fishlee.net/soft/bt_resouce_grabber/donation.html";
			this.toolStripStatusLabel6.Text = "捐助木魚";
			this.toolStripStatusLabel6.Visible = false;
			// 
			// tip
			// 
			this.tip.AutomaticDelay = 0;
			this.tip.ShowAlways = true;
			this.tip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.tip.ToolTipTitle = "比目鱼";
			// 
			// pTip
			// 
			this.pTip.BackColor = System.Drawing.SystemColors.Window;
			this.pTip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pTip.Controls.Add(this.label11);
			this.pTip.Controls.Add(this.label10);
			this.pTip.Controls.Add(this.label8);
			this.pTip.Controls.Add(this.panel4);
			this.pTip.Controls.Add(this.panel3);
			this.pTip.Controls.Add(this.label9);
			this.pTip.Controls.Add(this.label4);
			this.pTip.Controls.Add(this.label7);
			this.pTip.Controls.Add(this.panel2);
			this.pTip.Controls.Add(this.label5);
			this.pTip.Controls.Add(this.label6);
			this.pTip.Controls.Add(this.pictureBox2);
			this.pTip.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pTip.Location = new System.Drawing.Point(352, 131);
			this.pTip.Name = "pTip";
			this.pTip.Size = new System.Drawing.Size(544, 233);
			this.pTip.TabIndex = 6;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label11.ForeColor = System.Drawing.Color.DarkGray;
			this.label11.Location = new System.Drawing.Point(315, 214);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(224, 17);
			this.label11.TabIndex = 5;
			this.label11.Text = "如果有疑问或建议，请前往论坛或知识库";
			// 
			// label10
			// 
			this.label10.ForeColor = System.Drawing.Color.ForestGreen;
			this.label10.Location = new System.Drawing.Point(358, 83);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(183, 125);
			this.label10.TabIndex = 4;
			this.label10.Text = "尽量避免执行下载的资源中的任何可执行文件。\r\n如果需要，尽量在打开文件前杀毒。";
			// 
			// label8
			// 
			this.label8.ForeColor = System.Drawing.Color.RoyalBlue;
			this.label8.Location = new System.Drawing.Point(185, 83);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(183, 125);
			this.label8.TabIndex = 4;
			this.label8.Text = "建议先下载种子，无法下载的直接复制磁力链。\r\n建议用各种云盘和离线下载下载资源，否则速度可能较慢。\r\n如果资源无源，请尝试不同的资源。尽量避免下载无关信息过多的资" +
	"源。";
			// 
			// panel4
			// 
			this.panel4.BackColor = System.Drawing.Color.ForestGreen;
			this.panel4.Location = new System.Drawing.Point(361, 75);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(167, 1);
			this.panel4.TabIndex = 3;
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.Color.RoyalBlue;
			this.panel3.Location = new System.Drawing.Point(188, 75);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(167, 1);
			this.panel3.TabIndex = 3;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label9.ForeColor = System.Drawing.Color.ForestGreen;
			this.label9.Location = new System.Drawing.Point(358, 57);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(56, 17);
			this.label9.TabIndex = 2;
			this.label9.Text = "安全提醒";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ForeColor = System.Drawing.Color.Crimson;
			this.label4.Location = new System.Drawing.Point(8, 83);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(159, 85);
			this.label4.TabIndex = 4;
			this.label4.Text = "· 软件资源\r\n· 正在上映的影视\r\n· 过于冷门的资源\r\n· 文件名中包含exe的资源\r\n· 大小与预期明显不符的资源";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label7.ForeColor = System.Drawing.Color.RoyalBlue;
			this.label7.Location = new System.Drawing.Point(185, 57);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(56, 17);
			this.label7.TabIndex = 2;
			this.label7.Text = "下载提示";
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.Crimson;
			this.panel2.Location = new System.Drawing.Point(15, 75);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(167, 1);
			this.panel2.TabIndex = 3;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label5.ForeColor = System.Drawing.Color.Crimson;
			this.label5.Location = new System.Drawing.Point(13, 57);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(140, 17);
			this.label5.TabIndex = 2;
			this.label5.Text = "不建议用来搜索以下内容";
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label6.Location = new System.Drawing.Point(44, 18);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(495, 21);
			this.label6.TabIndex = 1;
			this.label6.Text = "如果搜索结果出现错误或无法搜索，请尝试点击引擎图标直接访问网站 :)";
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = global::BtResourceGrabber.Properties.Resources.promotion;
			this.pictureBox2.Location = new System.Drawing.Point(6, 12);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(32, 32);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox2.TabIndex = 0;
			this.pictureBox2.TabStop = false;
			// 
			// el
			// 
			this.el.Dock = System.Windows.Forms.DockStyle.Fill;
			this.el.Location = new System.Drawing.Point(0, 62);
			this.el.Name = "el";
			this.el.Size = new System.Drawing.Size(1200, 555);
			this.el.TabIndex = 5;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1200, 640);
			this.Controls.Add(this.pTip);
			this.Controls.Add(this.el);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.ts);
			this.Controls.Add(this.ss);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "比目鱼 v{1} [{0}]";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ts.ResumeLayout(false);
			this.ts.PerformLayout();
			this.ss.ResumeLayout(false);
			this.ss.PerformLayout();
			this.pTip.ResumeLayout(false);
			this.pTip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnGo;
		private System.Windows.Forms.ComboBox cbLoadCount;
		private System.Windows.Forms.ComboBox cbSort;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ToolStrip ts;
		private System.Windows.Forms.ToolStripButton tsTools;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolTip tt;
		private System.Windows.Forms.ToolStripButton tsInfos;
		private System.Windows.Forms.ComboBox cbKey;
		private System.Windows.Forms.ComboBox cbSortDirection;
		private System.Windows.Forms.ToolStripButton tsOption;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.StatusStrip ss;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.SaveFileDialog savePath;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
		private System.Windows.Forms.ToolStripButton tsEngineTest;
		private System.Windows.Forms.ToolTip tip;
		private System.Windows.Forms.ToolStripButton tsDownloadManually;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
		private System.Windows.Forms.ToolStripMenuItem stStrictFilter;
		private System.Windows.Forms.ToolStripMenuItem stShowMultilineTab;
		private Controls.EngineList el;
		private System.Windows.Forms.ToolStripMenuItem stStatistics;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tsUsingFloat;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
		private System.Windows.Forms.Panel pTip;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label11;
	}
}