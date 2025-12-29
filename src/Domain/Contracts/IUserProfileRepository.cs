using Dotby.Domain.Entities.Models;

namespace Dotby.Domain.Contracts
{
    public interface IUserProfileRepository
    {
        Task<User> GetUserProfileAsync(string userId, bool trackChanges);
    }
}