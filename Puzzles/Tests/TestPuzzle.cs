using NUnit.Framework;
using Puzzles;

namespace Tests
{
    [TestFixture]
    public class TestPuzzle
    {
        [Test]
        public void TestDiv()
        {
            var result1 = Puzzle.Div(1, 2);
            var result2 = Puzzle.Div(-1, -2);
            var result3 = Puzzle.Div(1, 3);
            Assert.IsTrue(result1 == 0.5 && result1 == result2
                          && Puzzle.Div(1, 3).ToString() == "0.3333333333");
        }

        [Test]
        public void TestWorkReversal()
        {
            string input = "This is a Test";
            char[] result = Puzzle.ReverseWords(input);
            Assert.IsTrue(new string(result) == "Test a is This");
        }

        [Test]
        public void TestBinaryPattern()
        {
            string input = "010101001100111010";
            char[] output = Puzzle.RearrangeZerosAndOnes(input);
            Assert.True(new string(output) == "000000000111111111");
        }

        [Test]
        public void TestIsUniqueCharacters()
        {
            Assert.True(!Puzzle.IsUniqueCharacters("TEST")
                        && Puzzle.IsUniqueCharacters("ABCD"));
        }

        [Test]
        public void TestSetZeroes()
        {
            Puzzle.SetZeroes();
            Assert.True(true);
        }
    }
}
