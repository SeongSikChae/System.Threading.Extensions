using Microsoft.Extensions.Logging;

namespace System.Threading.Executions.Tests
{
	using Diagnostics;

	[TestClass]
	public class DefaultExecutorTests
	{
		[TestMethod]
		public void ExecuteTesst()
		{
			using ICountdown countdown = CountdownFactory.Create(1);

			DefaultExecutor executor = new DefaultExecutor(new TraceLoggerFactory());
			executor.Execute(new DummyExecution(countdown), TimeSpan.FromSeconds(5));
			countdown.Wait();
		}

		private sealed class DummyExecution(ICountdown countdown) : SyncExecution
		{
			public override string Id => "Dummy";

			public override void Dispose()
			{
			}

			public override void Execute(CancellationToken cancellationToken)
			{
				Trace.WriteLine("Execute");
				countdown.Signal();
			}
		}

		private sealed class TraceLoggerFactory : ILoggerFactory
		{
			public void AddProvider(ILoggerProvider provider)
			{
				throw new NotImplementedException();
			}

			public ILogger CreateLogger(string categoryName)
			{
				Type? t = typeof(ITaskScheduler).Assembly.GetType(categoryName);
				ArgumentNullException.ThrowIfNull(t);
				Type loggerType = typeof(TraceLogger<>).MakeGenericType(t);
				ILogger? logger = Activator.CreateInstance(loggerType) as ILogger;
				ArgumentNullException.ThrowIfNull(logger);
				return logger;
			}

			public void Dispose()
			{
			}
		}

		private sealed class TraceLogger<T> : ILogger<T>
		{
			public IDisposable? BeginScope<TState>(TState state) where TState : notnull
			{
				throw new NotImplementedException();
			}

			public bool IsEnabled(LogLevel logLevel)
			{
				throw new NotImplementedException();
			}

			public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
			{
				Trace.WriteLine(formatter(state, exception));
			}
		}
	}
}