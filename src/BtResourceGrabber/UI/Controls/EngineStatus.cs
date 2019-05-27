using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BtResourceGrabber.UI.Controls
{
	using System.Diagnostics;
	using System.Drawing;
	using System.Windows.Forms;
	using BtResourceGrabber.Entities;

	class EngineStatus : PictureBox
	{
		EngineSearchContext _context;
		Image _hotImage, _standbyImage, _succImage, _failImage, _hotAnimiationImage, _disabledImage;
		Timer _aniTimer;

		public EngineStatus(EngineSearchContext context)
		{
			_context = context;
			SizeMode = PictureBoxSizeMode.AutoSize;

			_hotImage = context.Provider.Info.Icon;
			_standbyImage = Utility.MakeGrayImage(_hotImage);
			ProcessImage();
			Image = context.Provider.Disabled ? _disabledImage : _standbyImage;

			_context.Provider.DisabledChanged += (s, e) =>
			{
				Image = context.Provider.Disabled ? _disabledImage : _standbyImage;
			};
			_context.SearchBegin += (s, e) =>
			{
				Image = DateTime.Now.Millisecond < 500 ? _hotImage : _hotAnimiationImage;
				_aniTimer.Start();
			};
			_context.SearchCancelled += (s, e) =>
			{
				Image = _standbyImage;
				_aniTimer.Stop();
			};
			_context.SearchFailed += (s, e) =>
			{
				Image = _failImage;
				_aniTimer.Stop();
			};
			_context.SearchComplete += (s, e) =>
			{
				Image = _succImage;
				_aniTimer.Stop();
			};

			_aniTimer = new Timer()
			{
				Interval = 100
			};
			_aniTimer.Tick += (s, e) =>
			{
				Image = DateTime.Now.Millisecond < 500 ? _hotImage : _hotAnimiationImage;
			};

			Cursor = Cursors.Hand;
			Click += EngineStatus_Click;
		}

		private void EngineStatus_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(_context.Provider.HomePage);
			}
			catch (Exception)
			{
			}
		}

		void ProcessImage()
		{
			_succImage = new Bitmap(_standbyImage);
			using (var g = Graphics.FromImage(_succImage))
			{
				//g.FillRectangle(Brushes.Green, 10, 10, 5, 5);
				g.DrawImage(Properties.Resources.Clear_Green_Button, 7, 7, 8, 8);
				g.Save();
			}
			_failImage = new Bitmap(_standbyImage);
			using (var g = Graphics.FromImage(_failImage))
			{
				g.DrawImage(Properties.Resources.block_16, 7, 7, 8, 8);
				//g.FillRectangle(Brushes.Red, 10, 10, 5, 5);
				g.Save();
			}
			_hotAnimiationImage = new Bitmap(_hotImage);
			using (var g = Graphics.FromImage(_hotAnimiationImage))
			{
				g.DrawImage(Properties.Resources._131, 7, 7, 8, 8);
				//g.FillRectangle(Brushes.Red, 10, 10, 5, 5);
				g.Save();
			}
			_disabledImage = new Bitmap(_standbyImage);
			using (var g = Graphics.FromImage(_disabledImage))
			{
				g.DrawImage(Properties.Resources.delete_16, 7, 7, 8, 8);
				//g.FillRectangle(Brushes.Red, 10, 10, 5, 5);
				g.Save();
			}
		}
	}
}
