namespace BtResourceGrabber.UI.Controls
{
	using System.ComponentModel;
	using System.Drawing;
	using System.Linq;
	using System.Windows.Forms;

	public class ListBoxEx : ListBox
	{
		ListItemRenderer _render;

		public ListBoxEx()
		{
			_render = new ListItemRenderer(this);
			DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
		}

		/// <summary>
		/// 获得文本字体
		/// </summary>
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
				if (DefaultCaptionFont == null)
					DefaultCaptionFont = new Font(base.Font, FontStyle.Bold);
			}
		}

		/// <summary>
		/// 获得或设置项目的边距
		/// </summary>
		public Padding ItemPadding
		{
			get { return _render.ItemPadding; }
			set { _render.ItemPadding = value; }
		}

		/// <summary>
		/// 获得或设置是否绘制描述
		/// </summary>
		[DefaultValue(false)]
		public bool ShowDescription
		{
			get { return _render.ShowDescription; }
			set { _render.ShowDescription = value; }
		}

		/// <summary>
		/// 获得或设置描述和标题的距离
		/// </summary>
		[DefaultValue(3)]
		public int DescriptionSpace
		{
			get { return _render.DescriptionSpace; }
			set { _render.DescriptionSpace = value; }
		}

		/// <summary>
		/// 获得或设置默认标题字体
		/// </summary>
		public Font DefaultCaptionFont
		{
			get { return _render.DefaultCaptionFont; }
			set { _render.DefaultCaptionFont = value; }
		}

		/// <summary>
		/// 标题样式
		/// </summary>
		public Font CaptionFont
		{
			get { return _render.CaptionFont; }
			set { _render.CaptionFont = value; }
		}

		/// <summary>
		/// 获得或设置项目和文字之间的距离
		/// </summary>
		[DefaultValue(5)]
		public int ImageTextSpace
		{
			get { return _render.ImageTextSpace; }
			set { _render.ImageTextSpace = value; }
		}

		/// <filterpriority>1</filterpriority>
		/// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				if (base.ForeColor == value) return;

				base.ForeColor = value;
				_render.ForeColor = value;
			}
		}

		/// <summary>
		/// 获得或设置选定项的颜色
		/// </summary>
		public Color SelectedTextColor
		{
			get { return _render.SelectedTextColor; }
			set
			{
				_render.SelectedTextColor = value;
			}
		}

		/// <summary>
		/// 获得或设置描述文本颜色
		/// </summary>
		public Color DescriptionColor
		{
			get { return _render.DescriptionColor; }
			set
			{
				_render.DescriptionColor = value;
			}
		}

		#region 设计器方法

		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeSelectedTextColor()
		{
			return SelectedTextColor != SystemColors.HighlightText;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeDescriptionColor()
		{
			return DescriptionColor != SystemColors.GrayText;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeItemPadding()
		{
			return ItemPadding.All == 5;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeCaptionFont()
		{
			return CaptionFont != null;
		}
		#endregion

	}
}
