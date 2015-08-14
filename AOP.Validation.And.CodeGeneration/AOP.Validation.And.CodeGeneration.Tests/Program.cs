namespace AOP.Validation.And.CodeGeneration.Tests
{
    using System;
    using System.Reflection;

    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            string[] my_args = { Assembly.GetExecutingAssembly().Location };

            int returnCode = NUnit.ConsoleRunner.Runner.Main(my_args);
            Console.ReadKey();
            if (returnCode != 0)
                Console.Beep();
        }
    }
}