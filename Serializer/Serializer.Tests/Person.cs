namespace Serializer.Tests
{
    using System;

    /// <summary>
    /// Sample entity to test serialization.
    /// </summary>
    public class Person : IEquatable<Person>
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Determines if the instances are equal.
        /// </summary>
        /// <param name="other">Another instance.</param>
        /// <returns>True if the instances are equal.</returns>
        public bool Equals(Person other)
        {
            return this.FirstName == other.FirstName && this.LastName == other.LastName;
        }

        #endregion
    }
}