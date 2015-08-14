namespace EF.DB.Interception.Context
{
    using System.Data.Entity;
    using EF.DB.Interception.Entities;

    public class CompanyContext : DbContext
    {
        public CompanyContext()
            : base("Company")
        {
        }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

        // Fluent approach to modifying defaults
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(u => u.FirstName)
                .IsUnicode(false)
                .HasMaxLength(50);

            modelBuilder.Entity<Employee>()
                .Property(u => u.LastName)
                .IsUnicode(false)
                .HasMaxLength(50);

            modelBuilder.Entity<Employee>()
                .Property(u => u.Title)
                .IsUnicode(false)
                .HasMaxLength(100);

            modelBuilder.Entity<Employee>()
                .Property(u => u.Salary)
                .HasPrecision(10, 2);
        }
    }
}