namespace BtResourceGrabber.UI.Controls
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Drawing;
	using System.Linq;
	using System.Windows.Forms;

	/// <summary>
	/// 列表项渲染类
	/// </summary>
	class ListItemRenderer : IDisposable
	{
		ListControl _listControl;

		/// <summary>
		/// 创建 <see cref="ListItemRenderer" />  的新实例(ListItemRenderer)
		/// </summary>
		/// <param name="listControl">The list control.</param>
		/// <exception cref="System.ArgumentNullException">listControl;listControl is null.</exception>
		/// <exception cref="System.NotSupportedException">control type not supported.</exception>
		public ListItemRenderer(ListControl listControl)
		{
			if (listControl == null)
				throw new ArgumentNullException("listControl", "listControl is null.");
			_listControl = listControl;

			if (_listControl is ComboBox)
			{
				var cb = _listControl as ComboBox;
				cb.MeasureItem += ListBoxEx_MeasureItem;
				cb.DrawItem += ListBoxEx_DrawItem;
				cb.DropDownHeight = 200;
				if (cb.DrawMode != DrawMode.Normal)
				{
					//cb.ItemHeight = GetComboDefaultItemHeight();
				}
			}
			else if (_listControl is ListBoxEx)
			{
				var lb = _listControl as ListBoxEx;
				lb.MeasureItem += ListBoxEx_MeasureItem;
				lb.DrawItem += ListBoxEx_DrawItem;
			}
			else
			{
				throw new NotSupportedException("control type not supported.");
			}

			//设置默认参数
			DefaultCaptionFont = new Font(_listControl.Font, FontStyle.Bold);
			ItemPadding = new Padding(5);
			ImageTextSpace = 5;
			DescriptionSpace = 3;

			//默认颜色
			ForeColor = _listControl.ForeColor;
			SelectedTextColor = SystemColors.HighlightText;
			DescriptionColor = SystemColors.GrayText;
		}

		/// <summary>
		/// 获得或设置项目的边距
		/// </summary>
		public Padding ItemPadding { get; set; }

		/// <summary>
		/// 获得或设置是否绘制描述
		/// </summary>
		[DefaultValue(false)]
		public bool ShowDescription { get; set; }

		/// <summary>
		/// 获得或设置描述和标题的距离
		/// </summary>
		[DefaultValue(3)]
		public int DescriptionSpace { get; set; }

		/// <summary>
		/// 获得或设置默认的标题字体
		/// </summary>
		public Font DefaultCaptionFont
		{
			get;
			set;
		}

		/// <summary>
		/// 标题样式
		/// </summary>
		public Font CaptionFont { get; set; }

		/// <summary>
		/// 获得或设置项目和文字之间的距离
		/// </summary>
		[DefaultValue(5)]
		public int ImageTextSpace { get; set; }

		Color _foreColor;

		/// <filterpriority>1</filterpriority>
		/// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		public Color ForeColor
		{
			get
			{
				return _foreColor;
			}
			set
			{
				if (_foreColor == value) return;

				_foreColor = value;
				if (_foreBrush != null) _foreBrush.Dispose();
				_foreBrush = new SolidBrush(value);
			}
		}

		private Color _selectedTextColor;

		/// <summary>
		/// 获得或设置选定项的颜色
		/// </summary>
		public Color SelectedTextColor
		{
			get { return _selectedTextColor; }
			set
			{
				if (_selectedTextColor == value) return;

				_selectedTextColor = value;
				if (_selectedBrush != null) _selectedBrush.Dispose();
				_selectedBrush = new SolidBrush(value);
			}
		}

		private Color _descriptionColor;
		/// <summary>
		/// 获得或设置描述文本颜色
		/// </summary>
		public Color DescriptionColor
		{
			get { return _descriptionColor; }
			set
			{
				if (_descriptionColor == value) return;

				_descriptionColor = value;
				if (_descriptionBrush != null) _descriptionBrush.Dispose();
				_descriptionBrush = new SolidBrush(value);
			}
		}


		SolidBrush _foreBrush, _descriptionBrush;
		SolidBrush _selectedBrush;
		static Dictionary<Color, SolidBrush> _brushCache = new Dictionary<Color, SolidBrush>();

		/// <summary>
		/// 获得显示的项
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		object GetItem(int index)
		{
			if (index < 0) return null;
			if (_listControl is ComboBox)
			{
				var cb = _listControl as ComboBox;
				return index < cb.Items.Count ? cb.Items[index] : null;
			}
			if (_listControl is ListBox)
			{
				var cb = _listControl as ListBox;
				return index < cb.Items.Count ? cb.Items[index] : null;
			}
			return null;
		}

		void ListBoxEx_DrawItem(object sender, DrawItemEventArgs e)
		{
			var objItem = GetItem(e.Index);
			if (objItem == null) return;

			var item = objItem as IListBoxExItem;

			//绘制默认
			e.DrawBackground();
			e.DrawFocusRectangle();

			var startX = ItemPadding.Left + e.Bounds.Left;
			var startY = ItemPadding.Top + e.Bounds.Top;
			var itemWidth = e.Bounds.Width - ItemPadding.Left - ItemPadding.Right;
			var itemHeight = e.Bounds.Height - ItemPadding.Top - ItemPadding.Bottom;

			if (item != null)
			{
				if (!item.PreventDefaultDrawing)
				{
					if (item.Image != null)
					{
						e.Graphics.DrawImage(item.Image, startX,
							(itemHeight - item.Image.Height) / 2 + startY,
							item.Image.Width, item.Image.Height);
						startX += item.Image.Width + ImageTextSpace;
						itemWidth -= item.Image.Width - ImageTextSpace;
					}
					var sizeCaption = e.Graphics.MeasureString(item.DisplayText, ShowDescription ? CaptionFont : _listControl.Font, itemWidth);
					var sizeDescription = string.IsNullOrEmpty(item.Description) || !ShowDescription ? (SizeF?)null : e.Graphics.MeasureString(item.Description, _listControl.Font, itemWidth);
					//计算文字的padding
					var textPadding = (itemHeight - (int)sizeCaption.Height - (sizeDescription.HasValue ? (int)sizeDescription.Value.Height : 0)) / 2;
					if (textPadding < 0) textPadding = 0;
					//判断颜色
					SolidBrush catpionBrush;
					if (e.State.IsSelected())
					{
						catpionBrush = _selectedBrush;
					}
					else
					{
						if (item.ForeColor.HasValue)
						{
							if (!_brushCache.ContainsKey(item.ForeColor.Value))
							{
								catpionBrush = new SolidBrush(item.ForeColor.Value);
								_brushCache.Add(item.ForeColor.Value, catpionBrush);
							}
							else
								catpionBrush = _brushCache[item.ForeColor.Value];
						}
						else catpionBrush = _foreBrush;
					}

					var textBlockSize = new SizeF(itemWidth, itemHeight);
					//标题
					e.Graphics.DrawString(item.DisplayText, ShowDescription ? CaptionFont : _listControl.Font, catpionBrush, new RectangleF(new PointF(startX, startY + textPadding), textBlockSize));
					//副标题
					if (ShowDescription && !item.Description.IsNullOrEmpty())
					{
						startY += (int)sizeCaption.Height + DescriptionSpace;

						var descriptionBrush = e.State.IsSelected() ? _selectedBrush : _descriptionBrush;
						var startPoint = new PointF(startX, startY);
						var region = new RectangleF(startPoint, textBlockSize);

						e.Graphics.DrawString(item.Description, _listControl.Font, descriptionBrush, region);
					}
				}
				item.DrawItem(e);
			}
			else
			{
				var text = objItem.ToString();

				e.Graphics.DrawString(text, _listControl.Font, e.State.IsSelected() ? _selectedBrush : _foreBrush,
					new RectangleF(new PointF(startX, startY), new SizeF(e.Bounds.Width - ItemPadding.Left - ItemPadding.Right, e.Bounds.Height - ItemPadding.Top - ItemPadding.Bottom)));
			}
		}

		void ListBoxEx_MeasureItem(object sender, MeasureItemEventArgs e)
		{
			var objItem = GetItem(e.Index);
			if (objItem == null) return;


			var item = objItem as IListBoxExItem;
			if (item == null)
			{
				//默认的显示文字
				var text = objItem.ToString();
				var size = e.Graphics.MeasureString(text, _listControl.Font, _listControl.Width - ItemPadding.Left - ItemPadding.Right);
				e.ItemHeight = (int)size.Height + ItemPadding.Top + ItemPadding.Bottom;
				e.ItemWidth = (int)size.Width + ItemPadding.Left + ItemPadding.Right;

				return;
			}

			if (item.PreventDefaultDrawing) item.MeasureItem(e);
			else
			{
				//
				var width = _listControl.Width;//e.ItemWidth;
				var height = e.ItemHeight;

				//检查图像
				MeasureImage(item, ref width, ref height);
				//检查文字
				MeasureCaptionSize(e.Graphics, item, ref width, ref height);

				e.ItemHeight = height;
				e.ItemWidth = width;

				item.MeasureItem(e);
			}

		}

		void MeasureImage(IListBoxExItem item, ref int width, ref int height)
		{
			if (item.Image == null) return;

			width = Math.Max(ItemPadding.Left + ItemPadding.Right + item.Image.Width, width);
			height = Math.Max(ItemPadding.Top + ItemPadding.Bottom + item.Image.Height, height);
		}

		void MeasureCaptionSize(Graphics g, IListBoxExItem item, ref int width, ref int height)
		{
			if (item.DisplayText.IsNullOrEmpty()) return;

			var font = ShowDescription ? CaptionFont : _listControl.Font;
			//获得文字区最大长度，准备换行
			var maxTextWidth = width - (item.Image == null ? 0 : item.Image.Width) - ImageTextSpace - ItemPadding.Left - ItemPadding.Right;
			var size = g.MeasureString(item.DisplayText, font, maxTextWidth > 10 ? maxTextWidth : width);
			var sizeDescription = SizeF.Empty;
			var space = 0;

			if (ShowDescription && !item.Description.IsNullOrEmpty())
			{
				sizeDescription = g.MeasureString(item.Description, _listControl.Font, maxTextWidth);
				space = DescriptionSpace;
			}

			height = Math.Max(height, ItemPadding.Top + ItemPadding.Bottom + space + (int)size.Height + (int)sizeDescription.Height);
		}



		#region Dispose方法实现

		bool _disposed;

		/// <summary>
		/// 释放资源
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;
			_disposed = true;

			if (disposing)
			{
				if (_selectedBrush != null) _selectedBrush.Dispose();
				if (_foreBrush != null) _foreBrush.Dispose();
				if (_descriptionBrush != null) _descriptionBrush.Dispose();

				_brushCache.Values.ForEach(s => s.Dispose());

			}

			//挂起终结器
			GC.SuppressFinalize(this);
		}

		#endregion

		/// <summary>
		/// 获得CB在默认情况下的首选高度
		/// </summary>
		/// <returns></returns>
		int GetComboDefaultItemHeight()
		{
			var cb = _listControl as ComboBox;
			var ds = cb.DrawMode;

			var height = cb.ItemHeight;
			cb.DrawMode = ds;

			return height;
		}

	}
}
