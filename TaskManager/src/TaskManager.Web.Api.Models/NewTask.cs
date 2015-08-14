namespace TaskManager.Web.Api.Models
{
    using System;
    using System.Collections.Generic;

    public class NewTask
    {
        public List<User> Assignees { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? StartDate { get; set; }

        public string Subject { get; set; }
    }
}