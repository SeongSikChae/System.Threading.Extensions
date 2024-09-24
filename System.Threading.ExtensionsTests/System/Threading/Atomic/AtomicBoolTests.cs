namespace System.Threading.Atomic.Tests
{
	[TestClass]
	public class AtomicBoolTests
	{
		[TestMethod]
		public void ValuePropertyTest()
		{
			AtomicBool value = new AtomicBool();
			Assert.IsFalse(value.Value);
			value.Value = true;
			Assert.IsTrue(value.Value);
		}

		[TestMethod]
		public void CompareAndSetTest()
		{
			AtomicBool value = new AtomicBool();
			Assert.IsFalse(value.CompareAndSet(true, false));
			Assert.IsTrue(value.CompareAndSet(false, true));
		}
	}
}