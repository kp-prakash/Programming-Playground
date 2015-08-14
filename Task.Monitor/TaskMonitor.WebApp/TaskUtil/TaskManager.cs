namespace TaskMonitor.WebApp.TaskUtil
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Globalization;
    using System.Linq;
    using TaskMonitor.DataAccess;
    using TaskMonitor.WebApp.Models;

    /// <summary>
    /// Task Manager - Wraps TaskMonitorContext
    /// </summary>
    public class TaskManager : ITaskManager
    {
        private readonly TaskMonitorContext taskMonitorContext;

        /// <summary>
        /// Creates new instance of TaskManager
        /// </summary>
        public TaskManager()
        {
            taskMonitorContext = new TaskMonitorContext();
        }


        /// <summary>
        /// Posts the specified new task.
        /// </summary>
        /// <param name="newTaskViewModel">The new task.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns>
        /// Created task.
        /// </returns>
        public Task CreateTask(NewTaskViewModel newTaskViewModel, string userName)
        {
            var task = new Task
            {
                UserName = userName,
                CreatedDate = DateTime.UtcNow,
                Description = newTaskViewModel.Description
            };

            if (null != newTaskViewModel.StartDate)
            {
                task.StartDate = newTaskViewModel.StartDate.Value.ToUniversalTime();
            }

            if (null != newTaskViewModel.EndDate)
            {
                task.EndDate = newTaskViewModel.EndDate.Value.ToUniversalTime();
            }

            taskMonitorContext.Tasks.Add(task);
            taskMonitorContext.SaveChanges();
            return task;
        }

        /// <summary>
        /// Get all tasks.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>
        /// All the tasks.
        /// </returns>
        public IEnumerable<TaskViewModel> Get(string userName)
        {
            IEnumerable<TaskViewModel> tasks = taskMonitorContext
                .Tasks
                .Select(task =>
                    new TaskViewModel
                    {
                        Description = task.Description,
                        StartDateTime = task.StartDate,
                        EndDateTime = task.EndDate
                    })
                .Distinct()
                .OrderBy(task => task.StartDateTime)
                .ToList();
            return ConvertDateTimeToLocal(tasks);
        }


        /// <summary>
        /// Tasks done today
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>
        /// Tasks done today
        /// </returns>
        public IEnumerable<TaskViewModel> GetTasksDoneToday(string userName)
        {
            return GetTasksOnDate(DateTime.Today.ToString("yyyyMMdd"), userName);
        }

        /// <summary>
        /// Tasks done in last one week
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>
        /// Tasks done in last one week
        /// </returns>
        public IEnumerable<TaskViewModel> GetTasksDoneInLastWeek(string userName)
        {
            var today = DateTime.Today;
            var fromDateinclusive = today.Subtract(new TimeSpan(6, 0, 0, 0));
            var toDateExclusive = today.AddDays(1); // Add one day as  it is end date is exclusive.
            return GetTasksDoneInDateRange(fromDateinclusive, toDateExclusive, userName);
        }

        /// <summary>
        /// Tasks done in past one month
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>
        /// Tasks done in past one month
        /// </returns>
        public IEnumerable<TaskViewModel> GetTasksDoneInPastOneMonth(string userName)
        {
            var today = DateTime.Today;
            var fromDateinclusive = today.AddMonths(-1);
            var toDateExclusive = DateTime.Today.AddDays(1); // Add one day as  it is end date is exclusive.
            return GetTasksDoneInDateRange(fromDateinclusive, toDateExclusive, userName);
        }

        /// <summary>
        /// Tasks done in last calendar month
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>
        /// Tasks done in last calendar month
        /// </returns>
        public IEnumerable<TaskViewModel> GetTasksDoneInLastMonth(string userName)
        {
            var today = DateTime.Today;
            var day = DateTime.Today.Day - 1;
            var fromDateinclusive = today.AddMonths(-1).AddDays(-day); // Gets first day of previous month.
            var toDateExclusive = fromDateinclusive.AddMonths(1); // Gets first day of this month.
            return GetTasksDoneInDateRange(fromDateinclusive, toDateExclusive, userName);
        }

        /// <summary>
        /// Get tasks on a given month of an year
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public IEnumerable<TaskViewModel> GetTasksOnMonth(string date, string userName)
        {
            date = date + "01"; //To get first date of the month in yyyyMMdd
            DateTime fromDateTimeInclusive;
            if (DateTime.TryParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out fromDateTimeInclusive))
            {
                var toDateTimeExclusive = fromDateTimeInclusive.AddMonths(1);
                return GetTasksDoneInDateRange(fromDateTimeInclusive, toDateTimeExclusive, userName);
            }
            return null;
        }

        /// <summary>
        /// Get tasks on a given date
        /// </summary>
        /// <param name="date">Date as string YYYYMMDD</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public IEnumerable<TaskViewModel> GetTasksOnDate(string date, string userName)
        {
            DateTime givenDay;
            if (DateTime.TryParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out givenDay))
            {
                var nextDay = givenDay.AddDays(1);
                return GetTasksDoneInDateRange(givenDay, nextDay, userName);
            }
            return null;
        }

        /// <summary>
        /// Gets the tasks done in date range.
        /// </summary>
        /// <param name="fromDateInclusive">From date inclusive.</param>
        /// <param name="toDateExlusive">To date exlusive.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        private IEnumerable<TaskViewModel> GetTasksDoneInDateRange(DateTime fromDateInclusive, DateTime toDateExlusive, string userName)
        {
            IEnumerable<TaskViewModel> tasksInGivenDateRange
                    = taskMonitorContext
                        .Tasks
                        .Where(task => task.UserName == userName && task.StartDate >= fromDateInclusive && task.StartDate < toDateExlusive)
                        .OrderBy(task => task.StartDate)
                        .Distinct()
                        .Select(task =>
                            new TaskViewModel
                            {
                                Description = task.Description,
                                StartDateTime = task.StartDate,
                                EndDateTime = task.EndDate
                            })
                        .ToList();
            return ConvertDateTimeToLocal(tasksInGivenDateRange);
        }

        private IEnumerable<TaskViewModel> ConvertDateTimeToLocal(IEnumerable<TaskViewModel> tasks)
        {
            if (null == tasks) return null;
            var convertedTasks = tasks as IList<TaskViewModel> ?? tasks.ToList();
            foreach (var task in convertedTasks)
            {
                task.StartDateTime = task.StartDateTime.ToLocalTime();
                task.EndDateTime = task.EndDateTime.HasValue ? task.EndDateTime.Value.ToLocalTime() : task.EndDateTime;
            }
            return convertedTasks;
        }
    }
}