using Microsoft.EntityFrameworkCore;
using HackerSpace.Shared.Interfaces;
using HackerSpace.Shared.Models;
namespace HackerSpace.Data.Services
{
    public class BadgeService : IBadgesPageDataService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public BadgeService(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        // Update badges
        public async Task<List<Badge>> GetAllAsync()
        {
            using var context = _factory.CreateDbContext();
            return await context.Badges.ToListAsync();
        }

        public async Task AddAsync(Badge badge)
        {
            using var context = _factory.CreateDbContext();
            badge.Id = Guid.NewGuid().ToString(); // ensure ID is created if not provided
            context.Badges.Add(badge);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Badge badge)
        {
            using var context = _factory.CreateDbContext();

            var existing = await context.Badges.FirstOrDefaultAsync(x => x.Id == badge.Id);
            if (existing == null)
                throw new InvalidOperationException("Badge not found");

            context.Entry(existing).CurrentValues.SetValues(badge);
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(string id)
        {
            using var context = _factory.CreateDbContext();

            var badge = await context.Badges.FirstOrDefaultAsync(b => b.Id == id);
            if (badge == null)
                throw new InvalidOperationException("Badge not found");

            context.Badges.Remove(badge);
            await context.SaveChangesAsync();
        }
    }
}
