using System;

namespace Base36
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter number: ");
                string value = Console.ReadLine();
                string ulongValue = string.Empty;
                long longVal = 0;
                int intVal = 0;
                
                if (!long.TryParse(value, out longVal))
                {
                    Console.WriteLine("Error converting to long!");
                    continue;
                }
                string result = longVal.EncodeLongAsBase36();
                long decodeAsLong = result.DecodeBase36AsLong();
                Console.WriteLine(result);
                Console.WriteLine(decodeAsLong);
                
                if (!int.TryParse(value, out intVal))
                {
                    Console.WriteLine("Error converting to int!");
                    continue;
                }
                result = intVal.EncodeIntAsBase36();
                int decodeAsInt = result.DecodeBase36AsInt();
                Console.WriteLine(result);
                Console.WriteLine(decodeAsInt);

                Console.ReadKey();
            }
        }
    }
}