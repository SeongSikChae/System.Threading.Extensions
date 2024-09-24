namespace System.Threading.Atomic
{
	/// <summary>
	/// Interface for values ​​that can be updated atomically
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IAtomic<T> where T : struct
	{
		/// <summary>
		/// 기존 값을 반환하면서 특정 값을 더합니다.
		/// </summary>
		/// <param name="newValue">더해질 값</param>
		/// <returns>더해지기 이전 값</returns>
		T GetThenAdd(T newValue);

		/// <summary>
		/// 특정 값을 더한 후 결과를 반환합니다.
		/// </summary>
		/// <param name="newValue">더해질 값</param>
		/// <returns>더해진 후 결과 값</returns>
		T AddThenGet(T newValue);

		/// <summary>
		/// 기존값을 반환 후 값을 증가시킵니다.
		/// </summary>
		/// <returns>증가 이전 값</returns>
		T GetThenIncrement();

		/// <summary>
		/// 증가 후 결과 값을 반환합니다.
		/// </summary>
		/// <returns>증가 이후 결과값</returns>
		T IncrementThenGet();

		/// <summary>
		/// 기존값을 반환 후 값을 감소시킵니다.
		/// </summary>
		/// <returns>감소 이전 값</returns>
		T GetThenDecrement();

		/// <summary>
		/// 값을 감소 시킨 후 반환합니다.
		/// </summary>
		/// <returns>감소 이후 값</returns>
		T DecrementThenGet();

		/// <summary>
		/// 값을 비교하여 같으면 신규값으로 변경하고, 다르면 변경하지 않습니다.
		/// </summary>
		/// <param name="expect">비교대상 값</param>
		/// <param name="update">변경될 값</param>
		/// <returns>변경 여부</returns>
		bool CompareAndSet(T expect, T update);

		/// <summary>
		/// 원자성이 보장되는 상태로 값을 읽고 씁니다.
		/// </summary>
		T Value { get; set; }
	}
}
