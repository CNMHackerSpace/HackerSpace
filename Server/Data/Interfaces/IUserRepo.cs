using Shared.Models;

namespace Server.Data.Interfaces
{
    public interface IUserRepo
    {
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<User?> GetByIdAsync(int id);
        public Task<User?> AddAsync(User badge);
        public Task UpdateAsync(User badge);
        public Task DeleteAsync(int id);
        public Task<User?> GetByUidAsync(string uid);
    }
}
