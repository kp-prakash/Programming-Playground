namespace TaskManager.Common.Logging
{
    using System;
    using log4net;

    public interface ILogManager
    {
        ILog GetLog(Type typeAssociatedWithRequestedLog);
    }
}