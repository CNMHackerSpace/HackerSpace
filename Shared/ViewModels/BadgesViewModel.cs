using SharedClasses.Models;

namespace SharedClasses.ViewModels
{
    public class BadgesViewModel
    {
        //public IEnumerable<Role> Roles { get; set; } = new List<Role>();
        public IEnumerable<HSBadge> Badges { get; set;} = new List<HSBadge>();
    }
}
