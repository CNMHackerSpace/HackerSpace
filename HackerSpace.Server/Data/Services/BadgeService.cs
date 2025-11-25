using HackerSpace.Data.Interfaces;
using Hackerspace.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HackerSpace.Data.Services
{
    // Inherit the service
    // Defines how service should operate
    public class BadgeService : IBadgeService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public BadgeService(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<Badge>> GetAllAsync()
        {
            using var context = _factory.CreateDbContext();
            return await context.Badges.ToListAsync();
        }
    }
}
