using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRG.Service
{
	using System.ComponentModel.Composition.Hosting;
	using BRG.Entities;

	public abstract class AddinBase : IAddin
	{
		/// <summary>
		/// 扩展信息
		/// </summary>
		public IServiceInfo Info { get; protected set; }

		/// <summary>
		/// 初始化
		/// </summary>
		public virtual void Init(CompositionContainer container)
		{
		}

		bool _disabled;

		/// <summary>
		/// 获得或设置是否禁用
		/// </summary>
		public bool Disabled
		{
			get { return _disabled; }
			set
			{
				if (_disabled == value)
					return;

				_disabled = value;
				OnDisabledChanged();
			}
		}

		public event EventHandler DisabledChanged;

		/// <summary>
		/// 是否支持选项
		/// </summary>
		public bool SupportOption { get; protected set; } = false;

		/// <summary>
		/// 显示选项
		/// </summary>
		public void ShowOption()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 连接
		/// </summary>
		public void Connect()
		{
		}

		/// <summary>
		/// 断开连接
		/// </summary>
		public void Disconnect()
		{
		}

		/// <summary>
		/// 从指定版本升级
		/// </summary>
		/// <param name="oldVersion"></param>
		public virtual void UpgradeFrom(string oldVersion, string currentVersion)
		{

		}

		protected virtual void OnDisabledChanged()
		{
			DisabledChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
