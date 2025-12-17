using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IService
{
    public interface IPersolaDetails
    {
        Task<IEnumerable<PersonalDetail>> GetAllPersonalDetailsAsync();
        PersonalDetail GetPersonalDetailByIdAsync(int id);
        Task AddPersonalDetailAsync(PersonalDetail personalDetail);
        Task UpdatePersonalDetailAsync(PersonalDetail personalDetail);
        Task DeletePersonalDetailAsync(int id);
        Task AddAllAsync(
        PersonalDetail personalDetail,
        List<AcademicQualification> academicQualifications,
        List<JobExperience> jobExperiences);
        Task<PersonalDetail> GetByIdWithRelatedDataAsync(int id);
        Task UpdateAllAsync(
    PersonalDetail personalDetail,
    List<AcademicQualification> academics,
    List<JobExperience> jobs);
    }
}
