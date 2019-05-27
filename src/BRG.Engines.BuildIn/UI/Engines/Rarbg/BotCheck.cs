namespace BRG.Engines.BuildIn.UI.Engines.Rarbg
{
	using System;
	using System.Drawing;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;
	using System.Windows.Forms;
	using BRG.Engines.BuildIn.SearchProviders;
	using FSLib.Network.Http;

	internal partial class BotCheck : Form
	{
		RarbgSearchProvider _provider;
		string _capKey = null;
		bool _verified = false;

		public BotCheck(RarbgSearchProvider provider)
		{
			InitializeComponent();

			_provider = provider;
			_verified = false;
		}



		private void BotCheck_Load(object sender, EventArgs e)
		{
			LoadCaptcha();
			lblStatus.Click += (x, y) => LoadCaptcha();
			FormClosing += (x, y) =>
			{
				if (_verified)
					DialogResult = DialogResult.Yes;
				else DialogResult = chkSkip.Checked ? DialogResult.Cancel : DialogResult.No;
			};
			txtCode.KeyUp += (x, y) =>
			{
				if (txtCode.TextLength == 5)
					CheckCaptcha();
			};
		}

		void CheckCaptcha()
		{
			lblStatus.Text = "正在校验中...";
			txtCode.Enabled = false;

			var data = new
			{
				solve_string = txtCode.Text,
				captcha_id = _capKey,
				submitted_bot_captcha = 1
			};

			Task.Factory.StartNew(() =>
			{
				var nc = _provider.NetworkClient;
				var ctx = nc.Create<string>(HttpMethod.Post, "https://rarbg.com/bot_check.php", "https://rarbg.com/bot_check.php", data).Send();

				if (!ctx.IsValid())
				{
					this.Invoke(() =>
					{
						lblStatus.Text = "网络错误，请点击此文本重新验证。";
					});
					return;
				}

				if (ctx.Result.IndexOf("successfully") != -1)
				{
					//success
					this.Invoke(() =>
					{
						lblStatus.Text = "验证成功！";
						_verified = true;
						Close();
					});
					return;
				}

				this.Invoke(() =>
				{
					lblStatus.Text = "验证失败，请点击此文本重新验证。";
				});
			});
		}


		void LoadCaptcha()
		{
			lblStatus.Text = "正在加载验证码...";
			pbCap.Image = Properties.Resources._16px_loading_1;
			txtCode.Enabled = false;

			Task.Factory.StartNew(() =>
			{
				var nc = _provider.NetworkClient;

				var ctx = nc.Create<string>(HttpMethod.Get, "https://rarbg.com/bot_check.php", "https://rarbg.com/torrents.php").Send();
				if (!ctx.IsValid())
				{
					SetLoadingFailed();
					return;
				}

				//get url
				_capKey = Regex.Match(ctx.Result, @"captcha2/([^\.]+)", RegexOptions.IgnoreCase).GetGroupValue(1);
				if (_capKey.IsNullOrEmpty())
				{
					SetLoadingFailed();
					return;
				}

				//loadimage
				var imgCtx = nc.Create<Image>(HttpMethod.Get, $"https://rarbg.com/captcha2/{_capKey}.png", "https://rarbg.com/bot_check.php").Send();
				if (!imgCtx.IsValid())
				{
					SetLoadingFailed();
					return;
				}
				this.Invoke(() =>
				{
					pbCap.Image = imgCtx.Result;
					txtCode.Enabled = true;
					txtCode.Clear();
					txtCode.Focus();
				});
			});

		}

		void SetLoadingFailed()
		{
			this.Invoke(() =>
			{
				lblStatus.Text = "验证码加载失败，点击此文本重新加载";
				pbCap.Image = Properties.Resources.warning;
			});
		}
	}
}
