using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TaskMonitor.WebApp
{
    using System.Web.Routing;
    using System.Web.Security;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
