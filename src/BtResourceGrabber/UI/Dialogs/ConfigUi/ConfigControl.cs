using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BtResourceGrabber.UI.Dialogs.ConfigUi
{
	using System.Drawing;
	using System.Windows.Forms;

	using Controls;

	class ConfigControl : FunctionalUserControl
	{
		public Image Image { get; set; }

		public virtual bool Save()
		{
			return true;
		}
	}
}
