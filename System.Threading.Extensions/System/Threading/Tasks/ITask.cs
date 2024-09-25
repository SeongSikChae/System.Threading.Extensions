namespace System.Threading.Tasks
{
	/// <summary>
	/// 태스크 유형
	/// </summary>
	public enum TaskType
	{
		/// <summary>
		/// 비동기 태스크
		/// </summary>
		Async, 
		/// <summary>
		/// 동기 태스크
		/// </summary>
		Sync
	}

	/// <summary>
	/// 태스크 인터페이스
	/// </summary>
	public interface ITask : IDisposable
	{
		/// <summary>
		/// 태스크 유형
		/// </summary>
		TaskType Type { get; }
	}

	/// <summary>
	/// 비동기 태스크 추상
	/// </summary>
	public abstract class AsyncTask : ITask
	{
		/// <summary>
		/// 태스크 유형
		/// </summary>
		public TaskType Type => TaskType.Async;

		/// <summary>
		/// 비동기 태스크 실행부
		/// </summary>
		/// <param name="cancellationToken">태스크 동작 취소</param>
		/// <returns></returns>
		public abstract Task RunAsync(CancellationToken cancellationToken);

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public abstract void Dispose();
	}

	/// <summary>
	/// 동기 태스크 추상
	/// </summary>
	public abstract class SyncTask : ITask
	{
		/// <summary>
		/// 태스크 유형
		/// </summary>
		public TaskType Type => TaskType.Sync;

		/// <summary>
		/// 동기 태스크 실행부
		/// </summary>
		/// <param name="cancellationToken">태스크 동작 취소</param>
		public abstract void Run(CancellationToken cancellationToken);

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public abstract void Dispose();
	}
}
