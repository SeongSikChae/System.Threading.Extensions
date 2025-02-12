namespace System.Threading.PerformanceCounter
{
	using Atomic;
	using Collections.Concurrent;

	/// <summary>
	/// 성능 모니터링을 위한 카운터를 제공합니다.
	/// </summary>
	public sealed class PerformanceCounterProvider
	{
		private readonly ConcurrentDictionary<string, AtomicInt64> map = new ConcurrentDictionary<string, AtomicInt64>();

		/// <summary>
		/// 특정한 ID를 부여받은 카운터를 가져옵니다.
		/// </summary>
		public AtomicInt64 GetCounter(string id)
		{
			if (!map.TryGetValue(id, out AtomicInt64? counter)) 
			{
				counter = new AtomicInt64();
				map.TryAdd(id, counter);
			}	
			return counter;
		}
	}
}
