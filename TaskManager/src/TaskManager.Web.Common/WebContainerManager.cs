namespace TaskManager.Web.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Dependencies;

    public static class WebContainerManager
    {
        public static T Get<T>()
        {
            var service = GetDependencyResolver().GetService(typeof(T));
            if (service == null)
            {
                throw new NullReferenceException(string.Format(
                    "Requested service of type {0}, but null was found.",
                    typeof(T).FullName));
            }
            return (T)service;
        }

        public static IEnumerable<T> GetAll<T>()
        {
            var services = GetDependencyResolver().GetServices(typeof(T));
            var serviceList = services as IList<object> ?? services.ToList();
            if (!serviceList.Any())
            {
                throw new NullReferenceException(string.Format(
                    "Requested services of type {0}, but none were found.",
                    typeof(T).FullName));
            }

            return serviceList.Cast<T>();
        }

        public static IDependencyResolver GetDependencyResolver()
        {
            var dependencyResolver = GlobalConfiguration.Configuration.DependencyResolver;
            if (null != dependencyResolver)
            {
                return dependencyResolver;
            }

            throw new InvalidOperationException("The dependency resolver has not been set.");
        }
    }
}