using Dotby.Domain.Entities.Models;

namespace Dotby.Domain.Contracts
{
    public interface ICartRepository
    {
        Task<Cart?> GetByIdAsync(Guid cartId, bool trackChanges);
        Task<Cart?> GetByUserIdAsync(string userId, bool trackChanges);
        void AddCart(Cart cart);
    }
}