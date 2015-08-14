namespace Mastering.Entity.Framework.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tasks")]
    public class Task : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? EndDate { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; set; }

        public virtual TaskCategory Category { get; set; }
        public virtual User User { get; set; }
    }
}