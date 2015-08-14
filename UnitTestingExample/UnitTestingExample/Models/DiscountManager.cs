using UnitTestingExample.Interfaces;
using System;
namespace UnitTestingExample.Models
{
    public class DiscountManager : IDiscountManager
    {

        public decimal GetPriceAfterDiscount(decimal price)
        {
            if(price < 0)
            {
                throw new ArgumentOutOfRangeException("price");
            }
            if (price > 100)
            {
                return price * 0.9M;
            }

            if (price > 10 && price <= 100)
            {
                return price * 0.95M;
            }
            return price;
        }
    }
}