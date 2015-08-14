namespace TaskManager.Web.Common.Security
{
    using System;
    using TaskManager.Common.Security;

    public interface IWebUserSession : IUserSession
    {
        string ApiVersionInUse { get; }

        string HttpRequestMethod { get; }

        Uri RequestUri { get; }
    }
}