namespace System.Threading.Tests
{
	[TestClass]
	public class CountdownFactoryTests
	{
		[TestMethod]
		public void AddCountTest()
		{
			using ICountdown countdown = CountdownFactory.Create(10);
			countdown.AddCount(1);
			Assert.AreEqual(11, countdown.CurrentCount);
		}

		[TestMethod]
		public void TryAddCountTest()
		{
			{
				using ICountdown countdown = CountdownFactory.Create(0);
				Assert.IsFalse(countdown.TryAddCount(1));
			}

			{
				using ICountdown countdown = CountdownFactory.Create(1);
				Assert.IsTrue(countdown.TryAddCount(1));
				Assert.AreEqual(2, countdown.CurrentCount);
			}
		}

		[TestMethod]
		public void SignalTest()
		{
			using ICountdown countdown = CountdownFactory.Create(1);
			countdown.Signal(1);
			Assert.AreEqual(0, countdown.CurrentCount);
			Assert.ThrowsException<InvalidOperationException>(() => countdown.Signal(1));
		}

		[TestMethod]
		public void ResetTest()
		{
			using ICountdown countdown = CountdownFactory.Create(5);
			countdown.Signal();
			countdown.Signal();
			countdown.Reset();
			Assert.AreEqual(5, countdown.CurrentCount);
			countdown.Reset(10);
			Assert.AreEqual(10, countdown.CurrentCount);
		}

		[TestMethod]
		public void WaitTest()
		{
			using ICountdown countdown = CountdownFactory.Create(10);
			Task t = Task.Run(() =>
			{
				while (true)
				{
					if (countdown.IsSet)
						break;
					countdown.Signal();
				}
			});

			countdown.Wait();
			t.Wait();
		}
	}
}