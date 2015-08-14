using AOPUsingPOSTSharp.Aspects;

namespace AOPUsingPOSTSharp.Models
{
    /// <summary>
    /// Model for Address.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [ObjectInitializationAspect]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [ObjectInitializationAspect]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        /// <value>
        /// The zip.
        /// </value>
        [ObjectInitializationAspect]
        public int Zip { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}", State, Country, Zip);
        }
    }
}