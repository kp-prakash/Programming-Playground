using System.Text.RegularExpressions;

namespace Utils
{
    /// <summary>
    /// Extension methods for strings.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Filters the numbers from a string.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns>Filtered title without any numbers</returns>
        public static string FilterNumbers(this string title)
        {
            return Regex.Replace(title, @"[0-9]", string.Empty);
        }
    }
}
