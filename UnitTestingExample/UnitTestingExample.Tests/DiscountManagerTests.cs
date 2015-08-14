using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestingExample.Interfaces;

namespace UnitTestingExample.Tests
{
    using UnitTestingExample.Models;

    /*
     * Price > $100 10% Discount
     * Price Between $10 and $100 5% Discount
     * Price < $10 No Discount
     * Price < $0 Throw an Exception
     */
    [TestClass]
    public class DiscountManagerTests
    {
        private IDiscountManager _discountManager;

        [TestInitialize]
        public void StartUp()
        {
            _discountManager = new DiscountManager();
        }

        /// <summary>
        /// Computes the price after discount when price is more than hundred.
        /// </summary>
        [TestMethod]
        public void ComputePriceAfterDiscountWhenPriceIsMoreThanHundred()
        {
            decimal price = 110;
            decimal priceAfterDiscount = _discountManager.GetPriceAfterDiscount(price);
            Assert.AreEqual(price * 0.9M, priceAfterDiscount);
        }

        [TestMethod]
        public void ComputePriceAfterDiscountWhenPriceIsBetweenTenAndHundred()
        {
            decimal price = 80;
            decimal priceAfterDiscount = _discountManager.GetPriceAfterDiscount(price);
            Assert.AreEqual(price * 0.95M, priceAfterDiscount);
        }

        [TestMethod]
        public void ComputePriceAfterDiscountWhenPriceIsLessThanTen()
        {
            decimal price = 6;
            decimal priceAfterDiscount = _discountManager.GetPriceAfterDiscount(price);
            Assert.AreEqual(price, priceAfterDiscount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ComputePriceAfterDiscountWhenPriceIsLessThanZero()
        {
            decimal price = -1;
            _discountManager.GetPriceAfterDiscount(price);
        }
    }
}
