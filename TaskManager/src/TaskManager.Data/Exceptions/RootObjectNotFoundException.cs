namespace TaskManager.Data.Exceptions
{
    using System;

    [Serializable]
    public class RootObjectNotFoundException : Exception
    {
        public RootObjectNotFoundException(string message)
            : base(message)
        {
        }
    }
}