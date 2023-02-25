using Data.Models;

namespace Data.Interfaces
{
    public interface IBadgesRepo
    {
        public Task<IEnumerable<Badge>> GetBadgesAsync();
    }
}
