using Data.Interfaces;
using Data.Models;

namespace Data.Mocks
{
    public class BadgesRepoMock : IBadgesRepo
    {
        private List<Badge> _badges;
        private int _count = 0;
        public BadgesRepoMock() 
        {
            _badges = Enumerable.Range(1, 5).Select(index => new Badge
            {

                Id = ++_count,
                Title = $"Badge{_count}",
                Description = $"Lorem Ipsum Dolera Something Something est."
            })
            .ToList();
        }

        public async Task<Badge?> GetBadgeAsync(int id)
        {
            return await Task.FromResult(_badges.Where(item => item.Id == id).FirstOrDefault());
        }

        public async Task<IEnumerable<Badge>> GetBadgesAsync()
        {
            return await Task.FromResult(_badges); 
        }

        public Task UpdateBadgeAsync(Badge badge)
        {
            Badge? currentBadge = _badges.Where(item => item.Id == badge.Id).FirstOrDefault();
            if (currentBadge != null)
            {
                badge.Title = currentBadge.Title;
                badge.Description = currentBadge.Description;
            }
            return Task.CompletedTask;
        }

        public Task<Badge?> AddBadgeAsync(Badge badge)
        {
            badge.Id = ++_count;
            _badges.Add(badge);
            return Task.FromResult((Badge?)badge);
        }

        public Task DeleteBadgeAsync(int id)
        {
            Badge? badge = _badges.Where(item => item.Id == id).FirstOrDefault();
            if (badge != null)
            {
                _badges.Remove(badge);
            }
            return Task.CompletedTask;
        }
    }
}
