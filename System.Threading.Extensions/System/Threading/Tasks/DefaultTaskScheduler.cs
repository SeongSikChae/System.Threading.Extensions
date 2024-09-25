using Microsoft.Extensions.Logging;
using Quartz;

namespace System.Threading.Tasks
{
	using Atomic;
	using Collections.Concurrent;

	/// <summary>
	/// 기본 태스크 스케줄러 구현체
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="loggerFactory"></param>
	public sealed class DefaultTaskScheduler(ILogger<DefaultTaskScheduler> logger, ILoggerFactory loggerFactory) : ITaskScheduler
	{
		private readonly ConcurrentDictionary<string, ITaskWorker> tasks = new ConcurrentDictionary<string, ITaskWorker>();

		/// <summary>
		/// TimeSpan Interval 주기로 실행되는 태스크 스케줄 추가
		/// </summary>
		/// <param name="id"></param>
		/// <param name="task"></param>
		/// <param name="interval"></param>
		/// <exception cref="TaskExistException"></exception>
		public void AddTask(string id, ITask task, TimeSpan interval)
		{
			if (waitForShutdown.Value)
				return;
			if (tasks.ContainsKey(id))
				throw new TaskExistException($"task '{id}' exist already");
			ITaskWorker worker = new IntervalTaskWorker(id, task, interval, loggerFactory.CreateLogger<IntervalTaskWorker>());
			worker.Start();
			tasks.TryAdd(id, worker);
		}

		private readonly AtomicBool waitForShutdown = new AtomicBool();

		/// <summary>
		/// Cron Expression 주기로 실행되는 태스크 스케줄 추가
		/// </summary>
		/// <param name="id"></param>
		/// <param name="task"></param>
		/// <param name="expression"></param>
		/// <exception cref="TaskExistException"></exception>
		public void AddTask(string id, ITask task, CronExpression expression)
		{
			if (waitForShutdown.Value)
				return;
			if (tasks.ContainsKey(id))
				throw new TaskExistException($"task '{id}' exist already");
			ITaskWorker worker = new CronTaskWorker(id, task, expression, loggerFactory.CreateLogger<CronTaskWorker>());
			worker.Start();
			tasks.TryAdd(id, worker);
		}

		/// <summary>
		/// 특정 ID에 맞는 태스크 스케줄이 존재하는지 여부
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool ContainTask(string id)
		{
			return tasks.ContainsKey(id);
		}

		/// <summary>
		/// 특정 ID에 맞는 태스크 스케줄의 다음 실행 시간을 가져온다.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public DateTime? NextExecutionTime(string id)
		{
			if (tasks.TryGetValue(id, out ITaskWorker? worker))
				return worker.NextExecutionTime;
			return null;
		}

		/// <summary>
		/// 특정 ID에 맞는 대기중인 태스크를 깨웁니다.
		/// </summary>
		/// <param name="id"></param>
		public void WakeUp(string id)
		{
			if (tasks.TryGetValue(id, out ITaskWorker? worker))
				worker.WakeUp();
		}

		/// <summary>
		/// 특정 ID에 맞는 태스크를 스케줄에서 삭제합니다.
		/// </summary>
		/// <param name="id"></param>
		public void RemoveTask(string id)
		{
			if (tasks.TryGetValue(id, out ITaskWorker? worker))
			{
				worker.Stop();
				tasks.TryRemove(id, out _);
			}
		}

		/// <summary>
		/// 모든 태스크 스케줄이 중지되기를 기다립니다.
		/// </summary>
		public void WaitForShutdown()
		{
			waitForShutdown.Value = true;
			IEnumerable<string> enumerable = tasks.Keys.ToList();
			foreach (string id in enumerable)
			{
				logger.Information($"shutdown wait '{id}");
				if (tasks.TryGetValue(id, out ITaskWorker? worker))
					worker.Join();
				logger.Information($"shutdown completed '{id}'");
			}
		}
	}
}
