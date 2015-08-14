namespace Mastering.Entity.Framework.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TaskCategories")]
    public class TaskCategory : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? Id { get; set; }

        [Required]
        public string CategoryName { get; set; }
    }
}