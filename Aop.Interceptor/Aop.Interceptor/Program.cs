namespace AOP.Interceptor
{
    using System;

    internal class Program
    {
        #region Methods

        private static void Main(string[] args)
        {
            IoCContainer.Initialize();
            var myType = IoCContainer.Resolve<ITask>();
            myType.DoSomething("PrintToConsole", 1);
            Console.ReadKey();
        }

        #endregion
    }
}