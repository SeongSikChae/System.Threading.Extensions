
using System.Collections.Concurrent;
using System.Threading.Atomic;

namespace System.Threading.PerformanceCounter
{
	/// <summary>
	/// 성능 카운터 메시지 로거
	/// </summary>
	/// <remarks>
	/// 성능 카운터 메시지 로거를 생성합니다.
	/// </remarks>
	public sealed class PerformanceLogger(PerformanceCounterProvider performanceCounterProvider, IPerformaceLogListener performaceLogListener, ITaskScheduler taskScheduler) : SyncTask
	{
		/// <summary>
		/// 성능 카운터 메시지 로거 TASK ID
		/// </summary>
		public const string TASK_ID = "PerformanceLoggerTask";

		private HashSet<string> idSet = new HashSet<string>();
		private readonly ConcurrentDictionary<string, Entry> entries = new ConcurrentDictionary<string, Entry>();

		/// <summary>
		/// 성능 카운터 메시지 로거를 초기화 합니다.
		/// </summary>
		public void Initialize(HashSet<string> idSet, TimeSpan interval)
		{
			taskScheduler.AddTask(TASK_ID, this, interval);
			this.idSet = idSet;

			foreach (string id in idSet)
			{
				AtomicInt64 counter = performanceCounterProvider.GetCounter(id);
				Entry entry = new Entry(counter);
				entries.TryAdd(id, entry);
			}
		}

		/// <inheritdoc />
		public override void Run(CancellationToken cancellationToken)
		{
			Dictionary<string, PerformanceMetric> dic = new Dictionary<string, PerformanceMetric>();
			foreach (KeyValuePair<string, Entry> pair in entries)
			{
				PerformanceMetric metric = new PerformanceMetric();
				long total = pair.Value.Counter.Value;
				metric.Count = total - pair.Value.Previous;
				metric.Total = total;
				dic.Add(pair.Key, metric);
			}
			performaceLogListener.Listen(dic);
		}

		/// <inheritdoc />
		public override void Dispose()
		{
			taskScheduler.RemoveTask(TASK_ID);
		}
	}
}
