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
        public IActionResult GetAsync()
        {
            User? user;
            //From https://stackoverflow.com/questions/46112258/how-do-i-get-current-user-in-net-core-web-api-from-jwt-token
            string? uid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //bool? isAuthenticated = User.Identity?.IsAuthenticated;
            if (uid != null)
            {
                user = new User();// _userRepo.GetByUidAsync(uid) ?? new User(); 
                user.Name = "Rob";
                user.Email = "rgarner011235@gmail.com";
                user.UID = "0cc9593b-4d4a-4a5c-a508-71313e9b11b0";
            }
            else
            {
                user = new User();
            }

            return Ok(user);
        }
    }
}
