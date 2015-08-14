namespace RealEstate.Tests
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.IO;
    using MongoDB.Bson.Serialization;
    using NUnit.Framework;

    public class BsonDocumentTests
    {
        public BsonDocumentTests()
        {
            // This ensures that the JSON result is indented!
            JsonWriterSettings.Defaults.Indent = true;
        }

        [Test]
        public void AddElements()
        {
            var person = new BsonDocument
            {
                {"fristName", new BsonString("Srihari")},
                {"lastName", new BsonString("Sridharan")},
                {"age", new BsonInt32(32)},
                {"isAlive", true} // Note here we aren't using BsonBoolean() instead relying on implicit cast!
            };
            Console.WriteLine(person);
        }

        [Test]
        public void AddingArrays()
        {
            // Demo for BsonArray! Array of addresses!
            var person = new BsonDocument
            {
                {"fristName", new BsonString("Srihari")},
                {"lastName", new BsonString("Sridharan")},
                {"age", new BsonInt32(32)},
                {"address", new BsonArray(new[] {"Home: Chennai 600100", "Office: Chennai 600130"})}
            };
            Console.WriteLine(person);
        }

        [Test]
        public void BsonValueConversions()
        {
            var person = new BsonDocument
            {
                {"fristName", new BsonString("Srihari")},
                {"lastName", new BsonString("Sridharan")},
                {"age", new BsonInt32(32)}
            };
            var age = person["age"];

            // NOTE: Remember AsInt32 performs a cast behind the scenes and can throw InvalidCastException
            var ageAfterTenYears = person["age"].AsInt32 + 10;

            // Using AsDouble will result in exception. Instead use ToDouble()
            // var asDoubleAge = person["age"].AsDouble;
            var toDouble = person["age"].ToDouble();

            // Write values...
            Console.WriteLine("Age {0}", age);
            Console.WriteLine("Age after 10 years {0}", ageAfterTenYears);
            Console.WriteLine("Double data type conversion: {0}", toDouble);
            Console.WriteLine(person["age"].IsInt32); // True
            Console.WriteLine(person["age"].IsString); // False
        }

        [Test]
        public void EmbeddedDocument()
        {
            var person = new BsonDocument
            {
                {"fristName", new BsonString("Srihari")},
                {"lastName", new BsonString("Sridharan")},
                {"age", new BsonInt32(32)},
                {
                    "contact", new BsonDocument
                    {
                        {"phone", "123-456-7890"},
                        {"email", "ss@abc.com"}
                    }
                }
            };
            Console.WriteLine(person);
        }

        [Test]
        public void EmptyDocument()
        {
            var document = new BsonDocument();
            Console.WriteLine(document.ToJson());
            Console.WriteLine(document); // ToString and ToJson are one and the same.
        }

        [Test]
        public void ToBson()
        {
            var person = new BsonDocument
            {
                {"fristName", new BsonString("Srihari")},
                {"lastName", new BsonString("Sridharan")},
                {"age", new BsonInt32(32)},
                {
                    "contact", new BsonDocument
                    {
                        {"phone", "123-456-7890"},
                        {"email", "ss@abc.com"}
                    }
                }
            };
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