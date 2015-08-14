using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sum.Of.Subsets
{
    class Program
    {
        private static int[] w = { 60, 45, 30, 45, 45, 5, 60, 45, 30, 30, 45, 60, 60, 45, 30, 30, 60, 30, 30 };
        private static int[] x = new int[w.Length];

        static void Main(string[] args)
        {
            Array.Sort(w);
            SumOfSubsets(0, 0, w.Sum());
            foreach (var result in allResults)
            {
                Console.Write(result);
            }
            Console.ReadLine();
        }

        private static int m = 180;
        static List<string> allResults = new List<string>();
        private static bool SumOfSubsets(int s, int k, int r)
        {
            bool found = false;
            x[k] = 1;
            if (s + w[k] == m)
            {
                Print(k);
                return true;
            }
            else if (s + w[k] + w[k + 1] <= m)
                found = SumOfSubsets(s + w[k], k + 1, r - w[k]);
            if(found) return true;
            if (((s + r - w[k]) >= m) && ((s + w[k + 1]) <= m))
            {
                x[k] = 0;
                found = SumOfSubsets(s, k + 1, r - w[k]);
            }
            return false;
        }

        private static void Print(int k)
        {
            var stringBuilder = new StringBuilder();
            for (int i = 0; i <= k; i++)
            {
                if (x[i] == 1)
                {
                    stringBuilder.Append(string.Format("{0} ", w[i]));
                }
            }
            stringBuilder.Append(Environment.NewLine);
            string value = stringBuilder.ToString();
            //if (!allResults.Contains(value))
                allResults.Add(value);
        }
    }
}
