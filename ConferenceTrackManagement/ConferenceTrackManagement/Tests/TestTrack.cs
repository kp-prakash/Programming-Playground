using System;
using System.Collections.Generic;
using System.Text;
using Model;
using NUnit.Framework;
using Shared;

namespace Tests
{
    /// <summary>
    /// Unit test cases for testing Track implementation.
    /// </summary>
    [TestFixture]
    public class TestTrack
    {
        /// <summary>
        /// Tests the schedule for track with valid talk duration.
        /// </summary>
        [Test]
        public void TestScheduleTrackWithValidTalkDuration()
        {
            ITrack track = new Track(DateTime.Now);
            ITalk talkA = Factory.Instance.GetNewTalk("Talk A", 60);
            ITalk talkB = Factory.Instance.GetNewTalk("Talk B", 60);
            ITalk talkC = Factory.Instance.GetNewTalk("Talk C", 60);
            ITalk talkD = Factory.Instance.GetNewTalk("Talk D", 60);
            ITalk talkE = Factory.Instance.GetNewTalk("Talk E", 60);
            ITalk talkF = Factory.Instance.GetNewTalk("Talk F", 60);
            var unscheduledTalks = new List<ITalk> { talkA, talkB, talkC, talkD, talkE, talkF };
            bool result = track.ScheduleTrack(unscheduledTalks);
            Assert.True(result && track.BufferDuration == 60
                        && unscheduledTalks.Count == 0
                        && track.MorningSession.RemainingDuration == 0
                        && track.AfternoonSession.RemainingDuration == 0);
        }

        /// <summary>
        /// Tests the schedule for track with invalid talk duration.
        /// </summary>
        [Test]
        public void TestScheduleTrackWithInvalidTalkDuration()
        {
            //The talks below don't form a subset.
            ITrack track = new Track(DateTime.Now);
            ITalk talkA = Factory.Instance.GetNewTalk("Talk A", 55);
            ITalk talkB = Factory.Instance.GetNewTalk("Talk B", 55);
            ITalk talkC = Factory.Instance.GetNewTalk("Talk C", 55);
            ITalk talkD = Factory.Instance.GetNewTalk("Talk D", 65);
            ITalk talkE = Factory.Instance.GetNewTalk("Talk E", 65);
            ITalk talkF = Factory.Instance.GetNewTalk("Talk F", 65);
            var unscheduledTalks = new List<ITalk> { talkA, talkB, talkC, talkD, talkE, talkF };
            bool result = track.ScheduleTrack(unscheduledTalks);
            Assert.True(!result && track.BufferDuration == 60
                        && unscheduledTalks.Count == 6
                        && track.MorningSession.RemainingDuration == 180
                        && track.AfternoonSession.RemainingDuration == 180);
        }

        /// <summary>
        /// Tests the string representation of a Track.
        /// </summary>
        [Test]
        public void TestTrackToString()
        {
            ITrack track = new Track(DateTime.Now);
            ITalk talkA = Factory.Instance.GetNewTalk("Talk A", 60);
            ITalk talkB = Factory.Instance.GetNewTalk("Talk B", 60);
            ITalk talkC = Factory.Instance.GetNewTalk("Talk C", 60);
            ITalk talkD = Factory.Instance.GetNewTalk("Talk D", 60);
            ITalk talkE = Factory.Instance.GetNewTalk("Talk E", 60);
            ITalk talkF = Factory.Instance.GetNewTalk("Talk F", 60);
            var unscheduledTalks = new List<ITalk> { talkA, talkB, talkC, talkD, talkE, talkF };
            bool result = track.ScheduleTrack(unscheduledTalks);
            Assert.True(track.ToString().Equals(new StringBuilder("09:00AM Talk A 60min").Append(Environment.NewLine)
                                                .Append("10:00AM Talk B 60min").Append(Environment.NewLine)
                                                .Append("11:00AM Talk C 60min").Append(Environment.NewLine)
                                                .Append("12:00PM Lunch").Append(Environment.NewLine)
                                                .Append("01:00PM Talk D 60min").Append(Environment.NewLine)
                                                .Append("02:00PM Talk E 60min").Append(Environment.NewLine)
                                                .Append("03:00PM Talk F 60min").Append(Environment.NewLine)
                                                .Append("04:00PM Networking Event").Append(Environment.NewLine).ToString()));
        }
    }
}