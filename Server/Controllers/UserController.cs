using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Server.Data.Interfaces;
using Shared.Models;
using System.Security.Claims;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<BadgesController> _logger;
        private readonly IAuthorizationService _authorizationService;

        public UserController(ILogger<BadgesController> logger,IAuthorizationService authorizationService)
        {
            _logger = logger;
            _authorizationService = authorizationService;
            _logger.Log(LogLevel.Information, $"UserController");
        }
        //[HttpGet]
        //public async Task<string> GetUser()
        //{
        //    _logger.Log(LogLevel.Information, "GetUser executed.");
        //    var user = User;
        //    var name = User.Identity.Name;

        //    return "Get User Called";
        //}
        [HttpGet]
        public string GetUserId()
        {
            //From https://stackoverflow.com/questions/46112258/how-do-i-get-current-user-in-net-core-web-api-from-jwt-token
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }
    }
}
