using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using BtResourceGrabber.Service;
using BtResourceGrabber.UI.Dialogs;

namespace BtResourceGrabber
{
	using System.IO;
	using BRG;
	using BtResourceGrabber.UI.Dialogs.Messages;

	using FSLib.Extension.FishLib;

	static class Program
	{

		internal static bool IsShutodown;
		internal static bool IsRunning;
		internal static string NewFeatureVersion = null;
		internal static string CurrentVersion = ApplicationRunTimeContext.GetProcessMainModule().FileVersionInfo.FileVersion;
		internal static bool IsTraceEnabled = false;
		internal static string LogFile;
		internal static TextWriterTraceListener LogListener;


		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			IsRunning = true;
			ServiceManager.Instance.Init();
			AppContext.Instance.Init();
			AppContext.Instance.DataContext.Init();
			ServiceManager.Instance.Connect();
			var mainForm = new MainForm();
			mainForm.CreateControl();


			if (AppContext.Instance.Options.FirstRun)
			{
				if (new License().ShowDialog() != DialogResult.OK)
					return;

				AppContext.Instance.Options.FirstRun = false;
			}

			Application.Run(mainForm);
			IsShutodown = true;

			ServiceManager.Instance.Disconnect();
			AppContext.Instance.Shutdown();
		}
	}
}
