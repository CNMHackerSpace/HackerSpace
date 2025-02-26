using Microsoft.EntityFrameworkCore;
using Server.Data;
using SharedClasses.Interfaces;
using SharedClasses.Models;

namespace Server.Services.DALs
{
    public class BadgesDAL: IBadgesDAL
    {
        ApplicationDbContext _dbContext;
        public BadgesDAL(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(HackerspaceBadge badge)
        {
            _dbContext.HackerspaceBadges.Add(badge);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = _dbContext.HackerspaceBadges.FirstOrDefault(item => item.Id == id);
            _dbContext.Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<HackerspaceBadge>> GetAllAsync()
        {
            return await _dbContext.HackerspaceBadges.ToListAsync();
        }

        public async Task<HackerspaceBadge?> GetByIdAsync(int id)
        {
            return await _dbContext.HackerspaceBadges.FirstOrDefaultAsync(badge => badge.Id == id);
        }

        public async Task UpdateAsync(HackerspaceBadge badge)
        {
            var currentBadge = _dbContext.HackerspaceBadges.FirstOrDefault(item => item.Id == badge.Id);
            currentBadge.Title = badge.Title;
            currentBadge.Description = badge.Description;
            currentBadge.TurnInInstructions = badge.TurnInInstructions;
            currentBadge.Title = badge.Title;
            currentBadge.FileName = badge.FileName;
            await _dbContext.SaveChangesAsync();
        }
    }
}
