namespace BRG.Service
{
	using System;
	using BRG.Entities;

	/// <summary>
	/// 服务基础
	/// </summary>
	public interface IServiceBase : IAddin
	{

		/// <summary>
		/// 测试服务状态
		/// </summary>
		/// <returns></returns>
		TestStatus Test();


		/// <summary>
		/// 是否需要科学上网
		/// </summary>
		bool RequireBypassGfw { get; }

	}
}
