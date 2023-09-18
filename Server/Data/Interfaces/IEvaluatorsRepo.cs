using Shared.Models;

namespace Server.Data.Interfaces
{
    public interface IEvaluatorsRepo
    {
        Task AddAsync(Evaluator item);
        Task<Evaluator> AddEvaluatorAsync(Evaluator evaluator);
        Task AddEvaluatorsForBadgeAsync(int badgeId, IEnumerable<string> evaluators);
        Task DeleteAsync(int id);
        Task<IEnumerable<Evaluator>> GetAllAsync();
        Task<IEnumerable<Evaluator>> GetAllByBadgeIdAsync(int id);
        Task<Evaluator?> GetByIdAsync(int id);
        Task UpdateAsync(Evaluator evaluator);
    }
}
