using DataAccessLayer.Entities;

namespace WebUI.Models
{
    public class PersonalDetailCreateVM
    {
        public PersonalDetail PersonalDetail { get; set; } = new PersonalDetail();
        public List<AcademicQualification> AcademicQualifications { get; set; } = new List<AcademicQualification>();
        public List<JobExperience> JobExperiences { get; set; } = new List<JobExperience>();
    }
}
