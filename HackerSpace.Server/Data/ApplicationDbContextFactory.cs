using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

// Added a DbContextFactory class
// Create DbContext instances for EF
namespace HackerSpace.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Use real connection string here
            builder.UseSqlite("Data Source=HackerSpace.db;Cache=Shared");

            return new ApplicationDbContext(builder.Options);
        }
    }
}
