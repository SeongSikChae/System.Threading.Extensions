namespace System.Threading
{
	/// <summary>
	/// 카운트가 0에 도달하면 신호가 전달되는 요소에 대한 Interface
	/// </summary>
	public interface ICountdown : IDisposable
	{
		/// <summary>
		/// 현재 카운트
		/// </summary>
		int CurrentCount { get; }

		/// <summary>
		/// 초기 카운트
		/// </summary>
		int InitialCount { get; }

		/// <summary>
		/// 이벤트가 설정되었는지 여부
		/// </summary>
		public bool IsSet { get; }

		/// <summary>
		/// 카운트를 정해진 값 만큼 증가 시킵니다.
		/// </summary>
		/// <param name="signalCount"></param>
		void AddCount(int signalCount = 1);

		/// <summary>
		/// 카운트를 정해진 값 만큼 증가를 시도합니다.
		/// </summary>
		/// <param name="signalCount"></param>
		/// <returns></returns>
		bool TryAddCount(int signalCount);

		/// <summary>
		/// 카운트를 1만큼 증가를 시도합니다.
		/// </summary>
		/// <returns></returns>
		bool TryAddCount();

		/// <summary>
		/// 카운트를 정해진 값만 큼 감소 시킵니다.
		/// </summary>
		/// <param name="signalCount"></param>
		/// <returns></returns>
		bool Signal(int signalCount);

		/// <summary>
		/// 카운트를 1만큼 감소 시킵니다.
		/// </summary>
		/// <returns></returns>
		bool Signal();

		/// <summary>
		/// 카운트를 정해진 값으로 재설정 합니다.
		/// </summary>
		/// <param name="count"></param>
		void Reset(int count);

		/// <summary>
		/// 카운트를 초기값으로 재설정 합니다.
		/// </summary>
		void Reset();

		/// <summary>
		/// 카운트가 0이 될 때 까지 현재 스레드를 차단합니다.
		/// </summary>
		void Wait();

		/// <summary>
		/// 카운트가 0이 되거나 Cancel 신호가 발생될 때 까지 현재 스레드를 차단합니다.
		/// </summary>
		/// <param name="cancellationToken"></param>
		void Wait(CancellationToken cancellationToken);

		/// <summary>
		/// 카운트가 0이 되거나 정해진 시간(TimeSpan) 동안  현재 스레드를 차단합니다.
		/// </summary>
		/// <param name="timeout"></param>
		/// <returns></returns>
		bool Wait(TimeSpan timeout);

		/// <summary>
		/// NullCountdown
		/// </summary>
		static readonly ICountdown Null = new NullCountdown(0);
	}
}
