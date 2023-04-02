using Shared.Models;

namespace Server.Data.Interfaces
{
    public interface IUserRepo
    {
        public Task<IEnumerable<UserProfile>> GetAllAsync();
        public Task<UserProfile?> GetByIdAsync(int id);
        public Task AddAsync(UserProfile badge);
        public Task UpdateAsync(UserProfile badge);
        public Task DeleteAsync(int id);
        public Task<UserProfile?> GetByUidAsync(string uid);
    }
}
