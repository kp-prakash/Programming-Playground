namespace RealEstate.Tests.Rentals
{
    using MongoDB.Bson;
    using NUnit.Framework;
    using RealEstate.Rentals;

    [TestFixture]
    public class RentalTests: AssertionHelper
    {
        [Test]
        // METHOD_SCENARIO_EXPECTATION
        public void ToDocument_RentalWithPrice_PriceRepresentedAsDouble()
        {
            var rental = new Rental{Price = 1};
            var document = rental.ToBsonDocument();
            Expect(document["Price"].BsonType, Is.EqualTo(BsonType.Double));
        }

        [Test]
        public void ToDocument_RentalWithAnId_IdIsRepresentedAsAnObjectId()
        {
            var rental = new Rental
            {
                Id = ObjectId.GenerateNewId().ToString()
            };
            var document = rental.ToBsonDocument();
            Expect(document["_id"].BsonType, Is.EqualTo(BsonType.ObjectId));
        }
    }
}
