using System;
using System.Collections.Generic;
using Model;
using NUnit.Framework;
using Shared;

namespace Tests
{
    /// <summary>
    /// Unit test cases for Conference implementation.
    /// </summary>
    [TestFixture]
    public class TestConference
    {
        /// <summary>
        /// Tests the conference constructor.
        /// </summary>
        [Test]
        public void TestConferenceConstructor()
        {
            List<ITalk> talks = GetTalks(TestData.Talks);
            IConference conference = Factory.Instance.GetNewConference(talks);
            Assert.True(conference.UnscheduledTalks.Count == talks.Count
                        && conference.Date == DateTime.Today);
        }

        /// <summary>
        /// Tests the ScheduleTalks.
        /// </summary>
        [Test]
        public void TestScheduleTalks()
        {
            List<ITalk> talks = GetTalks(TestData.Talks);
            IConference conference = Factory.Instance.GetNewConference(talks);
            conference.ScheduleTalks();
            //UnscheduledTalks will be empty after all the talks are scheduled.
            Assert.True(conference.UnscheduledTalks.Count == 0);
        }

        /// <summary>
        /// Tests the scheduler with a set having no subset sum.
        /// </summary>
        [Test]
        public void TestScheduleTalksFailure()
        {
            List<ITalk> talks = GetTalks(TestData.TalksWithInvalidSum);
            IConference conference = Factory.Instance.GetNewConference(talks);
            conference.ScheduleTalks();
            //UnscheduledTalks will be empty after all the talks are scheduled.
            Assert.True(conference.UnscheduledTalks.Count == 4);
        }

        /// <summary>
        /// Tests the scheduler with set of talks with balance talks 
        /// after scheduling possible ones.
        /// </summary>
        [Test]
        public void TestScheduleTalksWithBalanceTalks()
        {
            List<ITalk> talks = GetTalks(TestData.TalksWithBalanceTalks);
            IConference conference = Factory.Instance.GetNewConference(talks);
            conference.ScheduleTalks();
            Assert.True(conference.UnscheduledTalks.Count == 4);
        }

        /// <summary>
        /// Tests the scheduler with talks to test buffer duration allocation.
        /// </summary>
        [Test]
        public void TestScheduleTalksWithTalksToTestBufferAllocation()
        {
            List<ITalk> talks = GetTalks(TestData.TalksToTestBufferAllocation);
            IConference conference = Factory.Instance.GetNewConference(talks);
            conference.ScheduleTalks();
            Assert.True(conference.UnscheduledTalks.Count == 0);
        }

        /// <summary>
        /// Parses input and returns set of talk.
        /// </summary>
        /// <param name="input">The input strings having talk details.</param>
        /// <returns></returns>
        private List<ITalk> GetTalks(List<string> input)
        {
            List<ITalk> talks = InputParser.Instance.GetTalks(input);
            return talks;
        }
    }
}