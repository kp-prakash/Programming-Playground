using System;
using System.Collections.Generic;

namespace Shared
{
    /// <summary>
    /// Interface representing a conference.
    /// </summary>
    public interface IConference
    {
        /// <summary>
        /// Gets or sets the unscheduled talks.
        /// </summary>
        /// <value>
        /// The unscheduled talks.
        /// </value>
        List<ITalk> UnscheduledTalks { get; set; }

        /// <summary>
        /// Gets or sets the tracks.
        /// </summary>
        /// <value>
        /// The tracks.
        /// </value>
        List<ITrack> Tracks { get; set; }

        /// <summary>
        /// Gets or sets the conference start date.
        /// </summary>
        /// <value>
        /// The conference start date.
        /// </value>
        DateTime Date { get; set; }

        /// <summary>
        /// Schedules the talks.
        /// </summary>
        void ScheduleTalks();
    }
}