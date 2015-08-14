namespace TaskManager.Web.Common
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http.Dependencies;
    using Ninject;

    public sealed class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel container;

        public NinjectDependencyResolver(IKernel kernel)
        {
            container = kernel;
        }

        public IKernel Container
        {
            get { return container; }
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public object GetService(Type serviceType)
        {
            return container.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.GetAll(serviceType);
        }
    }
}