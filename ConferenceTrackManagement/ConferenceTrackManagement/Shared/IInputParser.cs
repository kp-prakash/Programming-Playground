using System.Collections.Generic;

namespace Shared
{
    /// <summary>
    /// Interface representing an input parser.
    /// </summary>
    public interface IInputParser
    {
        /// <summary>
        /// Passes the input strings and creates the list of talks to be scheduled.
        /// </summary>
        /// <param name="inputs">The conference talks as strings.</param>
        /// <returns>List of unscheduled talks.</returns>
        List<ITalk> GetTalks(List<string> inputs);
    }
}
