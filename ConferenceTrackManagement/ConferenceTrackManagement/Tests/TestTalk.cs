using System;
using Model;
using NUnit.Framework;
using Shared;

namespace Tests
{
    /// <summary>
    /// Unit test cases for Talk implementation.
    /// </summary>
    [TestFixture]
    public class TestTalk
    {
        [Test]
        public void TestTalkTitle()
        {
            ITalk talk = Factory.Instance.GetNewTalk("123456Test7890", 60);
            Assert.True(talk.Title.Equals("Test"));
        }

        /// <summary>
        /// Tests string representation of a talk.
        /// </summary>
        [Test]
        public void TestTalkToString()
        {
            ITalk talk = Factory.Instance.GetNewTalk("Test", 60);
            talk.StartTime = DateTime.Today.AddHours(9);
            //Passing in DateTime.Now tests for filtering date component during objcet creation.
            ITalk lunch = Factory.Instance.GetNewLunch(DateTime.Now);
            ITalk networkingEvent = Factory.Instance.GetNewNetworkingEvent(DateTime.Now);
            Assert.True(talk.ToString().Equals("09:00AM Test 60min")
                && lunch.ToString().Equals("12:00PM Lunch")
                && networkingEvent.ToString().Equals("04:00PM Networking Event"));
        }
    }
}
