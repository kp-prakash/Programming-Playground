namespace TaskMonitor.DataImport
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Task
    {
        [Column(TypeName = "datetime2")]
        public DateTime? CreatedDate { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? EndDate { get; set; }

        public long Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; set; }
    }
}