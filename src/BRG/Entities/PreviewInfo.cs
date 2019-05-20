namespace BRG.Entities
{
	using System.Drawing;

	/// <summary>
	/// 预览信息
	/// </summary>
	public class PreviewInfo
	{
		string _webUrl;
		string _description;
		string _imageUrl;

		public string WebUrl
		{
			get { return _webUrl; }
			set
			{
				_webUrl = value;
			}
		}

		public string Description
		{
			get { return _description; }
			set
			{
				_description = value;
			}
		}

		public string ImageUrl
		{
			get { return _imageUrl; }
			set
			{
				_imageUrl = value;
			}
		}

		public Image PreviewImage { get; set; }
	}
}
