using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BtResourceGrabber.UI.Controls.Preview
{
	using System.Drawing;
	using System.Windows.Forms;
	using BRG.Entities;
	using Entities;

	interface IPreviewHandler
	{
		/// <summary>
		/// 更新预览
		/// </summary>
		/// <param name="resource"></param>
		/// <param name="bounds">显示区域</param>
		void UpdatePreview(IResourceInfo resource, Rectangle bounds);

		/// <summary>
		/// 获得此刻的窗口面积
		/// </summary>
		Size ClientSize { get; }

		/// <summary>
		/// 窗口面积发生变化
		/// </summary>
		event EventHandler ClientSizeChanged;

		/// <summary>
		/// 是否可见
		/// </summary>
		void Hide();

		/// <summary>
		/// 位置
		/// </summary>
		Point Location { get; set; }

		/// <summary>
		/// 附加UI
		/// </summary>
		/// <param name="parentControl"></param>
		/// <param name="displayControl"></param>
		void AttachUI(Control parentControl, Control displayControl);
	}
}
