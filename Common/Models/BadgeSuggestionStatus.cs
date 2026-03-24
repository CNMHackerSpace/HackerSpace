// Copyright (c) 2025. All rights reserved.

namespace Common.Models
{
    /// <summary>
    /// Represents the review status of a badge suggestion.
    /// </summary>
    public enum BadgeSuggestionStatus
    {
        /// <summary>
        /// The suggestion is awaiting admin review.
        /// </summary>
        Pending,

        /// <summary>
        /// The suggestion has been approved and converted to a badge.
        /// </summary>
        Approved,

        /// <summary>
        /// The suggestion has been rejected by an admin.
        /// </summary>
        Rejected,
    }
}
