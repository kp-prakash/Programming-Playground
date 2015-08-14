using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Airline.Seating
{
    public class SeatManager
    {
        private readonly int columns;
        private readonly int rows;
        private readonly BitArray seats;

        public SeatManager(int rowCount, int columnCount)
        {
            rows = rowCount;
            columns = columnCount;
            seats = new BitArray(rows * columns);
        }

        public BitArray Seats
        {
            get { return seats; }
        }

        public string[] GetSeats(int count)
        {
            List<int> seatsFound = null;
            for (int row = 0; row < rows; row++)
            {
                int startIndex = row * columns;
                int endIndex = startIndex + columns - 1;
                var collectionOfSeatNumbers = GetCollectionOfSeatNumbers(count, startIndex);

                seatsFound = collectionOfSeatNumbers.Count > 0
                    ? FindSeats(collectionOfSeatNumbers)
                    : FindASeat(startIndex, endIndex);

                if (seatsFound.Count() != 0)
                {
                    break;
                }
            }

            if (seatsFound == null || !seatsFound.Any())
            {
                Console.WriteLine("Unable to find seat!");
            }

            return GetSeatLocations(seatsFound);
        }

        private static List<List<int>> GetCollectionOfSeatNumbers(int count, int startIndex)
        {
            var collectionOfSeatNumbers = new List<List<int>>();
            switch (count)
            {
                case 2:
                    collectionOfSeatNumbers.Add(new List<int> { startIndex + 0, startIndex + 1 });
                    collectionOfSeatNumbers.Add(new List<int> { startIndex + 6, startIndex + 7 });
                    break;

                case 3:
                    collectionOfSeatNumbers.Add(new List<int> { startIndex + 2, startIndex + 3, startIndex + 4 });
                    collectionOfSeatNumbers.Add(new List<int> { startIndex + 3, startIndex + 4, startIndex + 5 });
                    break;

                case 4:
                    collectionOfSeatNumbers.Add(new List<int>
                    {
                        startIndex + 2,
                        startIndex + 3,
                        startIndex + 4,
                        startIndex + 5
                    });
                    break;
            }
            return collectionOfSeatNumbers;
        }

        private List<int> FindASeat(int startIndex, int endIndex)
        {
            var seatsFound = new List<int>();
            for (int i = startIndex; i <= endIndex; i++)
            {
                if (!seats.Get(i))
                {
                    seatsFound.Add(i);
                    seats.Set(i, true);
                    break;
                }
            }
            return seatsFound;
        }

        private List<int> FindSeats(IEnumerable<IEnumerable<int>> collectionOfSeatNumbers)
        {
            var result = new List<int>();
            foreach (var seatsToSearch in collectionOfSeatNumbers)
            {
                var seatNumbers = seatsToSearch as int[] ?? seatsToSearch.ToArray();
                bool alreadyAllocated = seatNumbers.Any(seatNumber => seats.Get(seatNumber));
                if (!alreadyAllocated)
                {
                    result.AddRange(seatNumbers);
                    MarkSeatsAsAllocated(seatNumbers);
                    break;
                }
            }
            return result;
        }

        private string[] GetSeatLocations(List<int> seatsFound)
        {
            var result = new string[seatsFound.Count];
            for (int i = 0; i < result.Length; i++)
            {
                int seatNumber = seatsFound[i];
                int row = seatNumber / columns + 1;
                int column = seatNumber % columns;
                var col = (Columns)column;
                result[i] = string.Format("{0}{1}", row, col.ToString().ToLower());
            }
            return result;
        }

        private void MarkSeatsAsAllocated(IEnumerable<int> seatNumbers)
        {
            foreach (var seatNumber in seatNumbers)
            {
                seats.Set(seatNumber, true);
            }
        }
    }
}