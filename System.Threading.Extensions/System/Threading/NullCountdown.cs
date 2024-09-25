namespace System.Threading
{
	using Atomic;

	internal sealed class NullCountdown(int initialCount = 0) : ICountdown
    {
		private readonly AtomicInt32 count = new AtomicInt32(initialCount);

		public int CurrentCount => count.Value;

		public int InitialCount => initialCount;

		public bool IsSet => CurrentCount == 0;

		public void AddCount(int signalCount = 1)
		{
			TryAddCount(signalCount);
		}

		public bool TryAddCount(int signalCount)
		{
			if (!IsSet)
			{
				count.IncrementThenGet();
				return true;
			}
			return false;
		}

		public bool TryAddCount()
		{
			return TryAddCount(1);
		}

		public bool Signal(int signalCount)
		{
			if (IsSet)
				return false;
			count.AddThenGet(-signalCount);
			return true;
		}

		public bool Signal()
		{
			return Signal(1);
		}

		public void Reset(int count)
		{
			this.count.Value = count;
		}

		public void Reset()
		{
			count.Value = InitialCount;
		}

		public void Wait()
		{
			bool flag = Wait(Timeout.InfiniteTimeSpan, CancellationToken.None);
			if (!flag)
				throw new TimeoutException();
		}

		public void Wait(CancellationToken cancellationToken)
		{
			bool flag = Wait(Timeout.InfiniteTimeSpan, cancellationToken);
			if (!flag)
				throw new TimeoutException();
		}

		public bool Wait(TimeSpan timeout)
		{
			return Wait(timeout, CancellationToken.None);
		}

		private bool Wait(TimeSpan timeout, CancellationToken cancellationToken)
		{
			try
			{
				Task.Run(() =>
				{
					while (true)
					{
						if (IsSet)
							break;
						Thread.Sleep(100);
					}
				}, cancellationToken).Wait(cancellationToken);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public void Dispose()
		{
		}
	}
}
