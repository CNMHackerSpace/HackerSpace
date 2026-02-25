// Copyright (c) 2025. All rights reserved.

using HackerSpace.Shared.Models;

namespace HackerSpace.Server.Services
{
    /// <summary>
    /// Service interface for badge view and submission operations.
    /// </summary>
    public interface IBadgeViewPageDataService
    {
        /// <summary>
        /// Gets a badge by its unique identifier.
        /// </summary>
        /// <param name="id">The badge ID.</param>
        /// <returns>The badge if found; otherwise, null.</returns>
        Task<Badge?> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates a new submission for a badge.
        /// </summary>
        /// <param name="submission">The submission to create.</param>
        Task CreateSubmissionAsync(Submission submission);

        /// <summary>
        /// Gets all evaluator email addresses for a given badge.
        /// </summary>
        /// <param name="badgeId">The badge ID.</param>
        /// <returns>A list of evaluator email addresses.</returns>
        Task<List<string>> GetEvaluatorEmailsAsync(Guid badgeId);
    }
}