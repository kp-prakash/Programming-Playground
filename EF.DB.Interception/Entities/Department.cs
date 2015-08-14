namespace EF.DB.Interception.Entities
{
    using System.Collections.Generic;

    public class Department
    {
        public int DepartmentId { get; set; }

        public string DeptFloor { get; set; }

        public string DeptName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}