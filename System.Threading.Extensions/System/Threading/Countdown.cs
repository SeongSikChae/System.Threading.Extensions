namespace System.Threading
{
	internal sealed class Countdown(int initialCount = 0) : ICountdown
	{
		private readonly CountdownEvent countdown = new CountdownEvent(initialCount);

		public int CurrentCount => countdown.CurrentCount;

		public int InitialCount => countdown.InitialCount;

		public bool IsSet => countdown.IsSet;

		public void AddCount(int signalCount = 1)
		{
			countdown.AddCount(signalCount);
		}

		public bool TryAddCount(int signalCount)
		{
			return countdown.TryAddCount(signalCount);
		}

		public bool TryAddCount()
		{
			return countdown.TryAddCount();
		}

		public bool Signal(int signalCount)
		{
			return countdown.Signal(signalCount);
		}

		public bool Signal()
		{
			return countdown.Signal();
		}

		public void Reset(int count)
		{
			countdown.Reset(count);
		}

		public void Reset()
		{
			countdown.Reset();
		}

		public void Wait()
		{
			countdown.Wait();
		}

		public void Wait(CancellationToken cancellationToken)
		{
			countdown.Wait(cancellationToken);
		}

		public bool Wait(TimeSpan timeout)
		{
			return countdown.Wait(timeout);
		}

		private bool disposedValue;

		public void Dispose()
		{
			if (!disposedValue)
			{
				countdown.Dispose();
				disposedValue = true;
				GC.SuppressFinalize(this);
			}
		}
	}
}
