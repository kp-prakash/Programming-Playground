using System;
using Shared;

namespace Model
{
    /// <summary>
    /// Reperesents a talk by a speaker
    /// </summary>
    public class Talk : ITalk
    {
        /// <summary>
        /// Gets or sets the title of the talk.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the start time of the talk.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the duration of the talk.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether talk is scheduled.
        /// </summary>
        /// <value>
        /// <c>true</c> if talk is scheduled; otherwise, <c>false</c>.
        /// </value>
        public bool IsScheduled { get; set; }

        /// <summary>
        /// Gets or sets the type of the event.
        /// </summary>
        /// <value>
        /// The type of the event.
        /// </value>
        public EventTypes EventType { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string duration = (EventType == EventTypes.Lunch || EventType == EventTypes.Networking)
                                  ? string.Empty
                                  : (Duration == Constants.LightningTalkDuration)
                                        ? " " + Titles.Lightning
                                        : string.Format(Constants.TalkDurationFormat, Duration);
            return StartTime.ToString(Constants.TalkStartTimeFormat) + Title + duration;
        }
    }
}