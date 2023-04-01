using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Data.Interfaces;
using Shared.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpGet]
        public async Task<string> GetUser()
        {
            _logger.Log(LogLevel.Information, "GetUser executed.");
            var user = User;
            var name = User.Identity.Name;
            
            return "Get User Called";
        }
    }
}
