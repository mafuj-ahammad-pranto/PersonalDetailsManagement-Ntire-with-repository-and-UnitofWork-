using BusinessLogicLayer.IService;
using DataAccessLayer.Entities;
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
    }
}
