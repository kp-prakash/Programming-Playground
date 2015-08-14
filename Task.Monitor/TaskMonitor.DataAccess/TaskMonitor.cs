namespace TaskMonitor.DataAccess
{
    using System.ComponentModel.DataAnnotations.Schema;
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
            //base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Task>()
            //    .HasKey(i => i.Id);
            //modelBuilder.Entity<Task>()
            //  .Property(i => i.Id)
            //  .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
            //  .HasColumnName("Id");
        }
    }
}