using Dotby.Domain.Contracts;
using Dotby.Domain.Entities.Models;

namespace Dotby.Domain.Contracts
{
    public interface IRepositoryManager
    {
        IUserProfileRepository User { get; }
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        ICartRepository Cart { get; }
        ICartItemRepository CartItem { get; }
        Task SaveAsync();
    }
}