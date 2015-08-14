using System;
using System.Reflection;

namespace Tests
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            string[] args = { Assembly.GetExecutingAssembly().Location };
            var returnCode = NUnit.ConsoleRunner.Runner.Main(args);
            if (returnCode != 0) Console.Beep();
        }
    }
}