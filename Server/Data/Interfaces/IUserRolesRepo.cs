using Shared.Models;

namespace Server.Data.Interfaces
{
    public interface IUserRolesRepo
    {
        public Task<IEnumerable<UserRole>> GetAllAsync();
        public Task<IEnumerable<Role>> GetAllByUidAsync(string uid);
        public Task AddAsync(UserRole userProfile);
        public Task UpdateAsync(UserRole userProfile);
        public Task DeleteAsync(int id);
    }
}
