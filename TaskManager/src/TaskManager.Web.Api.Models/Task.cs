using System;
using System.Collections.Generic;

namespace TaskManager.Web.Api.Models
{
    public class Task : ILinkContaining
    {
        private List<Link> links;

        public List<User> Assignees { get; set; }

        public DateTime? CompletedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? DueDate { get; set; }

        public List<Link> Links
        {
            get { return links ?? (links = new List<Link>()); }
            set { links = value; }
        }

        public DateTime? StartDate { get; set; }

        public Status Status { get; set; }

        public string Subject { get; set; }

        public long? TaskId { get; set; }

        public void AddLink(Link link)
        {
            Links.Add(link);
        }
    }
}