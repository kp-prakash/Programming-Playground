namespace TaskManager.Web.Common.ErrorHandling
{
    using System.Web.Http.ExceptionHandling;
    using log4net;
    using TaskManager.Common.Logging;

    public class SimpleExceptionLogger : ExceptionLogger
    {
        private readonly ILog log;

        public SimpleExceptionLogger(ILogManager logManager)
        {
            log = logManager.GetLog(typeof(SimpleExceptionLogger));
        }

        public override void Log(ExceptionLoggerContext context)
        {
            log.Error("Unhandled exception", context.Exception);
        }
    }
}