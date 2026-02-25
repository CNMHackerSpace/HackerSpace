// Copyright (c) 2025. All rights reserved.

namespace HackerSpace.WebClient
{
    /// <summary>
    /// Minimal DTO containing persisted user information exposed to the client application.
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public required string UserId { get; set; }

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        public required string Email { get; set; }
    }
}
