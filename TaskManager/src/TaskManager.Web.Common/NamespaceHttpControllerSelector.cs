namespace TaskManager.Web.Common
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dispatcher;
    using System.Web.Http.Routing;

    public class NamespaceHttpControllerSelector : IHttpControllerSelector
    {
        private readonly HttpConfiguration configuration;

        private readonly Lazy<Dictionary<string, HttpControllerDescriptor>> controllers;

        public NamespaceHttpControllerSelector(HttpConfiguration config)
        {
            configuration = config;
            controllers = new Lazy<Dictionary<string, HttpControllerDescriptor>>(InitializeControllerDictionary);
        }

        public IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
        {
            return controllers.Value;
        }

        public HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            IHttpRouteData routeData = request.GetRouteData();
            if (routeData == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            string controllerName = GetControllerName(routeData);
            if (controllerName == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            string namespaceName = GetVersion(routeData);
            if (namespaceName == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            string controllerKey = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", namespaceName, controllerName);
            HttpControllerDescriptor httpControllerDescriptor;
            if (controllers.Value.TryGetValue(controllerKey, out httpControllerDescriptor))
            {
                return httpControllerDescriptor;
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        private string GetControllerName(IHttpRouteData routeData)
        {
            IHttpRouteData subRoute = routeData.GetSubRoutes().FirstOrDefault();
            if (subRoute == null)
            {
                return null;
            }

            object dataTokenValue = subRoute.Route.DataTokens.First().Value;
            if (dataTokenValue == null)
            {
                return null;
            }

            string controllerName
                = ((HttpActionDescriptor[])dataTokenValue).First()
                    .ControllerDescriptor.ControllerName.Replace("Controller", string.Empty);
            return controllerName;
        }

        private T GetRouteVariable<T>(IHttpRouteData routeData, string name)
        {
            object result;
            if (routeData.Values.TryGetValue(name, out result))
            {
                return (T)result;
            }
            return default(T);
        }

        private string GetVersion(IHttpRouteData routeData)
        {
            IHttpRouteData subRouteData = routeData.GetSubRoutes().FirstOrDefault();
            return subRouteData == null ? null : GetRouteVariable<string>(subRouteData, "apiVersion");
        }

        private Dictionary<string, HttpControllerDescriptor> InitializeControllerDictionary()
        {
            var dictionary = new Dictionary<string, HttpControllerDescriptor>(StringComparer.OrdinalIgnoreCase);
            IAssembliesResolver assembliesResolver = configuration.Services.GetAssembliesResolver();
            IHttpControllerTypeResolver controllersResolver = configuration.Services.GetHttpControllerTypeResolver();
            ICollection<Type> controllerTypes = controllersResolver.GetControllerTypes(assembliesResolver);

            foreach (Type controllerType in controllerTypes)
            {
                if (controllerType.Namespace != null)
                {
                    string[] segments = controllerType.Namespace.Split(Type.Delimiter);
                    string controllerName = controllerType.Name.Remove(controllerType.Name.Length -
                                                                       DefaultHttpControllerSelector.ControllerSuffix
                                                                           .Length);
                    string controllerKey = string.Format(
                        CultureInfo.InvariantCulture,
                        "{0}.{1}", segments[segments.Length - 1],
                        controllerName);
                    if (!dictionary.ContainsKey(controllerKey))
                    {
                        dictionary[controllerKey]
                            = new HttpControllerDescriptor(configuration, controllerType.Name, controllerType);
                    }
                }
            }

            return dictionary;
        }
    }
}