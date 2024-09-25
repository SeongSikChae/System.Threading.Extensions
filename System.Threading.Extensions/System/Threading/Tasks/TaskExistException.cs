namespace System.Threading.Tasks
{
	/// <summary>
	/// 태스크 스케줄러에 태스크가 존재할 때 예외
	/// </summary>
	/// <param name="message"></param>
	/// <param name="innerException"></param>
	public sealed class TaskExistException(string? message, Exception? innerException) : Exception(message, innerException)
	{
		/// <summary>
		/// 태스크 스케줄러에 태스크가 존재할 때 예외(메시지만)
		/// </summary>
		/// <param name="message"></param>
		public TaskExistException(string? message) : this(message, null) { }

		/// <summary>
		/// 태스크 스케줄러에 태스크가 존재할 때 예외(내부예외)
		/// </summary>
		/// <param name="innerException"></param>
		public TaskExistException(Exception? innerException) : this(null, innerException) { }
	}
}
