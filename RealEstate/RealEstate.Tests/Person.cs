namespace RealEstate.Tests
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    [BsonIgnoreExtraElements] // Ignore extra elements while deserializing. Provides compatibility!
    public class Person
    {
        public Person()
        {
            Address = new List<string>();
        }

        [BsonId]
        public int PersonId { get; set; }

        // NOTE:
        // BsonElement attribute is not a mandatory. Properties are automatically serialized.
        // BsonElement is used to rename attribute / property and also order them.
        // It could be used to include private members are part of serialized output.

        [BsonElement("address")]
        public List<string> Address { get; private set; }

        [BsonElement("age")]
        public int Age { get; set; }

        // Mongo stores date time as UTC. To get local time on deserialization,
        // use [BsonDateTimeOptions(Kind = DateTimeKind.Local)] attribute.
        [BsonDateTimeOptions(Kind = DateTimeKind.Local, DateOnly = true)]
        public DateTime BirthTime { get; set; }

        [BsonElement("contact")]
        [BsonIgnoreIfNull]
        public Contact Contact { get; set; }

        // Note: Characters and Enums are represented by integers by default.

        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonIgnore]
        public string IgnoreValue { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        // Decimal doesn't have equivalent Bson type and is serialized as string.
        // Use the BsonRepresentation attribute to set type for serialization / representation.
        [BsonRepresentation(BsonType.Double)]
        public decimal NetWorth { get; set; }

        // This attribute is ignored b'coz of [BsonIgnore]
    }
}