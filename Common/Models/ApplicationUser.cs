// Copyright (c) 2025. All rights reserved.

using System;
using Microsoft.AspNetCore.Identity;

namespace Common.Models
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

        /// <summary>
        /// Represents a badge application submitted by an application user.
        /// </summary>
        public class BadgeApplication
        {
            /// <summary>
            /// Gets or sets the unique identifier for the badge application.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Gets or sets the user name of the applicant.
            /// </summary>
            public string UserName { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets the name of the requested badge.
            /// </summary>
            public string BadgeName { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets the status of the badge application (Pending, Approved, Rejected).
            /// </summary>
            public string? Status { get; set; }

            /// <summary>
            /// Gets or sets the date the application was submitted.
            /// </summary>
            public DateTime DateSubmitted { get; set; }
        }
    }
}
