namespace RealEstate.Tests
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.IO;
    using MongoDB.Bson.Serialization;
    using NUnit.Framework;

    public class PocoTests
    {
        public PocoTests()
        {
            JsonWriterSettings.Defaults.Indent = true;
        }

        [Test]
        public void Automatic()
        {
            var person = new Person
            {
                PersonId = 12,
                FirstName = "Stephen",
                LastName = "Hawking",
                Age = 32,
                NetWorth = new Decimal(100.44),
                BirthTime = new DateTime(1971, 10, 2)
            };
            person.Address.Add("Chennai 600100");
            person.Address.Add("Chennai 600130");
            person.Contact = new Contact { Email = "ss@abc.com", Phone = "123-456-7890" };

            var person1 = new Person
            {
                PersonId = 12,
                FirstName = "Stephen",
                LastName = "Hawking",
                Age = 32,
                NetWorth = new Decimal(120.44),
                BirthTime = new DateTime(1971, 10, 2)
            };
            person1.Address.Add("Chennai 600100");
            person1.Address.Add("Chennai 600130");

            Console.WriteLine(person.ToJson());
            Console.WriteLine(person1.ToJson());

            Console.WriteLine("BSON Document Representation:");
            var bson = person.ToBson();
            Console.WriteLine(BitConverter.ToString(bson));
            var deserializedPerson = BsonSerializer.Deserialize<BsonDocument>(bson);
            Console.WriteLine();
            Console.WriteLine("Deserialized BSON:");
            Console.WriteLine(deserializedPerson);
        }
    }
}