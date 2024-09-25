namespace System.Threading.Executions
{
    /// <summary>
    /// 실행 유형
    /// </summary>
    public enum ExecutionType
    {
        /// <summary>
        /// 비동기 실행
        /// </summary>
        Async,
        /// <summary>
        /// 동기 실행
        /// </summary>
        Sync
    }

    /// <summary>
    /// 실행 인터페이스
    /// </summary>
	public interface IExecution : IDisposable
    {
        /// <summary>
        /// Execution Id
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 실행 유형
        /// </summary>
        ExecutionType Type { get; }
    }

    /// <summary>
    /// 비동기 실행 추상
    /// </summary>
	public abstract class AsyncExecution : IExecution
	{
		/// <summary>
		/// Execution Id
		/// </summary>
		public abstract string Id { get; }

        /// <summary>
        /// 실행 유형
        /// </summary>
		public ExecutionType Type => ExecutionType.Async;

        /// <summary>
        /// 비동기 실행부
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
		public abstract Task ExecuteAsync(CancellationToken cancellationToken);

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public abstract void Dispose();
	}

    /// <summary>
    /// 동기 실행 추상
    /// </summary>
    public abstract class SyncExecution : IExecution
    {
		/// <summary>
		/// Execution Id
		/// </summary>
		public abstract string Id { get; }

		/// <summary>
		/// 실행 유형
		/// </summary>
		public ExecutionType Type => ExecutionType.Sync;

        /// <summary>
        /// 동기 실행부
        /// </summary>
        /// <param name="cancellationToken"></param>
        public abstract void Execute(CancellationToken cancellationToken);

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public abstract void Dispose();
    }
}
