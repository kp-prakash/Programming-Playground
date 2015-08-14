namespace Mastering.Entity.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Mastering.Entity.Framework.Context;
    using Mastering.Entity.Framework.Entities;

    internal class Program
    {
        private static void Main()
        {
            using (var taskMonitorContext = new TaskMonitorContext())
            {
                var taskCategory = new TaskCategory { CategoryName = "Meeting", CreatedUtc = DateTime.UtcNow };
                var user = new User { UserName = "Srihari", Tasks = new List<Task>(), CreatedUtc = DateTime.UtcNow };
                var task = new Task
                {
                    Category = taskCategory,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(2).AddHours(3).AddMinutes(30),
                    Description = "This is a sample task!",
                    User = user,
                    CreatedUtc = DateTime.UtcNow
                };
                user.Tasks.Add(task);
                taskMonitorContext.Database.BeginTransaction(IsolationLevel.ReadCommitted);
                var transaction = taskMonitorContext.Database.CurrentTransaction;
                taskMonitorContext.Tasks.Add(task);
                taskMonitorContext.TaskCategories.Add(taskCategory);
                taskMonitorContext.Users.Add(user);
                taskMonitorContext.SaveChanges();
                transaction.Rollback();
            }
        }
    }
}