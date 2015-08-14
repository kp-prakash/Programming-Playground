using System;

namespace Shared
{
    /// <summary>
    /// Custom exception class.
    /// </summary>
    public class TrackManagementException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackManagementException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public TrackManagementException(string message):base(message)
        {
        }
    }
}