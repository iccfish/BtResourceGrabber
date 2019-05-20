namespace BRG.Engines.Handlers
{
	using System;

	public class LoadFailedException : Exception
	{
		/// <summary>
		/// 创建 <see cref="LoadFailedException" />  的新实例(LoadFailedException)
		/// </summary>
		public LoadFailedException()
			: base("资源搜索失败")
		{
			
		}
	}
}
