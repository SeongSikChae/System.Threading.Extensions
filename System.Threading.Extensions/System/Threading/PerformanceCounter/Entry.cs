using System.Threading.Atomic;

namespace System.Threading.PerformanceCounter
{
	internal class Entry(AtomicInt64 counter)
	{
		public AtomicInt64 Counter => counter;

		public long Previous { get; set; }
	}
}
