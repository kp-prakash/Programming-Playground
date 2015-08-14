using System.Collections.Generic;

namespace TaskManager.Web.Api.Models
{
    public class User
    {
        private List<Link> links;

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public List<Link> Links
        {
            get { return links ?? (links = new List<Link>()); }
            set { links = value; }
        }

        public long UserId { get; set; }

        public string Username { get; set; }

        public void AddLink(Link link)
        {
            Links.Add(link);
        }
    }
}