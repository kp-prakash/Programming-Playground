using System;
using PostSharp.Aspects;

namespace AOPUsingPOSTSharp.Aspects
{
    /// <summary>
    /// A simple aspect to demonstrate object initialization.
    /// </summary>
    [Serializable]
    public class ObjectInitializationAspect : LocationInterceptionAspect
    {
        #region Location Interception Aspect - Demo
        /// <summary>
        /// Method invoked <i>instead</i> of the <c>Get</c> semantic of the field or property to which the current aspect is applied,
        /// i.e. when the value of this field or property is retrieved.
        /// </summary>
        /// <param name="args">Advice arguments.</param>
        public override void OnGetValue(LocationInterceptionArgs args)
        {
            if (null == args.GetCurrentValue())
                Console.WriteLine(string.Format("Property ({0}) not initialized",
                    args.LocationFullName));
        } 
        #endregion
    }
}