using Microsoft.Extensions.Logging;
using System.Text;

namespace System.Threading.PerformanceCounter
{
	/// <summary>
	/// 성능 카운터 로깅 메시지 수신기
	/// </summary>
	public interface IPerformaceLogListener
	{
		/// <summary>
		/// 성능 카운터 로깅 메시지 수신
		/// </summary>
		void Listen(IDictionary<string, PerformanceMetric> dic);
	}

	/// <summary>
	/// 기본 성능 카운터 로깅 메시지 수신기
	/// </summary>
	public class DefaultPerformaceLogListener(ILogger<DefaultPerformaceLogListener> logger) : IPerformaceLogListener
	{
		/// <inheritdoc />
		public void Listen(IDictionary<string, PerformanceMetric> dic)
		{
			StringBuilder sb = new StringBuilder();
			foreach (KeyValuePair<string, PerformanceMetric> pair in dic)
				sb.Append($"{pair.Key}:{pair.Value.Count:N0}/{pair.Value:N0};");
			logger.Information(sb.ToString());
		}
	}
}
