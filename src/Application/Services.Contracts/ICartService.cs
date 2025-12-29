using Dotby.Application.DTOs;

namespace Dotby.Application.Services.Contracts
{
    public interface ICartService
    {
        Task<CartDto> GetCartAsync(string userId);
        Task<CartItemDto> AddToCartAsync(string userId, AddToCartDto addToCartDto);
        Task<CartItemDto> UpdateCartItemAsync(string userId, Guid productId, UpdateCartItemDto updateCartItemDto);
        Task RemoveFromCartAsync(string userId, Guid productId);
        Task ClearCartAsync(string userId);
    }
}