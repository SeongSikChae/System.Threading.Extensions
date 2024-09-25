using Microsoft.Extensions.Logging;
using Quartz;

namespace System.Threading.Tasks
{
	using Atomic;

	internal sealed class CronTaskWorker(string id, ITask jobTask, CronExpression expression, ILogger<CronTaskWorker> logger) : ITaskWorker
	{
		public DateTime? NextExecutionTime => nextExecutionTime.Value > -1 ? nextExecutionTime.Value.FromUnixTimeMilliseconds(DateTimeKind.Local) : null;

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
						DateTimeOffset currentOffset = DateTimeOffset.UtcNow;
						DateTimeOffset? afterOffset = expression.GetTimeAfter(currentOffset);
						if (afterOffset.HasValue)
						{
							TimeSpan span = afterOffset.Value.AddSeconds(TimeZoneInfo.Local.BaseUtcOffset.TotalSeconds) - currentOffset.AddSeconds(TimeZoneInfo.Local.BaseUtcOffset.TotalSeconds);
							nextExecutionTime.Value = DateTime.Now.AddMilliseconds(span.TotalMilliseconds).ToMilliseconds();
							task?.Wait((int)span.TotalMilliseconds, interruptTokenSource.Token);
						}
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
