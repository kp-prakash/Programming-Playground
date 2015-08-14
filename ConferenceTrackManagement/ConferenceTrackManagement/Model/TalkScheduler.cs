using System.Collections.Generic;
using System.Linq;
using Shared;
using Utils;

namespace Model
{
    /// <summary>
    /// Implementation to schedule talks based on 'sum of subsets' algorithm.
    /// Returns first subset of talks that matches the given duration.
    /// </summary>
    public sealed class TalkScheduler : ITalkScheduler
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="TalkScheduler" /> class from being created.
        /// </summary>
        private TalkScheduler()
        {
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static ITalkScheduler Instance
        {
            get { return Singleton<TalkScheduler>.Instance; }
        }

        private List<ITalk> _result;

        /// <summary>
        /// Schedules the talks for the given duration.
        /// </summary>
        /// <param name="unscheduledTalks">The unscheduled talks.</param>
        /// <param name="duration">The duration.</param>
        /// <returns>
        /// List of scheduled talks.
        /// </returns>
        public List<ITalk> ScheduleTalks(List<ITalk> unscheduledTalks, int duration)
        {
            _result = null; //If a subset is found this will not be null.
            const int sum = 0;
            const int startIndex = 0;
            var talks = unscheduledTalks.OrderBy(talk => talk.Duration).ToList();
            int remainder = talks.Sum(talk => talk.Duration);
            ScheduleTalks(talks, duration, sum, startIndex, remainder);
            return _result;
        }

        /// <summary>
        /// Schedules the talks using backtracking.
        /// </summary>
        /// <param name="talks">The talks.</param>
        /// <param name="totalDuration">The total duration.</param>
        /// <param name="sum">The sum.</param>
        /// <param name="index">The index.</param>
        /// <param name="remainder">The remainder.</param>
        /// <returns></returns>
        private bool ScheduleTalks(List<ITalk> talks, int totalDuration, int sum, int index, int remainder)
        {
            bool found = false;
            talks[index].IsScheduled = true;
            if (sum + talks[index].Duration == totalDuration)
            {
                PopulateResult(talks, index);
                return true;
            }
            else if (sum + talks[index].Duration + talks[index + 1].Duration <= totalDuration)
            {
                found = ScheduleTalks(talks, totalDuration, sum + talks[index].Duration, index + 1,
                                      remainder - talks[index].Duration);
            }
            if (found) return true;
            if ((sum + remainder - talks[index].Duration >= totalDuration)
                && (sum + talks[index + 1].Duration <= totalDuration))
            {
                talks[index].IsScheduled = false;
                found = ScheduleTalks(talks, totalDuration, sum, index + 1, remainder - talks[index].Duration);
            }
            return false;
        }

        /// <summary>
        /// Populates the result after finding the first subset.
        /// </summary>
        /// <param name="talks">The talks.</param>
        /// <param name="index">The index.</param>
        private void PopulateResult(List<ITalk> talks, int index)
        {
            _result = new List<ITalk>();
            for (int i = 0; i <= index; i++)
            {
                if (talks[i].IsScheduled)
                    _result.Add(talks[i]);
            }
        }
    }
}