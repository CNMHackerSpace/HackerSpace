using HackerSpace.Shared.Models;

namespace HackerSpace.Shared.Interfaces
{
    public interface IBadgesPageDataService
    {
        public Task<List<Badge>> GetAllAsync();
        public Task UpdateAsync(Badge badge);
        public Task AddAsync(Badge badge);
        public Task RemoveAsync(Guid id);
    }
}
