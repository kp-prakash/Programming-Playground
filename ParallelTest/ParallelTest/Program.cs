using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelTest
{
    /*Interlocked.Increment(ref int) was not working as expected and gave some erroneous 
     * results with 2,60,000 entries being written into a file! Did enough research today 
     * before using this to find the flaw and the solution. After fixing the issue, it worked fine! 
     * In order to verify the results, the elementary formula (n(n+1)/2) came very handy. 
     * It would have been difficult to verify 2,60,000 out-of-sequence entries by hand.
     * This is a very useful link. 
     * http://stackoverflow.com/questions/4772757/interlocked-increment-not-working-the-way-id-expect-in-the-task-parallel-libr
     */
    public class Program
    {
        public static void Main()
        {
            var start = Environment.TickCount;
            var array = new int[10000];
            var count = 0;
            Parallel.ForEach(array, element =>
                                        {
                                            var myCounter = Interlocked.Increment(ref count);
                                            Console.WriteLine("Element {0} {1}", element, myCounter);
                                        });
            var end = Environment.TickCount;
            var elapsedTime = end - start;
            var ts = new TimeSpan(elapsedTime);
            Console.WriteLine(ts.ToString());
        }
    }
}
