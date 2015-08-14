using System;
using System.Collections;

namespace Airline.Seating
{
    public class PrintUtils
    {
        public static void PrintSeatAllocation(BitArray bitArray, int columns)
        {
            Console.WriteLine("Please find the seat allocation map below.");
            Console.WriteLine("A - Available, B - Booked");
            var count = 0;
            foreach (bool value in bitArray)
            {
                count++;
                var aisle = GetAisle(count);
                Console.ForegroundColor = value ? ConsoleColor.Red : ConsoleColor.Green;
                Console.Write(value ? "{0}B" : "{0}A", aisle);

                if (count == columns)
                {
                    Console.WriteLine();
                    count = 0;
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static string GetAisle(int count)
        {
            var aisle = string.Empty;
            if (count == 3 || count == 7)
            {
                aisle = "   ";
            }
            return aisle;
        }
    }
}