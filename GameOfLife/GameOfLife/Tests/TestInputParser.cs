using NUnit.Framework;
using Entities;
using Shared;
namespace Tests
{
    [TestFixture]
    public class TestInputParser
    {
        [Test]
        public void TestInitialize()
        {
            const int expectedRowCount = 3;
            const int expectedColumnCount = 3;
            InputParser.Instance.InitializeGame(Patterns.Blinker1);
            var currentGame = InputParser.Instance.CurrentGame;
            Assert.True(null != currentGame && expectedRowCount.Equals(currentGame.RowCount)
                        && expectedColumnCount.Equals(currentGame.ColumnCount)
                        && Patterns.Blinker1.Equals(currentGame.CurrentGeneration.ToString()));
        }
    }
}