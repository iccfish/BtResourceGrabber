using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BtResourceGrabber.UI.Controls.Preview
{
	using System.Drawing;
	using System.Windows.Forms;
	using BRG.Entities;
	using BtResourceGrabber.Entities;

	class PreviewInfoLoading :PictureBox, IPreviewHandler
	{
		public PreviewInfoLoading()
		{
			Image = Properties.Resources._32px_loading_1;
			SizeMode = PictureBoxSizeMode.AutoSize;
			BorderStyle = BorderStyle.FixedSingle;
			BackColor = Color.White;
			Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
		}

		#region Implementation of IPreviewHandler

		/// <summary>
		/// 更新预览
		/// </summary>
		/// <param name="resource"></param>
		/// <param name="bounds">显示区域</param>
		public void UpdatePreview(IResourceInfo resource, Rectangle bounds)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 附加UI
		/// </summary>
		/// <param name="parentControl"></param>
		/// <param name="displayControl"></param>
		public void AttachUI(Control parentControl, Control displayControl)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
