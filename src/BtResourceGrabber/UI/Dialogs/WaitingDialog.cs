using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BtResourceGrabber.UI.Dialogs
{
	public partial class WaitingDialog : Form
	{
		public WaitingDialog()
		{
			InitializeComponent();
		}

		public Task Task
		{
			get; set;
		}

		private async void WaitingDialog_Load(object sender, EventArgs e)
		{
			await Task;
			Close();
		}
	}
}
