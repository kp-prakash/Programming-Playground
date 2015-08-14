namespace Mastering.Entity.Framework.Context
{
    using System.Data.Entity;
    using Mastering.Entity.Framework.Entities;

    public class TaskMonitorContext : DbContext
    {
        public TaskMonitorContext() : base("TaskMonitorContext")
        {
        }

        public DbSet<TaskCategory> TaskCategories { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<User> Users { get; set; }
    }
}