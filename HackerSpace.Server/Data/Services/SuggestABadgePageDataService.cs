// Copyright (c) 2025. All rights reserved.

using Microsoft.EntityFrameworkCore;
using HackerSpace.Shared.Interfaces;
using HackerSpace.Shared.Models;

namespace HackerSpace.Data.Services
{
    /// <summary>
    /// Provides data access methods for managing badge suggestions.
    /// </summary>
    public class SuggestABadgePageDataService : ISuggestABadgePageDataService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SuggestABadgePageDataService"/> class.
        /// </summary>
        /// <param name="factory">The factory to create <see cref="ApplicationDbContext"/> instances.</param>
        public SuggestABadgePageDataService(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Adds a new badge suggestion asynchronously.
        /// </summary>
        /// <param name="suggestion">The <see cref="BadgeSuggestion"/> to add.</param>
        public async Task AddAsync(BadgeSuggestion suggestion)
        {
            using var context = _factory.CreateDbContext();
            suggestion.Id = Guid.NewGuid();
            context.BadgeSuggestions.Add(suggestion);
            await context.SaveChangesAsync();
        }
    }
}
