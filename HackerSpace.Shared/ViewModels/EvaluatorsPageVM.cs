// Copyright (c) CNM. All rights reserved.

using System.Collections.Generic;
using HackerSpace.Shared.Models;

namespace HackerSpace.Shared.ViewModels
{
    /// <summary>
    /// View model containing the data required by the Evaluators page.
    /// </summary>
    /// <remarks>
    /// Contains collections of <see cref="Evaluator"/> and <see cref="ApplicationUser"/> so the UI can
    /// render evaluator details alongside their corresponding user information.
    /// </remarks>
    public class EvaluatorsPageVM
    {
        /// <summary>
        /// Gets or sets the collection of evaluators to show on the page.
        /// </summary>
        /// <value>
        /// A list of <see cref="Evaluator"/> instances, or <c>null</c> if evaluators have not been loaded.
        /// </value>
        public List<Evaluator>? Evaluators { get; set; }

        /// <summary>
        /// Gets or sets the collection of application users associated with the evaluators.
        /// </summary>
        /// <value>
        /// A list of <see cref="ApplicationUser"/> instances, or <c>null</c> if user information is not available.
        /// </value>
        public List<ApplicationUser>? Users { get; set; }
    }
}
