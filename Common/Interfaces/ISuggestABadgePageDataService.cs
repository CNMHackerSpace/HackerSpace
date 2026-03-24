// Copyright (c) 2025. All rights reserved.

using Common.Models;

namespace Common.Interfaces
{
    /// <summary>
    /// Defines data operations for the Suggest a Badge feature.
    /// Provides methods to create, query, approve and reject <see cref="BadgeSuggestion"/> instances.
    /// </summary>
    public interface ISuggestABadgePageDataService
    {
        /// <summary>
        /// Asynchronously creates a new badge suggestion.
        /// </summary>
        /// <param name="suggestion">The badge suggestion to create.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous create operation.</returns>
        Task CreateSuggestionAsync(BadgeSuggestion suggestion);

        /// <summary>
        /// Asynchronously retrieves all badge suggestions.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. The task result contains
        /// a <see cref="List{BadgeSuggestion}"/> of all suggestions.
        /// </returns>
        Task<List<BadgeSuggestion>> GetAllSuggestionsAsync();

        /// <summary>
        /// Asynchronously retrieves all badge suggestions with a pending status.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. The task result contains
        /// a <see cref="List{BadgeSuggestion}"/> of pending suggestions.
        /// </returns>
        Task<List<BadgeSuggestion>> GetPendingSuggestionsAsync();

        /// <summary>
        /// Asynchronously retrieves all badge suggestions submitted by a specific user.
        /// </summary>
        /// <param name="userId">The identity user ID of the submitter.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. The task result contains
        /// a <see cref="List{BadgeSuggestion}"/> of the user's suggestions.
        /// </returns>
        Task<List<BadgeSuggestion>> GetSuggestionsByUserIdAsync(string userId);

        /// <summary>
        /// Asynchronously retrieves a badge suggestion by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the suggestion.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. The task result contains
        /// the <see cref="BadgeSuggestion"/> if found; otherwise <c>null</c>.
        /// </returns>
        Task<BadgeSuggestion?> GetByIdAsync(Guid id);

        /// <summary>
        /// Asynchronously updates an existing badge suggestion.
        /// </summary>
        /// <param name="suggestion">The suggestion instance containing updated values.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous update operation.</returns>
        Task UpdateSuggestionAsync(BadgeSuggestion suggestion);

        /// <summary>
        /// Asynchronously approves a badge suggestion, creating a new hidden <see cref="Badge"/> from its data.
        /// </summary>
        /// <param name="id">The unique identifier of the suggestion to approve.</param>
        /// <param name="reviewerId">The identity user ID of the admin approving the suggestion.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous approve operation.</returns>
        Task ApproveSuggestionAsync(Guid id, string reviewerId);

        /// <summary>
        /// Asynchronously approves a badge suggestion, creating a new hidden <see cref="Badge"/> from the provided edited values.
        /// The original suggestion record is preserved unchanged for audit history.
        /// </summary>
        /// <param name="id">The unique identifier of the suggestion to approve.</param>
        /// <param name="reviewerId">The identity user ID of the admin approving the suggestion.</param>
        /// <param name="title">The edited title for the new badge.</param>
        /// <param name="description">The edited description for the new badge.</param>
        /// <param name="turnInInstructions">The edited turn-in instructions for the new badge.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous approve operation.</returns>
        Task ApproveSuggestionAsync(Guid id, string reviewerId, string title, string description, string turnInInstructions);

        /// <summary>
        /// Asynchronously rejects a badge suggestion with optional admin notes.
        /// </summary>
        /// <param name="id">The unique identifier of the suggestion to reject.</param>
        /// <param name="reviewerId">The identity user ID of the admin rejecting the suggestion.</param>
        /// <param name="notes">Optional notes explaining the rejection reason.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous reject operation.</returns>
        Task RejectSuggestionAsync(Guid id, string reviewerId, string? notes);
    }
}
