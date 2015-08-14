namespace RESTContactManager
{
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Activation;

    /// <summary>
    /// Contact Manager.
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ContactManager : IContactManager
    {
        private readonly List<Contact> _contacts = new List<Contact>();
        private int _contactCount;

        /// <summary>
        /// Creates the contact.
        /// </summary>
        /// <param name="newContact">The contact.</param>
        /// <returns></returns>
        public Contact CreateContact(Contact newContact)
        {
            newContact.Id = (++_contactCount).ToString();
            _contacts.Add(newContact);
            return newContact;
        }

        /// <summary>
        /// Gets all contacts.
        /// </summary>
        /// <returns></returns>
        public IList<Contact> GetAllContacts()
        {
            return _contacts.ToList();
        }

        /// <summary>
        /// Gets the contact.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Contact GetContact(string id)
        {
            return _contacts.FirstOrDefault(contact => contact.Id.Equals(id));
        }

        /// <summary>
        /// Updates the contact.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="contactToBeUpdated">The contact to be updated.</param>
        /// <returns></returns>
        public Contact UpdateContact(string id, Contact contactToBeUpdated)
        {
            var foundContact = _contacts.FirstOrDefault(contact => contact.Id.Equals(id));
            if (foundContact == null)
            {
                return null;
            }
            foundContact.FirstName = contactToBeUpdated.FirstName;
            foundContact.LastName = contactToBeUpdated.LastName;
            foundContact.Cell = contactToBeUpdated.Cell;
            foundContact.DayPhone = contactToBeUpdated.DayPhone;
            foundContact.Email = contactToBeUpdated.Email;
            return foundContact;
        }

        /// <summary>
        /// Deletes the contact.
        /// </summary>
        /// <param name="id">The id.</param>
        public void DeleteContact(string id)
        {
            _contacts.RemoveAll(contact => contact.Id.Equals(id));
        }
    }
}