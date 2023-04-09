using Server.Data.Interfaces;
using Shared.Models;

namespace Server.Data.Mocks
{
    public class UserRolesRepoMock : IUserRolesRepo
    {
        List<UserRole> _usserRoles;
        public UserRolesRepoMock()
        {
            _usserRoles = new List<UserRole>
            {
                new UserRole()
                {
                    Id = 1,
                    Role = Role.Admin,
                    uid = "0cc9593b-4d4a-4a5c-a508-71313e9b11b0"
                }
            };
        }
        public async Task AddAsync(UserRole userProfile)
        {
            _usserRoles.Add(userProfile);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            _usserRoles.Remove(_usserRoles.Where(ur => ur.Id == id).FirstOrDefault());
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            return await Task.FromResult(_usserRoles);
        }

        public async Task<IEnumerable<Role>> GetAllByUidAsync(string uid)
        {
            return await Task.FromResult(_usserRoles.Where(ur=>ur.uid == uid).Select(ur=>ur.Role));
        }

        public async Task UpdateAsync(UserRole userProfile)
        {
            var currentUserRole = _usserRoles.FirstOrDefault(ur => ur.Id == userProfile.Id);
            if (currentUserRole != null)
            {
                currentUserRole.uid = userProfile.uid;
                currentUserRole.Role = userProfile.Role;
            }
            await Task.CompletedTask;
        }
    }
}
