// Copyright (c) 2025. All rights reserved.

using Common.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// Application DbContext here - describe database schema and tables
// Separate DbContext from DbContextFactory to not break EF Core migrations, not confuse Dependency injection, or break identity
namespace Server.Data
{
    /// <summary>
    /// EF Core database context for the application, including Identity and application-specific sets.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The EF Core options for this context.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

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
