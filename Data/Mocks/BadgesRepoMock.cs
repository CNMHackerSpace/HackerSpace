using Data.Interfaces;
using Data.Models;

namespace Data.Mocks
{
    public class BadgesRepoMock : IBadgesRepo
    {
        public async Task<IEnumerable<Badge>> GetBadgesAsync()
        {
            return await Task.FromResult(Enumerable.Range(1, 5).Select(index => new Badge
            {

                Id = new Random().Next(),
                Title = $"Badge{new Random().Next()}",
                Description = $"Lorem Ipsum Dolera Something Something est."
            })
            .ToArray());
        }
    }
}
