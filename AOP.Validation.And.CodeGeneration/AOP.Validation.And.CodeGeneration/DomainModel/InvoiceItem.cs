namespace AOP.Validation.And.CodeGeneration.DomainModel
{
    public class InvoiceItem : Entity
    {
        public virtual double Amount { get; private set; }

        public virtual int Count { get; private set; }

        public virtual string Name { get; private set; }
    }
}