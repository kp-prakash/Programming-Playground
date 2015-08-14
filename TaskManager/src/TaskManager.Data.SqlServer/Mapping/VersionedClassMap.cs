namespace TaskManager.Data.SqlServer.Mapping
{
    using FluentNHibernate.Mapping;
    using TaskManager.Data.Entities;

    public abstract class VersionedClassMap<T> : ClassMap<T> where T : IVersionedEntity
    {
        protected VersionedClassMap()
        {
            Version(x => x.Version) // Version property
                .Column("ts") // Indicates the database column supporting versioning.
                .CustomSqlType("Rowversion") // SQL Data Type.
                .Generated.Always() // NHibernate should always let the database generate the value, as opposed to you or NHibernate supplying the value.
                .UnsavedValue("null"); // Prior to a database save, the in-memory value of the Version property will be null.
        }
    }
}