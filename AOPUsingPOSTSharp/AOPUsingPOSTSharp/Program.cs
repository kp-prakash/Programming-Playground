using System;
using AOPUsingPOSTSharp.Models;
using AOPUsingPOSTSharp.Services;

namespace AOPUsingPOSTSharp
{
    /// <summary>
    /// Main entry point.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Calls the customer service.
        /// </summary>
        private static void CallCustomerService()
        {
            var customerServices = new CustomerServices();
            Register(customerServices);
            customerServices.Save(new Customer { Name = "Srihari" });
            UnRegister(customerServices);

            //Call the method below, to demonstrate OnException Aspect.
            //customerServices.GetAllCustomers();
        }

        /// <summary>
        /// Customers the successfully saved.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private static void CustomerSuccessfullySaved(object sender, EventArgs e)
        {
            Console.WriteLine("CUSTOMER SAVED SUCCESSFULLY");
        }

        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args">The args.</param>
        private static void Main(string[] args)
        {
            Console.WriteLine("-----Starting Application-----");
            CallCustomerService();
            Console.WriteLine("-----Stopping Application-----");
            Console.WriteLine("Please press any key to proceed...");
            Console.ReadKey();
        }

        /// <summary>
        /// Registers the specified customer services.
        /// </summary>
        /// <param name="customerServices">The customer services.</param>
        private static void Register(CustomerServices customerServices)
        {
            customerServices.CustomerSuccessfullySaved
                += new EventHandler(CustomerSuccessfullySaved);
        }

        /// <summary>
        /// Uns the register.
        /// </summary>
        /// <param name="customerServices">The customer services.</param>
        private static void UnRegister(CustomerServices customerServices)
        {
            customerServices.CustomerSuccessfullySaved
                -= new EventHandler(CustomerSuccessfullySaved);
        }
    }
}

/* Sample Output:
        -----Starting Application-----
        Event CustomerSuccessfullySaved added.
        Entered Method: CustomerServices.Save
        Property (AOPUsingPOSTSharp.Models.Customer.BillingAddress) not initialized
        Saved customer with address !
        Event CustomerSuccessfullySaved invoked.
        CUSTOMER SAVED SUCCESSFULLY
        Success: CustomerServices.Save return Saved customer!
        Exitting method: CustomerServices.Save
        Event CustomerSuccessfullySaved removed.
        -----Stopping Application-----
        Please press any key to proceed...
*/