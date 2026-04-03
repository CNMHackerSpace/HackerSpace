using Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Server.Services;

namespace Server.Components.Pages
{
    public class MyBadgesModel : PageModel
    {
        private readonly IBadgeViewPageDataService badgeService;
        private readonly UserManager<ApplicationUser> userManager;

        public List<Badge> Badges { get; set; }

        public MyBadgesModel(
            IBadgeViewPageDataService badgeService,
            UserManager<ApplicationUser> userManager)
        {
            this.badgeService = badgeService;
            this.userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            var user = await userManager.GetUserAsync(User);
            Badges = await badgeService.GetBadgesForUserAsync(user.Id);
        }
    }

    public class MyBadges
    {
    }
}
