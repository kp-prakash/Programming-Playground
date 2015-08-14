namespace EF.DB.Interception
{
    using System;
    using System.Linq;
    using EF.DB.Interception.Context;
    using EF.DB.Interception.Entities;

    internal class Program
    {
        private static void Main()
        {
            using (var companyContext = new CompanyContext())
            {
                Console.Write("Enter department name:");
                string departmentName = Console.ReadLine();

                var department = new Department { DeptName = departmentName };
                companyContext.Departments.Add(department);
                companyContext.SaveChanges();

                var deptQuery = from dept in companyContext.Departments
                                select new { deptName = dept.DeptName };

                foreach (var dept in deptQuery)
                {
                    Console.WriteLine(dept.deptName);
                }
            }
        }
    }
}