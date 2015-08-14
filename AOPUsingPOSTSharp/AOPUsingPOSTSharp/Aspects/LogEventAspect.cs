using System;
using PostSharp.Aspects;

namespace AOPUsingPOSTSharp.Aspects
{
    /// <summary>
    /// A simple aspect to demonstrate event interception.
    /// </summary>
    [Serializable]
    public class LogEventAspect : EventInterceptionAspect
    {
        #region Event Interception Aspect - Demo
        /// <summary>
        /// Method invoked <i>instead</i> of the <c>Add</c> semantic of the event to which the current aspect is applied,
        /// i.e. when a new delegate is added to this event.
        /// </summary>
        /// <param name="args">Handler arguments.</param>
        public override void OnAddHandler(EventInterceptionArgs args)
        {
            Console.WriteLine(string.Format("Event {0} added.", args.Event.Name));

            //If ProceedAddHandler is not called the handler that
            //was supposed to be added will never get added.
            args.ProceedAddHandler();
        }

        /// <summary>
        /// Method invoked when the event to which the current aspect is applied is fired, <i>for each</i> delegate
        /// of this event, and <i>instead of</i> invoking this delegate.
        /// </summary>
        /// <param name="args">Handler arguments.</param>
        public override void OnInvokeHandler(EventInterceptionArgs args)
        {
            Console.WriteLine(string.Format("Event {0} invoked.", args.Event.Name));

            //If ProceedInvokeHandler is not called the handler that
            //was supposed to be invoked will never get invoked.
            args.ProceedInvokeHandler();
        }

        /// <summary>
        /// Method invoked <i>instead</i> of the <c>Remove</c> semantic of the event to which the current aspect is applied,
        /// i.e. when a delegate is removed from this event.
        /// </summary>
        /// <param name="args">Handler arguments.</param>
        public override void OnRemoveHandler(EventInterceptionArgs args)
        {
            Console.WriteLine(string.Format("Event {0} removed.", args.Event.Name));

            //If ProceedRemoveHandler is not called the handler that
            //was supposed to be removed will never get removed.
            args.ProceedRemoveHandler();
        } 
        #endregion
    }
}