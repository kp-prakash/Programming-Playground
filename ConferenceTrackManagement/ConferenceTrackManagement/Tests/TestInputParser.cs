using Model;
using NUnit.Framework;
using Shared;

namespace Tests
{
    /// <summary>
    /// Unit test cases for InputParser implementation.
    /// </summary>
    [TestFixture]
    public class TestInputParser
    {
        /// <summary>
        /// Tests the get talks method which parses input strings.
        /// </summary>
        [Test]
        public void TestGetTalks()
        {
            var talks = InputParser.Instance.GetTalks(TestData.Talks);
            //Verify talk count.
            Assert.IsTrue(talks.Count == 19);
            //Verify any talk instance randomly.
            Assert.True(talks[0].Title == "Writing Fast Tests Against Enterprise Rails"
                        && talks[0].Duration == 60
                        && talks[0].EventType == EventTypes.Talk);
        }
    }
}