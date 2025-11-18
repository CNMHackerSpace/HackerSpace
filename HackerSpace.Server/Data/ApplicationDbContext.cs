using HackerSpace.Shared.Models;
using HackerSpace.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HackerSpace.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Evaluator> Evaluators { get; set; }
    }
}
