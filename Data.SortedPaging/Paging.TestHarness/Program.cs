using System;
using System.Data.Entity;
using System.Linq;
using Data.SortedPaging;

namespace Paging.TestHarness
{
    internal class Program
    {
        private static SchoolContext db = new SchoolContext();
        private const int PageSize = 2;

        private static void Main(string[] args)
        {
            Database.SetInitializer<SchoolContext>(new SchoolInitializer());
            var students = db.Students;
            Console.WriteLine("\n");
            UsingTotalNumberOfPages(students);

            Console.WriteLine("\n");
            UsingIsLastPage(students);

            Console.WriteLine("\n");
            GetFirstPageTest(students);

            Console.WriteLine("\n");
            GetLastPageTest(students);

            Console.WriteLine("\n");
            GetPreviousPageTest(students);

            Console.WriteLine("\n");
            GetNextPageTest(students);

            Console.ReadLine();
        }

        private static void UsingTotalNumberOfPages(DbSet<Student> students)
        {
            //More efficient client code.
            int start = Environment.TickCount;
            int numberOfPages = students.GetTotalNumberOfPages(PageSize);
            for (int i = 0; i < numberOfPages; ++i)
            {
                Console.WriteLine("PAGE # {0}", i + 1);
                var result = students.GetNextPage(s => s.StudentID, PageSize, i);
                PrintResult(result);
            }
            int stop = Environment.TickCount;
            Console.WriteLine("Time taken: {0}", stop - start);
            Console.WriteLine("----------------------------------------");
        }

        private static void PrintResult(IQueryable<Student> result)
        {
            foreach (var student in result)
            {
                Console.WriteLine(student.FullName);
            }
            Console.WriteLine("----------------------------------------");
        }

        private static void UsingIsLastPage(DbSet<Student> students)
        {
            //Less efficient client code.
            int start = Environment.TickCount;
            int j = 0;
            while (!students.IsLastPage(s => s.StudentID, PageSize, j))
            {
                Console.WriteLine("PAGE # {0}", j + 1);
                var result = students.GetNextPage(s => s.StudentID, PageSize, j);
                PrintResult(result);
                j++;
            }
            int stop = Environment.TickCount;
            Console.WriteLine("Time taken: {0}", stop - start);
            Console.WriteLine("----------------------------------------");
        }

        private static void GetFirstPageTest(DbSet<Student> students)
        {
            Console.WriteLine("First Page:");
            var result = students.GetFirstPage(s => s.StudentID, PageSize);
            PrintResult(result);
        }

        private static void GetLastPageTest(DbSet<Student> students)
        {
            Console.WriteLine("Last Page:");
            var result = students.GetLastPage(s => s.StudentID, PageSize);
            PrintResult(result);
        }

        private static void GetPreviousPageTest(DbSet<Student> students)
        {
            for (int i = 3; i >= 2; --i)
            {
                Console.WriteLine("Previous Page {0} i.e Page {1}:", i, i - 1);
                var result = students.GetPreviousPage(s => s.StudentID, PageSize, i);
                PrintResult(result);
            }
        }

        private static void GetNextPageTest(DbSet<Student> students)
        {
            for (int i = 1; i < 3; ++i)
            {
                Console.WriteLine("Next Page {0} i.e Page {1}:", i, i + 1);
                var result = students.GetNextPage(s => s.StudentID, PageSize, i);
                PrintResult(result);
            }
        }
    }
}