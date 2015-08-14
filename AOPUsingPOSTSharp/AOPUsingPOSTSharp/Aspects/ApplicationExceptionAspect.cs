using System;
using System.Reflection;
using PostSharp.Aspects;

namespace AOPUsingPOSTSharp.Aspects
{
    /// <summary>
    /// Simple aspect to demonstrate exception handling.
    /// </summary>
    [Serializable]
    public class ApplicationExceptionAspect : OnExceptionAspect
    {
        #region Exception Aspect - Demo
        /// <summary>
        /// Override this method and return the type of exception
        /// that should be used in the catch block.
        /// </summary>
        /// <param name="targetMethod">Target method where this aspect is used.</param>
        /// <returns>Exception's type.</returns>
        public override Type GetExceptionType(MethodBase targetMethod)
        {
            return typeof(ApplicationException);
        }

        /// <summary>
        /// Method executed <b>after</b> the body of methods to which this aspect is applied,
        /// in case that the method resulted with an exception (i.e., in a <c>catch</c> block).
        /// </summary>
        /// <param name="args">Advice arguments.</param>
        public override void OnException(MethodExecutionArgs args)
        {
            Console.WriteLine(string.Format("Exception: {0}\n\n{1}",
                                args.Exception.GetType().Name,
                                args.Exception.StackTrace));
        } 
        #endregion
    }
}