namespace System.Threading.PerformanceCounter
{
	/// <summary>
	/// 성능 카운터 메시지
	/// </summary>
	public sealed class PerformanceMetric
	{
		/// <summary>
		/// 현재 성능 카운터
		/// </summary>
		public long Count { get; set; }

		/// <summary>
		/// 전체 성능 카운터
		/// </summary>
		public long Total { get; set; }
	}
}
