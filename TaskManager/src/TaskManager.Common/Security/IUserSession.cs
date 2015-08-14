namespace TaskManager.Common.Security
{
    public interface IUserSession
    {
        string FirstName { get; }

        string LastName { get; }

        string UserName { get; }

        bool IsInRole(string roleName);
    }
}