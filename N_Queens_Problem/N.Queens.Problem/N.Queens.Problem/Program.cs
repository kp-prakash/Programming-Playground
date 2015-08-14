using System;

namespace N.Queens.Problem
{
    class Program
    {
        static void Main()
        {
            var nQueens = new NQueens(8);
            int count = 0;
            nQueens.ResolveNQueens(0, ref count);
            Console.WriteLine("Total solutions found: {0}", count);
            Console.ReadLine();
        }
    }
}