using HackerSpace.Shared.Interfaces;
using HackerSpace.Shared.Models;

namespace HackerSpace.Data.Mocks
{
    /// <summary>
    /// In-memory mock implementation of <see cref="IBadgesPageDataService"/> used for development and testing.
    /// </summary>
    public class BadgesPageServiceMock : IBadgesPageDataService
    {
        private List<Badge> _badges = new List<Badge>()
        {
            new Badge
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Beginner Hacker",
                Description = "Awarded for completing the beginner hacking course.",
                TurnInInstructions = "Submit your course completion certificate.",
                IsVisible = true
            },
            new Badge
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Security Expert",
                Description = "Awarded for demonstrating advanced security skills.",
                TurnInInstructions = "Submit a report on your security project.",
                IsVisible = false
            },
            new Badge
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Code Contributor",
                Description = "Awarded for contributing to open-source projects.",
                TurnInInstructions = "Submit your GitHub profile link.",
                IsVisible = true
            },
            new Badge
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Bug Hunter",
                Description = "Awarded for finding and reporting bugs.",
                TurnInInstructions = "Submit your bug report.",
                IsVisible = false
            },
            new Badge
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Community Helper",
                Description = "Awarded for helping others in the community.",
                TurnInInstructions = "Submit testimonials from community members.",
                IsVisible = true
            }
        };

        /// <summary>
        /// Asynchronously returns the list of badges held by the mock store.
        /// </summary>
        /// <returns>A task whose result is the list of <see cref="Badge"/> instances.</returns>
        public Task<List<Badge>> GetAllAsync()
        {
            return Task.FromResult(_badges);
        }

        /// <summary>
        /// Updates the badge in the mock store that matches the provided badge's identifier.
        /// </summary>
        /// <param name="badge">The badge containing updated values.</param>
        /// <returns>A task that completes when the update is applied.</returns>
        /// <exception cref="ArgumentException">Thrown if the badge to update does not exist.</exception>
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

        /// <summary>
        /// Adds a new badge to the mock store. The badge's Id will be replaced with a newly generated GUID.
        /// </summary>
        /// <param name="badge">The badge to add.</param>
        /// <returns>A task that completes when the badge has been added.</returns>
        public Task AddAsync(Badge badge)
        {
            badge.Id = Guid.NewGuid().ToString();
            _badges.Add(badge);
            return Task.FromResult(true);
        }

        /// <summary>
        /// Removes the badge with the specified identifier from the mock store.
        /// </summary>
        /// <param name="id">The unique identifier of the badge to remove.</param>
        /// <returns>A task that completes when the badge has been removed.</returns>
        /// <exception cref="ArgumentException">Thrown if the badge to remove does not exist.</exception>
        public Task RemoveAsync(string id)
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
