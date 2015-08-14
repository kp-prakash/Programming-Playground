using System.ComponentModel.DataAnnotations;

namespace Paging.TestHarness
{
    public class OfficeAssignment
    {
        [Key]
        public int InstructorID { get; set; }

        [MaxLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }

        public virtual Instructor Instructor { get; set; }
    }
}