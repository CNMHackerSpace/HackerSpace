using Hackerspace.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

// Defines what service should do
namespace HackerSpace.Data.Interfaces
{
    public interface IBadgeService
    {
        Task<List<Badge>> GetAllAsync();
    }
}
