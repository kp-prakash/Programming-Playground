namespace TaskMonitor.WebApp.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// View model for creating a new task.
    /// </summary>
    public class NewTaskViewModel
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required]
        [StringLength(200, ErrorMessage = "Please enter a valid description!", MinimumLength = 2)]
        [Display(Name = "Task Description")]
        [DataType(DataType.Text)]
        public string Description { get; set; }


        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        [Display(Name = "Task End Time")]
        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }

        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        [Required]
        [Display(Name = "Task Start Time")]
        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }
    }
}