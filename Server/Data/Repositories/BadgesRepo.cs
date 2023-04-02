using Microsoft.EntityFrameworkCore;
using Server.Data.Interfaces;
using Shared.Models;

namespace Server.Data.Repositories
{
    public class BadgesRepo : IBadgesRepo
    {
        ApplicationDbContext _dbContext;
        public BadgesRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;            
        }
        public async Task AddAsync(Badge badge)
        {
            _dbContext.Badges.Add(badge);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = _dbContext.Badges.FirstOrDefault(item => item.Id == id);
            _dbContext.Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Badge>> GetAllAsync()
        {
            return await _dbContext.Badges.ToListAsync();
        }

        public async Task<Badge?> GetByIdAsync(int id)
        {
            return await _dbContext.Badges.FirstOrDefaultAsync(badge => badge.Id == id);
        }

        public async Task UpdateAsync(Badge badge)
        {
            var currentBadge = _dbContext.Badges.FirstOrDefault(item => item.Id == badge.Id);
            currentBadge.Title = badge.Title;
            currentBadge.Description = badge.Description;
            currentBadge.Title = badge.Title;
            currentBadge.FileName = badge.FileName;
            await _dbContext.SaveChangesAsync();
        }
    }
}
