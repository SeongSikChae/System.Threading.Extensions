namespace System.Threading
{
	/// <summary>
	/// async-await 에 대한 임계 영역을 을 제공합니다.
	/// </summary>
	public sealed class AsyncLock : IDisposable
	{
		private readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

		/// <summary>
		/// 특정 비동기 작업에 대한 임계 영역을 을 제공합니다.
		/// </summary>
		/// <param name="func"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task LockAsync(Func<CancellationToken, Task> func, CancellationToken cancellationToken)
		{
			try
			{
				await semaphoreSlim.WaitAsync(cancellationToken);
				await func(cancellationToken);
			}
			finally
			{
				semaphoreSlim.Release();
			}
		}

		private bool disposedValue;

		/// <summary>
		///		Performs application-defined tasks associated with freeing, releasing, or resetting
		///     unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			if (!disposedValue)
			{
				disposedValue = true;
				GC.SuppressFinalize(this);
			}
		}
	}
}
