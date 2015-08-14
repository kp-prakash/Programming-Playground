using System;

namespace AOP.WcfAspects.DataContracts
{
    public class CustomerForListing
    {
        public string City { get; set; }

        public string Country { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}