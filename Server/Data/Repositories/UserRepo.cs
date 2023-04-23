//using Microsoft.EntityFrameworkCore;
//using Server.Data.Interfaces;
//using Shared.Models;

//namespace Server.Data.Repositories
//{
//    public class UserRepo : IUserRepo
//    {
//        private readonly ApplicationDbContext _dbContext;
//        public UserRepo(ApplicationDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }
//        public async Task AddAsync(UserProfile userProfile)
//        {
//            _dbContext.UserProfiles.Add(userProfile);
//            await _dbContext.SaveChangesAsync();
//        }

//        public async Task DeleteAsync(int id)
//        {
//            _dbContext.Remove(_dbContext.UserProfiles.Where(p => p.Id == id));
//            await _dbContext.SaveChangesAsync();
//        }

//        public async Task<IEnumerable<UserProfile>> GetAllAsync()
//        {
//            return await _dbContext.UserProfiles.ToListAsync();
//        }

//        public async Task<UserProfile?> GetByIdAsync(int id)
//        {
//            return await _dbContext.UserProfiles.Where(p=>p.Id == id).FirstOrDefaultAsync();
//        }

//        public async Task<UserProfile?> GetByUidAsync(string uid)
//        {
//            return await _dbContext.UserProfiles.Where(p=>p.UID == uid).FirstOrDefaultAsync();
//        }

//        public async Task UpdateAsync(UserProfile userProfile)
//        {
//            var currentUserProfile = await _dbContext.UserProfiles.Where(p =>p.Id == userProfile.Id).FirstOrDefaultAsync();
//            if (currentUserProfile != null)
//            {
//                currentUserProfile.UID = userProfile.UID;
//                currentUserProfile.Name = userProfile.Name;
//                currentUserProfile.Email = userProfile.Email;
//                await _dbContext.SaveChangesAsync();
//            }
//        }
//    }
//}
