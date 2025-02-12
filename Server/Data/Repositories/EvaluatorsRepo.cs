using Microsoft.EntityFrameworkCore;
using Server.Data.Interfaces;
using Shared.Models;

namespace Server.Data.Repositories
{
    public class EvaluatorsRepo : IEvaluatorsRepo
    {
        ApplicationDbContext _dbContext;
        public EvaluatorsRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Evaluator item)
        {
            _dbContext.Evaluators.Add(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddEvaluatorsForBadgeAsync(int badgeId, IEnumerable<string> evaluators)
        {
            //Remove evaluators that are not listed
            var evaluatorsToRemove = _dbContext.Evaluators.Where(e=>e.BadgeId==badgeId && !evaluators.Contains(e.UserId)).ToList();
            foreach(var evaluator in evaluatorsToRemove)
            {
                _dbContext.Remove(evaluator);
                await _dbContext.SaveChangesAsync();
            }
            //Add evaluators not in the list yet
            var evaluatorsToAdd = _dbContext.Evaluators.Where(e=>e.BadgeId == badgeId).Select(e=>e.UserId).ToList();
            foreach (var evaluator in evaluators)
            {
                if(!evaluatorsToAdd.Contains(evaluator))
                {
                    _dbContext.Add(new Evaluator { UserId=evaluator, BadgeId=badgeId});
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            var item = _dbContext.Evaluators.FirstOrDefault(item => item.Id == id);
            if (item != null)
            {
                _dbContext.Remove(item);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Evaluator>> GetAllAsync()
        {
            return await _dbContext.Evaluators.ToListAsync();
        }

        public async Task<IEnumerable<Evaluator>> GetAllByBadgeIdAsync(int id)
        {
            return await _dbContext.Evaluators.Where(e=>e.BadgeId==id).ToListAsync();
        }

        public async Task<Evaluator?> GetByIdAsync(int id)
        {
            return await _dbContext.Evaluators.FirstOrDefaultAsync(badge => badge.Id == id);
        }

        public async Task UpdateAsync(Evaluator evaluator)
        {
            var currentEvaluator = _dbContext.Evaluators.FirstOrDefault(item => item.Id == evaluator.Id);
            if (currentEvaluator != null)
            {
                currentEvaluator.BadgeId = evaluator.BadgeId;
                currentEvaluator.UserId = evaluator.UserId;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
