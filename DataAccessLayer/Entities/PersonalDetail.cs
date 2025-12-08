using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class PersonalDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }

        // Navigation Properties (1 person → many)
        public ICollection<AcademicQualification> AcademicQualifications { get; set; }
        public ICollection<JobExperience> JobExperiences { get; set; }
    }
}
