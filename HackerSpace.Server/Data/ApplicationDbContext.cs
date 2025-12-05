// Copyright (c) CNM. All rights reserved.

using HackerSpace.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// Application DbContext here - describe database schema and tables
// Separate DbContext from DbContextFactory to not break EF Core migrations, not confuse Dependency injection, or break identity
namespace HackerSpace.Data
{
    /// <summary>
    /// EF Core database context for the application, including Identity and application-specific sets.
    /// </summary>
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        /// <summary>
        /// Gets or sets the <see cref="DbSet{Badge}"/> used to query and save <see cref="Badge"/> instances.
        /// </summary>
        public DbSet<Badge> Badges { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{Evaluator}"/> used to query and save <see cref="Evaluator"/> instances.
        /// </summary>
        public DbSet<Evaluator> Evaluators { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{Submission}"/> used to query and save <see cref="Submission"/> instances.
        /// </summary>
        public DbSet<Submission> Submissions { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{SubmissionLink}"/> used to query and save <see cref="SubmissionLink"/> instances.
        /// </summary>
        public DbSet<SubmissionLink> SubmissionLinks { get; set; }
    }
}
