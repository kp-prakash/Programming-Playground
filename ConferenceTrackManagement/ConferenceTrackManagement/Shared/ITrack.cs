using System.Collections.Generic;

namespace Shared
{
    /// <summary>
    /// Interface representing track of events on a day.
    /// </summary>
    public interface ITrack
    {
        /// <summary>
        /// Gets the morning session.
        /// </summary>
        /// <value>
        /// The morning session.
        /// </value>
        ISession MorningSession { get; }

        /// <summary>
        /// Gets the afternoon session.
        /// </summary>
        /// <value>
        /// The afternoon session.
        /// </value>
        ISession AfternoonSession { get; }

        /// <summary>
        /// Gets or sets the buffer duration.
        /// </summary>
        /// <value>
        /// The buffer duration.
        /// </value>
        int BufferDuration { get; set; }

        /// <summary>
        /// Schedules the talks for track.
        /// </summary>
        /// <param name="unscheduledTalks">The unscheduled talks.</param>
        bool ScheduleTrack(List<ITalk> unscheduledTalks);
    }
}
