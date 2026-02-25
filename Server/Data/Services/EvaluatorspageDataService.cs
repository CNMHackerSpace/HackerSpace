// Copyright (c) 2025. All rights reserved.

using Common.Interfaces;
using Common.Models;
using Common.ViewModels;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Data.Services
{
    /// <summary>
    /// Provides data services for the Evaluators page, including CRUD operations for <see cref="Evaluator"/> entities
    /// and retrieval of the <see cref="EvaluatorsPageVM"/> view model.
    /// </summary>
    public class EvaluatorspageDataService : IEvaluatorsPageDataService
    {
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EvaluatorspageDataService"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public EvaluatorspageDataService(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <inheritdoc />
        public async Task<List<Evaluator>?> GetAllAsync()
        {
            return await this.context.Evaluators.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Evaluator?> GetAsync(Guid id)
        {
            return await this.context.Evaluators.FindAsync(id);
        }

        /// <inheritdoc />
        public async Task<EvaluatorsPageVM> GetEvaluatorsPageVMAsync()
        {
            var evaluators = await this.context.Evaluators.ToListAsync();
            var users = await this.context.Users.ToListAsync();
            return new EvaluatorsPageVM
            {
                Evaluators = evaluators,
                Users = users,
            };
        }

        /// <inheritdoc />
        public async Task AddAsync(Evaluator evaluator)
        {
            evaluator.Id = Guid.NewGuid();
            this.context.Evaluators.Add(evaluator);
            await this.context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateAsync(Evaluator evaluator)
        {
            var currentEvaluator = await this.context.Evaluators.FindAsync(evaluator.Id);
            if (currentEvaluator == null)
            {
                throw new Exception("Evaluator to update not found");
            }

            currentEvaluator.UserId = evaluator.UserId;
            currentEvaluator.User = evaluator.User;
            currentEvaluator.NotificationEmail = evaluator.NotificationEmail;
            await this.context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task RemoveAsync(Guid id)
        {
            var currentEvaluator = await this.context.Evaluators.FindAsync(id);
            if (currentEvaluator == null)
            {
                throw new Exception("Evaluator to delete not found");
            }

            this.context.Evaluators.Remove(currentEvaluator);
            await this.context.SaveChangesAsync();
        }
    }
}
