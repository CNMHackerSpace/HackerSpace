// Copyright (c) CNM. All rights reserved.

using System.ComponentModel.DataAnnotations;

namespace HackerSpace.Shared.Models
{
    /// <summary>
    /// Represents a link associated with a submission.
    /// </summary>
    public class SubmissionLink
    {
        /// <summary>
        /// Gets or sets the primary key for the submission link.
        /// </summary>
        [Key]
        public Guid? Id { get; set; }

        /// <summary>
        /// Gets or sets the foreign key referencing the associated submission.
        /// </summary>
        [Required]
        public Guid? SubmissionId { get; set; }

        /// <summary>
        /// Gets or sets the associated <see cref="Submission"/> entity.
        /// </summary>
        public Submission? Submission { get; set; }

        /// <summary>
        /// Gets or sets the title of the submission link.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the URL of the submission link.
        /// </summary>
        [Required]
        public string Url { get; set; } = string.Empty;
    }
}