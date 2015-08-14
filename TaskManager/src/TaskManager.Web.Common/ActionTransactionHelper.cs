namespace TaskManager.Web.Common
{
    using NHibernate;
    using NHibernate.Context;

    public class ActionTransactionHelper : IActionTransactionHelper
    {
        private readonly ISessionFactory sessionFactory;

        public ActionTransactionHelper(ISessionFactory sessionFactoryInstance)
        {
            sessionFactory = sessionFactoryInstance;
        }

        public bool SessionClosed { get; private set; }

        public bool TransactionHandled { get; private set; }

        public void BeginTransaction()
        {
            if (!CurrentSessionContext.HasBind(sessionFactory))
            {
                return;
            }

            var session = sessionFactory.GetCurrentSession();
            if (null != session)
            {
                session.BeginTransaction();
            }
        }

        public void CloseSession()
        {
            if (!CurrentSessionContext.HasBind(sessionFactory))
            {
                return;
            }

            var session = sessionFactory.GetCurrentSession();
            session.Close();
            session.Dispose();
            CurrentSessionContext.Unbind(sessionFactory);
            SessionClosed = true;
        }

        public void EndTransaction(System.Web.Http.Filters.HttpActionExecutedContext httpActionExecutedContext)
        {
            if (!CurrentSessionContext.HasBind(sessionFactory))
            {
                return;
            }

            var session = sessionFactory.GetCurrentSession();

            if (null == session)
            {
                return;
            }

            if (!session.Transaction.IsActive)
            {
                return;
            }

            if (httpActionExecutedContext.Exception == null)
            {
                session.Flush();
                session.Transaction.Commit();
            }
            else
            {
                session.Transaction.Rollback();
            }

            TransactionHandled = true;
        }
    }
}