using System;

namespace Airline.Seating
{
    internal class Program
    {
        private static void Main()
        {
            const int rows = 24;
            const int columns = 8;
            var seatManager = new SeatManager(rows, columns);
            while (true)
            {
                Console.WriteLine("Enter number of passengers:");
                var numberOfSeatsRequired = Console.ReadLine();
                int passengerCount;
                if (int.TryParse(numberOfSeatsRequired, out passengerCount))
                {
                    if (passengerCount == 0 || passengerCount > 4)
                    {
                        Console.WriteLine("Please enter a value less than 4.");
                        continue;
                    }
                    string[] seats = seatManager.GetSeats(passengerCount);
                    foreach (var seat in seats)
                    {
                        Console.Write("{0} ", seat);
                    }

                    Console.WriteLine();
                    Console.WriteLine("Press 'p' to print map.");
                    Console.WriteLine("Press 'e' to exit.");
                    Console.WriteLine("Press any key to continue...");
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    Console.Clear();
                    if (keyInfo.Key == ConsoleKey.P)
                    {
                        PrintUtils.PrintSeatAllocation(seatManager.Seats, columns);
                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue:");
                        Console.ReadKey();
                    }

                    if (keyInfo.Key == ConsoleKey.E)
                    {
                        break;
                    }
                }
            }
        }
    }
}