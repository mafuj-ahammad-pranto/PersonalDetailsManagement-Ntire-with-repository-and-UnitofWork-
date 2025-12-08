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
    }
}
