namespace TaskManager.Data.Exceptions
{
    using System;

    public class ChildObjectNotFoundException : Exception
    {
        public ChildObjectNotFoundException(string message)
            : base(message)
        {
        }
    }
}