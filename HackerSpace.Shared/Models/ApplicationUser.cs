using Microsoft.AspNetCore.Identity;

namespace HackerSpace.Shared.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// First name of the person.
        /// </summary>
        public string? First { get; set; }

        /// <summary>
        /// Middle name of the person.
        /// </summary>  
        public string? Middle { get; set; }

        /// <summary>
        /// Last name of the person.
        /// </summary>
        public string? Last { get; set; }
    }

}
