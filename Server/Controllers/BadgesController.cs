﻿using Server.Data.Interfaces;
using Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Microsoft.AspNetCore.Authorization;
using Shared.ViewModels;
using System.Security.Claims;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
 public class BadgesController : ControllerBase
    {
        private readonly ILogger<BadgesController> _logger;
        private readonly IBadgesRepo _badgesRepo;
        private readonly IUserRolesRepo _userRolesRepo;
        public BadgesController(ILogger<BadgesController> logger, IBadgesRepo badgesRepo, IUserRolesRepo userRolesRepo)
        {
            _logger = logger;
            _badgesRepo = badgesRepo;
            _userRolesRepo = userRolesRepo; 
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

        
        [HttpPost]
        public async Task AddBadge(Badge badge)
        {
            _logger.Log(LogLevel.Information, "AddBadge Executed.");
            await _badgesRepo.AddAsync(badge);
        }

        [HttpPut]
        public async void UpdateBadge(Badge badge)
        {
            _logger.Log(LogLevel.Information, "UpdateBadge Executed.");
            await _badgesRepo.UpdateAsync(badge);
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async void DeleteBadge(int id)
        {
            _logger.Log(LogLevel.Information, "DeleteBadge Executed.");
            await _badgesRepo.DeleteAsync(id);
        }
    }
}
