using Dotby.Domain.Contracts;
using Dotby.Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotby.Infrastructure.Repository
{
    public class CartItemRepository : RepositoryBase<CartItem>, ICartItemRepository
    {

        public CartItemRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        
        public async Task<CartItem?> GetCartItemAsync(Guid cartId, Guid productId, bool trackChanges) => await FindByCondition(c => c.CartId.Equals(cartId) && c.ProductId.Equals(productId), trackChanges)
            .SingleOrDefaultAsync();

        public Task<CartItem?> AddItemAsync(CartItem cartItem, bool trackChanges)
        {
            Create(cartItem);
            return Task.FromResult<CartItem?>(cartItem);
        }
        public void UpdateItem(CartItem cartItem, bool trackChanges) => Update(cartItem);

        public void  RemoveItem(CartItem cartItem, bool trackChanges) => Delete(cartItem);

        public async Task ClearCartAsync(Guid cartId, bool trackChanges)
        {

            var items = await FindByCondition(ci => ci.CartId.Equals(cartId), trackChanges)
            .ToListAsync();
            
            DeleteRange(items); 
        }

    }
}