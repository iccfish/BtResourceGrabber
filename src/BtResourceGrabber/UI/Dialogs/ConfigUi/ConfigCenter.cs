using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BtResourceGrabber.UI.Dialogs.ConfigUi
{
	using System.Drawing;
	using System.Windows.Forms;

	using BRG;

	class ConfigCenter : FunctionalForm
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TabControl oc;
		private System.Windows.Forms.ImageList il;
		private System.ComponentModel.IContainer components;

		public ConfigCenter()
		{
			InitializeComponent();

			AddOption(new GeneralOption());
			AddOption(new FilterConfig());
			AddOption(new MarkOption());
			AddOption(new NetworkOption());
			AddOption(new HistoryOption());
			AddOption(new EnginePropertyConfig());

			FormClosing += (sender, args) => AppContext.Instance.OnRequestRefreshMarkCollection(this);
		}

		public void AddOption(ConfigControl option)
		{
			var tp = new TabPage
			{
				Text = option.Text,
				ImageIndex = il.Images.Count
			};
			tp.Controls.Add(option);
			option.Dock = DockStyle.Fill;
			option.BackColor = Color.White;
			il.Images.Add(option.Image);
			oc.TabPages.Add(tp);
		}

		public IEnumerable<T> FindConfigUI<T>() where T : ConfigControl
		{
			return oc.TabPages.OfType<TabPage>().Select(s => s.Controls[0] as ConfigControl).OfType<T>();
		}

		public ConfigControl SelectedConfig
		{
			get => oc.SelectedTab.Controls[0] as ConfigControl;
			set
			{
				oc.SelectedTab = oc.TabPages.OfType<TabPage>().FirstOrDefault(s => s.Controls[0] == value) ?? oc.SelectedTab;
			}
		}

		public void SelectConfigUI<T>() where T : ConfigControl => SelectedConfig = FindConfigUI<T>().First();

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnOk = new System.Windows.Forms.Button();
			this.oc = new System.Windows.Forms.TabControl();
			this.il = new System.Windows.Forms.ImageList(this.components);
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnOk);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 399);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(574, 43);
			this.panel1.TabIndex = 0;
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(228, 0);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(110, 33);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "确定(&O)";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// oc
			// 
			this.oc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oc.ImageList = this.il;
			this.oc.Location = new System.Drawing.Point(0, 0);
			this.oc.Name = "oc";
			this.oc.SelectedIndex = 0;
			this.oc.Size = new System.Drawing.Size(574, 399);
			this.oc.TabIndex = 1;
			// 
			// il
			// 
			this.il.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.il.ImageSize = new System.Drawing.Size(16, 16);
			this.il.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// ConfigCenter
			// 
			this.ClientSize = new System.Drawing.Size(574, 442);
			this.Controls.Add(this.oc);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConfigCenter";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "配置中心";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
