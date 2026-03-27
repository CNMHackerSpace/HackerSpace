// Copyright (c) 2025. All rights reserved.

using Common.DTOs;

namespace Common.Interfaces
{
    /// <summary>
    /// Defines data access operations for user records.
    /// </summary>
    public interface IUsersDataService
    {
        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of user data transfer objects.</returns>
        Task<List<UserDto>> GetAllUsersAsync();

        /// <summary>
        /// Retrieves a user by identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>The matching user data transfer object, or <c>null</c> if not found.</returns>
        Task<UserDto?> GetUserByIdAsync(string userId);

        /// <summary>
        /// Creates a new user DTO with default values and available role selections.
        /// </summary>
        /// <returns>A new user DTO.</returns>
        Task<UserDto> CreateNewUserAsync();

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userDto">The user data transfer object to create.</param>
        /// <returns>A task that represents the asynchronous create operation.</returns>
        Task AddUserAsync(UserDto userDto);

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="userDto">The user data transfer object containing updated values.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        Task UpdateUserAsync(UserDto userDto);

        /// <summary>
        /// Deletes a user by identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to delete.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        Task DeleteUserAsync(string userId);
    }
}
