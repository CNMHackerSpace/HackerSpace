using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedClasses.Interfaces;
using SharedClasses.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BadgesController : ControllerBase
    {
        private readonly ILogger<BadgesController> _logger;
        private readonly IBadgesDAL _badgesRepo;
        public BadgesController(ILogger<BadgesController> logger, IBadgesDAL badgesRepo)
        {
            _logger = logger;
            _badgesRepo = badgesRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<HackerspaceBadge>> GetBadges()
        {
            _logger.Log(LogLevel.Information, "GetBadges executed.");
            return await _badgesRepo.GetAllAsync();
        }

        [HttpGet]
        [Route("{Id:int}")]
        public async Task<HackerspaceBadge?> GetBadge(int id)
        {
            _logger.Log(LogLevel.Information, "GetBadges executed.");
            return await _badgesRepo.GetByIdAsync(id);
        }

       // [Authorize(Roles = "admin, badgecreator")]
       // [AllowAnonymous]
        [HttpPost]
        public async Task AddBadge([FromBody] HackerspaceBadge badge)
        {
            _logger.Log(LogLevel.Information, "AddBadge Executed.");
            await _badgesRepo.AddAsync(badge);
        }

        //[Authorize(Roles = "admin, badgecreator")]
        // [AllowAnonymous]
        [HttpPut]
        public async void UpdateBadge([FromBody] HackerspaceBadge badge)
        {
            _logger.Log(LogLevel.Information, "UpdateBadge Executed.");
            await _badgesRepo.UpdateAsync(badge);
        }

       // [Authorize(Roles = "admin")]
       // [AllowAnonymous]
        [HttpDelete]
        [Route("{Id:int}")]
        public async void DeleteBadge(int id)
        {
            _logger.Log(LogLevel.Information, "DeleteBadge Executed.");
            await _badgesRepo.DeleteAsync(id);
        }
    }
}


