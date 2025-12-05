// Copyright (c) CNM. All rights reserved.

using System.ComponentModel.DataAnnotations;

namespace HackerSpace.Shared.Models
{
    /// <summary>
    /// Represents a user submission for a badge request.
    /// </summary>
    public class Submission
    {
        /// <summary>
        /// Gets or sets primary key for the submission.
        /// </summary>
        [Key]
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets identifier of the associated badge.
        /// </summary>
        [Required]
        public string BadgeId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets optional navigation property to the <c>Badge</c> entity.
        /// </summary>
        public Badge? Badge { get; set; }

        /// <summary>
        /// Gets or sets the submission text provided by the applicant.
        /// </summary>
        [Required]
        [MaxLength(5000)]
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the identifier of the user applying for the badge (for example, an identity principal id).
        /// </summary>
        [Required]
        public string ApplicantId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets optional human-friendly applicant name for quick display.
        /// </summary>
        [MaxLength(200)]
        public string? ApplicantName { get; set; }

        /// <summary>
        /// Gets or sets links (GitHub repositories, cloud documents, etc.) that support this submission.
        /// </summary>
        public ICollection<SubmissionLink> Links { get; set; } = new List<SubmissionLink>();

        /// <summary>
        /// Gets or sets uTC timestamp when the submission was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}