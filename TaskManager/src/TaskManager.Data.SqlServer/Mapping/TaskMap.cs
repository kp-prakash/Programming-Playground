namespace TaskManager.Data.SqlServer.Mapping
{
    using FluentNHibernate.Mapping;
    using TaskManager.Data.Entities;

    public class TaskMap : VersionedClassMap<Task>
    {
        public TaskMap()
        {
            Id(x => x.TaskId);
            Map(x => x.Subject).Not.Nullable();
            Map(x => x.StartDate).Nullable();
            Map(x => x.DueDate).Nullable();
            Map(x => x.CompletedDate).Nullable();
            Map(x => x.CreatedDate).Not.Nullable();

            References(x => x.Status, "StatusId");
            References(x => x.CreatedBy, "CreatedUserId");

            HasManyToMany(x => x.Users)
                .Access
                .ReadOnlyPropertyThroughCamelCaseField(Prefix.None);
            // TODO: This was originally Prefix.Underscore - see if this is fine
        }
    }
}