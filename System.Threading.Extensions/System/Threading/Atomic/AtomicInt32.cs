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

		/// <summary>
		/// 원자성이 보장되는 상태로 System.Int32 값을 읽고 씁니다.
		/// </summary>
		public int Value
		{
			get => Interlocked.CompareExchange(ref Unsafe.AsRef(in atomicValue), 0, 0);
			set => Interlocked.Exchange(ref atomicValue, value);
		}

		/// <summary>
		/// 특정 값을 더한 후 결과를 반환합니다.
		/// </summary>
		/// <param name="newValue">더해질 값</param>
		/// <returns>더해진 후 결과 값</returns>
		public int AddThenGet(int newValue)
		{
			return Interlocked.Add(ref atomicValue, newValue);
		}

		/// <summary>
		/// 값을 비교하여 같으면 신규값으로 변경하고, 다르면 변경하지 않습니다.
		/// </summary>
		/// <param name="expect">비교대상 값</param>
		/// <param name="update">변경될 값</param>
		/// <returns>변경 여부</returns>
		public bool CompareAndSet(int expect, int update)
		{
			return Interlocked.CompareExchange(ref Unsafe.AsRef(in atomicValue), update, expect) == expect;
		}
		/// <summary>
		/// 값을 감소 시킨 후 반환합니다.
		/// </summary>
		/// <returns>감소 이후 값</returns>
		public int DecrementThenGet()
		{
			return Interlocked.Decrement(ref atomicValue);
		}

		/// <summary>
		/// 기존 값을 반환하면서 특정 값을 더합니다.
		/// </summary>
		/// <param name="newValue">더해질 값</param>
		/// <returns>더해지기 이전 값</returns>
		public int GetThenAdd(int newValue)
		{
			return Interlocked.Exchange(ref atomicValue, atomicValue + newValue);
		}

		/// <summary>
		/// 기존값을 반환 후 값을 감소시킵니다.
		/// </summary>
		/// <returns>감소 이전 값</returns>
		public int GetThenDecrement()
		{
			return Interlocked.Exchange(ref atomicValue, atomicValue - 1);
		}

		/// <summary>
		/// 기존값을 반환 후 값을 증가시킵니다.
		/// </summary>
		/// <returns>증가 이전 값</returns>
		public int GetThenIncrement()
		{
			return Interlocked.Exchange(ref atomicValue, atomicValue + 1);
		}

		/// <summary>
		/// 증가 후 결과 값을 반환합니다.
		/// </summary>
		/// <returns>증가 이후 결과값</returns>
		public int IncrementThenGet()
		{
			return Interlocked.Increment(ref atomicValue);
		}
	}
}
