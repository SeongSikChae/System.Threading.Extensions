using Microsoft.Extensions.Logging;

namespace System.Threading.Tasks
{
	using Atomic;

	internal sealed class IntervalTaskWorker(string id, ITask jobTask, TimeSpan interval, ILogger<IntervalTaskWorker> logger) : ITaskWorker
	{
		public DateTime? NextExecutionTime
		{
			get => nextExecutionTime.Value > -1 ? nextExecutionTime.Value.FromUnixTimeMilliseconds(DateTimeKind.Local) : null;
		}

		private readonly AtomicInt64 nextExecutionTime = new AtomicInt64(-1);

		public void Start()
		{
			logger.Information($"task '{id}' starting.");
			task = Task.Run(Run);
		}

		private Task? task;
		private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
		private CancellationTokenSource interruptTokenSource = new CancellationTokenSource();

		public void Run()
		{
			Thread.CurrentThread.Name = id;
			logger.Information($"task '{id}' started.");
			while (true)
			{
				if (cancellationTokenSource.IsCancellationRequested)
					break;
				try
				{
					try
					{
						nextExecutionTime.Value = DateTime.Now.AddMilliseconds(interval.TotalMilliseconds).ToMilliseconds();
						task?.Wait((int)interval.TotalMilliseconds, interruptTokenSource.Token);
					}
					catch (OperationCanceledException)
					{
						interruptTokenSource = new CancellationTokenSource();
					}
					if (cancellationTokenSource.IsCancellationRequested)
						break;
					switch (jobTask)
					{
						case AsyncTask asyncTask:
							asyncTask.RunAsync(cancellationTokenSource.Token).Wait();
							break;
						case SyncTask syncTask:
							syncTask.Run(cancellationTokenSource.Token);
							break;
					}
				}
				catch (Exception e)
				{
					logger.Error($"error in task '{id}'", e);
				}
			}
			logger.Information($"task '{id}' stopped.");
		}

		public void WakeUp()
		{
			interruptTokenSource.Cancel();
		}

		public void Join()
		{
			task?.Wait(cancellationTokenSource.Token);
		}

		public void Stop()
		{
			logger.Information($"task '{id}' stopping.");
			cancellationTokenSource.Cancel();
			interruptTokenSource.Cancel();
			task?.Wait();
		}
	}
}
