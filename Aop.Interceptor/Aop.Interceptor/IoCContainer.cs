namespace AOP.Interceptor
{
    using Castle.Windsor;

    public class IoCContainer
    {
        #region Constants and Fields

        private static IWindsorContainer _container;

        #endregion

        #region Public Methods and Operators

        public static void Initialize()
        {
            //Code based registration
            _container = new WindsorContainer();
            _container.Register(new Registration());

            //Config based registration
            // Use code below to use config based registration.
            //_container = new WindsorContainer("castle.config");
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        #endregion
    }
}