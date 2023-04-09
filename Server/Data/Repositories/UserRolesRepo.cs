using Microsoft.EntityFrameworkCore;
using Server.Data.Interfaces;
using Shared.Models;

namespace Server.Data.Repositories
{
    public class UserRolesRepo : IUserRolesRepo
    {
        ApplicationDbContext _dbContext;

        public UserRolesRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext; 
        }
        public async Task AddAsync(UserRole userProfile)
        {
            _dbContext.Add(userProfile);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _dbContext.UserRoles.Remove(_dbContext.UserRoles.Where(ur => ur.Id == id).FirstOrDefault());
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            return await _dbContext.UserRoles.ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetAllByUidAsync(string uid)
        {
            var roles = await _dbContext.UserRoles.Where(ur => ur.uid == uid).Select(ur=>ur.Role).ToListAsync();
            return roles;
        }

        public async Task UpdateAsync(UserRole userProfile)
        {
            var currentUserRole = _dbContext.UserRoles.FirstOrDefault(ur => ur.Id == userProfile.Id);
            if (currentUserRole != null)
            {
                currentUserRole.uid = userProfile.uid;
                currentUserRole.Role = userProfile.Role;
            }
            await Task.CompletedTask;
        }
    }
}
