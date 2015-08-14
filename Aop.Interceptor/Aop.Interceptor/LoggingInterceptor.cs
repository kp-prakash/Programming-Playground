namespace AOP.Interceptor
{
    using System;

    using Castle.DynamicProxy;

    public class LoggingInterceptor : IInterceptor
    {
        #region Public Methods and Operators

        public void Intercept(IInvocation invocation)
        {
            try
            {
                Console.WriteLine("Logging On Start");
                invocation.Proceed();
                Console.WriteLine("Logging On Success");
            }
            catch (Exception e)
            {
                Console.WriteLine("Logging an exception has occurred");
                throw;
            }
            finally
            {
                Console.WriteLine("Logging on Exit");
            }
        }

        #endregion
    }
}