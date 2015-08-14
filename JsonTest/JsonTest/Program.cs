using System;
using System.Collections.Generic;
using ServiceStack;

namespace JsonTest
{
    using ServiceStack.Text;

    internal class Person
    {
        public Person()
        {
            Children = new List<Person>();
        }

        public string Address { get; set; }

        public int? Age { get; set; }

        public List<Person> Children { get; set; }

        public string Name { get; set; }
    }

    internal class Program
    {
        private static void Main()
        {
            JsConfig.IncludeNullValues = true;
            var c1 = new Person { Name = "John", Address = "USA", Age = null };
            var c2 = new Person { Name = "John", Address = "USA", Age = 12 };
            var children = new List<Person> {c1};
            const string name = "Jim";
            // Uncomment lines below and check  - Children attribute is omitted from JSON result
            // children = null;
            // name = null;
            var p1 = new { Name = name, Address = "USA", Age = 40, Children = children };
            var p2 = new Person { Name = "Jim", Address = "USA", Age = null };
            p2.Children.Add(c2);
            Console.WriteLine(p1.ToJson());
            Console.WriteLine(p2.ToJson());
            //string xml =
            //    "<data><collection-title /><collection-short-title /><copyright><copyryear>2014</copyryear> <copyrholder /> </copyright><language>English</language> <object-id>1_24531314</object-id> <versionset-id>24531314</versionset-id> <document-id /> <document-path /> <status /> <document-type /> <last-modified-date /> <access /> <topic /> </data>";
            //XmlReader xmlReader = new XmlTextReader(xml, XmlNodeType.Document, null);
            //Console.WriteLine((new XmlSerializer(typeof(Data)).Deserialize(xmlReader)).ToJson());
            //Console.ReadLine();
        }
    }

    //[XmlRoot("data")]
    //[DataContract(Name = "data")]
    //public class Data
    //{
    //    [XmlElement("collection-title")]
    //    [DataMember(Name = "collection-title")]
    //    public string CollectionTitle { get; set; }

    //    [XmlElement("collection-short-title")]
    //    [DataMember(Name = "collection-short-title")]
    //    public string CollectionShortTitle { get; set; }

    //    [XmlElement("copyright")]
    //    [DataMember(Name = "copyright")]
    //    public Copyright Copyright { get; set; }

    //    [XmlElement("language")]
    //    [DataMember(Name = "language")]
    //    public string Language { get; set; }

    //    [XmlElement("object-id")]
    //    [DataMember(Name = "object-id")]
    //    public string ObjectId { get; set; }

    //    [XmlElement("versionset-id")]
    //    [DataMember(Name = "versionset-id")]
    //    public string VersionSetId { get; set; }

    //    [XmlElement("document-id")]
    //    [DataMember(Name = "document-id")]
    //    public string DocumentId { get; set; }

    //    [XmlElement("document-path")]
    //    [DataMember(Name = "document-path")]
    //    public string DocumentPath { get; set; }

    //    [XmlElement("status")]
    //    [DataMember(Name = "status")]
    //    public string Status { get; set; }

    //    [XmlElement("document-type")]
    //    [DataMember(Name = "document-type")]
    //    public string DocumentType { get; set; }

    //    [XmlElement("last-modified-date")]
    //    [DataMember(Name = "last-modified-date")]
    //    public string LastModifiedDate { get; set; }

    //    [XmlElement("access")]
    //    [DataMember(Name = "access")]
    //    public string Access { get; set; }

    //    [XmlElement("topic")]
    //    [DataMember(Name = "topic")]
    //    public string Topic { get; set; }

    //}

    //public class Copyright
    //{
    //    [XmlElement("copyryear")]
    //    [DataMember(Name = "copyryear")]
    //    public int Year { get; set; }

    //    [XmlElement("copyrholder")]
    //    [DataMember(Name = "copyrholder")]
    //    public string Holder { get; set; }
    //}
}