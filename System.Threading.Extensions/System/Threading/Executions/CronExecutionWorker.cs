using Microsoft.Extensions.Logging;
using Quartz;

namespace System.Threading.Executions
{
	internal sealed class CronExecutionWorker(IExecution execution, CronExpression expression, CancellationToken cancellationToken, ILogger<CronExecutionWorker> logger) : IExecutionWorker
	{
		public void Start()
		{
			logger.Information($"execution '{execution.Id}' starting.");
			task = Task.Run(Execute);
		}

		private Task? task;

		public void Execute()
		{
			Thread.CurrentThread.Name = execution.Id;
			logger.Information($"execution '{execution.Id}' waiting.");
			try
			{
				DateTimeOffset currentOffset = DateTimeOffset.UtcNow;
				DateTimeOffset? afterOffset = expression.GetTimeAfter(currentOffset);
				if (afterOffset.HasValue)
				{
					TimeSpan span = afterOffset.Value.AddSeconds(TimeZoneInfo.Local.BaseUtcOffset.TotalSeconds) - currentOffset.AddSeconds(TimeZoneInfo.Local.BaseUtcOffset.TotalSeconds);
					task?.Wait((int)span.TotalMilliseconds, cancellationToken);
				}
				if (!cancellationToken.IsCancellationRequested)
				{
					switch (execution)
					{
						case AsyncExecution asyncExecution:
							asyncExecution.ExecuteAsync(cancellationToken).Wait();
							break;
						case SyncExecution syncExecution:
							syncExecution.Execute(cancellationToken);
							break;
					}
				}
				logger.Information($"execution '{execution.Id}' executed.");
			}
			catch (Exception e)
			{
				logger.Error($"error in exection '{execution.Id}'", e);
			}
			logger.Information($"execution '{execution.Id}' stopped.");
		}		
	}
}
