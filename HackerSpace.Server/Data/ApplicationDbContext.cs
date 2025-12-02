using Hackerspace.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// Application DbContext here - describe database schema and tables
// Separate DbContext from DbContextFactory to not break EF Core migrations, not confuse Dependency injection, or break identity
namespace HackerSpace.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Badge> Badges { get; set; }
    }
}
