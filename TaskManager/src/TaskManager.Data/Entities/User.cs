namespace TaskManager.Data.Entities
{
    public class User : IVersionedEntity
    {
        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual long UserId { get; set; }

        public virtual string UserName { get; set; }

        public virtual byte[] Version { get; set; }
    }
}