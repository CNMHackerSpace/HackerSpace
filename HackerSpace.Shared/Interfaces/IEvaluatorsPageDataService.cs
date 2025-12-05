// Copyright (c) CNM. All rights reserved.

using HackerSpace.Shared.Models;
using HackerSpace.Shared.ViewModels;

namespace HackerSpace.Shared.Interfaces
{
    /// <summary>
    /// Defines data operations for the Evaluators page.
    /// Provides methods to query, add, update and remove <see cref="Evaluator"/> instances.
    /// </summary>
    public interface IEvaluatorsPageDataService
    {
        /// <summary>
        /// Asynchronously retrieves the view model for the Evaluators page, including evaluators and users.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. The task result contains
        /// an <see cref="EvaluatorsPageVM"/> with the data for the Evaluators page.
        /// </returns>
        public Task<EvaluatorsPageVM> GetEvaluatorsPageVMAsync();

        /// <summary>
        /// Asynchronously retrieves all evaluators.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. The task result contains
        /// a <see cref="List{Evaluator}"/> of evaluators, or <c>null</c> if none are available.
        /// </returns>
        public Task<List<Evaluator>?> GetAllAsync();

        /// <summary>
        /// Asynchronously retrieves a single evaluator by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the evaluator to retrieve.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. The task result contains
        /// the <see cref="Evaluator"/> if found; otherwise <c>null</c>.
        /// </returns>
        public Task<Evaluator?> GetAsync(string id);

        /// <summary>
        /// Asynchronously updates the provided evaluator.
        /// </summary>
        /// <param name="evaluator">The evaluator instance containing updated values.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous update operation.</returns>
        public Task UpdateAsync(Evaluator evaluator);

        /// <summary>
        /// Asynchronously adds a new evaluator.
        /// </summary>
        /// <param name="evaluator">The evaluator to add.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous add operation.</returns>
        public Task AddAsync(Evaluator evaluator);

        /// <summary>
        /// Asynchronously removes the evaluator with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the evaluator to remove.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous remove operation.</returns>
        public Task RemoveAsync(string id);
    }
}
