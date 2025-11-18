using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerSpace.Shared.Models
{
    public class Evaluator
    {
        /// <summary>
        /// Unique id for the badge.
        /// </summary>
        public Guid Id { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public string? NotificationEmail { get; set; }
    }
}
