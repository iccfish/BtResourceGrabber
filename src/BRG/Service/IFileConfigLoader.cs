using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRG.Service
{
	public interface IFileConfigLoader
	{
		string Root { get; }
		/// <summary>
		/// 加载
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		T Load<T>(string catalog = null);

		/// <summary>
		/// 保存
		/// </summary>
		void Save<T>(T obj, string catalog = null);
	}
}
