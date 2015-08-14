using System;
using System.Collections.Generic;
using Shared;
using Utils;

namespace Model
{
    /// <summary>
    /// Parses the string input and creates instances of <see cref="Talk" /> class.
    /// </summary>
    public class InputParser : IInputParser
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="InputParser" /> class from being created.
        /// </summary>
        private InputParser()
        {
        }

        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        /// <value>
        /// The singleton instance.
        /// </value>
        public static IInputParser Instance
        {
            get { return Singleton<InputParser>.Instance; }
        }

        /// <summary>
        /// Passes the input strings and creates the list of talks to be scheduled.
        /// </summary>
        /// <param name="inputs">The conference talks as strings.</param>
        /// <returns> List of unscheduled talks. </returns>
        public List<ITalk> GetTalks(List<string> inputs)
        {
            var talks = new List<ITalk>();
            if (null == inputs || inputs.Count == 0)
            {
                return talks;
            }
            foreach (string entry in inputs)
            {
                var inputEntry = entry.Trim();
                int indexOf = inputEntry.LastIndexOf(Constants.Space, StringComparison.Ordinal);
                string title = inputEntry.Substring(0, indexOf);
                string talkDuration = inputEntry.Substring(indexOf + 1).ToLower();
                int duration = GetDuration(talkDuration);
                ITalk talk = Factory.Instance.GetNewTalk(title, duration);
                talks.Add(talk);
            }
            return talks;
        }

        /// <summary>
        /// Converts the string representation of the talk duration to integer.
        /// </summary>
        /// <param name="talkDuration">Duration of the talk as string.</param>
        /// <returns>Duration.</returns>
        private static int GetDuration(string talkDuration)
        {
            int duration;
            switch (talkDuration)
            {
                case Titles.Lightning:
                    duration = 5;
                    break;
                default:
                    int.TryParse(talkDuration.Replace("min", string.Empty), out duration);
                    if (duration < 0)
                        duration = Math.Abs(duration);//In case if someone sends the pattern Talk -60min.
                    break;
            }
            return duration;
        }
    }
}
