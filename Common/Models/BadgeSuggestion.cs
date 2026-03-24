// Copyright (c) 2025. All rights reserved.

using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    /// <summary>
    /// Represents a badge suggestion submitted by a user for admin review.
    /// </summary>
    public class BadgeSuggestion
    {
        /// <summary>
        /// Gets or sets the unique identifier for the badge suggestion.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.Empty;

        /// <summary>
        /// Gets or sets the suggested badge title.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the suggested badge description.
        /// </summary>
        [Required]
        [MaxLength(5000)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets optional turn-in instructions for the suggested badge.
        /// </summary>
        [MaxLength(5000)]
        public string TurnInInstructions { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the identity user ID of the user who submitted the suggestion.
        /// </summary>
        [Required]
        public string SuggestedById { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the display name of the user who submitted the suggestion.
        /// </summary>
        [MaxLength(200)]
        public string SuggestedByName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the review status of the suggestion.
        /// </summary>
        public BadgeSuggestionStatus Status { get; set; } = BadgeSuggestionStatus.Pending;

        /// <summary>
        /// Gets or sets the UTC timestamp when the suggestion was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the UTC timestamp when the suggestion was reviewed, if applicable.
        /// </summary>
        public DateTime? ReviewedAt { get; set; }

        /// <summary>
        /// Gets or sets the identity user ID of the admin who reviewed the suggestion, if applicable.
        /// </summary>
        public string? ReviewedById { get; set; }

        /// <summary>
        /// Gets or sets optional notes from the admin about the review decision.
        /// </summary>
        [MaxLength(2000)]
        public string? AdminNotes { get; set; }
    }
}
