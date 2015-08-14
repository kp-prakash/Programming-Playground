using AOPUsingPOSTSharp.Aspects;

namespace AOPUsingPOSTSharp.Models
{
    /// <summary>
    /// Models a Customer
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets or sets the billing address.
        /// </summary>
        /// <value>
        /// The billing address.
        /// </value>
        [ObjectInitializationAspect]
        public Address BillingAddress { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [ObjectInitializationAspect]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [ObjectInitializationAspect]
        public string Name { get; set; }
    }
}