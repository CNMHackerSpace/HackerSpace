using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Server.Data.Interfaces;
using Server.Data.Repositories;
using Shared.Models;
using System.Security.Claims;

namespace Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes")]
    public class RolesController : ControllerBase
    {
        private readonly ILogger<BadgesController> _logger;
        private readonly IUserRolesRepo _userRolesRepo;

        public RolesController(ILogger<BadgesController> logger, IUserRolesRepo userRolesRepo)
        {
            _logger = logger;
            _userRolesRepo = userRolesRepo;
            _logger.Log(LogLevel.Information, $"UserController");
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            IEnumerable<UserRole> userRoles;
            string? uid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (uid != null)
            {
                userRoles = await _userRolesRepo.GetAllByUidAsync(uid);
            }
            else
            {
                return NotFound();
            }

            return Ok(userRoles);
        }
    }
}
