using System;
using System.Collections.Generic;
using Model;
using NUnit.Framework;
using Shared;

namespace Tests
{
    /// <summary>
    /// Unit test case for Factory implementation.
    /// </summary>
    [TestFixture]
    public class TestFactory
    {
        /// <summary>
        /// Tests the get new conference with date.
        /// </summary>
        [Test]
        public void TestGetNewConferenceWithDate()
        {
            List<ITalk> talks = InputParser.Instance.GetTalks(TestData.Talks);
            IConference conference = Factory.Instance.GetNewConference(talks, DateTime.Today);
            Assert.True(null != conference);
        }

        /// <summary>
        /// Tests the get new conference.
        /// </summary>
        [Test]
        public void TestGetNewConference()
        {
            List<ITalk> talks = InputParser.Instance.GetTalks(TestData.Talks);
            IConference conference = Factory.Instance.GetNewConference(talks);
            Assert.True(null != conference && conference.Date == DateTime.Today);
        }

        /// <summary>
        /// Tests the get new track.
        /// </summary>
        [Test]
        public void TestGetNewTrack()
        {
            ITrack track = Factory.Instance.GetNewTrack(DateTime.Today);
            Assert.True(null != track);
        }

        /// <summary>
        /// Tests the get new session.
        /// </summary>
        [Test]
        public void TestGetNewSession()
        {
            ISession morningSession = Factory.Instance.GetNewSession(SessionTypes.Morning, DateTime.Today);
            bool morningSessionConditions = (null != morningSession
                                             && morningSession.SessionType == SessionTypes.Morning
                                             && null != morningSession.Talks
                                             && morningSession.RemainingDuration == 180
                                             && morningSession.BufferDuration == 0);
            ISession afternoonSession = Factory.Instance.GetNewSession(SessionTypes.Afternoon, DateTime.Today);
            bool afternoonSessionConditions = (null != afternoonSession
                                               && afternoonSession.SessionType == SessionTypes.Afternoon
                                               && null != afternoonSession.Talks
                                               && afternoonSession.RemainingDuration == 180
                                               && afternoonSession.BufferDuration == 60);
            Assert.True(morningSessionConditions && afternoonSessionConditions);
        }

        /// <summary>
        /// Tests the get new talk.
        /// </summary>
        [Test]
        public void TestGetNewTalk()
        {
            ITalk talk = Factory.Instance.GetNewTalk("TEST", 60);
            Assert.True(null != talk && talk.Duration == 60
                        && talk.Title == "TEST"
                        && talk.EventType == EventTypes.Talk
                        && !talk.IsScheduled);
        }

        /// <summary>
        /// Tests the get new lunch.
        /// </summary>
        [Test]
        public void TestGetNewLunch()
        {
            ITalk talk = Factory.Instance.GetNewLunch(DateTime.Today);
            Assert.True(null != talk
                        && talk.StartTime == DateTime.Today.AddHours(12)
                        && talk.Title == Titles.Lunch
                        && talk.EventType == EventTypes.Lunch
                        && talk.IsScheduled);
        }

        /// <summary>
        /// Tests the get new networking event.
        /// </summary>
        [Test]
        public void TestGetNewNetworkingEvent()
        {
            ITalk talk = Factory.Instance.GetNewNetworkingEvent(DateTime.Today);
            Assert.True(null != talk
                        && talk.StartTime == DateTime.Today.AddHours(16)
                        && talk.Title == Titles.NetworkingEvent
                        && talk.EventType == EventTypes.Networking
                        && talk.IsScheduled);
        }
    }
}