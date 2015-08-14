namespace EF.DB.Interception.Entities
{
    public class Employee
    {
        public virtual Department Department { get; set; }

        public int DeptId { get; set; }

        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public string Title { get; set; }
    }
}