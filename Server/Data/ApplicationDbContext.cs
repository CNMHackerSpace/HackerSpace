using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Badge> Badges { get; set; }
        public DbSet<Evaluator> Evaluators { get; set; }
    }
}
