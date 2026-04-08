// Copyright (c) 2025. All rights reserved.
using Common.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Components.Account.Pages
{
    /// <summary>
    /// Page model for the My Badges page.
    /// </summary>
    public class MyBadgesModel : PageModel
    {
        /// <summary>
        /// Gets or sets the list of badges for the current user.
        /// </summary>
        public List<Badge> Badges { get; set; } = new List<Badge>();
    }
}