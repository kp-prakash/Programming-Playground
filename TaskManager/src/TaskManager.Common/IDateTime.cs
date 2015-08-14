namespace TaskManager.Common
{
    using System;

    public interface IDateTime
    {
        DateTime UtcNow { get; }
    }
}