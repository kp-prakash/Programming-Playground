namespace TaskManager.Web.Api
{
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using log4net.Config;
    using NHibernate;
    using NHibernate.Context;
    using Ninject;
    using Ninject.Activation;
    using Ninject.Web.Common;
    using TaskManager.Common;
    using TaskManager.Common.Logging;
    using TaskManager.Common.Security;
    using TaskManager.Common.TypeMapping;
    using TaskManager.Data.Entities;
    using TaskManager.Data.SqlServer.Mapping;
    using TaskManager.Web.Api.AutoMappingConfiguration;
    using TaskManager.Web.Api.MaintenanceProcessing;
    using TaskManager.Web.Common;
    using TaskManager.Web.Common.Security;

    public class NinjectConfigurator
    {
        public void Configure(IKernel container)
        {
            AddBindings(container);
        }

        private void AddBindings(IKernel container)
        {
            ConfigureLog4Net(container);
            ConfigureUserSession(container);
            ConfigureNHibernate(container);
            ConfigureAutoMapper(container);
            container.Bind<IDateTime>().To<DateTimeAdapter>().InSingletonScope();
            container.Bind<IAddTaskQueryProcessor>().To<AddTaskQueryProcessor>().InRequestScope();
            container.Bind<IAddTaskMaintenanceProcessor>().To<AddTaskMaintenanceProcessor>().InRequestScope();
        }

        private void ConfigureAutoMapper(IKernel container)
        {
            container.Bind<IAutoMapper>().To<AutoMapperAdapter>().InSingletonScope();
            container.Bind<IAutoMapperTypeConfigurator>()
                .To<StatusEntityToStatusAutoMapperTypeConfigurator>()
                .InSingletonScope();
            container.Bind<IAutoMapperTypeConfigurator>()
                .To<StatusToStatusEntityAutoMapperTypeConfigurator>()
                .InSingletonScope();
            container.Bind<IAutoMapperTypeConfigurator>()
                .To<UserEntityToUserAutoMapperTypeConfigurator>()
                .InSingletonScope();
            container.Bind<IAutoMapperTypeConfigurator>()
                .To<UserToUserEntityAutoMapperTypeConfigurator>()
                .InSingletonScope();
            container.Bind<IAutoMapperTypeConfigurator>()
                .To<NewTaskToTaskEntityAutoMapperTypeConfigurator>()
                .InSingletonScope();
            container.Bind<IAutoMapperTypeConfigurator>()
                .To<TaskEntityToTaskAutoMapperTypeConfigurator>()
                .InSingletonScope();
        }

        private void ConfigureLog4Net(IKernel container)
        {
            XmlConfigurator.Configure();

            var logManager = new LogManagerAdapter();
            container.Bind<ILogManager>().ToConstant(logManager);
        }

        private void ConfigureNHibernate(IKernel container)
        {
            var sessionFactory =
                Fluently.Configure()
                    .Database(
                        MsSqlConfiguration.MsSql2012.ConnectionString(c => c.FromConnectionStringWithKey("TaskManager")))
                    .CurrentSessionContext("web")
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TaskMap>())
                    .BuildSessionFactory();

            container.Bind<ISessionFactory>().ToConstant(sessionFactory);
            container.Bind<ISession>().ToMethod(CreateSession).InRequestScope();
            container.Bind<IActionTransactionHelper>().To<ActionTransactionHelper>().InRequestScope();
        }

        private void ConfigureUserSession(IKernel container)
        {
            var userSession = new UserSession();
            container.Bind<IUserSession>().ToConstant(userSession).InSingletonScope();
            container.Bind<IWebUserSession>().ToConstant(userSession).InSingletonScope();
        }

        private ISession CreateSession(IContext context)
        {
            var sessionFactory = context.Kernel.Get<ISessionFactory>();
            if (!CurrentSessionContext.HasBind(sessionFactory))
            {
                var session = sessionFactory.OpenSession();
                CurrentSessionContext.Bind(session);
            }

            return sessionFactory.GetCurrentSession();
        }
    }
}