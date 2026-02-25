// Copyright (c) 2025. All rights reserved.

using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    /// <summary>
    /// Represents a user-submitted suggestion for a new badge.
    /// </summary>
    public class BadgeSuggestion
    {
        /// <summary>
        /// Gets or sets primary key for the badge suggestion.
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
        /// Gets or sets the description of the suggested badge.
        /// </summary>
        [Required]
        [MaxLength(5000)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the optional image file name for the suggested badge.
        /// </summary>
        public string? ImageFileName { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who suggested the badge.
        /// </summary>
        [Required]
        public string SuggestedById { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the display name of the user who suggested the badge.
        /// </summary>
        [MaxLength(200)]
        public string SuggestedByName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets UTC timestamp when the suggestion was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
