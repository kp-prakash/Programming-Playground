using System;
using System.Collections.Generic;
using System.Text;
using Shared;

namespace Model
{
    /// <summary>
    /// Represents the track of events on a day.
    /// </summary>
    public class Track : ITrack
    {
        /// <summary>
        /// The morning session.
        /// </summary>
        private readonly ISession _morningSession;
        
        /// <summary>
        /// The afternoon session.
        /// </summary>
        private readonly ISession _afternoonSession;
        
        /// <summary>
        /// The lunch.
        /// </summary>
        private readonly ITalk _lunch;
        
        /// <summary>
        /// The networking event.
        /// </summary>
        private readonly ITalk _networkingEvent;

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets the morning session.
        /// </summary>
        /// <value>
        /// The morning session.
        /// </value>
        public ISession MorningSession
        {
            get { return _morningSession; }
        }

        /// <summary>
        /// Gets the afternoon session.
        /// </summary>
        /// <value>
        /// The afternoon session.
        /// </value>
        public ISession AfternoonSession
        {
            get { return _afternoonSession; }
        }

        /// <summary>
        /// Gets or sets the buffer duration.
        /// </summary>
        /// <value>
        /// The buffer duration.
        /// </value>
        public int BufferDuration
        {
            get { return _afternoonSession.BufferDuration; }
            set { _afternoonSession.BufferDuration = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Track" /> class.
        /// </summary>
        /// <param name="date">The date.</param>
        public Track(DateTime date)
        {
            //Use only date component and excluded time.
            date = date.Date;
            Date = date;
            _morningSession = Factory.Instance.GetNewSession(SessionTypes.Morning, date);
            _afternoonSession = Factory.Instance.GetNewSession(SessionTypes.Afternoon, date);
            _lunch = Factory.Instance.GetNewLunch(Date);
            _networkingEvent = Factory.Instance.GetNewNetworkingEvent(date);
        }

        /// <summary>
        /// Schedules the talks for track.
        /// </summary>
        /// <param name="unscheduledTalks">The unscheduled talks.</param>
        public bool ScheduleTrack(List<ITalk> unscheduledTalks)
        {
            bool morningResult = MorningSession.ScheduleTalks(unscheduledTalks);
            if (unscheduledTalks.Count == 0)
                return morningResult;//No need to proceed if there are no talks.
            bool afterNoonResult = AfternoonSession.ScheduleTalks(unscheduledTalks);
            return morningResult && afterNoonResult;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (null != _networkingEvent && null != AfternoonSession)
            {
                //By default networking event start at 04:00PM, start at 05:00PM
                //if there are any events scheduled on / after 04:00:
                _networkingEvent.StartTime = AfternoonSession.BufferDuration == Constants.DefaultBufferTime
                                                 ? Date.AddHours(16)
                                                 : Date.AddHours(17);
            }
            var stringBuilder = new StringBuilder();
            if (null != MorningSession)
                stringBuilder.Append(MorningSession.ToString());
            if (null == AfternoonSession || null == AfternoonSession.Talks
                || AfternoonSession.Talks.Count == 0)
                return stringBuilder.ToString(); //Return when there is no afternoon session / talks.
            if (null != _lunch)
                stringBuilder.Append(_lunch.ToString()).Append(Environment.NewLine);
            if (null != AfternoonSession)
                stringBuilder.Append(AfternoonSession.ToString());
            if (null != _networkingEvent)
                stringBuilder.Append(_networkingEvent.ToString()).Append(Environment.NewLine);
            return stringBuilder.ToString();
        }
    }
}