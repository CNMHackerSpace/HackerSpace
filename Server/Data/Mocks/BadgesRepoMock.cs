using Server.Data.Interfaces;
using Shared.Models;

namespace Server.Data.Mocks
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
                Description = $"Lorem Ipsum Dolera Something Something est.",
                FileName = $"HackerSpaceLogoBlueGold.png"
            })
            .ToList();
        }

        public async Task<IEnumerable<Badge>> GetAllAsync()
        {
            return await Task.FromResult(_badges); 
        }

        public async Task<Badge?> GetByIdAsync(int id)
        {
            return await Task.FromResult(_badges.Where(item => item.Id == id).FirstOrDefault());
        }

        public Task<Badge?> AddAsync(Badge badge)
        {
            badge.Id = ++_count;
            _badges.Add(badge);
            return Task.FromResult((Badge?)badge);
        }

        public Task UpdateAsync(Badge badge)
        {
            Badge? currentBadge = _badges.Where(item => item.Id == badge.Id).FirstOrDefault();
            if (currentBadge != null)
            {
                badge.Title = currentBadge.Title;
                badge.Description = currentBadge.Description;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
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
