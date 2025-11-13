using Hackerspace.Shared.Interfaces;
using Hackerspace.Shared.Models;

namespace HackerSpace.Data.Mocks
{
    public class BadgesPageServiceMock : IBadgesPageDataService
    {
        private List<Badge> _badges = new List<Badge>()
        {
            new Badge
            {
                Id = Guid.NewGuid(),
                Title = "Beginner Hacker",
                Description = "Awarded for completing the beginner hacking course.",
                TurnInInstructions = "Submit your course completion certificate.",
                IsVisible = true
            },
            new Badge
            {
                Id = Guid.NewGuid(),
                Title = "Security Expert",
                Description = "Awarded for demonstrating advanced security skills.",
                TurnInInstructions = "Submit a report on your security project.",
                IsVisible = false
            },
            new Badge
            {
                Id = Guid.NewGuid(),
                Title = "Code Contributor",
                Description = "Awarded for contributing to open-source projects.",
                TurnInInstructions = "Submit your GitHub profile link.",
                IsVisible = true
            },
            new Badge
            {
                Id = Guid.NewGuid(),
                Title = "Bug Hunter",
                Description = "Awarded for finding and reporting bugs.",
                TurnInInstructions = "Submit your bug report.",
                IsVisible = false
            },
            new Badge
            {
                Id = Guid.NewGuid(),
                Title = "Community Helper",
                Description = "Awarded for helping others in the community.",
                TurnInInstructions = "Submit testimonials from community members.",
                IsVisible = true
            }
        };


        public Task<List<Badge>> GetAllAsync()
        {
            return Task.FromResult(_badges);
        }

        public Task UpdateAsync(Badge badge)
        {
            var existingBadge = _badges.Where(b=>b.Id == badge.Id).FirstOrDefault();
            if (existingBadge == null)
            {
                throw new ArgumentException("Badge does not exist. Can not update it.");
            }
            existingBadge.Title = badge.Title;
            existingBadge.Description = badge.Description;
            existingBadge.TurnInInstructions = badge.TurnInInstructions;
            existingBadge.IsVisible = badge.IsVisible;
            return Task.FromResult(true);
        }

        public Task AddAsync(Badge badge)
        {
            badge.Id = Guid.NewGuid();
            _badges.Add(badge);
            return Task.FromResult(true);
        }

        public Task RemoveAsync(Guid id)
        {
            var existingBadge = _badges.Where(b => b.Id == id).FirstOrDefault();
            if (existingBadge == null)
            {
                throw new ArgumentException("Badge does not exist. Can not update it.");
            }
            _badges.Remove(existingBadge);
            return Task.FromResult(true);
        }
    }
}
