// Copyright (c) 2025. All rights reserved.

using Microsoft.AspNetCore.Identity;

namespace HackerSpace.Shared.Models
{
    /// <summary>
    /// Represents an application user with extended profile information.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets first name of the person.
        /// </summary>
        public string? First { get; set; }

        /// <summary>
        /// Gets or sets middle name of the person.
        /// </summary>
        public string? Middle { get; set; }

        /// <summary>
        /// Gets or sets last name of the person.
        /// </summary>
        public string? Last { get; set; }
    }
}
