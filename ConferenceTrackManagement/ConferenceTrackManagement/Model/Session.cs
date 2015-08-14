using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shared;

namespace Model
{
    /// <summary>
    /// Represents a session.
    /// </summary>
    public class Session : ISession
    {
        /// <summary>
        /// Session remaining duration is 180 mins 
        /// i.e., 3 Hours 09:00AM - 12:00PM and 01:00PM - 04:00PM
        /// </summary>
        private int _remainingDuration = 180;

        /// <summary>
        /// Gets the talks scheduled for this session.
        /// </summary>
        /// <value>
        /// The talks.
        /// </value>
        public List<ITalk> Talks { get; private set; }

        /// <summary>
        /// Gets the type of the session.
        /// </summary>
        /// <value>
        /// The type of the session.
        /// </value>
        public SessionTypes SessionType { get; private set; }

        /// <summary>
        /// Gets or sets the next talk's start time.
        /// </summary>
        /// <value>
        /// The next talk's start time.
        /// </value>
        public DateTime NextTalkStartTime { get; set; }

        /// <summary>
        /// Gets or sets the buffer duration of this session.
        /// </summary>
        /// <value>
        /// The buffer duration of this session.
        /// </value>
        public int BufferDuration { get; set; }

        /// <summary>
        /// Gets or sets the remaining duration of this session.
        /// </summary>
        /// <value>
        /// The remaining duration of this session.
        /// </value>
        public int RemainingDuration
        {
            get { return _remainingDuration; }
            set { _remainingDuration = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Session" /> class.
        /// </summary>
        /// <param name="sessionType">Type of the session.</param>
        /// <param name="talks">The talks.</param>
        /// <param name="date">The date.</param>
        public Session(SessionTypes sessionType, List<ITalk> talks, DateTime date)
        {
            //Use only date component and excluded time.
            date = date.Date;
            SessionType = sessionType;
            NextTalkStartTime = date;
            NextTalkStartTime = (sessionType == SessionTypes.Morning)
                             ? NextTalkStartTime.AddHours(9) //Morning session starts @ 09:00 AM
                             : NextTalkStartTime.AddHours(13); //Afternoon session starts @ 01:00 PM
            BufferDuration = (sessionType == SessionTypes.Morning) ? 0 : Constants.DefaultBufferTime;
            Talks = talks;
        }

        /// <summary>
        /// Schedules the unscheduled talks.
        /// </summary>
        /// <param name="unscheduledTalks">The unscheduled talks.</param>
        public bool ScheduleTalks(List<ITalk> unscheduledTalks)
        {
            while (RemainingDuration > 0)
            {
                if (unscheduledTalks.Count == 0)
                    break;
                /* To handle the case when talks remain after utilizing the buffers. Just send 
                 * in the unscheduled talks total time if it is less than remaining duration.*/
                int remainingUnscheduledTime = unscheduledTalks.Sum(talk => talk.Duration);
                int remainingDuration = Math.Min(remainingUnscheduledTime, RemainingDuration);
                var scheduledTalks = TalkScheduler.Instance.ScheduleTalks(unscheduledTalks, remainingDuration);
                if (scheduledTalks == null) return false;//Condition met when there is no subsets with exact sum.
                AddTalks(scheduledTalks, unscheduledTalks);
            }
            return true;
        }

        /// <summary>
        /// Adds the talks to this session.
        /// </summary>
        /// <param name="scheduledTalks">The scheduled talks.</param>
        /// <param name="unscheduledTalks">The unscheduled talks.</param>
        public void AddTalks(List<ITalk> scheduledTalks, List<ITalk> unscheduledTalks)
        {
            if (null != scheduledTalks && scheduledTalks.Count != 0)
            {
                foreach (ITalk scheduledTalk in scheduledTalks)
                {
                    /* This method has more than one responsibility on purpose.
                     * Just remove the talk from unscheduled talks once it is scheduled.*/
                    AddTalk(scheduledTalk);
                    if (unscheduledTalks.Contains(scheduledTalk))
                        unscheduledTalks.Remove(scheduledTalk);
                }
            }
        }

        /// <summary>
        /// Adds the talk to this session.
        /// </summary>
        /// <param name="talk">The talk.</param>
        /// <exception cref="Shared.TrackManagementException">Unable to add talk to session.</exception>
        private void AddTalk(ITalk talk)
        {
            if (RemainingDuration <= 0 && BufferDuration <= 0)
                throw new TrackManagementException(Resource.UnableToAddTalk);
            SetTalkStartTime(talk);
            UpdateRemainingDuration(talk);
            Talks.Add(talk);
            UpdateNextSessionStartTime(talk.Duration);
        }

        /// <summary>
        /// Sets the talk's start time.
        /// </summary>
        /// <param name="talk">The talk.</param>
        private void SetTalkStartTime(ITalk talk)
        {
            talk.StartTime = NextTalkStartTime;
            talk.IsScheduled = true;
        }

        /// <summary>
        /// Updates the remaining duration.
        /// </summary>
        /// <param name="talk">The talk.</param>
        private void UpdateRemainingDuration(ITalk talk)
        {
            if (RemainingDuration > 0)
                RemainingDuration -= talk.Duration;
            else
                BufferDuration -= talk.Duration;
        }

        /// <summary>
        /// Updates the next session start time.
        /// </summary>
        /// <param name="duration">The duration.</param>
        private void UpdateNextSessionStartTime(int duration)
        {
            NextTalkStartTime = NextTalkStartTime.AddMinutes(duration);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (null == Talks || Talks.Count == 0) return string.Empty;
            var stringBuilder = new StringBuilder();
            foreach (ITalk talk in Talks)
            {
                if (null == talk) continue;
                stringBuilder.Append(talk.ToString()).Append(Environment.NewLine);
            }
            return stringBuilder.ToString();
        }
    }
}