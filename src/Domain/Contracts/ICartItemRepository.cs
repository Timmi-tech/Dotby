using Dotby.Domain.Entities.Models;

namespace Dotby.Domain.Contracts
{
    public interface ICartItemRepository
    {
        Task<CartItem?> GetCartItemAsync(Guid cartId, Guid productId, bool trackChanges);
        Task<CartItem?> AddItemAsync(CartItem cartItem, bool trackChanges);
        void UpdateItem(CartItem cartItem, bool trackChanges);
        void RemoveItem(CartItem cartItem, bool trackChanges);
        Task ClearCartAsync(Guid cartId, bool trackChanges);
    }

}