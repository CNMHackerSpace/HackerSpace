using Hackerspace.Shared.Models;

namespace Data.Interfaces
{
    public interface IBadgesRepo
    {
        public Task<IEnumerable<Badge>> GetBadgesAsync();
        public Task<Badge?> GetBadgeAsync(int id);
        public Task<Badge?> AddBadgeAsync(Badge badge);
        public Task UpdateBadgeAsync(Badge badge);
        public Task DeleteBadgeAsync(int id);
    }
}
