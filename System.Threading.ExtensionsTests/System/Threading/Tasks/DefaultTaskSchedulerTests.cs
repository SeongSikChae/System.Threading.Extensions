using Microsoft.Extensions.Logging;

namespace System.Threading.Tasks.Tests
{
	using Diagnostics;
	using Quartz;

	[TestClass]
	public class DefaultTaskSchedulerTests
	{
		[TestMethod]
		public void AddIntervalTaskTest()
		{
			using ICountdown countdown = CountdownFactory.Create(5);

			DefaultTaskScheduler taskScheduler = new DefaultTaskScheduler(new TraceLogger<DefaultTaskScheduler>(), new TraceLoggerFactory());
			taskScheduler.AddTask("T", new DummyTask(countdown), TimeSpan.FromSeconds(1));

			countdown.Wait();

			taskScheduler.RemoveTask("T");
			taskScheduler.WaitForShutdown();
		}

		[TestMethod]
		public void AddCronTaskTest()
		{
			using ICountdown countdown = CountdownFactory.Create(5);

			DefaultTaskScheduler taskScheduler = new DefaultTaskScheduler(new TraceLogger<DefaultTaskScheduler>(), new TraceLoggerFactory());
			taskScheduler.AddTask("T", new DummyTask(countdown), new CronExpression("* * * * * ?"));

			countdown.Wait();

			taskScheduler.RemoveTask("T");
			taskScheduler.WaitForShutdown();
		}

		[TestMethod]
		public void NextExecutionTimeTest()
		{
			using ICountdown countdown = CountdownFactory.Create(1);

			DefaultTaskScheduler taskScheduler = new DefaultTaskScheduler(new TraceLogger<DefaultTaskScheduler>(), new TraceLoggerFactory());
			taskScheduler.AddTask("T", new DummyTask(countdown), TimeSpan.FromMinutes(1));

			DateTime? nextExecutionTime = null;
			while (true)
			{
				nextExecutionTime = taskScheduler.NextExecutionTime("T");
				if (nextExecutionTime is null)
					continue;
				Trace.WriteLine($"{nextExecutionTime:yyyy-MM-dd HH:mm:ss.fff} {nextExecutionTime.Value.ToMilliseconds()}");
				break;
            }

			taskScheduler.WakeUp("T");
			countdown.Wait();

			Thread.Sleep(TimeSpan.FromSeconds(3));
			nextExecutionTime = taskScheduler.NextExecutionTime("T");
			

			Trace.WriteLine($"{nextExecutionTime:yyyy-MM-dd HH:mm:ss.fff} {(nextExecutionTime.HasValue ? nextExecutionTime.Value.ToMilliseconds() : -1)}");

			taskScheduler.RemoveTask("T");
			taskScheduler.WaitForShutdown();
		}

		private sealed class DummyTask(ICountdown countdown) : SyncTask
		{
			public override void Dispose()
			{
			}

			public override void Run(CancellationToken cancellationToken)
			{
				if (!countdown.IsSet)
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