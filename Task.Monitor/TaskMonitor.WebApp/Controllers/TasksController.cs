namespace TaskMonitor.WebApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Mvc;
    using TaskMonitor.DataAccess;
    using TaskMonitor.WebApp.Models;
    using TaskMonitor.WebApp.TaskUtil;

    /// <summary>
    /// Controller for managing tasks!
    /// </summary>
    [System.Web.Http.Authorize]
    [RequireHttps]
    [System.Web.Http.RoutePrefix("api/tasks")]
    public class TasksController : ApiController
    {
        private readonly ITaskManager taskManager;

        /// <summary>
        /// Creates a new instance of TasksController
        /// </summary>
        public TasksController()
        {
            taskManager = new TaskManager();
        }

        /// <summary>
        /// Posts the specified new task.
        /// </summary>
        /// <param name="newTask">The new task.</param>
        public void Post(NewTaskViewModel newTask)
        {
            if (!string.IsNullOrWhiteSpace(newTask.Description) && newTask.StartDate != null)
            {
                taskManager.CreateTask(newTask, User.Identity.Name);
            }
        }

        /// <summary>
        /// Get all tasks.
        /// </summary>
        /// <returns>All the tasks.</returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("", Name = "TasksDefault")]
        public IEnumerable<TaskViewModel> Get()
        {
            string userName = User.Identity.Name;
            return taskManager.Get(userName);
        }

        /// <summary>
        /// Tasks done in last calendar month
        /// </summary>
        /// <returns>Tasks done in last calendar month</returns>
        [System.Web.Http.Route("lastmonth", Name = "LastMonth")]
        [System.Web.Http.HttpGet]
        public IEnumerable<TaskViewModel> GetTasksDoneInLastMonth()
        {
            string userName = User.Identity.Name;
            return taskManager.GetTasksDoneInLastMonth(userName);
        }

        /// <summary>
        /// Tasks done in last one week
        /// </summary>
        /// <returns>Tasks done in last one week</returns>
        [System.Web.Http.Route("lastweek", Name = "LastWeek")]
        [System.Web.Http.HttpGet]
        public IEnumerable<TaskViewModel> GetTasksDoneInLastWeek()
        {
            string userName = User.Identity.Name;
            return taskManager.GetTasksDoneInLastWeek(userName);
        }

        /// <summary>
        /// Tasks done in past one month
        /// </summary>
        /// <returns>Tasks done in past one month</returns>
        [System.Web.Http.Route("pastmonth", Name = "PastMonth")]
        [System.Web.Http.HttpGet]
        public IEnumerable<TaskViewModel> GetTasksDoneInPastOneMonth()
        {
            string userName = User.Identity.Name;
            return taskManager.GetTasksDoneInPastOneMonth(userName);
        }

        /// <summary>
        /// Tasks done today
        /// </summary>
        /// <returns>Tasks done today</returns>
        [System.Web.Http.Route("today", Name = "Today")]
        [System.Web.Http.HttpGet]
        public IEnumerable<TaskViewModel> GetTasksDoneToday()
        {
            string userName = User.Identity.Name;
            return taskManager.GetTasksDoneToday(userName);
        }

        /// <summary>
        /// Get tasks on a given date
        /// </summary>
        /// <param name="date">Date as string YYYYMMDD</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("{date:length(8,8)}", Name = "TasksOnAGivenDate")]
        public IEnumerable<TaskViewModel> GetTasksOnDate(string date)
        {
            string userName = User.Identity.Name;
            return taskManager.GetTasksOnDate(date, userName);
        }

        /// <summary>
        /// Get tasks on a given month of an year
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("{date:length(6,6)}", Name = "TasksOnAGivenMonth")]
        public IEnumerable<TaskViewModel> GetTasksOnMonth(string date)
        {
            string userName = User.Identity.Name;
            return taskManager.GetTasksOnMonth(date, userName);
        }
    }
}