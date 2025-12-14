// Copyright (c) 2025. All rights reserved.

using Microsoft.EntityFrameworkCore;
using HackerSpace.Shared.Interfaces;
using HackerSpace.Shared.Models;

namespace HackerSpace.Data.Services
{
    /// <summary>
    /// Provides data access methods for managing badges on the badges page.
    /// </summary>
    public class BadgesPageDataService : IBadgesPageDataService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="BadgesPageDataService"/> class.
        /// </summary>
        /// <param name="factory">The factory to create <see cref="ApplicationDbContext"/> instances.</param>
        public BadgesPageDataService(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Gets all badges asynchronously.
        /// </summary>
        /// <returns>A list of all <see cref="Badge"/> objects.</returns>
        public async Task<List<Badge>> GetAllAsync()
        {
            using var context = _factory.CreateDbContext();
            return await context.Badges.ToListAsync();
        }

        /// <summary>
        /// Gets all visible badges asynchronously.
        /// </summary>
        /// <returns>A list of all visible <see cref="Badge"/> objects.</returns>
        public async Task<List<Badge>> GetVisibleAsync()
        {
            using var context = _factory.CreateDbContext();
            return await context.Badges.Where(b => b.IsVisible).ToListAsync();
        }

        /// <summary>
        /// Adds a new badge asynchronously.
        /// </summary>
        /// <param name="badge">The <see cref="Badge"/> to add.</param>
        public async Task AddAsync(Badge badge)
        {
            using var context = _factory.CreateDbContext();
            badge.Id = Guid.NewGuid(); // ensure ID is created if not provided
            context.Badges.Add(badge);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing badge asynchronously.
        /// </summary>
        /// <param name="badge">The <see cref="Badge"/> to update.</param>
        public async Task UpdateAsync(Badge badge)
        {
            using var context = _factory.CreateDbContext();

            var existing = await context.Badges.FirstOrDefaultAsync(x => x.Id == badge.Id);
            if (existing == null)
                throw new InvalidOperationException("Badge not found");

            context.Entry(existing).CurrentValues.SetValues(badge);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes a badge by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the badge to remove.</param>
        public async Task RemoveAsync(Guid id)
        {
            using var context = _factory.CreateDbContext();

            var badge = await context.Badges.FirstOrDefaultAsync(b => b.Id == id);
            if (badge == null)
                throw new InvalidOperationException("Badge not found");

            context.Badges.Remove(badge);
            await context.SaveChangesAsync();
        }
    }
}
