using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Data.Interfaces;
using Shared.Models;

namespace Server.Controllers
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
        }

        [HttpGet]
        public async Task<IEnumerable<Badge>> GetBadges()
        {
            _logger.Log(LogLevel.Information, "GetBadges executed.");
            return await _badgesRepo.GetAllAsync();
        }

        [HttpGet]
        [Route("{Id:int}")]
        public async Task<Badge?> GetBadge(int id)
        {
            _logger.Log(LogLevel.Information, "GetBadges executed.");
            return await _badgesRepo.GetByIdAsync(id);
        }

        [Authorize(Roles = "admin, badgecreator")]
        [HttpPost]
        public async Task AddBadge([FromBody] Badge badge)
        {
            _logger.Log(LogLevel.Information, "AddBadge Executed.");
            await _badgesRepo.AddAsync(badge);
        }

        [Authorize(Roles = "admin, badgecreator")]
        [HttpPut]
        public async void UpdateBadge([FromBody] Badge badge)
        {
            _logger.Log(LogLevel.Information, "UpdateBadge Executed.");
            await _badgesRepo.UpdateAsync(badge);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        [Route("{Id:int}")]
        public async void DeleteBadge(int id)
        {
            _logger.Log(LogLevel.Information, "DeleteBadge Executed.");
            await _badgesRepo.DeleteAsync(id);
        }
    }
}


