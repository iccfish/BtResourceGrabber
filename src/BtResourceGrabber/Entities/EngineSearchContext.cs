namespace BtResourceGrabber.Entities
{
	using System;
	using System.Diagnostics;
	using BRG;
	using BRG.Engines.Handlers;
	using BRG.Entities;
	using BRG.Service;
	using BtResourceGrabber.Service;

	public class EngineSearchContext
	{
		BackgroundWorker _worker;

		public int ErrorCount { get; set; } = 0;

		int _currentLoadedPage;
		bool _requireCancel;
		string _searchKey;

		ResourceFilterProcessor _filter = new ResourceFilterProcessor();

		public EngineSearchContext(IResourceProvider provider)
		{
			Provider = provider;

			_worker = new BackgroundWorker();
			_worker.DoWork += _worker_DoWork;
			_worker.WorkCompleted += _worker_WorkCompleted;
			_worker.WorkCancelled += _worker_WorkCancelled;
			_worker.WorkFailed += _worker_WorkFailed;
		}

		private void _worker_WorkFailed(object sender, RunworkEventArgs e)
		{
			if (_requireCancel)
			{
				_worker_WorkCancelled(sender, e);
			}
			else
			{
				HasMore = Result?.HasMore ?? ErrorCount < 3;
				AppContext.Instance.Statistics.UpdateRunningStatistics(true, Provider, 0, 0, 0, 1);
				CurrentLoadedPage--;
				SearchedCount--;
				ErrorCount++;
				OnSearchFailed();
			}
		}

		private void _worker_WorkCancelled(object sender, RunworkEventArgs e)
		{
			HasMore = Result?.HasMore ?? true;
			CurrentLoadedPage--;
			SearchedCount--;
			ErrorCount++;
			OnSearchCancelled();
		}

		private void _worker_WorkCompleted(object sender, RunworkEventArgs e)
		{
			AppContext.Instance.Statistics.UpdateRunningStatistics(true, Provider, 0, 0, 1, 0);
			HasMore = Result?.HasMore ?? true;
			OnSearchComplete();
		}

		private void _worker_DoWork(object sender, RunworkEventArgs e)
		{
			var opt = AppContext.Instance.Options;
			AppContext.Instance.Statistics.UpdateRunningStatistics(true, Provider, 1, 0, 0, 0);

			try
			{
				Result = Provider.Load(SearchKey, opt.SortType, opt.SortDirection, opt.PageSize, CurrentLoadedPage);
			}
			catch (Exception ex)
			{
				Trace.TraceError(ex.ToString());
			}

			if (Result == null)
				throw new LoadFailedException();
			_filter.Filter(Result, SearchKey);
		}

		public IResourceProvider Provider { get; private set; }

		public bool HasMore { get; private set; }


		public int CurrentLoadedPage
		{
			get { return _currentLoadedPage; }
			set
			{
				_currentLoadedPage = value;
			}
		}

		/// <summary>
		/// 已搜索次数
		/// </summary>
		public int SearchedCount { get; set; }


		public string SearchKey
		{
			get { return _searchKey; }
			set { _searchKey = _filter.ParseDirective(value); }
		}

		/// <summary>
		/// 获得当前的搜索结果
		/// </summary>
		public IResourceSearchInfo Result { get; private set; }

		/// <summary>
		/// 搜索完成
		/// </summary>
		public event EventHandler SearchComplete;

		public event EventHandler SearchCancelled;

		/// <summary>
		/// 引发 <see cref="SearchCancelled" /> 事件
		/// </summary>
		protected virtual void OnSearchCancelled()
		{
			var handler = SearchCancelled;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		public event EventHandler SearchFailed;

		/// <summary>
		/// 引发 <see cref="SearchFailed" /> 事件
		/// </summary>
		protected virtual void OnSearchFailed()
		{
			var handler = SearchFailed;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		public event EventHandler SearchBegin;

		/// <summary>
		/// 引发 <see cref="SearchBegin" /> 事件
		/// </summary>
		protected virtual void OnSearchBegin()
		{
			var handler = SearchBegin;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}


		/// <summary>
		/// 引发 <see cref="SearchComplete" /> 事件
		/// </summary>
		protected virtual void OnSearchComplete()
		{
			var handler = SearchComplete;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 取消任务
		/// </summary>
		public void Cancel()
		{
			_requireCancel = true;
			if (_worker.IsBusy)
				_worker.KillTask();
		}

		public void DoSearch()
		{
			if (_worker.IsBusy)
				return;

			Debug.WriteLine("Request Search.");
			_requireCancel = false;
			HasMore = true; //假定有下一页
			Result = null;
			OnSearchBegin();
			_worker.RunWorkASync();
		}

		public bool IsBusy { get { return _worker.IsBusy; } }
	}
}
