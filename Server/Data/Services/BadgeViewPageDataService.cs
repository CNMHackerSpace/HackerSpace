// Copyright (c) 2025. All rights reserved.

using HackerSpace.Data;
using HackerSpace.Server.Services;
using HackerSpace.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HackerSpace.Server.Data.Services
{
    /// <summary>
    /// Service implementation for badge view and submission operations.
    /// </summary>
    public class BadgeViewPageDataService : IBadgeViewPageDataService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BadgeViewPageDataService"/> class.
        /// </summary>
        public BadgeViewPageDataService(ApplicationDbContext context) => _context = context;

        /// <inheritdoc />
        public async Task<Badge?> GetByIdAsync(Guid id)
        {
            return await _context.Badges
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        /// <inheritdoc />
        public async Task CreateSubmissionAsync(Submission submission)
        {
            submission.Id = Guid.NewGuid();
            submission.CreatedAt = DateTime.UtcNow;

            // Ensure all links have IDs
            foreach (var link in submission.Links)
            {
                if (link.Id == Guid.Empty)
                {
                    link.Id = Guid.NewGuid();
                }
                
                link.SubmissionId = submission.Id;
            }

            _context.Submissions.Add(submission);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<List<string>> GetEvaluatorEmailsAsync(Guid badgeId)
        {
            // TODO: Replace with actual evaluator logic based on your user/role system
            // This is a placeholder that returns users with "Evaluator", "Instructor", or "Admin" roles
            // You'll need to adjust this based on how evaluators are associated with badges in your schema

            var evaluatorEmails = await _context.Users
                .Join(_context.UserRoles,
                    user => user.Id,
                    userRole => userRole.UserId,
                    (user, userRole) => new { user, userRole })
                .Join(_context.Roles,
                    combined => combined.userRole.RoleId,
                    role => role.Id,
                    (combined, role) => new { combined.user, role })
                .Where(x => x.role.Name == "Evaluator" || x.role.Name == "Instructor" || x.role.Name == "Admin")
                .Select(x => x.user.Email)
                .Distinct()
                .ToListAsync();

            return evaluatorEmails.Where(e => !string.IsNullOrEmpty(e)).Cast<string>().ToList();
        }
    }
}