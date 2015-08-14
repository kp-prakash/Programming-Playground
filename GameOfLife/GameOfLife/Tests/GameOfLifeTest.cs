using NUnit.Framework;
using Entities;
using Shared;

namespace Tests
{
    [TestFixture]
    public class GameOfLifeTest
    {
        [Test]
        public void TestGameOfLifeConstruction()
        {
            const int expectedRowCount = 2;
            const int expectedColumnCount = 2;
            var gameOfLife = Factory.Instance.GetNewGameOfLife(2, 2);
            Assert.True(expectedRowCount.Equals(gameOfLife.RowCount)
                        && expectedColumnCount.Equals(gameOfLife.ColumnCount));
        }

        [Test]
        public void TestProcessGeneration()
        {
            InputParser.Instance.InitializeGame(Patterns.Blinker1);
            var currentGame = InputParser.Instance.CurrentGame;
            var initialState = currentGame.CurrentGeneration.ToString();
            currentGame.ProcessGeneration();
            var nextState = currentGame.NextGeneration.ToString();
            Assert.True(initialState.Equals(Patterns.Blinker1)
                        && nextState.Equals(Patterns.Blinker2));
        }

        [Test]
        public void TestProcessGenerationTwice()
        {
            InputParser.Instance.InitializeGame(Patterns.Blinker1);
            var currentGame = InputParser.Instance.CurrentGame;
            currentGame.ProcessGeneration();
            var firstGenerationState = currentGame.NextGeneration.ToString();
            currentGame.ProcessGeneration();
            var secondGenerationState = currentGame.NextGeneration.ToString();
            Assert.True(secondGenerationState.Equals(Patterns.Blinker1)
                        && firstGenerationState.Equals(Patterns.Blinker2));            
        }
    }
}