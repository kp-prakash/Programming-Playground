namespace TaskManager.Data.Entities
{
    using System.Linq;
    using NHibernate;
    using TaskManager.Common;
    using TaskManager.Common.Security;
    using TaskManager.Data.Exceptions;

    public class AddTaskQueryProcessor : IAddTaskQueryProcessor
    {
        private readonly IDateTime dateTime;
        private readonly ISession session;
        private readonly IUserSession userSession;

        public AddTaskQueryProcessor(ISession session, IUserSession userSession, IDateTime dateTime)
        {
            this.dateTime = dateTime;
            this.session = session;
            this.userSession = userSession;
        }

        public void AddTask(Task task)
        {
            task.CreatedDate = dateTime.UtcNow;
            task.Status = session.QueryOver<Status>().Where(x => x.Name == "Not Started").SingleOrDefault();

            // TODO: Uncomment this line after security is implemented.
            // task.CreatedBy = session.QueryOver<User>().Where(x => x.UserName == userSession.UserName).SingleOrDefault();

            // TODO: Remove this line after security is implemented.
            // Temporary hack: all tasks created by User 1.
            task.CreatedBy = session.Get<User>(1L);
            if (task.Users != null && task.Users.Any())
            {
                for (var i = 0; i < task.Users.Count; i++)
                {
                    var user = task.Users[i];
                    var persistedUser = session.Get<User>(user.UserId);
                    if (persistedUser == null)
                    {
                        throw new ChildObjectNotFoundException("User not found!");
                    }
                    task.Users[i] = persistedUser;
                }
            }

            session.SaveOrUpdate(task);
        }
    }
}