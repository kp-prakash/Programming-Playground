namespace AOP.Interceptor
{
    using System;

    public class Task : ITask
    {
        #region Public Methods and Operators

        public void DoSomething(string taskName, int numberOfTimes)
        {
            //throw new DivideByZeroException("DBZ"); Test OnException interceptor.
            string message = string.Format(
                "Task.DoSomething execution '{0}' task {1} time{2}",
                taskName,
                numberOfTimes,
                (numberOfTimes > 1) ? "s" : string.Empty);
            Console.WriteLine(message);
        }

        #endregion
    }
}