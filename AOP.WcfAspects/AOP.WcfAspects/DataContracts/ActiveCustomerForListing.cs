using System;
using AOP.WcfAspects.Aspects;

namespace AOP.WcfAspects.DataContracts
{
    public class ActiveCustomerForListing
    {
        public Guid Id { get; set; }

        [NotADataMember]
        public string Name { get; set; }
    }
}