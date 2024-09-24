namespace System.Threading.Atomic
{
	using Runtime.CompilerServices;

	/// <summary>
	/// System.Int64 에 대한 원자성을 보장합니다.
	/// </summary>
	/// <param name="initialValue">초기값</param>
	public sealed class AtomicInt64(long initialValue = 0) : IAtomic<long>
	{
		private long atomicValue = initialValue;

		/// <summary>
		/// 원자성이 보장되는 상태로 System.Int64 값을 읽고 씁니다.
		/// </summary>
		public long Value
		{
			get => Interlocked.CompareExchange(ref Unsafe.AsRef(in atomicValue), 0, 0);
			set => Interlocked.Exchange(ref atomicValue, value);
		}

		/// <summary>
		/// 값을 비교하여 같으면 신규값으로 변경하고, 다르면 변경하지 않습니다.
		/// </summary>
		/// <param name="expect">비교대상 값</param>
		/// <param name="update">변경될 값</param>
		/// <returns>변경 여부</returns>
		public bool CompareAndSet(long expect, long update)
		{
			return Interlocked.CompareExchange(ref Unsafe.AsRef(in atomicValue), update, expect) == expect;
		}

		/// <summary>
		/// 기존 값을 반환하면서 특정 값을 더합니다.
		/// </summary>
		/// <param name="newValue">더해질 값</param>
		/// <returns>더해지기 이전 값</returns>
		public long GetThenAdd(long newValue)
		{
			return Interlocked.Exchange(ref atomicValue, atomicValue + newValue);
		}

		/// <summary>
		/// 특정 값을 더한 후 결과를 반환합니다.
		/// </summary>
		/// <param name="newValue">더해질 값</param>
		/// <returns>더해진 후 결과 값</returns>
		public long AddThenGet(long newValue)
		{
			return Interlocked.Add(ref atomicValue, newValue);
		}

		/// <summary>
		/// 기존값을 반환 후 값을 증가시킵니다.
		/// </summary>
		/// <returns>증가 이전 값</returns>
		public long GetThenIncrement()
		{
			return Interlocked.Exchange(ref atomicValue, atomicValue + 1);
		}

		/// <summary>
		/// 증가 후 결과 값을 반환합니다.
		/// </summary>
		/// <returns>증가 이후 결과값</returns>
		public long IncrementThenGet()
		{
			return Interlocked.Increment(ref atomicValue);
		}

		/// <summary>
		/// 기존값을 반환 후 값을 감소시킵니다.
		/// </summary>
		/// <returns>감소 이전 값</returns>
		public long GetThenDecrement()
		{
			return Interlocked.Exchange(ref atomicValue, atomicValue - 1);
		}

		/// <summary>
		/// 값을 감소 시킨 후 반환합니다.
		/// </summary>
		/// <returns>감소 이후 값</returns>
		public long DecrementThenGet()
		{
			return Interlocked.Decrement(ref atomicValue);
		}

		/// <summary>
		/// AtomicInt64 에 대한 ToString Override
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return $"{Value}";
		}
	}
}
