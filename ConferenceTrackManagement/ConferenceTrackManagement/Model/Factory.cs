using System;
using System.Collections.Generic;
using Shared;
using Utils;

namespace Model
{
    /// <summary>
    /// Creates instances of various objects.
    /// <remarks>This could be replaced with a Dependency Injection Container in future.</remarks>
    /// </summary>
    public class Factory : IFactory
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="Factory" /> class from being created.
        /// </summary>
        private Factory()
        {
        }

        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        /// <value>
        /// The singleton instance.
        /// </value>
        public static IFactory Instance
        {
            get { return Singleton<Factory>.Instance; }
        }

        /// <summary>
        /// Returns an instance of conference.
        /// </summary>
        /// <param name="talks">The unscheduled talks.</param>
        /// <returns>Conference</returns>
        public IConference GetNewConference(List<ITalk> talks)
        {
            return GetNewConference(talks, DateTime.Today);
        }

        /// <summary>
        /// Returns an instance of conference.
        /// </summary>
        /// <param name="talks">The unscheduled talks.</param>
        /// <param name="date">The conference date.</param>
        /// <returns>Conference</returns>
        public IConference GetNewConference(List<ITalk> talks, DateTime date)
        {
            //Use only the date component and exclude time even if consumer send date with time.
            date = date.Date;
            return new Conference(talks, new List<ITrack>(), date);
        }

        /// <summary>
        /// Returns an instance of track.
        /// </summary>
        /// <param name="date">The track date.</param>
        /// <returns>Track</returns>
        public ITrack GetNewTrack(DateTime date)
        {
            //Use only the date component and exclude time even if consumer send date with time.
            date = date.Date;
            return new Track(date);
        }

        /// <summary>
        /// Returns an instance of session.
        /// </summary>
        /// <param name="sessionType">Type of the session.</param>
        /// <param name="date">The date of the sesssion.</param>
        /// <returns>Session</returns>
        public ISession GetNewSession(SessionTypes sessionType, DateTime date)
        {
            //Use only the date component and exclude time even if consumer send date with time.
            date = date.Date;
            return new Session(sessionType, new List<ITalk>(), date);
        }

        /// <summary>
        /// Returns an instance of talk.
        /// </summary>
        /// <param name="title">The title for the talk.</param>
        /// <param name="duration">The duration of the talk.</param>
        /// <returns>Talk</returns>
        public ITalk GetNewTalk(string title, int duration)
        {
            return new Talk
                       {
                           Title = title.FilterNumbers(),
                           EventType = EventTypes.Talk,
                           Duration = duration
                       };
        }
        
        /// <summary>
        /// Returns an instance of talk representing lunch.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>Talk</returns>
        public ITalk GetNewLunch(DateTime date)
        {
            //Use only the date component and exclude time even if consumer send date with time.
            date = date.Date;
            DateTime lunchTime = date.AddHours(12);//Lunch @ 12:00 PM
            return new Talk
                       {
                           Title = Titles.Lunch,
                           EventType = EventTypes.Lunch,
                           StartTime = lunchTime,
                           IsScheduled = true
                       };
        }

        /// <summary>
        /// Returns an instance of talk representing networking event.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>Talk</returns>
        public ITalk GetNewNetworkingEvent(DateTime date)
        {
            //Use only the date component and exclude time even if consumer send date with time.
            date = date.Date;
            return new Talk
                       {
                           Title = Titles.NetworkingEvent,
                           EventType = EventTypes.Networking,
                           StartTime = date.AddHours(16), //Networking event cannot start before 04:00 PM.
                           IsScheduled = true
                       };
        }
    }
}