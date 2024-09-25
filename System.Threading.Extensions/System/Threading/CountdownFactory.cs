namespace System.Threading
{
	/// <summary>
	/// ICountdown 기본 구현체를 생성하는 Factory
	/// </summary>
	public static class CountdownFactory
	{
		/// <summary>
		/// 특정 초기값으로 ICountdown 기본 구현체를 생성
		/// </summary>
		/// <param name="initialCount"></param>
		/// <returns></returns>
		public static ICountdown Create(int initialCount = 0)
		{
			return new Countdown(initialCount);
		}
	}
}
