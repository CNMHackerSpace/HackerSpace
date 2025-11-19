// Copyright (c) CNM. All rights reserved.

namespace HackerSpace.Shared.Models
{
    /// <summary>
    /// Represents a HackerSpace badge that a person can earn.
    /// </summary>
    public class Badge
    {
        /// <summary>
        /// Gets or sets the unique identifier for the badge.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the badge title.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets a short paragraph description of the badge.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets detailed instructions for submitting a badge evaluation request.
        /// Include all information an assessor needs to determine whether to award the badge.
        /// </summary>
        public string? TurnInInstructions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the badge is visible.
        /// </summary>
        public bool? IsVisible { get; set; }
    }
}
