using System;
using System.Collections.Generic;
using AOPUsingPOSTSharp.Aspects;
using AOPUsingPOSTSharp.Models;

namespace AOPUsingPOSTSharp.Services
{
    /// <summary>
    /// Service class for customer instance.
    /// </summary>
    public class CustomerServices
    {
        //Static field to demonstrate CompileTimeValidate.
        private static string _name;

        /// <summary>
        /// Static method to demonstrate CompileTimeValidate.
        /// </summary>
        public static void MyStaticMethod()
        {
            // We should not be runing the MethodBoundayAspect for these.
            // See MethodBoundayAspect where CompileTimeValidation() is used to achieve this.
            _name = "Srihari";
            Console.WriteLine("Name: {0}", _name);
        }

        /// <summary>
        /// Occurs when [customer successfully saved].
        /// </summary>
        [LogEventAspect]
        public event EventHandler CustomerSuccessfullySaved;

        /// <summary>
        /// Gets all customers.
        /// </summary>
        /// <returns>Cusomers</returns>
        /// <exception cref="System.ApplicationException">Error while connecting to database!</exception>
        // Added as a Multicast Attribute [MethodBoundayAspect]
        [ApplicationExceptionAspect]
        public IEnumerable<Customer> GetAllCustomers()
        {
            throw new ApplicationException("Error while connecting to database!");
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns></returns>
        // Added as a Multicast Attribute [MethodBoundayAspect]
        public string Save(Customer customer)
        {
            Console.WriteLine("Saved customer with address {0}!", customer.BillingAddress);

            //To demonstrate EventInterceptionAspect
            if (null != CustomerSuccessfullySaved)
            {
                CustomerSuccessfullySaved(this, EventArgs.Empty);
            }

            return "Saved customer!";
        }
    }
}