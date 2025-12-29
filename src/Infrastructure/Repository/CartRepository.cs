using Dotby.Domain.Contracts;
using Dotby.Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotby.Infrastructure.Repository
{
    public class CartRepository : RepositoryBase<Cart>, ICartRepository
    {

        public CartRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<Cart?> GetByIdAsync(Guid cartId, bool trackChanges) => await FindByCondition(c => c.Id.Equals(cartId), trackChanges)
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .SingleOrDefaultAsync();

        public async Task<Cart?> GetByUserIdAsync(string userId, bool trackChanges) => await FindByCondition(c => c.UserId.Equals(userId), trackChanges)
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .SingleOrDefaultAsync();
        public void AddCart(Cart cart) => Create(cart);
        
    }
}