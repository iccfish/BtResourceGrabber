using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BtResourceGrabber.UI.Controls
{
	using System.Windows.Forms;

	class FunctionalUserControl : UserControl
	{
		/// <summary>
		/// 显示信息对话框
		/// </summary>
		/// <param name="content">要显示的内容</param>
		public void Information(string content)
		{
			Information("信息", content);
		}

		/// <summary>
		/// 显示信息对话框
		/// </summary>
		/// <param name="title">要显示的标题</param>
		/// <param name="content">要显示的内容</param>
		public void Information(string title, string content)
		{
			MessageBox.Show(this, content, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		/// <summary>
		/// 提示对话框
		/// </summary>
		/// <param name="content">提示内容</param>
		/// <param name="isYesNo">提示内容，true是 “是/否”，false为“确定”、“取消”</param>
		/// <returns></returns>
		public bool Question(string content, bool isYesNo = false)
		{
			return Question(content, "确定", isYesNo);
		}

		/// <summary>
		/// 提示对话框
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="content">提示内容</param>
		/// <param name="isYesNo">提示内容，true是 “是/否”，false为“确定”、“取消”</param>
		/// <returns></returns>
		public bool Question(string title, string content, bool isYesNo)
		{
			return MessageBox.Show(this, title, content, isYesNo ? MessageBoxButtons.YesNo : MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == (isYesNo ? DialogResult.Yes : DialogResult.OK);
		}


		/// <summary>
		/// 显示错误对话框
		/// </summary>
		/// <param name="content">要显示的内容</param>
		public void Error(string content)
		{
			Error("错误", content);
		}

		/// <summary>
		/// 显示错误对话框
		/// </summary>
		/// <param name="title">要显示的标题</param>
		/// <param name="content">要显示的内容</param>
		public void Error(string title, string content)
		{
			MessageBox.Show(this, content, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}


	}
}
