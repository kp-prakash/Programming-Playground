namespace RESTContactManager
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.ServiceModel.Web;

    [ServiceContract]
    public interface IContactManager
    {
        /// <summary>
        /// Creates the contact.
        /// </summary>
        /// <param name="newContact">The contact.</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "", Method = "POST")]
        Contact CreateContact(Contact newContact);

        /// <summary>
        /// Gets all contacts.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "")]
        IList<Contact> GetAllContacts();

        /// <summary>
        /// Gets the contact.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "{id}")]
        Contact GetContact(string id);

        /// <summary>
        /// Updates the contact.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="contactToBeUpdated">The contact to be updated.</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "{id}", Method = "PUT")]
        Contact UpdateContact(string id, Contact contactToBeUpdated);

        /// <summary>
        /// Deletes the contact.
        /// </summary>
        /// <param name="id">The id.</param>
        [OperationContract]
        [WebInvoke(UriTemplate = "{id}", Method = "DELETE")]
        void DeleteContact(string id);
    }
}