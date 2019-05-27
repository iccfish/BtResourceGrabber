namespace BtResourceGrabber.UI.Controls
{
	using System.Drawing;
	using System.Windows.Forms;

	/// <summary>
	/// 可扩展的列表项接口
	/// </summary>
	public interface IListBoxExItem
	{
		/// <summary>
		/// 获得文字
		/// </summary>
		string DisplayText { get; set; }

		/// <summary>
		/// 获得图片
		/// </summary>
		Image Image { get; set; }

		/// <summary>
		/// 获得副标题
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// 获得是否应该阻止默认的绘图
		/// </summary>
		bool PreventDefaultDrawing { get; set; }

		/// <summary>
		/// 调用项目
		/// </summary>
		/// <param name="e"></param>
		void MeasureItem(MeasureItemEventArgs e);

		/// <summary>
		/// 绘制内容
		/// </summary>
		/// <param name="e"></param>
		void DrawItem(DrawItemEventArgs e);

		/// <summary>
		/// 文字颜色
		/// </summary>
		Color? ForeColor { get; set; }

		/// <summary>
		/// 获得或设置保存的数据
		/// </summary>
		object Tag { get; set; }

	}
}