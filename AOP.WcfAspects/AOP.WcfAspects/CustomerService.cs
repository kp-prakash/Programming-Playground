using System;
using System.Collections.Generic;
using AOP.WcfAspects.DataContracts;

namespace AOP.WcfAspects
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CustomerService" in both code and config file together.
    public class CustomerService : ICustomerService
    {
        public IEnumerable<ActiveCustomerForListing> FetchActiveCustomersForListing()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerForListing> FetchCustomersForListing()
        {
            return null;
        }

        public void UpdateCustomerUsing(CustomerForUpdating customerToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}