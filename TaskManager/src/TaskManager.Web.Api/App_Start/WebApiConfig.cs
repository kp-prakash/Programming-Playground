namespace TaskManager.Web.Api
{
    using System.Web.Http;
    using System.Web.Http.Dispatcher;
    using System.Web.Http.ExceptionHandling;
    using System.Web.Http.Routing;
    using System.Web.Http.Tracing;
    using TaskManager.Common.Logging;
    using TaskManager.Web.Common;
    using TaskManager.Web.Common.ErrorHandling;
    using TaskManager.Web.Common.Routing;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var constraintResolver = new DefaultInlineConstraintResolver();
            constraintResolver.ConstraintMap.Add("apiVersionConstraint",
                typeof(ApiVersionConstraint));
            config.MapHttpAttributeRoutes(constraintResolver);
            config.Services.Replace(typeof(IHttpControllerSelector),
                new NamespaceHttpControllerSelector(config));

            // This call is enough for a simple tracing.
            // config.EnableSystemDiagnosticsTracing();

            config.Services.Replace(typeof(ITraceWriter),
                new SimpleTraceWriter(WebContainerManager.Get<ILogManager>()));
            config.Services.Replace(typeof(IExceptionLogger),
                new SimpleExceptionLogger(WebContainerManager.Get<ILogManager>()));
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
        }
    }
}