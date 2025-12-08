using DataAccessLayer;
using DataAccessLayer.Entities;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IRepository<PersonalDetail> PersonalDetails { get; }
        public IRepository<AcademicQualification> AcademicQualifications { get; }
        public IRepository<JobExperience> JobExperiences { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            PersonalDetails = new Repository<PersonalDetail>(_context);
            AcademicQualifications = new Repository<AcademicQualification>(_context);
            JobExperiences = new Repository<JobExperience>(_context);
        }

        public void Save() => _context.SaveChanges();
    }
}
