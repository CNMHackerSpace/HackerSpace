// Copyright (c) 2025. All rights reserved.

using System.ComponentModel.DataAnnotations;

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
        [Key]
        public Guid Id { get; set; } = Guid.Empty;

        /// <summary>
        /// Gets or sets the badge title.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a short paragraph description of the badge.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets detailed instructions for submitting a badge evaluation request.
        /// Include all information an assessor needs to determine whether to award the badge.
        /// </summary>
        public string TurnInInstructions { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the badge is visible.
        /// </summary>
        public bool IsVisible { get; set; } = false;

        /// <summary>
        /// Gets or sets the list of submissions associated with the badge.
        /// </summary>
        public List<Submission> Submissions { get; set; } = new ();
    }
}
