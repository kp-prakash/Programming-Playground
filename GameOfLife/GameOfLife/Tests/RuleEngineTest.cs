using NUnit.Framework;
using Shared;

namespace Tests
{
    [TestFixture]
    public class RuleEngineTest
    {
        /// <summary>
        /// 1. Any live cell with fewer than two live neighbours dies, as if by loneliness.
        /// </summary>
        [Test]
        public void TestRule1()
        {
            const int neighboursAlive = 1;
            const bool isAlive = true;
            var willbeAlive = RuleEngine.WillBeAlive(neighboursAlive, isAlive);
            Assert.True(!willbeAlive);
        }

        /// <summary>
        /// 2. Any live cell with more than three live neighbours dies, as if by overcrowding.
        /// </summary>
        [Test]
        public void TestRule2()
        {
            const int neighboursAlive = 4;
            const bool isAlive = true;
            var willbeAlive = RuleEngine.WillBeAlive(neighboursAlive, isAlive);
            Assert.True(!willbeAlive);
        }

        /// <summary>
        /// 3. Any live cell with two or three live neighbours lives, unchanged, to the next generation.
        /// A. Two live neighbours.
        /// </summary>
        [Test]
        public void TestRule3A()
        {
            const int neighboursAlive = 2;
            const bool isAlive = true;
            var willbeAlive = RuleEngine.WillBeAlive(neighboursAlive, isAlive);
            Assert.True(willbeAlive);
        }

        /// <summary>
        /// 3. Any live cell with two or three live neighbours lives, unchanged, to the next generation.
        /// B. Three live neighbours.
        /// </summary>
        [Test]
        public void TestRule3B()
        {
            const int neighboursAlive = 3;
            const bool isAlive = true;
            var willbeAlive = RuleEngine.WillBeAlive(neighboursAlive, isAlive);
            Assert.True(willbeAlive);
        }

        /// <summary>
        /// 4. Any dead cell with exactly three live neighbours comes to life.
        /// </summary>
        [Test]
        public void TestRule4()
        {
            const int neighboursAlive = 3;
            const bool isAlive = false;
            var willbeAlive = RuleEngine.WillBeAlive(neighboursAlive, isAlive);
            Assert.True(willbeAlive);
        }
    }
}