using System;
using System.Collections.Generic;
using Model;
using Shared;

namespace UserInterface
{
    /// <summary>
    /// Console application.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Program entry point.
        /// </summary>
        static void Main()
        {
            Console.WriteLine(Resource.ApplicationTitle);
            Console.WriteLine(Resource.Underline);
            Console.WriteLine();
            ScheduleConference(TestData.Talks);
            ScheduleConference(TestData.TalksWithInvalidSum);
            ScheduleConference(TestData.TalksWithBalanceTalks);
            ScheduleConference(TestData.TalksToTestBufferAllocation);
            Console.ReadLine();
        }

        /// <summary>
        /// Schedules the conference.
        /// </summary>
        /// <param name="testData">The test data.</param>
        private static void ScheduleConference(List<string> testData)
        {
            Console.WriteLine(Resource.Border);
            PrintInput(testData);
            Console.WriteLine(Resource.Boundary);
            PrintOutput(testData);
            Console.WriteLine(Resource.Border);
        }

        /// <summary>
        /// Prints the input.
        /// </summary>
        /// <param name="testData">The test data.</param>
        private static void PrintInput(IEnumerable<string> testData)
        {
            Console.WriteLine(Resource.InputTitle + Environment.NewLine);
            foreach (string unscheduledTalk in testData)
            {
                Console.WriteLine(unscheduledTalk);
            }
            Console.WriteLine(Environment.NewLine);
        }

        /// <summary>
        /// Prints the output.
        /// </summary>
        /// <param name="testData">The test data.</param>
        private static void PrintOutput(List<string> testData)
        {
            Console.WriteLine(Resource.OutputTitle + Environment.NewLine);
            var talks = InputParser.Instance.GetTalks(testData);
            IConference conference = Factory.Instance.GetNewConference(talks, DateTime.Today);
            conference.ScheduleTalks();
            Console.WriteLine();
            Console.Write(conference.ToString());
        }
    }
}
