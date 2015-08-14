namespace RESTContactManager
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Contact
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Cell { get; set; }

        [DataMember]
        public string DayPhone { get; set; }

        [DataMember]
        public string Email { get; set; }
    }
}