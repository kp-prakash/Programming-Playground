using System;
using System.Collections.Generic;

namespace Shared
{
    /// <summary>
    /// Interface representing factory.
    /// </summary>
    public interface IFactory
    {
        /// <summary>
        /// Returns an instance of conference.
        /// </summary>
        /// <param name="talks">The unscheduled talks.</param>
        /// <returns>Conference</returns>
        IConference GetNewConference(List<ITalk> talks);

        /// <summary>
        /// Returns an instance of conference.
        /// </summary>
        /// <param name="talks">The unscheduled talks.</param>
        /// <param name="date">The conference date.</param>
        /// <returns>Conference</returns>
        IConference GetNewConference(List<ITalk> talks, DateTime date);

        /// <summary>
        /// Returns an instance of track.
        /// </summary>
        /// <param name="date">The track date.</param>
        /// <returns>Track</returns>
        ITrack GetNewTrack(DateTime date);

        /// <summary>
        /// Returns an instance of session.
        /// </summary>
        /// <param name="sessionType">Type of the session.</param>
        /// <param name="date">The date of the sesssion.</param>
        /// <returns>Session</returns>
        ISession GetNewSession(SessionTypes sessionType, DateTime date);

        /// <summary>
        /// Returns an instance of talk.
        /// </summary>
        /// <param name="title">The title for the talk.</param>
        /// <param name="duration">The duration of the talk.</param>
        /// <returns>Talk</returns>
        ITalk GetNewTalk(string title, int duration);

        /// <summary>
        /// Returns an instance of talk representing lunch.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>Talk</returns>
        ITalk GetNewLunch(DateTime date);

        /// <summary>
        /// Returns an instance of talk representing networking event.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>Talk</returns>
        ITalk GetNewNetworkingEvent(DateTime date);
    }
}