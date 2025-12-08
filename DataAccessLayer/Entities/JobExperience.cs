using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class JobExperience
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public string Designation { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public string Responsibilities { get; set; }

        // Foreign Key
        public int PersonalDetailId { get; set; }

        // Navigation Property
        public PersonalDetail PersonalDetail { get; set; }
    }
}
