using System;
using System.Collections.Generic;

namespace Shared
{
    /// <summary>
    /// Interface representing a session.
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// Gets the talks scheduled for this session.
        /// </summary>
        /// <value>
        /// The talks.
        /// </value>
        List<ITalk> Talks { get; }

        /// <summary>
        /// Gets the type of the session.
        /// </summary>
        /// <value>
        /// The type of the session.
        /// </value>
        SessionTypes SessionType { get; }

        /// <summary>
        /// Gets or sets the remaining duration of this session.
        /// </summary>
        /// <value>
        /// The remaining duration of this session.
        /// </value>
        int RemainingDuration { get; set; }

        /// <summary>
        /// Gets or sets the buffer duration of this session.
        /// </summary>
        /// <value>
        /// The buffer duration of this session.
        /// </value>
        int BufferDuration { get; set; }

        /// <summary>
        /// Gets or sets the next talk's start time.
        /// </summary>
        /// <value>
        /// The next talk's start time.
        /// </value>
        DateTime NextTalkStartTime { get; set; }

        /// <summary>
        /// Adds the talks to this session.
        /// </summary>
        /// <param name="scheduledTalks">The scheduled talks.</param>
        /// <param name="unscheduledTalks">The unscheduled talks.</param>
        void AddTalks(List<ITalk> scheduledTalks, List<ITalk> unscheduledTalks);

        /// <summary>
        /// Schedules the unscheduled talks.
        /// </summary>
        /// <param name="unscheduledTalks">The unscheduled talks.</param>
        bool ScheduleTalks(List<ITalk> unscheduledTalks);
    }
}
