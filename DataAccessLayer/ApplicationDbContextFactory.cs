using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccessLayer
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-SH783SC\\SQLEXPRESS;Initial Catalog=EmployeeDB;Integrated Security=True;TrustServerCertificate=True");
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
