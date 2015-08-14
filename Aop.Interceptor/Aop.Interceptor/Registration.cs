namespace AOP.Interceptor
{
    using Castle.Core;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Registration;

    public class Registration : IRegistration
    {
        #region Public Methods and Operators

        public void Register(IKernelInternal kernel)
        {
            kernel.Register(Component.For<LoggingInterceptor>().ImplementedBy<LoggingInterceptor>());

            kernel.Register(
                Component.For<ITask>().ImplementedBy<Task>().Interceptors(
                    InterceptorReference.ForType<LoggingInterceptor>()).Anywhere);
            //Anywhere tells the registration process that this interceptor could be attached anywhere.
        }

        #endregion
    }
}