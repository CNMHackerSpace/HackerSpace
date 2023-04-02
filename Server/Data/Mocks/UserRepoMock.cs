using Server.Data.Interfaces;
using Shared.Models;

namespace Server.Data.Mocks
{
    public class UserRepoMock : IUserRepo
    {
        private List<UserProfile> _users;
        private int _count = 0;
        public UserRepoMock() 
        {
            _users = Enumerable.Range(1, 5).Select(index => new UserProfile
            {
                Id = ++_count,
                UID = Guid.NewGuid().ToString(),
                Name = $"Person{_count}",
                Email = $"Person{_count}@aserver.net"
            })
            .ToList();
            UserProfile user = new UserProfile();
            user.Name = "Rob";
            user.Email = "rgarner011235@gmail.com";
            user.UID = "0cc9593b-4d4a-4a5c-a508-71313e9b11b0";
            _users.Add(user);
        }
        
        public async Task<IEnumerable<UserProfile>> GetAllAsync()
        {
            return await Task.FromResult(_users); 
        }
        
        public async Task<UserProfile?> GetByIdAsync(int id)
        {
            return await Task.FromResult(_users.Where(item => item.Id == id).FirstOrDefault());
        }

        public Task UpdateAsync(UserProfile userProfile)
        {
            UserProfile? currentUser = _users.Where(item => item.Id == userProfile.Id).FirstOrDefault();
            if (currentUser != null)
            {
                userProfile.UID = currentUser.UID;
                userProfile.Name = currentUser.Name;
                userProfile.Email = currentUser.Email;   
            }
            return Task.CompletedTask;
        }

        public Task AddAsync(UserProfile userProfile)
        {
            userProfile.Id = ++_count;
            _users.Add(userProfile);
            return Task.FromResult((UserProfile?)userProfile);
        }

        public Task DeleteAsync(int id)
        {
            UserProfile? userProfile = _users.Where(item => item.Id == id).FirstOrDefault();
            if (userProfile != null)
            {
                _users.Remove(userProfile);
            }
            return Task.CompletedTask;
        }

        public async Task<UserProfile?> GetByUidAsync(string uid)
        {
            var result = await Task.FromResult(_users.Where(item => item.UID == uid).FirstOrDefault());
            return result;
        }
    }
}
