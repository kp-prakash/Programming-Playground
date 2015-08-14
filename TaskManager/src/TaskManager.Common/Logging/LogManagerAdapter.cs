namespace TaskManager.Common.Logging
{
    using System;
    using log4net;

    public class LogManagerAdapter : ILogManager
    {
        public ILog GetLog(Type typeAssociatedWithRequestedLog)
        {
            return LogManager.GetLogger(typeAssociatedWithRequestedLog);
        }
    }
}