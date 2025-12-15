using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class AcademicQualification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string DegreeName { get; set; }  // HSC/SSC/BSc/MSc etc.

        public string InstituteName { get; set; }

        public int PassingYear { get; set; }

        public double Result { get; set; }  // GPA or CGPA

        // Foreign Key
        public int? PersonalDetailId { get; set; }

        // Navigation Property
        public PersonalDetail PersonalDetail { get; set; }
    }
}
