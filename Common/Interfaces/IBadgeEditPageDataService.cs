// Copyright (c) 2025. All rights reserved.

using HackerSpace.Shared.Models;

namespace HackerSpace.Shared.Interfaces
{
    /// <summary>
    /// Provides data services for editing badges.
    /// </summary>
    public interface IBadgeEditPageDataService
    {
        /// <summary>
        /// Gets a <see cref="Badge"/> by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the badge.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Badge"/>.</returns>
        public Task<Badge> GetByIdAsync(Guid id);

        /// <summary>
        /// Adds a new <see cref="Badge"/> or updates an existing one.
        /// </summary>
        /// <param name="badge">The <see cref="Badge"/> to add or update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task AddOrUpdateAsync(Badge badge);
    }
}
