// Copyright (c) CNM. All rights reserved.

using HackerSpace.Shared.Models;

namespace HackerSpace.Shared.Interfaces
{
    /// <summary>
    /// Defines data operations for the Badges page.
    /// Implementations provide methods to query, add, update and remove <see cref="Badge"/> instances.
    /// </summary>
    public interface IBadgesPageDataService
    {
        /// <summary>
        /// Asynchronously retrieves all badges.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. The task result contains
        /// a <see cref="List{Badge}"/> of badges, or <c>null</c> if none are available.
        /// </returns>
        public Task<List<Badge>> GetAllAsync();

        /// <summary>
        /// Asynchronously updates the provided badge.
        /// </summary>
        /// <param name="badge">The badge instance containing updated values.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous update operation.</returns>
        public Task UpdateAsync(Badge badge);

        /// <summary>
        /// Asynchronously adds a new badge.
        /// </summary>
        /// <param name="badge">The badge to add.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous add operation.</returns>
        public Task AddAsync(Badge badge);

        /// <summary>
        /// Asynchronously removes the badge with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the badge to remove.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous remove operation.</returns>
        public Task RemoveAsync(Guid id);
    }
}
