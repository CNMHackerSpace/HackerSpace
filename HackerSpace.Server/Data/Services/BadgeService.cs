using HackerSpace.Data.Interfaces;
using Hackerspace.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Hackerspace.Shared.Interfaces;

namespace HackerSpace.Data.Services
{
    // Inherit the service
    // Defines how service should operate
    public class BadgeService : IBadgesPageDataService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public BadgeService(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public Task AddAsync(Badge badge)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Badge>> GetAllAsync()
        {
            using var context = _factory.CreateDbContext();
            return await context.Badges.ToListAsync();
        }

        public Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Badge badge)
        {
            throw new NotImplementedException();
        }
    }
}
