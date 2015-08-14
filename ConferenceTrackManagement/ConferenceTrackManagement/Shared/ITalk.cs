using System;

namespace Shared
{
    /// <summary>
    /// Interface representing a talk.
    /// </summary>
    public interface ITalk
    {
        /// <summary>
        /// Gets or sets the title of the talk.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the start time of the talk.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the duration of the talk.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        int Duration { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether talk is scheduled.
        /// </summary>
        /// <value>
        /// <c>true</c> if talk is scheduled; otherwise, <c>false</c>.
        /// </value>
        bool IsScheduled { get; set; }

        /// <summary>
        /// Gets or sets the type of the event.
        /// </summary>
        /// <value>
        /// The type of the event.
        /// </value>
        EventTypes EventType { get; set; }
    }
}