namespace BRG.Engines
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using BRG.Entities;
	using BRG.Service;

	class ResourceProcessorWrapper :AddinBase, IResourceProcessor
	{
		#region 单例模式

		static ResourceProcessorWrapper _instance;
		static readonly object _lockObject = new object();

		public static ResourceProcessorWrapper Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = new ResourceProcessorWrapper();
						}
					}
				}

				return _instance;
			}
		}

		#endregion

		/// <summary>
		/// 资源已加载，但是尚未处理
		/// </summary>
		/// <param name="searchResult"></param>
		public void ResourcesLoaded(IResourceSearchInfo searchResult)
		{
			var processors = AppContext.Instance.ResourceProcessors;
			if (processors == null)
				return;

			foreach (var processor in processors)
			{
				try
				{
					processor.ResourcesLoaded(searchResult);
				}
				catch (Exception ex)
				{
					Trace.TraceError(ex.ToString());
				}
			}
		}

		/// <summary>
		/// 资源已处理，准备供显示
		/// </summary>
		/// <param name="items"></param>
		public void ResourcesFetched(IResourceSearchInfo items)
		{
			var processors = AppContext.Instance.ResourceProcessors;
			if (processors == null)
				return;

			foreach (var processor in processors)
			{
				try
				{
					processor.ResourcesFetched(items);
				}
				catch (Exception ex)
				{
					Trace.TraceError(ex.ToString());
				}
			}
		}


	}
}
