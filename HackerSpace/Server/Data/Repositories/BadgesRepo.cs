using Data.Interfaces;
using Hackerspace.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HackerSpace.Server.Data.Repositories
{
    public class BadgesRepo : IBadgesRepo
    {
        private ApplicationDbContext _dbContext;

        public BadgesRepo(ApplicationDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        public async Task<Badge?> AddBadgeAsync(Badge badge)
        {
            var result = _dbContext.Badges.Add(badge);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteBadgeAsync(int id)
        {
            var item = _dbContext.Badges.Where(item => item.Id == id).FirstOrDefaultAsync();
            _dbContext.Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Badge?> GetBadgeAsync(int id)
        {
            return await _dbContext.Badges.Where(item => item.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Badge>> GetBadgesAsync()
        {
            return await _dbContext.Badges.ToListAsync();
        }

        public async Task UpdateBadgeAsync(Badge badge)
        {
            var currentItem = await _dbContext.Badges.FirstOrDefaultAsync(item=>item.Id == badge.Id);
            if (currentItem != null)
            {
                currentItem.Title = badge.Title;
                currentItem.Description = badge.Description;   
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
