using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming
{
    public class DataParallelism
    {
        #region Parallel.For

        static readonly int[] InventoryList = new[] { 100, 750, 400, 75, 900, 975, 275, 750, 600, 125, 300 };

        public static void Main()
        {
            Parallel.For(0, InventoryList.Length, index =>
            {
                var price = InventoryList[index];
                if (price > 500)
                {
                    InventoryList[index] = (int)(price * .8);
                }
                else
                {
                    InventoryList[index] = (int)(price * .9);
                }
                Console.WriteLine("Item {0,4} Price: {1, 7:f}",
                index, InventoryList[index]);
            });
            Console.ReadKey();
        } 

        #endregion

        #region Parallel For Break
        //private static void HalfOperation()
        //{
        //    Thread.SpinWait(int.MaxValue / 2);
        //}

        //private static void Main()
        //{
        //    const int cancelValue = 11;
        //    Parallel.For(0, 20, (index, loopState) =>
        //                            {
        //                                Console.WriteLine("Task {0} started...", index);
        //                                HalfOperation();
        //                                if (cancelValue == index)
        //                                {
        //                                    loopState.Break();
        //                                    Console.WriteLine(
        //                                        "Loop Operation cancelling. " +
        //                                        "Task {0} cancelled...", index);
        //                                    return;
        //                                }
        //                                if (loopState.LowestBreakIteration.HasValue)
        //                                {
        //                                    if (index > loopState.LowestBreakIteration)
        //                                    {
        //                                        Console.WriteLine("Task {0} cancelled", index);
        //                                        return;
        //                                    }
        //                                }
        //                                HalfOperation();
        //                                Console.WriteLine("Task {0} ended.", index);
        //                            });
        //    Console.WriteLine("Press enter to end");
        //    Console.ReadLine();
        //} 
        #endregion

        #region Handling Exceptions
        //public static void Main()
        //{
        //    try
        //    {
        //        Parallel.For(0, 10, (index) =>
        //        {
        //            Console.WriteLine("Task {0} started.", index);
        //            if (index == 4)
        //            {
        //                throw new Exception();
        //            }
        //            DoSomething();
        //            Console.WriteLine("Task {0} ended.", index);
        //        });
        //    }
        //    catch (AggregateException ax)
        //    {
        //        Console.WriteLine("\nError List: \n");
        //        foreach (var error in ax.InnerExceptions)
        //        {
        //            Console.WriteLine(error.Message);
        //        }
        //    }
        //}

        //private static void DoSomething()
        //{
        //    Thread.SpinWait(10000);
        //} 
        #endregion

        #region Reduction in Adding N Numbers
        //public static void Main()
        //{
        //    int total = 0;
        //    var @lock = new ReaderWriterLockSlim();
        //    var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        //    Parallel.For(0, numbers.Length, () => 0, (value, loopstate, subtotal) =>
        //                                                 {
        //                                                     subtotal += value;
        //                                                     Console.WriteLine("Subtotal : {0}", subtotal);
        //                                                     return subtotal;
        //                                                 }, (subtotal) =>
        //                                                        {
        //                                                            Console.WriteLine("Final Subtotal {0}", subtotal);
        //                                                            @lock.EnterWriteLock();
        //                                                            total += subtotal;
        //                                                            @lock.ExitWriteLock();
        //                                                        });
        //    Console.WriteLine("Total : {0}", total);
        //    Console.ReadKey();
        //}
        #endregion        
    }
}
