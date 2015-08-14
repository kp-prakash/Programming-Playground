using System;

namespace UnityUtils
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var types = AllClasses.FromAssembliesInBasePath();
            foreach (var type in types)
            {
                Console.WriteLine(type.FullName);
            }

            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();
        }
    }
}