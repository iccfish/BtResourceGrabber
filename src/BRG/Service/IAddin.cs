using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRG.Service
{
	using System.ComponentModel.Composition.Hosting;

	/// <summary>
	/// 插件接口
	/// </summary>
	public interface IAddin
	{
		/// <summary>
		/// 扩展信息
		/// </summary>
		BRG.Entities.IServiceInfo Info { get; }

		/// <summary>
		/// 初始化
		/// </summary>
		void Init(CompositionContainer container);

		/// <summary>
		/// 获得或设置是否禁用
		/// </summary>
		bool Disabled { get; set; }

		/// <summary>
		/// 当禁用状态发生变化时触发
		/// </summary>
		event EventHandler DisabledChanged;

		/// <summary>
		/// 是否支持选项
		/// </summary>
		bool SupportOption { get; }

		/// <summary>
		/// 显示选项
		/// </summary>
		void ShowOption();

		/// <summary>
		/// 连接
		/// </summary>
		void Connect();

		/// <summary>
		/// 断开连接
		/// </summary>
		void Disconnect();

		/// <summary>
		/// 从指定版本升级
		/// </summary>
		/// <param name="oldVersion"></param>
		void UpgradeFrom(string oldVersion, string currentVersion);
	}
}
