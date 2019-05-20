using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRG.Service
{
	using System.Windows.Forms;
	using BRG.Entities;

	/// <summary>
	/// 应用于资源右键菜单的扩展
	/// </summary>
	public interface IResourceContextMenuAddin : IAddin
	{
		/// <summary>
		/// 注册
		/// </summary>
		/// <param name="ctx"></param>
		/// <param name="providers"></param>
		void Register(ContextMenuStrip ctx, IResourceProvider[] providers);

		/// <summary>
		/// 打开的时候回调
		/// </summary>
		/// <param name="resourceInfos"></param>
		void OnOpening(params IResourceInfo[] resourceInfos);
	}
}
