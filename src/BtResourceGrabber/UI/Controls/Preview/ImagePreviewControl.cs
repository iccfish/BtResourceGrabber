using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BtResourceGrabber.UI.Controls.Preview
{
	using System.Drawing;
	using System.Windows.Forms;
	using BRG.Engines.Handlers;
	using BRG.Entities;
	using Entities;

	using FSLib.Network.Http;

	class ImagePreviewControl : PictureBox, IPreviewHandler
	{
		IResourceInfo _info;
		Rectangle _bounds;
		NetworkClient _network;

		public ImagePreviewControl()
		{
			_network = new NetworkClient(this);
			BackColor = Color.White;
			Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
			Cursor = Cursors.Hand;
			BorderStyle = BorderStyle.FixedSingle;
			Click += (s, e) => Hide();
		}

		/// <summary>
		/// 更新预览
		/// </summary>
		/// <param name="resource"></param>
		/// <param name="bounds">显示区域</param>
		public void UpdatePreview(IResourceInfo resource, Rectangle bounds)
		{
			if (resource == null || resource == _info)
				return;

			var isCallback = _info == resource;
			_info = resource;
			_bounds = bounds;

			Image = Properties.Resources._32px_loading_1;
			SizeMode = PictureBoxSizeMode.AutoSize;

			if (resource.PreviewInfo.ImageUrl.IsNullOrEmpty())
				return;

			//Image
			if (_info.PreviewInfo.PreviewImage == null)
			{
				_network.Create<Image>(HttpMethod.Get, resource.PreviewInfo.ImageUrl, resource.PreviewInfo.ImageUrl)
						.SendAsPromise().Done((s, e) =>
						{
							var img = e.Result?.Result;
							if (img != null)
							{
								resource.PreviewInfo.PreviewImage = img;
							}
							if (_info == resource)
								SetImage(img);
						}).Fail((s, e) =>
						{
							if (_info == resource)
								Image = Properties.Resources.preview_load_failed;
						});
			}
			else
			{
				SetImage(_info.PreviewInfo.PreviewImage);
			}
			Visible = true;
			BringToFront();
		}

		void SetImage(Image img)
		{
			img = img ?? Properties.Resources.preview_load_failed;

			//计算大小，最大大小不超过显示信息区域的四分之一。
			var maxHeight = (int)Math.Ceiling(_bounds.Height * 1.0 / 2);
			var maxWidth = (int)Math.Ceiling(_bounds.Width * 1.0 / 3);

			var imgRatio = img.Width * 1.0 / img.Height;
			var width = 0;
			var height = 0;
			if (imgRatio >= maxWidth * 1.0 / maxHeight)
			{
				//说明图片更宽，那么就以宽度为准。
				width = maxWidth;
				height = (int)(width / imgRatio);
			}
			else
			{
				height = maxHeight;
				width = (int)(height * imgRatio);
			}

			SizeMode = PictureBoxSizeMode.Zoom;
			Size = new Size(width, height);
			Image = img;
		}

		/// <summary>
		/// 附加UI
		/// </summary>
		/// <param name="parentControl"></param>
		/// <param name="displayControl"></param>
		public void AttachUI(Control parentControl, Control displayControl)
		{
			this.Visible = false;
			parentControl.Controls.Add(this);
		}

		public new void Hide()
		{
			base.Visible = false;
			_info = null;
		}
	}
}
