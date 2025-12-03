// Copyright (c) CNM. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HackerSpace.Shared.Models
{
    /// <summary>
    /// Represents a person who can evaluate badge submissions.
    /// </summary>
    public class Evaluator
    {
        /// <summary>
        /// Gets or sets the unique identifier for the evaluator.
        /// </summary>
        public Guid Id { get; set; } = Guid.Empty;

        /// <summary>
        /// Gets or sets the associated application user id (foreign key).
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Navigation property for the associated <see cref="ApplicationUser"/>.
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }

        /// <summary>
        /// Gets or sets the email address to use for notifications. If null, the
        /// linked user's email address will be used when available.
        /// </summary>
        public string? NotificationEmail { get; set; }

        /// <summary>
        /// Returns the effective notification email for this evaluator.
        /// </summary>
        /// <returns>The notification email or the linked user's email if notification email is null.</returns>
        public string? GetNotificationEmail() => this.NotificationEmail ?? this.User?.Email;
    }
}
