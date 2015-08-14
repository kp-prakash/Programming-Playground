namespace TaskMonitor.DataImport
{
    using System.Data.Entity;

    public partial class TaskMonitorContext : DbContext
    {
        public TaskMonitorContext()
            : base("name=TaskMonitor")
        {
        }

        public virtual DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}