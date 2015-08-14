using System;
using System.Linq;
using System.Text;

namespace Base36
{
    /// <summary>
    /// Converts numbers to base 36 strings and vice versa.
    /// </summary>
    public static class Base36Converter
    {
        private const string CharacterSet = "0123456789abcdefghijklmnopqrstuvwxyz";

        private static readonly char[] Characters = CharacterSet.ToCharArray();

        /// <summary>
        /// Decodes the specified base 36 input string as long.
        /// </summary>
        /// <param name="inputString">The base 36 input string.</param>
        /// <returns>
        /// Base 10 number.
        /// </returns>
        public static long DecodeBase36AsLong(this string inputString)
        {
            long result = 0;
            var power = 0;
            inputString = inputString.ToLower();
            for (var i = inputString.Length - 1; i >= 0; i--)
            {
                var character = inputString[i];
                var position = CharacterSet.IndexOf(character);
                if (position > -1)
                    result += position * (long)Math.Pow(CharacterSet.Length, power);
                else
                    return -1;
                power++;
            }
            return result;
        }

        /// <summary>
        /// Encodes the specified input number to base 36 string.
        /// </summary>
        /// <param name="inputNumber">The input number.</param>
        /// <returns>
        /// Base 36 string.
        /// </returns>
        public static string EncodeLongAsBase36(this long inputNumber)
        {
            var base36Builder = new StringBuilder();
            do
            {
                base36Builder.Append(Characters[inputNumber % CharacterSet.Length]);
                inputNumber /= CharacterSet.Length;
            } while (inputNumber != 0);
            string value = base36Builder.ToString();
            return value.Reverse();
        }

        /// <summary>
        /// Decodes the specified base 36 input string as int.
        /// </summary>
        /// <param name="inputString">The base 36 input string.</param>
        /// <returns>
        /// Base 10 number.
        /// </returns>
        public static int DecodeBase36AsInt(this string inputString)
        {
            int result = 0;
            var power = 0;
            inputString = inputString.ToLower();
            for (var i = inputString.Length - 1; i >= 0; i--)
            {
                var character = inputString[i];
                var position = CharacterSet.IndexOf(character);
                if (position > -1)
                    result += position * (int)Math.Pow(CharacterSet.Length, power);
                else
                    return -1;
                power++;
            }
            return result;
        }

        /// <summary>
        /// Encodes the specified input number to base 36 string.
        /// </summary>
        /// <param name="inputNumber">The input number.</param>
        /// <returns>
        /// Base 36 string.
        /// </returns>
        public static string EncodeIntAsBase36(this int inputNumber)
        {
            var base36Builder = new StringBuilder();
            do
            {
                base36Builder.Append(Characters[inputNumber % CharacterSet.Length]);
                inputNumber /= CharacterSet.Length;
            } while (inputNumber != 0);
            string value = base36Builder.ToString();
            return value.Reverse();
        }

        /// <summary>
        /// Reverses the specified string.
        /// </summary>
        /// <param name="s">Input string.</param>
        /// <returns>Reversed string</returns>
        public static string Reverse(this string s)
        {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}