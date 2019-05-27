using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BtResourceGrabber.UI.Controls.Preview
{
	using System.Drawing;
	using System.Windows.Forms;
	using BRG;
	using BRG.Entities;
	using Entities;

	class PreviewContext
	{
		Control _parentControl;
		Control _displayControl;
		Dictionary<int, IPreviewHandler> _handlers;
		PreviewInfoLoading _previewInfoLoading;
		IResourceInfo _currentInfo;

		/// <summary>
		/// 创建 <see cref="PreviewContext" />  的新实例(PreviewContext)
		/// </summary>
		/// <param name="parentControl"></param>
		/// <param name="displayControl">显示列表的控件</param>
		public PreviewContext(Control parentControl, Control displayControl)
		{
			_parentControl = parentControl;
			_displayControl = displayControl;

			_handlers = new Dictionary<int, IPreviewHandler>();
			_handlers.Add((int)PreviewType.Image, new ImagePreviewControl());

			foreach (var previewHandler in _handlers.Values)
			{
				previewHandler.ClientSizeChanged += (s, e) => LocatePreview(s as IPreviewHandler);
				previewHandler.Hide();
				previewHandler.AttachUI(parentControl, _displayControl);
			}

			_previewInfoLoading = new PreviewInfoLoading();
			_previewInfoLoading.Hide();
			parentControl.Controls.Add(_previewInfoLoading);
		}

		public void UpdatePreview(IResourceInfo info)
		{
			foreach (var previewHandler in _handlers.Values)
			{
				previewHandler.Hide();
			}

			if (info == null || info.SupportPreivewType == PreviewType.None)
			{
				_currentInfo = null;
				return;
			}

			_currentInfo = info;
            if (info.PreviewInfo == null)
            {
	            _previewInfoLoading.Show();
				LocatePreview(_previewInfoLoading);
	            _previewInfoLoading.BringToFront();

				info.PreviewInfoLoaded -= Info_PreviewInfoLoaded;
				info.PreviewInfoLoaded += Info_PreviewInfoLoaded;
				info.Provider.LoadPreviewInfo(info);
			}
			else
			{
				ShowPreview(info);
			}
		}

		private void Info_PreviewInfoLoaded(object sender, EventArgs e)
		{
			var info = sender as IResourceInfo;
			info.PreviewInfoLoaded -= Info_PreviewInfoLoaded;

			AppContext.Instance.MainForm.Invoke(() =>
			{
				if (info != _currentInfo)
					return;

				if (info.PreviewInfo == null)
				{
					_currentInfo = null;
					return;
				}

				ShowPreview(info);
			});
		}

		void ShowPreview(IResourceInfo resource)
		{
			var control = _handlers.GetValue((int)resource.SupportPreivewType);
			if (control == null)
			{
				return;
			}
			control.UpdatePreview(resource, _parentControl.ClientRectangle);
			LocatePreview(control);
		}

		void LocatePreview(IPreviewHandler handler)
		{
			var size = handler.ClientSize;
			var clientsize = _displayControl.ClientSize;

			var location = _displayControl.Location;
			location.Offset(clientsize.Width - size.Width - 20, clientsize.Height - size.Height - 2);
			handler.Location = location;
		}
	}
}
