using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Server.Data.Interfaces;
using Shared.Models;
using System.Net;
using System.Security.Claims;

//See https://www.learmoreseekmore.com/2022/11/dotnet7-api-crud-efcore.html for info on creating apis
namespace Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<BadgesController> _logger;
        private readonly IUserRepo _userRepo;

        public UserController(ILogger<BadgesController> logger,IUserRepo userRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
            _logger.Log(LogLevel.Information, $"UserController");
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            UserProfile? user;
            //From https://stackoverflow.com/questions/46112258/how-do-i-get-current-user-in-net-core-web-api-from-jwt-token
            string? uid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //bool? isAuthenticated = User.Identity?.IsAuthenticated;
            if (uid != null)
            {
                user = await _userRepo.GetByUidAsync(uid);
                if (user == null)
                {
                    //New user create a new database entry
                    user = new UserProfile();
                    user.UID = uid;
                    user.Name = "";
                    user.Email = "";
                    await _userRepo.AddAsync(user);
                }                
            }
            else
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
