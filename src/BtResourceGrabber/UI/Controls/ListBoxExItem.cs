namespace BtResourceGrabber.UI.Controls
{
	using System.Drawing;
	using System.Windows.Forms;

	/// <summary>
	/// 默认实现的列表项
	/// </summary>
	public class ListBoxExItem : IListBoxExItem
	{
		#region Implementation of IListBoxExItem

		/// <summary>
		/// 获得文字
		/// </summary>
		public string DisplayText { get; set; }

		/// <summary>
		/// 获得图片
		/// </summary>
		public Image Image { get; set; }

		/// <summary>
		/// 获得副标题
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// 获得是否应该阻止默认的绘图
		/// </summary>
		public bool PreventDefaultDrawing { get; set; }

		/// <summary>
		/// 调用项目
		/// </summary>
		/// <param name="e"></param>
		public virtual void MeasureItem(MeasureItemEventArgs e)
		{

		}

		/// <summary>
		/// 绘制内容
		/// </summary>
		/// <param name="e"></param>
		public virtual void DrawItem(DrawItemEventArgs e)
		{

		}

		/// <summary>
		/// 文字颜色
		/// </summary>
		public Color? ForeColor { get; set; }

		/// <summary>
		/// 获得或设置保存的数据
		/// </summary>
		public object Tag { get; set; }

		#endregion
	}
}