namespace BtResourceGrabber.Service
{
	using System;
	using System.Linq;
	using System.Threading;

	/// <summary>
	/// 任务运行事件数据
	/// </summary>
	public class RunworkEventArgs : EventArgs
	{
		/// <summary>
		/// 运行参数
		/// </summary>
		public object Argument { get; set; }

		/// <summary>
		/// 运行结果
		/// </summary>
		public object Result { get; set; }

		/// <summary>
		/// 运行中出现的错误
		/// </summary>
		public Exception Exception { get; set; }

		/// <summary>
		/// 正在运行任务的线程
		/// </summary>
		public Thread Thread { get; set; }

		/// <summary>
		/// 当前任务运行的进度
		/// </summary>
		public ProgressIdentify Progress { get; private set; }

		/// <summary>
		/// 获得或取得一个值，表明当前任务是否已经声明取消
		/// </summary>
		public bool CancellationPending { get; set; }

		/// <summary>
		/// 获得或设置一个值，表明当前正在执行的任务是否成功完成
		/// </summary>
		public bool Succeed { get; set; }

		/// <summary>
		/// 当前的任务管理对象
		/// </summary>
		BackgroundWorker _worker;

		/// <summary>
		/// Initializes a new instance of the RunworkEventArgs class.
		/// </summary>
		internal RunworkEventArgs(BackgroundWorker worker)
		{
			this.Progress = new ProgressIdentify();
			this._worker = worker;
			this.Succeed = false;
		}


		#region 内联类型
		/// <summary>
		/// 进度声明类
		/// </summary>
		public class ProgressIdentify
		{
			/// <summary>
			/// 任务总数
			/// </summary>
			public int TaskCount { get; set; }

			/// <summary>
			/// 任务进度
			/// </summary>
			public int TaskProgress { get; set; }

			/// <summary>
			/// 获得或设置任务进度的百分比
			/// </summary>
			public int TaskPercentage
			{
				get
				{
					if (TaskCount == 0)
					{
						if (TaskProgress >= 0 && TaskProgress <= 100) return TaskProgress;
						else return 0;
					}
					else
						return (int)Math.Floor(TaskProgress * 100.0 / TaskCount);
				}
				set
				{
					if (value < 0 || value > 100) throw new ArgumentOutOfRangeException();
					TaskCount = 0;
					TaskProgress = value;
				}
			}

			/// <summary>
			/// 进度信息
			/// </summary>
			public string StateMessage { get; set; }

			/// <summary>
			/// 获得当前操作的详细信息
			/// </summary>
			public string DetailMessage { get; set; }

			/// <summary>
			/// 用户自定义进度对象
			/// </summary>
			public object UserState { get; set; }

			/// <summary>
			/// 重置当前进度的状态
			/// </summary>
			public void Reset()
			{
				TaskCount = TaskProgress = 0;
				UserState = StateMessage = DetailMessage = null;

			}
		}
		#endregion
	
		/// <summary>
		/// 设置操作进度信息
		/// </summary>
		/// <param name="currentTaskIndex">当前任务索引</param>
		public void ReportProgress(int currentTaskIndex)
		{
			this.Progress.TaskProgress = currentTaskIndex;

			ReportProgress();
		}

		/// <summary>
		/// 设置操作进度信息
		/// </summary>
		/// <param name="currentTaskIndex">当前任务索引</param>
		/// <param name="state">操作信息</param>
		public void ReportProgress(int currentTaskIndex, string state)
		{
			this.Progress.StateMessage = state;
			this.Progress.TaskProgress = currentTaskIndex;

			ReportProgress();
		}

		/// <summary>
		/// 设置操作进度信息
		/// </summary>
		/// <param name="taskCount">任务的总数目</param>
		/// <param name="currentTaskIndex">当前任务索引</param>
		/// <param name="state">操作信息</param>
		public void ReportProgress(int taskCount, int currentTaskIndex, string state)
		{
			this.Progress.StateMessage = state;
			this.Progress.TaskCount = taskCount;
			this.Progress.TaskProgress = currentTaskIndex;

			ReportProgress();
		}


		/// <summary>
		/// 设置操作进度信息
		/// </summary>
		/// <param name="currentTaskIndex">当前任务索引</param>
		/// <param name="state">操作信息</param>
		/// <param name="detail">详细信息</param>
		public void ReportProgress(int currentTaskIndex, string state, string detail)
		{
			this.Progress.DetailMessage = detail;
			this.Progress.StateMessage = state;
			this.Progress.TaskProgress = currentTaskIndex;

			ReportProgress();
		}

		/// <summary>
		/// 设置操作进度信息
		/// </summary>
		/// <param name="taskCount">任务的总数目</param>
		/// <param name="currentTaskIndex">当前任务索引</param>
		/// <param name="state">操作信息</param>
		/// <param name="detail">详细信息</param>
		public void ReportProgress(int taskCount, int currentTaskIndex, string state, string detail)
		{
			this.Progress.DetailMessage = detail;
			this.Progress.StateMessage = state;
			this.Progress.TaskCount = taskCount;
			this.Progress.TaskProgress = currentTaskIndex;

			ReportProgress();
		}


		/// <summary>
		/// 设置操作进度信息
		/// </summary>
		/// <param name="taskCount">任务的总数目</param>
		/// <param name="currentTaskIndex">当前任务索引</param>
		/// <param name="state">操作信息</param>
		/// <param name="detail">详细信息</param>
		/// <param name="stateObject">操作的进度对象</param>
		public void ReportProgress(int taskCount, int currentTaskIndex, string state, string detail, object stateObject)
		{
			this.Progress.DetailMessage = detail;
			this.Progress.StateMessage = state;
			this.Progress.TaskCount = taskCount;
			this.Progress.TaskProgress = currentTaskIndex;

			ReportProgress();
		}

		/// <summary>
		/// 报告当前进度变化
		/// </summary>
		public void ReportProgress()
		{
			if (!_worker.WorkerSupportReportProgress) throw new InvalidOperationException("ReportProgressNotSupported");

			this._worker.ReportProgress();
		}
	}
}
