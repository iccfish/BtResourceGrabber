namespace BtResourceGrabber.UI.Dialogs.Messages
{
	using System;
	using System.Drawing;
	using System.IO;
	using System.Windows.Forms;
	using BRG;
	using BtResourceGrabber.Service;

	public partial class NewFeature : Form
	{
		public NewFeature()
		{
			InitializeComponent();

			var imgfile = PathUtility.Combine(AppContext.Instance.ConfigLoader.Root, "new_feature.png");
			if (File.Exists(imgfile))
			{
				var img = Image.FromFile(imgfile);
				pnf.Image = img;

				Size = new Size(img.Width + 30, Math.Min(500, img.Height));
			}

			AppContext.Instance.Options.NewFeatureVersion = Program.NewFeatureVersion;
		}

		private void NewFeature_Load(object sender, EventArgs e)
		{
		}
	}
}
