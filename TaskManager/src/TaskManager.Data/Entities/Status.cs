namespace TaskManager.Data.Entities
{
    public class Status : IVersionedEntity
    {
        public virtual string Name { get; set; }

        public virtual long Ordinal { get; set; }

        public virtual long StatusId { get; set; }

        public virtual byte[] Version { get; set; }
    }
}