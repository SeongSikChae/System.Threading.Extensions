namespace System.Threading.Atomic
{
	using Runtime.CompilerServices;

	/// <summary>
	/// System.Int32 에 대한 원자성을 보장합니다.
	/// </summary>
	/// <param name="initialValue"></param>
	public sealed class AtomicInt32(int initialValue = 0) : IAtomic<int>
	{
		private int atomicValue = initialValue;

		public int Value
		{
			get => Interlocked.CompareExchange(ref Unsafe.AsRef(in atomicValue), 0, 0);
			set => Interlocked.Exchange(ref atomicValue, value);
		}

		public int AddThenGet(int newValue)
		{
			return Interlocked.Add(ref atomicValue, newValue);
		}

		public bool CompareAndSet(int expect, int update)
		{
			return Interlocked.CompareExchange(ref Unsafe.AsRef(in atomicValue), update, expect) == expect;
		}

		public int DecrementThenGet()
		{
			return Interlocked.Decrement(ref atomicValue);
		}

		public int GetThenAdd(int newValue)
		{
			return Interlocked.Exchange(ref atomicValue, atomicValue + newValue);
		}

		public int GetThenDecrement()
		{
			return Interlocked.Exchange(ref atomicValue, atomicValue - 1);
		}

		public int GetThenIncrement()
		{
			return Interlocked.Exchange(ref atomicValue, atomicValue + 1);
		}

		public int IncrementThenGet()
		{
			return Interlocked.Increment(ref atomicValue);
		}
	}
}
