namespace TaskManager.Web.Common
{
    using System;
    using System.Net.Http;
    using System.Web.Http.Tracing;
    using log4net;
    using TaskManager.Common.Logging;

    public class SimpleTraceWriter : ITraceWriter
    {
        private const string NewLine = "=============================================================================";

        private readonly ILog log;

        public SimpleTraceWriter(ILogManager logManager)
        {
            log = logManager.GetLog(typeof(SimpleTraceWriter));
        }

        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            var traceRecord = new TraceRecord(request, category, level);
            traceAction(traceRecord);
            WriteTrace(traceRecord);
        }

        private void WriteTrace(TraceRecord traceRecord)
        {
            const string traceFormat =
                "RequestId={0};{1}Kind={2};{3}Status={4};{5}Operation={6};{7}Operator={8};{9}Category={10}{11}Request={12}{13}Message={14}{15}{16}{17}";

            var args = new object[]
            {
                traceRecord.RequestId,
                Environment.NewLine,
                traceRecord.Kind,
                Environment.NewLine,
                traceRecord.Status,
                Environment.NewLine,
                traceRecord.Operation,
                Environment.NewLine,
                traceRecord.Operator,
                Environment.NewLine,
                traceRecord.Category,
                Environment.NewLine,
                traceRecord.Request,
                Environment.NewLine,
                traceRecord.Message,
                Environment.NewLine,
                NewLine,
                Environment.NewLine
            };

            switch (traceRecord.Level)
            {
                case TraceLevel.Debug:
                    log.DebugFormat(traceFormat, args);
                    break;

                case TraceLevel.Info:
                    log.InfoFormat(traceFormat, args);
                    break;

                case TraceLevel.Warn:
                    log.WarnFormat(traceFormat, args);
                    break;

                case TraceLevel.Error:
                    log.ErrorFormat(traceFormat, args);
                    break;

                case TraceLevel.Fatal:
                    log.FatalFormat(traceFormat, args);
                    break;
            }
        }
    }
}