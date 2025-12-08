using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Entities;

namespace DataAccessLayer
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<PersonalDetail> PersonalDetails { get; set; }
        public DbSet<AcademicQualification> AcademicQualifications { get; set; }
        public DbSet<JobExperience> JobExperiences { get; set; }
    }
}
