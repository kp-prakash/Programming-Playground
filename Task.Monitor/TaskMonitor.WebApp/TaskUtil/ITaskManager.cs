namespace TaskMonitor.WebApp.TaskUtil
{
    using System.Collections.Generic;
    using TaskMonitor.DataAccess;
    using TaskMonitor.WebApp.Models;

    /// <summary>
    /// Interface for TaskManager
    /// </summary>
    public interface ITaskManager
    {
        /// <summary>
        /// Get all tasks.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>
        /// All the tasks.
        /// </returns>
        IEnumerable<TaskViewModel> Get(string userName);

        /// <summary>
        /// Tasks done today
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>
        /// Tasks done today
        /// </returns>
        IEnumerable<TaskViewModel> GetTasksDoneToday(string userName);

        /// <summary>
        /// Tasks done in last one week
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>
        /// Tasks done in last one week
        /// </returns>
        IEnumerable<TaskViewModel> GetTasksDoneInLastWeek(string userName);

        /// <summary>
        /// Tasks done in past one month
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>
        /// Tasks done in past one month
        /// </returns>
        IEnumerable<TaskViewModel> GetTasksDoneInPastOneMonth(string userName);

        /// <summary>
        /// Tasks done in last calendar month
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>
        /// Tasks done in last calendar month
        /// </returns>
        IEnumerable<TaskViewModel> GetTasksDoneInLastMonth(string userName);

        /// <summary>
        /// Get tasks on a given month of an year
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        IEnumerable<TaskViewModel> GetTasksOnMonth(string date, string userName);

        /// <summary>
        /// Get tasks on a given date
        /// </summary>
        /// <param name="date">Date as string YYYYMMDD</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        IEnumerable<TaskViewModel> GetTasksOnDate(string date, string userName);

        /// <summary>
        /// Creates the task.
        /// </summary>
        /// <param name="newTaskViewModel">The new task view model.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns>
        /// Newly created task.
        /// </returns>
        Task CreateTask(NewTaskViewModel newTaskViewModel, string userName);
    }
}