using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace HackerSpaceWasm.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
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
    }
}
