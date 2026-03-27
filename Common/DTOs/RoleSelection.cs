// Copyright (c) 2025. All rights reserved.

namespace Common.DTOs
{
    /// <summary>
    /// Represents a selectable role item.
    /// </summary>
    public class RoleSelection
    {
        /// <summary>
        /// Gets or sets the role name.
        /// </summary>
        public string RoleName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the role is selected.
        /// </summary>
        public bool IsSelected { get; set; }
    }
}
