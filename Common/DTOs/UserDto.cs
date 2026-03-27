// Copyright (c) 2025. All rights reserved.

namespace Common.DTOs
{
    /// <summary>
    /// Represents a user data transfer object.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Gets or sets the user's ID.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's email.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the user's email is confirmed.
        /// </summary>
        public bool EmailConfirmed { get; set; } = false;

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's confirmed password.
        /// </summary>
        public string ConfirmPassword { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of role selections for the user.
        /// </summary>
        public List<RoleSelection> RoleSelections { get; set; } = new ();
    }
}
