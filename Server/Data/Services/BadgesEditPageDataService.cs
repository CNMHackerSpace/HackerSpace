// Copyright (c) 2025. All rights reserved.

using Common.Interfaces;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Data.Services
{
    /// <summary>
    /// Provides data services for editing badges, including retrieval by identifier.
    /// </summary>
    public class BadgesEditPageDataService : IBadgeEditPageDataService
    {
        private readonly ApplicationDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BadgesEditPageDataService"/> class.
        /// </summary>
        /// <param name="dbContext">The application's database context.</param>
        public BadgesEditPageDataService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Retrieves a <see cref="Badge"/> by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the badge.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Badge"/>.</returns>
        public Task<Badge> GetByIdAsync(Guid id)
        {
            return this.dbContext.Badges.FirstAsync(badge => badge.Id == id);
        }

        /// <summary>
        /// Adds a new <see cref="Badge"/> or updates an existing one asynchronously.
        /// </summary>
        /// <param name="badge">The <see cref="Badge"/> to add or update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task AddOrUpdateAsync(Badge badge)
        {
            if (badge.Id == Guid.Empty)
            {
                badge.Id = Guid.NewGuid();
                this.dbContext.Badges.Add(badge);
            }
            else
            {
                this.dbContext.Badges.Update(badge);
            }

            return this.dbContext.SaveChangesAsync();
        }
    }
}
