﻿using System.ComponentModel.DataAnnotations;

namespace Contoso.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }

        public int CourseID { get; set; }

        public int PersonID { get; set; }

        [DisplayFormat(DataFormatString = "{0:#.#}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "No Grade")]
        public decimal? Grade { get; set; }

        public virtual Course Course { get; set; }

        public virtual Student Student { get; set; }
    }
}