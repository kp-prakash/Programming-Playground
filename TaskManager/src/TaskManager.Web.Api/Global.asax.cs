namespace TaskManager.Web.Api
{
    using System.Web;
    using System.Web.Http;
    using TaskManager.Common.Logging;
    using TaskManager.Common.TypeMapping;
    using TaskManager.Web.Common;

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            if (null != exception)
            {
                var log = WebContainerManager.Get<ILogManager>().GetLog(typeof(WebApiApplication));
                log.Error("Unhandled exception.", exception);
            }
        }

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            new AutoMapperConfigurator().Configure(WebContainerManager.GetAll<IAutoMapperTypeConfigurator>());
        }
    }
}