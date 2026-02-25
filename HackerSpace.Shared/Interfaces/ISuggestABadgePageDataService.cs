// Copyright (c) 2025. All rights reserved.

using HackerSpace.Shared.Models;

namespace HackerSpace.Shared.Interfaces
{
    /// <summary>
    /// Defines data operations for the Suggest a Badge page.
    /// </summary>
    public interface ISuggestABadgePageDataService
    {
        /// <summary>
        /// Asynchronously adds a new badge suggestion.
        /// </summary>
        /// <param name="suggestion">The badge suggestion to add.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous add operation.</returns>
        Task AddAsync(BadgeSuggestion suggestion);
    }
}
