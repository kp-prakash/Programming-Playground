using System;
using System.Collections.Generic;
using Model;
using NUnit.Framework;
using Shared;
using System.Text;

namespace Tests
{
    /// <summary>
    /// Unit test cases for Session implementation.
    /// </summary>
    [TestFixture]
    public class TestSession
    {
        /// <summary>
        /// Tests the scheduler using talks with matching subset duration.
        /// </summary>
        [Test]
        public void TestScheduleTalksWithMatchingDuration()
        {
            ISession session1 = Factory.Instance.GetNewSession(SessionTypes.Morning, DateTime.Now);
            ITalk talkA = Factory.Instance.GetNewTalk("Talk A", 60);
            ITalk talkB = Factory.Instance.GetNewTalk("Talk B", 60);
            ITalk talkC = Factory.Instance.GetNewTalk("Talk C", 60);
            var unscheduledTalks = new List<ITalk> { talkA, talkB, talkC };
            session1.ScheduleTalks(unscheduledTalks);
            Assert.True(session1.RemainingDuration == 0
                        && session1.Talks.Count == 3
                        && unscheduledTalks.Count == 0);
        }

        /// <summary>
        /// Tests the scheduler using talks with lesser duration.
        /// </summary>
        [Test]
        public void TestScheduleTalksWithLesserDuration()
        {
            ISession session2 = Factory.Instance.GetNewSession(SessionTypes.Morning, DateTime.Now);
            ITalk talkA = Factory.Instance.GetNewTalk("Talk A", 60);
            ITalk talkB = Factory.Instance.GetNewTalk("Talk B", 60);
            var unscheduledTalks = new List<ITalk> { talkA, talkB };
            session2.ScheduleTalks(unscheduledTalks);
            Assert.True(session2.RemainingDuration == 60
                        && session2.Talks.Count == 2
                        && unscheduledTalks.Count == 0);
        }

        /// <summary>
        /// Tests the scheduler using talks with extra duration.
        /// </summary>
        [Test]
        public void TestScheduleTalksWithExtraDuration()
        {
            ISession session3 = Factory.Instance.GetNewSession(SessionTypes.Morning, DateTime.Now);
            ITalk talkA = Factory.Instance.GetNewTalk("Talk A", 60);
            ITalk talkB = Factory.Instance.GetNewTalk("Talk B", 60);
            ITalk talkC = Factory.Instance.GetNewTalk("Talk C", 60);
            ITalk talkD = Factory.Instance.GetNewTalk("Talk D", 50);
            var unscheduledTalks = new List<ITalk> { talkA, talkB, talkC, talkD };
            session3.ScheduleTalks(unscheduledTalks);
            Assert.True(session3.RemainingDuration == 0
                        && session3.Talks.Count == 3
                        && unscheduledTalks.Count == 1);
        }

        /// <summary>
        /// Tests the scheduler using talks with no matching subset duration.
        /// </summary>
        [Test]
        public void TestScheduleTalksWithNoSubset()
        {
            ISession session4 = Factory.Instance.GetNewSession(SessionTypes.Morning, DateTime.Now);
            ITalk talkA = Factory.Instance.GetNewTalk("Talk A", 60);
            ITalk talkB = Factory.Instance.GetNewTalk("Talk B", 60);
            ITalk talkC = Factory.Instance.GetNewTalk("Talk C", 90);
            ITalk talkD = Factory.Instance.GetNewTalk("Talk D", 50);
            var unscheduledTalks = new List<ITalk> { talkA, talkB, talkC, talkD };
            session4.ScheduleTalks(unscheduledTalks);
            Assert.True(session4.RemainingDuration == 180
                        && session4.Talks.Count == 0
                        && unscheduledTalks.Count == 4);
        }

        /// <summary>
        /// Tests the addition of talks to session.
        /// </summary>
        [Test]
        public void TestAddTalks()
        {
            ISession session5 = Factory.Instance.GetNewSession(SessionTypes.Afternoon, DateTime.Now);
            ITalk talkA = Factory.Instance.GetNewTalk("Talk A", 60);
            ITalk talkB = Factory.Instance.GetNewTalk("Talk B", 60);
            ITalk talkC = Factory.Instance.GetNewTalk("Talk C", 60);
            var unscheduledTalks = new List<ITalk> { talkA, talkB, talkC };
            var scheduledTalks = new List<ITalk> { talkA, talkB, talkC };
            session5.AddTalks(scheduledTalks, unscheduledTalks);
            Assert.True(session5.RemainingDuration == 0
                        && session5.Talks[1].StartTime == DateTime.Today.AddHours(14) //start time check
                        && session5.Talks.Count == scheduledTalks.Count
                        && unscheduledTalks.Count == 0);
        }

        /// <summary>
        /// Tests string representation of a session.
        /// </summary>
        [Test]
        public void TestToString()
        {
            ISession session6 = Factory.Instance.GetNewSession(SessionTypes.Morning, DateTime.Now);
            ITalk talkA = Factory.Instance.GetNewTalk("Talk A", 60);
            ITalk talkB = Factory.Instance.GetNewTalk("Talk B", 60);
            ITalk talkC = Factory.Instance.GetNewTalk("Talk C", 60);
            var unscheduledTalks = new List<ITalk> { talkA, talkB, talkC };
            session6.ScheduleTalks(unscheduledTalks);
            Assert.True(session6.ToString().Equals(new StringBuilder("09:00AM Talk A 60min").Append(Environment.NewLine)
                                                       .Append("10:00AM Talk B 60min").Append(Environment.NewLine)
                                                       .Append("11:00AM Talk C 60min").Append(Environment.NewLine)
                                                       .ToString()));
        }
    }
}
