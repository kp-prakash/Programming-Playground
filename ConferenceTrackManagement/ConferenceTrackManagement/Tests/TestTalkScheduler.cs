using System.Collections.Generic;
using Model;
using NUnit.Framework;
using Shared;

namespace Tests
{
    /// <summary>
    /// Unit test cases for TalkScheduler implementation.
    /// </summary>
    [TestFixture]
    public class TestTalkScheduler
    {
        /// <summary>
        /// Tests the scheduler using talks with valid data.
        /// </summary>
        [Test]
        public void TestScheduleTalksWithValidData()
        {
            ITalk talkA = Factory.Instance.GetNewTalk("Talk A", 60);
            ITalk talkB = Factory.Instance.GetNewTalk("Talk B", 60);
            ITalk talkC = Factory.Instance.GetNewTalk("Talk C", 60);
            var unscheduledTalks = new List<ITalk> { talkA, talkB, talkC };
            List<ITalk> scheduledTalks = TalkScheduler.Instance.ScheduleTalks(unscheduledTalks, 180);
            Assert.True(null != scheduledTalks
                        && scheduledTalks.Count == 3);
        }

        /// <summary>
        /// Tests the scheduler using talks with invalid data.
        /// </summary>
        [Test]
        public void TestScheduleTalksWithInvalidData()
        {
            ITalk talkA = Factory.Instance.GetNewTalk("Talk A", 90);
            ITalk talkB = Factory.Instance.GetNewTalk("Talk B", 50);
            ITalk talkC = Factory.Instance.GetNewTalk("Talk C", 60);
            var unscheduledTalks = new List<ITalk> { talkA, talkB, talkC };
            List<ITalk> scheduledTalks = TalkScheduler.Instance.ScheduleTalks(unscheduledTalks, 180);
            Assert.True(null == scheduledTalks);
        }
    }
}