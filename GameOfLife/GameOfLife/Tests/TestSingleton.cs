using NUnit.Framework;
using Utils;

namespace Tests
{
    [TestFixture]
    public class TestSingleton
    {
        [Test]
        public void TestInstanceCreation()
        {
            var testSingleton1 = Singleton<TestSingleton>.Instance;
            var testSingleton2 = Singleton<TestSingleton>.Instance;
            Assert.True(testSingleton1 == testSingleton2);
        }
    }
}