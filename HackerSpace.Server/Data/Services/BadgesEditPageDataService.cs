// Copyright (c) 2025. All rights reserved.

using HackerSpace.Data;
using HackerSpace.Shared.Interfaces;
using HackerSpace.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HackerSpace.Server.Data.Services
{
    /// <summary>
    /// Provides data services for editing badges, including retrieval by identifier.
    /// </summary>
    public class BadgesEditPageDataService : IBadgeEditPageDataService
    {
        private readonly ApplicationDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BadgesEditPageDataService"/> class.
        /// </summary>
        /// <param name="dbContext">The application's database context.</param>
        public BadgesEditPageDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retrieves a <see cref="Badge"/> by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the badge.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Badge"/>.</returns>
        public Task<Badge> GetByIdAsync(Guid id)
        {
            return _dbContext.Badges.FirstAsync(badge => badge.Id == id);
        }

        /// <summary>
        /// Adds a new <see cref="Badge"/> or updates an existing one asynchronously.
        /// </summary>
        /// <param name="badge">The <see cref="Badge"/> to add or update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task AddOrUpdateAsync(Badge badge)
        {
            if (badge.Id == null || badge.Id == Guid.Empty)
            {
                badge.Id = Guid.NewGuid();
                _dbContext.Badges.Add(badge);
            }
            else
            {
                _dbContext.Badges.Update(badge);
            }   
            return _dbContext.SaveChangesAsync();
        }
    }
}
