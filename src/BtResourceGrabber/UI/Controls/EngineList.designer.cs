using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BtResourceGrabber.UI.Controls
{
	partial class EngineList
	{
		private System.ComponentModel.IContainer components;

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.ilProvider = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// ilProvider
			// 
			this.ilProvider.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.ilProvider.ImageSize = new System.Drawing.Size(16, 16);
			this.ilProvider.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// EngineList
			// 
			this.ImageList = this.ilProvider;
			this.ResumeLayout(false);

		}
		private ImageList ilProvider;

	}
}
