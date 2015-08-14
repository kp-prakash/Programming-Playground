using Entities;
using NUnit.Framework;
using Shared;

namespace Tests
{
    [TestFixture]
    public class CellTest
    {
        [Test]
        public void TestToggle()
        {
            var cell = Factory.Instance.GetNewCell();
            var isActive = cell.IsAlive;
            cell.Toggle();
            var isActiveNew = cell.IsAlive;
            Assert.IsTrue(isActive == !isActiveNew);
        }

        [Test]
        public void TestGetState()
        {
            var cell = Factory.Instance.GetNewCell();
            var oldState = cell.GetState();
            cell.Toggle();
            var newState = cell.GetState();
            Assert.IsTrue(oldState.Equals(Constants.Dead)
                          && newState.Equals(Constants.Alive));
        }

        [Test]
        public void TestClear()
        {
            var cell = Factory.Instance.GetNewCell();
            cell.Toggle();
            var isAliveBeforeClear = cell.IsAlive;
            cell.Clear();
            var isAliveAfterClear = cell.IsAlive;
            Assert.IsTrue(isAliveBeforeClear && !isAliveAfterClear);
        }
    }
}