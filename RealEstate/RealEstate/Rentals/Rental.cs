using System.Collections.Generic;

namespace RealEstate.Rentals
{
    using System.Linq;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Rental
    {
        private List<string> address = new List<string>();

        public Rental()
        {
            // To allow Mongo deserialize rental.
        }

        public Rental(PostRental postRental)
        {
            Description = postRental.Description;
            NumberOfRooms = postRental.NumberOfRooms;
            Price = postRental.Price;
            address = (postRental.Address ?? string.Empty).Split('\n').ToList();
        }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public string Id { get; set; }

        public string Description { get; set; }

        public int NumberOfRooms { get; set; }

        public List<string> Address
        {
            get { return address; }
            set { address = value; }
        }

        [BsonRepresentation(BsonType.Double)]
        public decimal Price { get; set; }
    }
}