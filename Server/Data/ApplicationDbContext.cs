using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedClasses.Models;
using SharedClasses.Models.BlogModels;

namespace Server.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<HackerspaceBadge> HackerspaceBadges { get; set; }
    public DbSet<Evaluator> Evaluators { get; set; }
}

