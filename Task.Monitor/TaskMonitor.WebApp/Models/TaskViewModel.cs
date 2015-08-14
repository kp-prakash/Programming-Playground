namespace TaskMonitor.WebApp.Models
{
    using System;

    public class TaskViewModel
    {
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
    }
}
