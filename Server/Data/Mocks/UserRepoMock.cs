using Server.Data.Interfaces;
using Shared.Models;

namespace Server.Data.Mocks
{
    public class UserRepoMock : IUserRepo
    {
        private List<User> _users;
        private int _count = 0;
        public UserRepoMock() 
        {
            _users = Enumerable.Range(1, 5).Select(index => new User
            {
                Id = ++_count,
                UID = Guid.NewGuid().ToString(),
                Name = $"Person{_count}",
                Email = $"Person{_count}@aserver.net"
            })
            .ToList();
        }
        
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Task.FromResult(_users); 
        }
        
        public async Task<User?> GetByIdAsync(int id)
        {
            return await Task.FromResult(_users.Where(item => item.Id == id).FirstOrDefault());
        }

        public Task UpdateAsync(User badge)
        {
            User? currentUser = _users.Where(item => item.Id == badge.Id).FirstOrDefault();
            if (currentUser != null)
            {
                badge.UID = currentUser.UID;
                badge.Name = currentUser.Name;
                badge.Email = currentUser.Email;   
            }
            return Task.CompletedTask;
        }

        public Task<User?> AddAsync(User badge)
        {
            badge.Id = ++_count;
            _users.Add(badge);
            return Task.FromResult((User?)badge);
        }

        public Task DeleteAsync(int id)
        {
            User? badge = _users.Where(item => item.Id == id).FirstOrDefault();
            if (badge != null)
            {
                _users.Remove(badge);
            }
            return Task.CompletedTask;
        }
    }
}
