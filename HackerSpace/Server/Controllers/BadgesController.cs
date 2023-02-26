using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HackerSpaceWasm.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BadgesController : ControllerBase
    {
        private readonly ILogger<BadgesController> _logger;
        private readonly IBadgesRepo _badgesRepo;

        public BadgesController(ILogger<BadgesController> logger, IBadgesRepo badgesRepo)
        {
            _logger = logger;
            _badgesRepo = badgesRepo;
            _logger.Log(LogLevel.Information, $"BadgesController");
        }

        [HttpGet]
        public async Task<IEnumerable<Badge>> GetBadges()
        {
            _logger.Log(LogLevel.Information, "GetBadges executed.");
            return await _badgesRepo.GetBadgesAsync();
        }

        [HttpGet]
        [Route("{Id:int}")]
        public async Task<Badge?> GetBadge(int id)
        {
            _logger.Log(LogLevel.Information, "GetBadges executed.");
            return await _badgesRepo.GetBadgeAsync(id);
        }

        [HttpPost]
        public async Task<Badge?> AddBadge(Badge badge)
        {
            _logger.Log(LogLevel.Information, "AddBadge Executed.");
            return await _badgesRepo.AddBadgeAsync(badge);
        }

        [HttpPut]
        public async void UpdateBadge(Badge badge)
        {
            _logger.Log(LogLevel.Information, "UpdateBadge Executed.");
            await _badgesRepo.UpdateBadgeAsync(badge);
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async void DeleteBadge(int id)
        {
            _logger.Log(LogLevel.Information, "DeleteBadge Executed.");
            await _badgesRepo.DeleteBadgeAsync(id);
        }
    }
}
