namespace System.Threading.Atomic.Tests
{
	[TestClass]
	public class AtomicInt32Tests
	{
		[TestMethod]
		public void ValuePropertyTest()
		{
			AtomicInt32 value = new AtomicInt32();
			Assert.AreEqual(0, value.Value);
			value.Value = 1;
			Assert.AreEqual(1, value.Value);
		}

		[TestMethod]
		public void CompareAndSetTest()
		{
			AtomicInt32 value = new AtomicInt32();
			Assert.IsFalse(value.CompareAndSet(1, 2));
			Assert.IsTrue(value.CompareAndSet(0, 1));
		}

		[TestMethod]
		public void GetThenAddTest()
		{
			AtomicInt32 value = new AtomicInt32();
			Assert.AreEqual(0, value.GetThenAdd(5));
			Assert.AreEqual(5, value.Value);
		}

		[TestMethod]
		public void AddThenGetTest()
		{
			AtomicInt32 value = new AtomicInt32();
			Assert.AreEqual(5, value.AddThenGet(5));
		}

		[TestMethod]
		public void GetThenIncrementTest()
		{
			AtomicInt32 value = new AtomicInt32();
			Assert.AreEqual(0, value.GetThenIncrement());
			Assert.AreEqual(1, value.Value);
		}

		[TestMethod]
		public void IncrementThenGetTest()
		{
			AtomicInt32 value = new AtomicInt32();
			Assert.AreEqual(1, value.IncrementThenGet());
		}

		[TestMethod]
		public void GetThenDecrementTest()
		{
			AtomicInt32 value = new AtomicInt32();
			Assert.AreEqual(0, value.GetThenDecrement());
			Assert.AreEqual(-1, value.Value);
		}

		[TestMethod]
		public void DecrementThenGetTest()
		{
			AtomicInt32 value = new AtomicInt32();
			Assert.AreEqual(-1, value.DecrementThenGet());
		}
	}
}