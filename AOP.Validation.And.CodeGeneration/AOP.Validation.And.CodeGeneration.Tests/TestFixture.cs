namespace AOP.Validation.And.CodeGeneration.Tests
{
    using System;

    using NUnit.Framework;

    using AOP.Validation.And.CodeGeneration.DomainModel;

    [TestFixture]
    public class TestFixture
    {
        [Test]
        public void TestEquality()
        {
            var id = Guid.NewGuid();
            var customer1 = new Customer("Joe", id);
            var customer2 = new Customer("Bill", id);

            Assert.That(customer1.Equals(customer2));
        }
    }
}