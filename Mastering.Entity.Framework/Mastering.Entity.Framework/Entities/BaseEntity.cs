namespace Mastering.Entity.Framework.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseEntity
    {
        [Required]
        public DateTime CreatedUtc { get; set; }
    }
}