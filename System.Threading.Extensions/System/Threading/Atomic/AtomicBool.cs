using System.Runtime.CompilerServices;

namespace System.Threading.Atomic
{
	/// <summary>
	/// System.Boolean 에 대한 원자성을 보장합니다.
	/// </summary>
	/// <param name="initialValue"></param>
	public sealed class AtomicBool(bool initialValue = false) : IAtomic<bool>
	{
		private int atomicValue = initialValue ? 1 : 0;

		/// <summary>
		/// 원자성이 보장되는 상태로 System.Boolean 값을 읽고 씁니다.
		/// </summary>
		public bool Value
		{
			get => Interlocked.CompareExchange(ref Unsafe.AsRef(in atomicValue), 0, 0) == 1;
			set => Interlocked.Exchange(ref atomicValue, value ? 1 : 0);
		}

		/// <summary>
		/// 지원되지 않습니다.
		/// </summary>
		/// <param name="newValue"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public bool AddThenGet(bool newValue)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 값을 비교하여 같으면 신규값으로 변경하고, 다르면 변경하지 않습니다.
		/// </summary>
		/// <param name="expect">비교대상 값</param>
		/// <param name="update">변경될 값</param>
		/// <returns>변경 여부</returns>
		public bool CompareAndSet(bool expect, bool update)
		{
			return Interlocked.CompareExchange(ref Unsafe.AsRef(in atomicValue), update ? 1 : 0, expect ? 1 : 0) == (expect ? 1 : 0);
		}

		/// <summary>
		/// 지원되지 않습니다.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public bool DecrementThenGet()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 지원되지 않습니다.
		/// </summary>
		/// <param name="newValue"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public bool GetThenAdd(bool newValue)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 지원되지 않습니다.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public bool GetThenDecrement()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 지원되지 않습니다.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public bool GetThenIncrement()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 지원되지 않습니다.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public bool IncrementThenGet()
		{
			throw new NotImplementedException();
		}
	}
}
