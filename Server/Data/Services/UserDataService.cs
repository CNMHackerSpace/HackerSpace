// Copyright (c) 2025. All rights reserved.

using Common.DTOs;
using Common.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Server.Data.Services
{
    /// <summary>
    /// Provides data operations for managing users and their role assignments.
    /// </summary>
    public class UsersDataService : IUsersDataService
    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersDataService"/> class.
        /// </summary>
        /// <param name="userManager">The user manager instance.</param>
        /// <param name="roleManager">The role manager instance.</param>
        public UsersDataService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        /// <summary>
        /// Retrieves all users with their role selections.
        /// </summary>
        /// <returns>A list of users.</returns>
        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            List<UserDto> userDtos = new List<UserDto>();
            foreach (ApplicationUser appUser in this.userManager.Users)
            {
                UserDto userDto = new()
                {
                    Id = appUser.Id,
                    Email = appUser.Email ?? string.Empty,
                    EmailConfirmed = appUser.EmailConfirmed,
                    Password = string.Empty, // Do not return the password
                    RoleSelections = this.roleManager.Roles.Select(role => new RoleSelection
                    {
                        RoleName = role.Name ?? string.Empty,
                        IsSelected = this.userManager.IsInRoleAsync(appUser, role.Name ?? string.Empty).Result,
                    }).ToList(),
                };
                userDtos.Add(userDto);
            }

            return userDtos;
        }

        /// <summary>
        /// Retrieves a user by identifier with role selections.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The user if found; otherwise, <see langword="null"/>.</returns>
        public async Task<UserDto?> GetUserByIdAsync(string userId)
        {
            ApplicationUser? appUser = await this.userManager.FindByIdAsync(userId);
            if (appUser == null)
            {
                return null;
            }

            var userDto = new UserDto
            {
                Email = appUser.Email ?? string.Empty,
                EmailConfirmed = appUser.EmailConfirmed,
                Password = string.Empty, // Do not return the password
                RoleSelections = this.roleManager.Roles.Select(role => new RoleSelection
                {
                    RoleName = role.Name ?? string.Empty,
                    IsSelected = this.userManager.IsInRoleAsync(appUser, role.Name ?? string.Empty).Result,
                }).ToList(),
            };
            return userDto;
        }

        /// <summary>
        /// Creates a new user and assigns selected roles.
        /// </summary>
        /// <param name="userDto">The user data to create.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task CreateUserAsync(UserDto userDto)
        {
            ApplicationUser appUser = new()
            {
                UserName = userDto.Email,
                Email = userDto.Email,
                EmailConfirmed = userDto.EmailConfirmed,
            };

            var result = await this.userManager.CreateAsync(appUser, userDto.Password);
            if (result.Succeeded)
            {
                List<string> rolesToAdd = userDto.RoleSelections.Where(r => r.IsSelected).Select(r => r.RoleName).ToList();

                if (rolesToAdd.Any())
                {
                    await this.userManager.AddToRolesAsync(appUser, rolesToAdd);
                }
            }
        }

        /// <summary>
        /// Updates an existing user, optional password, and role assignments.
        /// </summary>
        /// <param name="userDto">The updated user data.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateUserAsync(UserDto userDto)
        {
            ApplicationUser? appUser = await this.userManager.FindByIdAsync(userDto.Id);
            if (appUser == null)
            {
                return;
            }

            appUser.Email = userDto.Email;
            appUser.UserName = userDto.Email;
            appUser.EmailConfirmed = userDto.EmailConfirmed;
            IdentityResult result = await this.userManager.UpdateAsync(appUser);

            // If password is not empty, update it
            if (!string.IsNullOrEmpty(userDto.Password))
            {
                string? token = await this.userManager.GeneratePasswordResetTokenAsync(appUser);
                var passwordResult = await this.userManager.ResetPasswordAsync(appUser, token, userDto.Password);
            }

            // Update roles
            IList<string> currentRoles = await this.userManager.GetRolesAsync(appUser);
            List<string> rolesToAdd = userDto.RoleSelections.Where(r => r.IsSelected).Select(r => r.RoleName).Except(currentRoles).ToList();
            List<string> rolesToRemove = currentRoles.Except(userDto.RoleSelections.Where(r => r.IsSelected).Select(r => r.RoleName)).ToList();

            if (rolesToAdd.Any())
            {
                await this.userManager.AddToRolesAsync(appUser, rolesToAdd);
            }

            if (rolesToRemove.Any())
            {
                await this.userManager.RemoveFromRolesAsync(appUser, rolesToRemove);
            }
        }

        /// <summary>
        /// Deletes a user by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteUserAsync(string userId)
        {
            ApplicationUser? appUser = await this.userManager.FindByIdAsync(userId);
            if (appUser != null)
            {
                await this.userManager.DeleteAsync(appUser);
            }
        }
    }
}
