namespace TaskManager.Web.Common.Routing
{
    using System.Web.Http;

    public class ApiVersion1RoutePrefixAttribute : RoutePrefixAttribute
    {
        private const string PrefixRouteBase = RouteBase + "/";
        private const string RouteBase = "api/{apiVersion:apiVersionConstraint(v1)}";

        public ApiVersion1RoutePrefixAttribute(string routePrefix)
            : base(string.IsNullOrWhiteSpace(routePrefix) ? RouteBase : PrefixRouteBase + routePrefix)
        {
        }
    }
}