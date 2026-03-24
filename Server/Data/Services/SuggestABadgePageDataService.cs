// Copyright (c) 2025. All rights reserved.

using Common.Interfaces;
using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Server.Data.Services
{
    /// <summary>
    /// Provides data access methods for managing badge suggestions.
    /// </summary>
    public class SuggestABadgePageDataService : ISuggestABadgePageDataService
    {
        private readonly IDbContextFactory<ApplicationDbContext> factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SuggestABadgePageDataService"/> class.
        /// </summary>
        /// <param name="factory">The factory to create <see cref="ApplicationDbContext"/> instances.</param>
        public SuggestABadgePageDataService(IDbContextFactory<ApplicationDbContext> factory)
        {
            this.factory = factory;
        }

        /// <inheritdoc />
        public async Task CreateSuggestionAsync(BadgeSuggestion suggestion)
        {
            using var context = this.factory.CreateDbContext();
            suggestion.Id = Guid.NewGuid();
            suggestion.CreatedAt = DateTime.UtcNow;
            suggestion.Status = BadgeSuggestionStatus.Pending;
            context.BadgeSuggestions.Add(suggestion);
            await context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<List<BadgeSuggestion>> GetAllSuggestionsAsync()
        {
            using var context = this.factory.CreateDbContext();
            return await context.BadgeSuggestions
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<List<BadgeSuggestion>> GetPendingSuggestionsAsync()
        {
            using var context = this.factory.CreateDbContext();
            return await context.BadgeSuggestions
                .Where(s => s.Status == BadgeSuggestionStatus.Pending)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<List<BadgeSuggestion>> GetSuggestionsByUserIdAsync(string userId)
        {
            using var context = this.factory.CreateDbContext();
            return await context.BadgeSuggestions
                .Where(s => s.SuggestedById == userId)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<BadgeSuggestion?> GetByIdAsync(Guid id)
        {
            using var context = this.factory.CreateDbContext();
            return await context.BadgeSuggestions
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        /// <inheritdoc />
        public async Task UpdateSuggestionAsync(BadgeSuggestion suggestion)
        {
            using var context = this.factory.CreateDbContext();

            var existing = await context.BadgeSuggestions.FirstOrDefaultAsync(s => s.Id == suggestion.Id);
            if (existing == null)
            {
                throw new InvalidOperationException("Badge suggestion not found");
            }

            context.Entry(existing).CurrentValues.SetValues(suggestion);
            await context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task ApproveSuggestionAsync(Guid id, string reviewerId)
        {
            using var context = this.factory.CreateDbContext();

            var suggestion = await context.BadgeSuggestions.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            if (suggestion == null)
            {
                throw new InvalidOperationException("Badge suggestion not found");
            }

            await this.ApproveSuggestionAsync(id, reviewerId, suggestion.Title, suggestion.Description, suggestion.TurnInInstructions);
        }

        /// <inheritdoc />
        public async Task ApproveSuggestionAsync(Guid id, string reviewerId, string title, string description, string turnInInstructions)
        {
            using var context = this.factory.CreateDbContext();

            var suggestion = await context.BadgeSuggestions.FirstOrDefaultAsync(s => s.Id == id);
            if (suggestion == null)
            {
                throw new InvalidOperationException("Badge suggestion not found");
            }

            suggestion.Status = BadgeSuggestionStatus.Approved;
            suggestion.ReviewedAt = DateTime.UtcNow;
            suggestion.ReviewedById = reviewerId;

            var badge = new Badge
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                TurnInInstructions = turnInInstructions,
                IsVisible = false,
            };

            context.Badges.Add(badge);
            await context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task RejectSuggestionAsync(Guid id, string reviewerId, string? notes)
        {
            using var context = this.factory.CreateDbContext();

            var suggestion = await context.BadgeSuggestions.FirstOrDefaultAsync(s => s.Id == id);
            if (suggestion == null)
            {
                throw new InvalidOperationException("Badge suggestion not found");
            }

            suggestion.Status = BadgeSuggestionStatus.Rejected;
            suggestion.ReviewedAt = DateTime.UtcNow;
            suggestion.ReviewedById = reviewerId;
            suggestion.AdminNotes = notes;
            await context.SaveChangesAsync();
        }
    }
}
