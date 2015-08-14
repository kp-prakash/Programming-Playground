namespace RealEstate.Tests
{
    using MongoDB.Bson.Serialization.Attributes;

    public class Contact
    {
        [BsonElement("email")]
        [BsonIgnoreIfNull]
        public string Email { get; set; }

        [BsonElement("phone")]
        [BsonIgnoreIfNull]
        public string Phone { get; set; }
    }
}