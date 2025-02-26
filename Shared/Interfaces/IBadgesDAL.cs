using SharedClasses.Models;

namespace SharedClasses.Interfaces
{
    public interface IBadgesDAL
    {
        public Task<List<HackerspaceBadge>> GetAllAsync();
        public Task<HackerspaceBadge?> GetByIdAsync(int id);
        public Task AddAsync(HackerspaceBadge badge);
        public Task UpdateAsync(HackerspaceBadge badge);
        public Task DeleteAsync(int id);
    }
}
