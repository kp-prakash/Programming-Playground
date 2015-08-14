using System;
using PostSharp.Aspects;

namespace AOPUsingPOSTSharp.Aspects
{
    /// <summary>
    /// A simple aspect to demonstrate OnEntry, OnSuccess and OnExit.
    /// </summary>
    [Serializable]
    public class MethodBoundayAspect : OnMethodBoundaryAspect
    {
        #region Compile Time Initialize and Validate - Demo
        /// <summary>
        /// Field level variable to hold the compile time initialized method name.
        /// </summary>
        private string _fullMethodName;

        /// <summary>
        /// Method invoked at build time to initialize the instance fields of the current aspect. This method is invoked
        /// before any other build-time method.
        /// </summary>
        /// <param name="method">Method to which the current aspect is applied</param>
        /// <param name="aspectInfo">Reserved for future usage.</param>
        public override void CompileTimeInitialize(System.Reflection.MethodBase method, AspectInfo aspectInfo)
        {
            _fullMethodName = string.Format("{0}.{1}",
                                            method.DeclaringType.Name,
                                            method.Name);
        }

        /// <summary>
        /// Compile time validation to decide on use of aspect.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>True when validation succeeds.</returns>
        public override bool CompileTimeValidate(System.Reflection.MethodBase method)
        {
            //Return false for static methods and true for instance methods.
            //Hence this aspect will not be applied to static methods.
            return !method.IsStatic;
        }
        #endregion

        #region Runtime Initialize - Demo
        /// <summary>
        /// Initializes the current aspect for runtime behavior.
        /// </summary>
        /// <param name="method">Method to which the current aspect is applied.</param>
        public override void RuntimeInitialize(System.Reflection.MethodBase method)
        {
            if (method.Name.StartsWith("S"))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.BackgroundColor = ConsoleColor.Blue;
            }
        } 
        #endregion

        #region Method Bounday Aspect - Demo
        /// <summary>
        /// Method executed <b>before</b> the body of methods to which this aspect is applied.
        /// </summary>
        /// <param name="args">Event arguments specifying which method
        /// is being executed, which are its arguments, and how should the execution continue
        /// after the execution of <see cref="M:PostSharp.Aspects.IOnMethodBoundaryAspect.OnEntry(PostSharp.Aspects.MethodExecutionArgs)" />.</param>
        public override void OnEntry(MethodExecutionArgs args)
        {
            Console.WriteLine(string.Format("Entered Method: {0}", _fullMethodName));
        }

        /// <summary>
        /// Method executed <b>after</b> the body of methods to which this aspect is applied,
        /// even when the method exists with an exception (this method is invoked from
        /// the <c>finally</c> block).
        /// </summary>
        /// <param name="args">Event arguments specifying which method
        /// is being executed and which are its arguments.</param>
        public override void OnExit(MethodExecutionArgs args)
        {
            Console.WriteLine(string.Format("Exitting method: {0}", _fullMethodName));
        }

        /// <summary>
        /// Method executed <b>after</b> the body of methods to which this aspect is applied,
        /// but only when the method successfully returns (i.e. when no exception flies out
        /// the method.).
        /// </summary>
        /// <param name="args">Event arguments specifying which method
        /// is being executed and which are its arguments.</param>
        public override void OnSuccess(MethodExecutionArgs args)
        {
            var returnValue = (args.ReturnValue == null) ? "void" : args.ReturnValue.ToString();
            Console.WriteLine(string.Format("Success: {0} returned {1}",
                                _fullMethodName,
                                returnValue));
        }
        #endregion
    }
}