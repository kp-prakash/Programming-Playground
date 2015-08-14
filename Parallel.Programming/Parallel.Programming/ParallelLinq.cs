using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelProgramming
{
    public class ParallelLinq
    {
        #region Sequential Vs Parallel
        //public static void Main()
        //{
        //    var numbers = new List<int>();
        //    for(int i=1;i<100000;++i)
        //        numbers.Add(i);
        //    var squares = from number in numbers.AsParallel() select number * number;
        //    var sequentialSquares = from number in numbers select number * number;
        //    var sw = new Stopwatch();
        //    sw.Start();
        //    foreach (int number in squares)
        //    {
        //        Console.WriteLine(number);
        //    }
        //    sw.Stop();
        //    long parallelTime = sw.ElapsedMilliseconds;
        //    sw.Reset();
        //    sw.Start();
        //    foreach (int number in sequentialSquares)
        //    {
        //        Console.WriteLine(number);
        //    }
        //    sw.Stop();
        //    long sequentialTime = sw.ElapsedMilliseconds;
        //    sw.Reset();
        //    Console.WriteLine("Elapsed time for parallel query: {0}: ms", parallelTime);
        //    Console.WriteLine("Elapsed time for sequential query: {0}: ms", sequentialTime);
        //    Console.WriteLine("Press Enter to Continue");
        //    Console.ReadLine();
        //} 
        #endregion

        #region PrallelQuery.ForAll
        //public static void Main()
        //{
        //     var numbers = new List<int>();
        //     for(int i=1;i<100000;++i)
        //         numbers.Add(i);
        //     var squares = from number in numbers.AsParallel() select number * number;
        //     var sequentialSquares = from number in numbers select number * number;
        //     var sw = new Stopwatch();
        //     sw.Start();
        //     squares.ForAll(Console.WriteLine);
        //     sw.Stop();
        //     long parallelTime = sw.ElapsedMilliseconds;
        //     sw.Reset();
        //     sw.Start();
        //     foreach (int number in sequentialSquares)
        //         Console.WriteLine(number);
        //     sw.Stop();
        //     long sequentialTime = sw.ElapsedMilliseconds;
        //     sw.Reset();
        //     Console.WriteLine("Elapsed time for parallel query using PrallelQuery.ForAll: {0}: ms", parallelTime);
        //     Console.WriteLine("Elapsed time for sequential query: {0}: ms", sequentialTime);
        //     Console.WriteLine("Press Enter to Continue");
        //     Console.ReadLine();
        //} 
        #endregion

        #region WithExecutionMode
        //public static void Main()
        //{
        //    var numbers = new List<int>();
        //    for (int i = 1; i < 100000; ++i)
        //        numbers.Add(i);
        //    var squaresNotBuffered = from number in numbers.AsParallel().WithMergeOptions(ParallelMergeOptions.NotBuffered)
        //                             select number * number;
        //    var squaresFullyBuffered = from number in numbers.AsParallel().WithMergeOptions(ParallelMergeOptions.FullyBuffered)
        //                               select number * number;
        //    var squaresAutoBuffered = from number in numbers.AsParallel().WithMergeOptions(ParallelMergeOptions.AutoBuffered)
        //                              select number * number;

        //    var sw = new Stopwatch();
        //    sw.Start();
        //    squaresNotBuffered.ForAll(Console.WriteLine);
        //    sw.Stop();
        //    long notBufferedTime = sw.ElapsedMilliseconds;
        //    sw.Reset();
        //    sw.Start();
        //    squaresFullyBuffered.ForAll(Console.WriteLine);
        //    sw.Stop();
        //    long fullyBufferedTime = sw.ElapsedMilliseconds;
        //    sw.Reset();
        //    sw.Start();
        //    squaresAutoBuffered.ForAll(Console.WriteLine);
        //    sw.Stop();
        //    long autoBufferedTime = sw.ElapsedMilliseconds;
        //    sw.Reset();
        //    Console.WriteLine("Elapsed time for NotBuffered: {0}: ms", notBufferedTime);
        //    Console.WriteLine("Elapsed time for FullyBuffered: {0}: ms", fullyBufferedTime);
        //    Console.WriteLine("Elapsed time for AutoBuffered: {0}: ms", autoBufferedTime);
        //    Console.WriteLine("Press Enter to Continue");
        //    Console.ReadLine();
        //} 
        #endregion

        #region With Degree Of Parallelism
        //public static void Main()
        //{
        //    var numbers = new List<int>();
        //    for (int i = 1; i < 10000; ++i)
        //        numbers.Add(i);
        //    var dopOne = from number in numbers.AsParallel().WithDegreeOfParallelism(1)
        //                             select number * number;
        //    var dopTwo = from number in numbers.AsParallel().WithDegreeOfParallelism(2)
        //                               select number * number;
        //    var dopFour = from number in numbers.AsParallel().WithDegreeOfParallelism(4)
        //                              select number * number;
        //    var sw = new Stopwatch();
        //    sw.Start();
        //    dopOne.ForAll(Console.WriteLine);
        //    sw.Stop();
        //    long dopOneTime = sw.ElapsedMilliseconds;
        //    sw.Reset();
        //    sw.Start();
        //    dopTwo.ForAll(Console.WriteLine);
        //    sw.Stop();
        //    long dopTwoTime = sw.ElapsedMilliseconds;
        //    sw.Reset();
        //    sw.Start();
        //    dopFour.ForAll(Console.WriteLine);
        //    sw.Stop();
        //    long dopFourTime = sw.ElapsedMilliseconds;
        //    sw.Reset();
        //    Console.WriteLine("Elapsed time for 1 {0} ms", dopOneTime);
        //    Console.WriteLine("Elapsed time for 2 {0} ms", dopTwoTime);
        //    Console.WriteLine("Elapsed time for 4 {0} ms", dopFourTime);
        //    Console.WriteLine("Press Enter to Continue");
        //    Console.ReadLine();
        //} 
        #endregion

        #region AsOrdered
        //public static void Main()
        //{
        //    int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        //    var result = from number in numbers.AsParallel().AsOrdered()
        //                 select number * number;
        //    foreach (var i in result)
        //        Console.WriteLine(i);
        //    Console.ReadKey();
        //}
        #endregion

        #region Exception Handling
        //public static void Main(string[] args)
        //{
        //    int[] intArray = { 5, 1, 2, 7, 4, 0, 6, 2, 9, 0 };
        //    var results = intArray.AsParallel().Select(item => 1000 / item);
        //    try
        //    {
        //        results.ForAll(Console.WriteLine);
        //    }
        //    catch (AggregateException ex)
        //    {
        //        foreach (var inner in ex.InnerExceptions)
        //        {
        //            Console.WriteLine(inner.Message);
        //        }
        //    }
        //    Console.WriteLine("Press enter to exit");
        //    Console.ReadLine();
        //} 
        #endregion
    }
}