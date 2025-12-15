using BusinessLogicLayer.IService;
using DataAccessLayer.Entities;
using RepositoryLayer.Implementations;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public class PersolaDetails : IPersolaDetails
    {
        private readonly IUnitOfWork _UnitOfWork;
        public PersolaDetails(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        public Task AddPersonalDetailAsync(PersonalDetail personalDetail)
        {
            _UnitOfWork.PersonalDetails.Insert(personalDetail);
            _UnitOfWork.Save();
            return Task.CompletedTask;
        }


        public Task DeletePersonalDetailAsync(int id)
        {
            _UnitOfWork.PersonalDetails.Delete(id);
            _UnitOfWork.Save();
            return Task.CompletedTask;
        }

        public Task<IEnumerable<PersonalDetail>> GetAllPersonalDetailsAsync()
        {
            var data = _UnitOfWork.PersonalDetails.GetAll();
            return Task.FromResult(data);
        }


        public PersonalDetail GetPersonalDetailByIdAsync(int id)
        {
            var data = _UnitOfWork.PersonalDetails.GetById(id);
            return data;
        }

        public Task UpdatePersonalDetailAsync(PersonalDetail personalDetail)
        {
            _UnitOfWork.PersonalDetails.Update(personalDetail);
            _UnitOfWork.Save();
            return Task.CompletedTask;
        }
        public async Task AddAllAsync(
    PersonalDetail personalDetail,
    List<AcademicQualification> academics,
    List<JobExperience> jobs)
        {
            try
            {
                // Save PersonalDetail first to get the Id
                _UnitOfWork.PersonalDetails.Insert(personalDetail);
                _UnitOfWork.Save(); // This generates the Id

                // Save Academic Qualifications
                if (academics != null && academics.Any())
                {
                    foreach (var academic in academics)
                    {
                        academic.PersonalDetailId = personalDetail.Id;
                        academic.PersonalDetail = null; // Clear navigation property
                        _UnitOfWork.AcademicQualifications.Insert(academic);
                    }
                }

                // Save Job Experiences
                if (jobs != null && jobs.Any())
                {
                    foreach (var job in jobs)
                    {
                        job.PersonalDetailId = personalDetail.Id;
                        job.PersonalDetail = null; // Clear navigation property
                        _UnitOfWork.JobExperiences.Insert(job);
                    }
                }

                _UnitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log the error or handle it appropriately
                throw new Exception("Error saving personal details", ex);
            }

        }
        public async Task<PersonalDetail> GetByIdWithRelatedDataAsync(int id)
        {
            // Assuming you're using Entity Framework
            var personalDetail = _UnitOfWork.PersonalDetails.GetById(id);

            if (personalDetail != null)
            {
                // Load related data
                personalDetail.AcademicQualifications = _UnitOfWork.AcademicQualifications
                    .GetAll()
                    .Where(a => a.PersonalDetailId == id)
                    .OrderByDescending(a => a.PassingYear)
                    .ToList();

                personalDetail.JobExperiences = _UnitOfWork.JobExperiences
                    .GetAll()
                    .Where(j => j.PersonalDetailId == id)
                    .OrderByDescending(j => j.FromDate)
                    .ToList();
            }

            return personalDetail;
        }

    }
}
