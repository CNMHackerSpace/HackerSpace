using Shared.Models;

namespace Server.Data.Interfaces
{
    public interface IBadgesRepo
    {
        public Task<IEnumerable<Badge>> GetAllAsync();
        public Task<Badge?> GetByIdAsync(int id);
        public Task AddAsync(Badge badge);
        public Task UpdateAsync(Badge badge);
        public Task DeleteAsync(int id);
    }
}
