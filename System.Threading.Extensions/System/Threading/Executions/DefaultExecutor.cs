using Microsoft.Extensions.Logging;
using Quartz;

namespace System.Threading.Executions
{
	using Atomic;

	/// <summary>
	/// 기본 IExecution 실행기 구현체
	/// </summary>
	/// <param name="loggerFactory"></param>
	public sealed class DefaultExecutor(ILoggerFactory loggerFactory) : IExecutor
	{
		/// <summary>
		/// TimeSpan Interval 대기 후 실행 요청
		/// </summary>
		/// <param name="execution"></param>
		/// <param name="interval"></param>
		public void Execute(IExecution execution, TimeSpan interval)
		{
			if (waitForShutdown.Value)
				return;
			IExecutionWorker worker = new IntervalExecutionWorker(execution, interval, cancellationTokenSource.Token, loggerFactory.CreateLogger<IntervalExecutionWorker>());
			worker.Start();
		}

		private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
		private readonly AtomicBool waitForShutdown = new AtomicBool();

		/// <summary>
		/// Cron Expression 만큼 대기 후 실행 요청
		/// </summary>
		/// <param name="execution"></param>
		/// <param name="expression"></param>
		public void Execute(IExecution execution, CronExpression expression)
		{
			if (waitForShutdown.Value)
				return;
			IExecutionWorker worker = new CronExecutionWorker(execution, expression, cancellationTokenSource.Token, loggerFactory.CreateLogger<CronExecutionWorker>());
			worker.Start();
		}

		/// <summary>
		/// 모든 실행 요청 중단
		/// </summary>
		public void Shutdown()
		{
            if (!cancellationTokenSource.IsCancellationRequested)
				cancellationTokenSource.Cancel();
		}
	}
}
