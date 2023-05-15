using Shared.Models;

namespace Server.Data.Interfaces
{
    public interface IEvaluatorsRepo
    {
        public Task AddEvaluatorsForBadgeAsync(int badgeId, IEnumerable<string> evaluators);
    }
}
