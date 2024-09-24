namespace System.Threading.Atomic.Tests
{
	[TestClass]
	public class AtomicInt64Tests
	{
		[TestMethod]
		public void ValuePropertyTest()
		{
			AtomicInt64 value = new AtomicInt64();
			Assert.AreEqual(0, value.Value);
			value.Value = 1;
			Assert.AreEqual(1, value.Value);
		}

		[TestMethod]
		public void CompareAndSetTest()
		{
			AtomicInt64 value = new AtomicInt64();
			Assert.IsFalse(value.CompareAndSet(1, 2));
			Assert.IsTrue(value.CompareAndSet(0, 1));
		}

		[TestMethod]
		public void GetThenAddTest()
		{
			AtomicInt64 value = new AtomicInt64();
			Assert.AreEqual(0, value.GetThenAdd(5));
			Assert.AreEqual(5, value.Value);
		}

		[TestMethod]
		public void AddThenGetTest()
		{
			AtomicInt64 value = new AtomicInt64();
			Assert.AreEqual(5, value.AddThenGet(5));
		}

		[TestMethod]
		public void GetThenIncrementTest()
		{
			AtomicInt64 value = new AtomicInt64();
			Assert.AreEqual(0, value.GetThenIncrement());
			Assert.AreEqual(1, value.Value);
		}

		[TestMethod]
		public void IncrementThenGetTest()
		{
			AtomicInt64 value = new AtomicInt64();
			Assert.AreEqual(1, value.IncrementThenGet());
		}

		[TestMethod]
		public void GetThenDecrementTest()
		{
			AtomicInt64 value = new AtomicInt64();
			Assert.AreEqual(0, value.GetThenDecrement());
			Assert.AreEqual(-1, value.Value);
		}

		[TestMethod]
		public void DecrementThenGetTest()
		{
			AtomicInt64 value = new AtomicInt64();
			Assert.AreEqual(-1, value.DecrementThenGet());
		}
	}
}