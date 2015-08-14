using System.Collections.Generic;

namespace Shared
{
    /// <summary>
    /// Interface representing talk scheduler.
    /// </summary>
    public interface ITalkScheduler
    {
        /// <summary>
        /// Schedules the talks for the given duration.
        /// </summary>
        /// <param name="unscheduledTalks">The unscheduled talks.</param>
        /// <param name="duration">The duration.</param>
        /// <returns>List of scheduled talks.</returns>
        List<ITalk> ScheduleTalks(List<ITalk> unscheduledTalks, int duration);
    }
}
