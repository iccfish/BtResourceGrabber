using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BtResourceGrabber.UI.Controls
{
	using System.Windows.Forms;
	using BRG.Service;
	using BtResourceGrabber.Service;

	class MultiEngineTab : TabPage
	{
		MultiEngineTabContent _content;

		public IResourceProvider[] Providers { get; private set; }


		public MultiEngineTab(TabControl tcControl, params IResourceProvider[] providers)
		{
			tcControl.TabPages.Add(this);

			Providers = providers;
			ImageIndex = 0;
			Text = string.Format("{0} {1}",
				providers.Length == 1 ? providers[0].Info.Name : BtResourceGrabber.SR.MultipleSearch,
				(providers.All(s => s.SupportUnicode) ? BtResourceGrabber.SR.SupportUnicodeTabText : ""));
			if (providers.Length == 1 && providers[0].Info.Icon != null)
			{
				ImageKey = providers[0].Info.Name;
			}

			_content = new MultiEngineTabContent(providers)
			{
				Dock = DockStyle.Fill
			};

			Controls.Add(_content);
		}

		/// <summary>
		/// 获得显示的界面
		/// </summary>
		public MultiEngineTabContent EngineUI { get { return _content; } }

	}
}
