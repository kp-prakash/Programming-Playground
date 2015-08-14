using System;
using System.Linq;

namespace Puzzles
{
    public class Puzzle
    {
        #region Division without using / and %
        private static int _limit = 0;
        public static double Div(double numerator, double denominator)
        {
            double result = Divide(numerator, denominator);
            _limit = 0;
            return result;
        }

        private static double Divide(double numerator, double denominator)
        {
            bool negativeResult = false;
            double temp = 1.0;
            if (denominator == 0)
            {
                throw new DivideByZeroException();
            }
            if (numerator == 0 || _limit > 10)
            {
                return 0;
            }
            if (numerator < 0 && denominator < 0)
            {
                numerator *= -1;
                denominator *= -1;
            }
            else if (numerator < 0 && denominator >= 1)
            {
                numerator *= -1;
                negativeResult = true;
            }
            else if (numerator >= 1 && denominator < 0)
            {
                denominator *= -1;
                negativeResult = true;
            }
            if (numerator < denominator)
            {
                _limit++;
                numerator *= 10;
                temp = 0.1 * Divide(numerator, denominator);
                numerator = denominator;
            }
            return (negativeResult ? -1 : 1) * (temp + Divide(numerator - denominator, denominator));
        }
        #endregion

        #region Reversing the words in a string
        public static char[] ReverseWords(string input)
        {
            //This does an in memory replacement.
            //First rever the whole string.
            //Then reverse individual words in the string.
            char[] array = input.ToArray();
            int startIndex = 0;
            int length = array.Length;
            Console.WriteLine(array);
            Reverse(array, startIndex, length);
            int prevIndex = 0;
            while (startIndex <= array.Length)
            {
                if (startIndex == array.Length)
                {
                    Reverse(array, prevIndex, startIndex);
                    break;
                }
                if (array[startIndex] == ' ')
                {
                    Reverse(array, prevIndex, startIndex);
                    startIndex++;
                    prevIndex = startIndex;
                    continue;
                }
                startIndex++;
            }
            return array;
        }

        //private static void Reverse(char[] array, int startIndex, int length)
        //{
        //    bool isEven = (length - startIndex) % 2 == 0;
        //    int mid = startIndex + (length - startIndex) / 2;
        //    int index1 = isEven ? mid - 1 : mid;
        //    int index2 = mid;
        //    while (index1 >= startIndex && index2 < array.Length)
        //    {
        //        char temp = array[index1];
        //        array[index1] = array[index2];
        //        array[index2] = temp;
        //        index2++;
        //        index1--;
        //    }
        //}
        private static void Reverse(char[] array, int startIndex, int length)
        {
            int endIndex = length - 1;
            while (startIndex < endIndex)
            {
                char temp = array[startIndex];
                array[startIndex] = array[endIndex];
                array[endIndex] = temp;
                startIndex++;
                endIndex--;
            }
        }
        #endregion

        #region Rearranging ones and zeros.
        public static char[] RearrangeZerosAndOnes(string pattern)
        {
            char[] array = pattern.ToArray();
            int start = 0;
            int end = array.Length - 1;
            while (start < end)
            {
                while (start < end && array[start] == '0')
                    start++;
                while (start < end && array[end] == '1')
                    end--;
                if (start < end && array[start] > array[end])
                {
                    var temp = array[start];
                    array[start] = array[end];
                    array[end] = temp;
                }
                start++;
                end--;
            }
            return array;
        }
        #endregion

        #region Is Unique Characters
        public static bool IsUniqueCharacters(string input)
        {
            long result = 0;
            foreach (char c in input)
            {
                if ((result & (1 << (c - 'a'))) > 0) return false;
                result = result | (1 << (c - 'a'));
            }
            return true;
        }
        #endregion

        #region Set Rows and Cols to 0
        public static void SetZeroes()
        {
            int[][] matrix = {
                                 new int[] {1, 2, 3, 4, 5}, 
                                 new int[] {1, 2, 3, 4, 5},
                                 new int[] {1, 2, 0, 4, 5},
                                 new int[] {1, 2, 3, 4, 5},
                                 new int[] {1, 2, 3, 4, 5}
                             };
            Print(matrix);

            var row = new int[matrix.Length];
            var col = new int[matrix[0].Length];

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    if (matrix[i][j] == 0)
                    {
                        row[i] = 1;
                        col[j] = 1;
                    }
                }
            }

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    if (row[i] == 1 || col[j]==1)
                        matrix[i][j] = 0;
                }
            }
            Print(matrix);
        }

        private static void Print(int[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    Console.Write("{0} ", matrix[i][j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        #endregion
    }
}