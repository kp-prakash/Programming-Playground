using System;
using System.Collections.Generic;
using System.Linq;

namespace AOP.Validation.And.CodeGeneration.DomainModel
{
    public class Invoice : Entity
    {
        private IList<InvoiceItem> _items = new List<InvoiceItem>();

        public virtual DateTime InvoiceDate { get; private set; }

        public virtual IEnumerable<InvoiceItem> Items { get { return _items; } }

        public virtual int Number { get; private set; }

        public virtual void AddItem(InvoiceItem itemToAdd)
        {
            _items.Add(itemToAdd);
        }

        public virtual double GetTotalAmount()
        {
            return _items.Select(x => x.Amount * x.Count).Sum();
        }
    }
}