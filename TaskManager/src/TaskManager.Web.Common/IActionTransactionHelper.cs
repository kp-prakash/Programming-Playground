namespace TaskManager.Web.Common
{
    using System.Web.Http.Filters;

    public interface IActionTransactionHelper
    {
        void BeginTransaction();

        void CloseSession();

        void EndTransaction(HttpActionExecutedContext httpActionExecutedContext);
    }
}