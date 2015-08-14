namespace TaskMonitor.WebApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using TaskMonitor.DataAccess;
    using TaskMonitor.WebApp.Models;
    using TaskMonitor.WebApp.TaskUtil;

    /// <summary>
    /// Home Controller.
    /// </summary>
    [Authorize]
    [RequireHttps]
    public class HomeController : Controller
    {
        /// <summary>
        /// The task manager
        /// </summary>
        private ITaskManager taskManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        public HomeController()
        {
            taskManager = new TaskManager();
        }

        /// <summary>
        /// Abouts this application.
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "This application lets you track your tasks performed on a day to day basis!";
            return View();
        }

        /// <summary>
        /// Contacts view.
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Srihari Sridharan.";
            return View();
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.Message = "Working on something important? Post it! Monitor your progress with Task Monitor!";
            return View();
        }

        /// <summary>
        /// Home page!
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(NewTaskViewModel newTask)
        {
            ViewBag.Message = "Please enter task details!";
            if (!ModelState.IsValid)
            {
                return View(newTask);
            }
            taskManager.CreateTask(newTask, User.Identity.Name);
            return View(newTask);
        }

        /// <summary>
        /// Returns the tasks performed today by default.
        /// </summary>
        /// <returns></returns>
        public ActionResult Today()
        {
            string userName = User.Identity.Name;
            IEnumerable<TaskViewModel> tasks = taskManager.GetTasksDoneToday(userName);
            var tasksDictionary = ConvertToTasksDictionaryOfDates(tasks);
            // UpdateViewBagWithTaskMetaModel(tasks);
            ViewBag.Title = "Task Monitor - Today's report";
            ViewBag.Message = "Tasks done today: ";
            return View(tasksDictionary);
        }

        /// <summary>
        /// Returns the tasks performed in last calendar month
        /// </summary>
        /// <returns></returns>
        public ActionResult LastMonth()
        {
            string userName = User.Identity.Name;
            IEnumerable<TaskViewModel> tasks = taskManager.GetTasksDoneInLastMonth(userName);
            var tasksDictionary = ConvertToTasksDictionaryOfDates(tasks);
            // UpdateViewBagWithTaskMetaModel(tasks);
            ViewBag.Title = "Task Monitor - Report for last month";
            ViewBag.Message = "Tasks done in last one month: ";
            return View("Month", tasksDictionary);
        }

        /// <summary>
        /// Returns the tasks performed in past one month
        /// </summary>
        /// <returns></returns>
        public ActionResult PastMonth()
        {
            string userName = User.Identity.Name;
            IEnumerable<TaskViewModel> tasks = taskManager.GetTasksDoneInPastOneMonth(userName);
            var tasksDictionary = ConvertToTasksDictionaryOfDates(tasks);
            // UpdateViewBagWithTaskMetaModel(tasks);
            ViewBag.Title = "Task Monitor - Report for past one month";
            ViewBag.Message = "Tasks done in past one month: ";
            return View("Month", tasksDictionary);
        }

        private static IEnumerable<IGrouping<string, TaskViewModel>> ConvertToTasksDictionaryOfDates(IEnumerable<TaskViewModel> tasks)
        {
            var taskViewModels = tasks as IList<TaskViewModel> ?? tasks.ToList();
            taskViewModels.ToList().Sort((taskA, taskB) => DateTime.Compare(taskA.StartDateTime, taskB.StartDateTime));
            var tasksDictionary = taskViewModels.GroupBy(task => task.StartDateTime.ToShortDateString());
            return tasksDictionary;
        }

        //// TODO: Move this to a utility class.
        ///// <summary>
        ///// Computes the task meta details.
        ///// </summary>
        ///// <param name="tasks">The tasks.</param>
        ///// <returns></returns>
        //private TasksMetaModel ComputeTaskMetaDetails(IEnumerable<TaskViewModel> tasks)
        //{
        //    // TODO: Fix this computation... there is a problem out here!
        //    var taskMetaModel = new TasksMetaModel();
        //    TimeSpan? lastMaxTimeDiff = new TimeSpan(0, 0, 0, 0);
        //    TimeSpan? lastMinTimeDiff = new TimeSpan(0, 0, 0, 0);
        //    foreach (var taskViewModel in tasks)
        //    {
        //        var currentTimeDiff = taskViewModel.EndDateTime.HasValue
        //            ? taskViewModel.EndDateTime.Value.Subtract(taskViewModel.StartDateTime)
        //            : new TimeSpan(0, 0, 0, 0);
        //        if (currentTimeDiff > lastMaxTimeDiff)
        //        {
        //            lastMaxTimeDiff = currentTimeDiff;
        //            taskMetaModel.TaskOnWhichMaximumTimeWasSpent = taskViewModel.Description;
        //            taskMetaModel.MaxTimeOnATaskInMinutes = lastMaxTimeDiff.Value.TotalMinutes;
        //        }
        //        if (currentTimeDiff < lastMinTimeDiff)
        //        {
        //            lastMinTimeDiff = currentTimeDiff;
        //            taskMetaModel.TaskOnWhichMinimumTimeWasSpent = taskViewModel.Description;
        //            taskMetaModel.MinTimeOnATaskInMinutes = lastMinTimeDiff.Value.TotalMinutes;
        //        }
        //    }
        //    return taskMetaModel;
        //}

        //private void UpdateViewBagWithTaskMetaModel(IEnumerable<TaskViewModel> tasks)
        //{
        //    TasksMetaModel taskMetaModel = ComputeTaskMetaDetails(tasks);
        //    ViewBag.MaxTimeOnATaskInMinutes = taskMetaModel.MaxTimeOnATaskInMinutes;
        //    ViewBag.MinTimeOnATaskInMinutes = taskMetaModel.MinTimeOnATaskInMinutes;
        //    ViewBag.TaskOnWhichMaximumTimeWasSpent = taskMetaModel.TaskOnWhichMaximumTimeWasSpent;
        //    ViewBag.TaskOnWhichMinimumTimeWasSpent = taskMetaModel.TaskOnWhichMinimumTimeWasSpent;
        //}
    }
}