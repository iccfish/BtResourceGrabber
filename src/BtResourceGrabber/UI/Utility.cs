using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BtResourceGrabber.UI
{
	using System.Drawing;
	using System.Drawing.Imaging;
	using System.Runtime.InteropServices;

	using BRG.Service;

	class Utility
	{
		/// <summary>
		/// 将16px的图像转换为20px的图像
		/// </summary>
		/// <param name="img"></param>
		/// <returns></returns>
		public static Image Get20PxImageFrom16PxImg(Image img)
		{
			var image = new Bitmap(20, 20, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			using (var g = Graphics.FromImage(image))
			{
				g.DrawImage(img, 2, 2, 16, 16);
			}

			return image;
		}

		static Dictionary<IServiceBase, Image> _20pxImageIconCache = new Dictionary<IServiceBase, Image>();

		public static Image Get20PxImage(IServiceBase provider)
		{
			if (provider?.Info?.Icon == null)
				return null;

			var img = _20pxImageIconCache.GetValue(provider);
			if (img == null)
			{
				img = Get20PxImageFrom16PxImg(provider.Info.Icon);
				_20pxImageIconCache.Add(provider, img);
			}

			return img;
		}

		/// <summary>
		/// 将16px的图像转换为20px的图像
		/// </summary>
		/// <param name="img"></param>
		/// <returns></returns>
		public static Image Get24PxImageFrom16PxImg(Image img)
		{
			var image = new Bitmap(24, 24, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			using (var g = Graphics.FromImage(image))
			{
				g.DrawImage(img, 4, 4, 16, 16);
			}

			return image;
		}

		[DllImport("user32.dll")]
		public static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

		public const int SW_SHOWNOACTIVATE = 4;

		/// <summary>
		/// 获得灰度图像
		/// </summary>
		/// <param name="source">源图像</param>
		/// <returns></returns>
		public static System.Drawing.Image MakeGrayImage(System.Drawing.Image source)
		{
			var nbmp = new Bitmap(source);

			var bmpdata = nbmp.LockBits(new Rectangle(0, 0, nbmp.Width, nbmp.Height), ImageLockMode.ReadWrite, nbmp.PixelFormat);
			var startAddress = bmpdata.Scan0;

			var pixelWidth = Math.Abs(bmpdata.Stride) / bmpdata.Width;
			var bytesLength = pixelWidth * bmpdata.Width * bmpdata.Height;
			var buffer = new byte[bytesLength];

			Marshal.Copy(startAddress, buffer, 0, buffer.Length);
			for (var i = 0; i < buffer.Length; i += pixelWidth)
			{
				var value = (buffer[i + 2] * 0.299 + buffer[i + 1] * 0.587 + buffer[i] * 0.114);
				buffer[i] = buffer[i + 1] = buffer[i + 2] = (byte)value;
			}

			Marshal.Copy(buffer, 0, startAddress, buffer.Length);
			nbmp.UnlockBits(bmpdata);

			return nbmp;
		}

	}
}
