using Shared.Models;

namespace Shared.ViewModels
{
    public class BadgesViewModel
    {
        public IEnumerable<Role> Roles { get; set; } = new List<Role>();
        public IEnumerable<Badge> Badges { get; set;} = new List<Badge>();
    }
}
