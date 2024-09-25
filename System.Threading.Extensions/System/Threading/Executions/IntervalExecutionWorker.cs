using Microsoft.Extensions.Logging;

namespace System.Threading.Executions
{
	internal sealed class IntervalExecutionWorker(IExecution execution, TimeSpan interval, CancellationToken cancellationToken, ILogger<IntervalExecutionWorker> logger) : IExecutionWorker
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
				task?.Wait((int)interval.TotalMilliseconds, cancellationToken);
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
