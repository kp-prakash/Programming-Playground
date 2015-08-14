using System;

namespace AOP.Validation.And.CodeGeneration.DomainModel
{
    public class Customer : Entity
    {
        public Customer()
        {
        }

        public Customer(string name, Guid id)
        {
            Name = name;
            Id = id;
        }

        public virtual string AddressLine1 { get; private set; }

        public virtual string AddressLine2 { get; private set; }

        public virtual string AddressLine3 { get; private set; }

        public virtual string City { get; private set; }

        public virtual string Country { get; private set; }

        public virtual string Name { get; private set; }

        public virtual string Province { get; private set; }

        public virtual string GetFormattedAddress()
        {
            return null;
        }
    }
}