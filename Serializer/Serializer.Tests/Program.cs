namespace Serializer.Tests
{
    using System;
    using System.Reflection;

    using NUnit.ConsoleRunner;

    /// <summary>
    /// Entry point.
    /// </summary>
    internal class Program
    {
        #region Methods

        /// <summary>
        /// Entry point.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            string[] myArgs = { Assembly.GetExecutingAssembly().Location };
            int returnCode = Runner.Main(myArgs);
            if (returnCode != 0)
            {
                Console.Beep();
            }
        }

        #endregion
    }
}