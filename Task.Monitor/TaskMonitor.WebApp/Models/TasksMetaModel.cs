namespace TaskMonitor.WebApp.Models
{
    /// <summary>
    /// Tasks meta model
    /// </summary>
    public class TasksMetaModel
    {
        /// <summary>
        /// Gets or sets the task on which maximum time was spent.
        /// </summary>
        /// <value>
        /// The task on which maximum time was spent.
        /// </value>
        public string TaskOnWhichMaximumTimeWasSpent { get; set; }

        /// <summary>
        /// Gets or sets the task on which minimum time was spent.
        /// </summary>
        /// <value>
        /// The task on which minimum time was spent.
        /// </value>
        public string TaskOnWhichMinimumTimeWasSpent { get; set; }

        /// <summary>
        /// Gets or sets the maximum time on a task in minutes.
        /// </summary>
        /// <value>
        /// The maximum time on a task in minutes.
        /// </value>
        public double MaxTimeOnATaskInMinutes { get; set; }

        /// <summary>
        /// Gets or sets the minimum time on a task in minutes.
        /// </summary>
        /// <value>
        /// The minimum time on a task in minutes.
        /// </value>
        public double MinTimeOnATaskInMinutes { get; set; }
    }
}