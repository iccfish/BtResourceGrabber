namespace BRG.Entities
{
	using System.ComponentModel;
	using System.Drawing;
	using System.FishExtension;

	public class HashMark:INotifyPropertyChanged
	{
		/// <summary>
		/// 创建 <see cref="HashMark" />  的新实例(HashMark)
		/// </summary>
		public HashMark()
		{
			_color = Color.Black;
			_backColor = Color.White;
		}

		/// <summary>
		/// 创建 <see cref="HashMark" />  的新实例(HashMark)
		/// </summary>
		/// <param name="color"></param>
		public HashMark(Color color)
		{
			_color = color;
		}


		Color _color;
		Color _backColor;


		/// <summary>
		/// 颜色
		/// </summary>
		public Color Color
		{
			get { return _color; }
			set
			{
				if (value.Equals(_color)) return;
				_color = value;
				OnPropertyChanged("Color");
			}
		}

		public Color BackColor
		{
			get { return _backColor; }
			set
			{
				if (value.Equals(_backColor)) return;
				_backColor = value;
				OnPropertyChanged("BackColor");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
