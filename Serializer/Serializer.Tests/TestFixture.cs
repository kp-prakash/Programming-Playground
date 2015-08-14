namespace Serializer.Tests
{
    using System.Linq;

    using NUnit.Framework;

    /// <summary>
    /// Tests for Serialization.
    /// </summary>
    [TestFixture]
    public class SerializationTest
    {
        #region Public Methods and Operators

        [Test]
        public void TestBasicDataType()
        {
            const int data = 12;
            string output = Serializer<int>.Serialize(data);
            int deserializedData = Serializer<int>.Deserialize(output);
            Assert.True(data == deserializedData);
        }

        /// <summary>
        /// Tests the object serialization.
        /// </summary>
        [Test]
        public void TestObjectSerialization()
        {
            SerializableDictionary<int, Person> objectToSerialize = GetObjectToSerialize();
            string xml = Serializer<SerializableDictionary<int, Person>>.Serialize(objectToSerialize);
            SerializableDictionary<int, Person> deserializedObject =
                Serializer<SerializableDictionary<int, Person>>.Deserialize(xml);
            Assert.IsTrue(this.EquatePersonDictionaries(objectToSerialize, deserializedObject));
        }

        /// <summary>
        /// Tests the serializer with primitive types.
        /// </summary>
        [Test]
        public void TestPrimitiveTypeSerialization()
        {
            var objectToSerialize = new SerializableDictionary<int, string> { { 1, "A" }, { 2, "B" } };
            string xml = Serializer<SerializableDictionary<int, string>>.Serialize(objectToSerialize);
            SerializableDictionary<int, string> deserializedObject =
                Serializer<SerializableDictionary<int, string>>.Deserialize(xml);
            Assert.IsTrue(this.EquateDictionaries(objectToSerialize, deserializedObject));
        }

        /// <summary>
        /// Tests the serialization to file.
        /// </summary>
        [Test]
        public void TestSerializationToFile()
        {
            SerializableDictionary<int, Person> objectToSerialize = GetObjectToSerialize();
            Serializer<SerializableDictionary<int, Person>>.SerializeToFile(objectToSerialize, "Person.xml");
            SerializableDictionary<int, Person> deserializedObject =
                Serializer<SerializableDictionary<int, Person>>.DeserializeFromFile("Person.xml");
            Assert.IsTrue(this.EquatePersonDictionaries(objectToSerialize, deserializedObject));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets sample object to serialize.
        /// </summary>
        /// <returns></returns>
        private static SerializableDictionary<int, Person> GetObjectToSerialize()
        {
            var objectToSerialize = new SerializableDictionary<int, Person>
                {
                    { 1, new Person { FirstName = "FN1", LastName = "LN1" } },
                    { 2, new Person { FirstName = "FN2", LastName = "LN2" } }
                };
            return objectToSerialize;
        }

        /// <summary>
        /// Equates the dictionaries.
        /// </summary>
        /// <param name="objectToSerialize">The object to be serialized.</param>
        /// <param name="deserializedObject">The deserialized object.</param>
        /// <returns>True if both the dictionaries are equal.</returns>
        private bool EquateDictionaries(
            SerializableDictionary<int, string> objectToSerialize,
            SerializableDictionary<int, string> deserializedObject)
        {
            if (null == objectToSerialize || null == deserializedObject)
            {
                return false;
            }
            return objectToSerialize.Keys.All(key => objectToSerialize[key] == deserializedObject[key]);
        }

        /// <summary>
        /// Equates the person dictionaries.
        /// </summary>
        /// <param name="objectToSerialize">The object to be serialized.</param>
        /// <param name="deserializedObject">The deserialized object.</param>
        /// <returns>True if both the dictionaries are equal.</returns>
        private bool EquatePersonDictionaries(
            SerializableDictionary<int, Person> objectToSerialize,
            SerializableDictionary<int, Person> deserializedObject)
        {
            if (null == objectToSerialize || null == deserializedObject)
            {
                return false;
            }
            return objectToSerialize.Keys.All(key => objectToSerialize[key].Equals(deserializedObject[key]));
        }

        #endregion
    }
}