using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paging.TestHarness
{
    public class Student
    {
        public int StudentID { get; set; }

        [Column("FirstName")]
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstMiddleName { get; set; }

        [Column("LastName")]
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Enrollment Date")]
        [Required(ErrorMessage = "Enrollment Date is required.")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? EnrollmentDate { get; set; }

        public string FullName
        {
            get { return LastName + ", " + FirstMiddleName; }
        }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}