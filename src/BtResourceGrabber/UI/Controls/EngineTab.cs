using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BtResourceGrabber.Service;

namespace BtResourceGrabber.UI.Controls
{
	class EngineTab : TabPage
	{
		public ITorrentResourceProvider TorrentResourceProvider { get; private set; }

		/// <summary>
		/// 创建 <see cref="EngineTab" />  的新实例(EngineTab)
		/// </summary>
		/// <param name="torrentResourceProvider"></param>
		public EngineTab(ITorrentResourceProvider torrentResourceProvider)
		{
			TorrentResourceProvider = torrentResourceProvider;
			Controls.Add((EngineUI = new EngineTabContent(torrentResourceProvider) { Dock = DockStyle.Fill }));
			Text = string.Format("{0} {1}", torrentResourceProvider.Info.Name, (torrentResourceProvider.SupportUnicode ? BtResourceGrabber.SR.SupportUnicodeTabText : ""));
			//Text = $"{torrentResourceProvider.Info.Name}";
		}

		/// <summary>
		/// 获得显示的界面
		/// </summary>
		public EngineTabContent EngineUI { get; private set; }
	}
}
