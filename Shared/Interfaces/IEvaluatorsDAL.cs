namespace SharedClasses.Interfaces
{
    public interface IEvaluatorsDAL
    {
        public Task AddEvaluatorsForBadgeAsync(int badgeId, IEnumerable<string> evaluators);
    }
}
