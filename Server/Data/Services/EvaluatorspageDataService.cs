// Copyright (c) 2025. All rights reserved.

using HackerSpace.Data;
using HackerSpace.Shared.Interfaces;
using HackerSpace.Shared.Models;
using HackerSpace.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HackerSpace.Server.Data.Services
{
    /// <summary>
    /// Provides data services for the Evaluators page, including CRUD operations for <see cref="Evaluator"/> entities
    /// and retrieval of the <see cref="EvaluatorsPageVM"/> view model.
    /// </summary>
    public class EvaluatorspageDataService : IEvaluatorsPageDataService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EvaluatorspageDataService"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public EvaluatorspageDataService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<List<Evaluator>?> GetAllAsync()
        {
            return await _context.Evaluators.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Evaluator?> GetAsync(Guid id)
        {
            return await _context.Evaluators.FindAsync(id);
        }

        /// <inheritdoc />
        public async Task<EvaluatorsPageVM> GetEvaluatorsPageVMAsync()
        {            
            var evaluators = await _context.Evaluators.ToListAsync();
            var users = await _context.Users.ToListAsync();
            return new EvaluatorsPageVM
            {
                Evaluators = evaluators,
                Users = users
            };
        }

        /// <inheritdoc />
        public async Task AddAsync(Evaluator evaluator)
        {
            evaluator.Id = Guid.NewGuid();
            _context.Evaluators.Add(evaluator);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateAsync(Evaluator evaluator)
        {
            var currentEvaluator = await _context.Evaluators.FindAsync(evaluator.Id);
            if (currentEvaluator == null)
            {
                throw new Exception("Evaluator to update not found");
            }
            currentEvaluator.UserId = evaluator.UserId;
            currentEvaluator.User = evaluator.User;
            currentEvaluator.NotificationEmail = evaluator.NotificationEmail;
            await _context.SaveChangesAsync();

        }

        /// <inheritdoc />
        public async Task RemoveAsync(Guid id)
        {
            var currentEvaluator = await _context.Evaluators.FindAsync(id);
            if (currentEvaluator == null)
            {
                throw new Exception("Evaluator to delete not found");
            }
            _context.Evaluators.Remove(currentEvaluator);
            await _context.SaveChangesAsync();
        }
    }
}
