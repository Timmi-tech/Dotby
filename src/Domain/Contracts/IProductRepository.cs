using Dotby.Domain.Entities.Models;

namespace Dotby.Domain.Contracts
{
    public interface IProductRepository
    {

        Task<IEnumerable<Product>> GetProductsAsync(Guid categoryId, bool trackChanges);
        Task<IEnumerable<Product>> GetAllProductAsync(bool trackChanges);
        Task<Product?> GetProductAsync(Guid categoryId, Guid Id, bool trackChanges);
        Task<Product?> GetProductAsync(Guid productId, bool trackChanges);
        void CreateProduct(Product product);

    }
}