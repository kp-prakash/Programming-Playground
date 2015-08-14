using System;

namespace MineSweeper.Utils
{
    public static class RandomGenerator
    {
        public static Random GetRandomNumberGenerator(int rows, int columns)
        {
            DateTime now = DateTime.Now;
            var random = new Random(rows * columns * (now.Hour + now.Minute + now.Second + now.Millisecond));
            return random;
        }

        public static Random GetRandomNumberGenerator()
        {
            DateTime now = DateTime.Now;
            var random = new Random(now.Hour * now.Minute * now.Second * now.Millisecond);
            return random;
        }
    }
}