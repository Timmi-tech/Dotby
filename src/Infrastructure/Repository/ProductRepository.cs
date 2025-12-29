
using Dotby.Domain.Contracts;
using Dotby.Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotby.Infrastructure.Repository
{

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<Product>> GetProductsAsync(Guid categoryId, bool trackChanges) => await FindByCondition(p => p.CategoryId.Equals(categoryId), trackChanges)
            .OrderBy(p => p.Name)
            .ToListAsync();

        public async Task<Product?> GetProductAsync(Guid categoryId, Guid Id, bool trackChanges) =>
        await FindByCondition(p => p.CategoryId.Equals(categoryId) && p.Id.Equals(Id), trackChanges)
            .Include(c => c.Category)
            .SingleOrDefaultAsync();

        public async Task<IEnumerable<Product>> GetAllProductAsync(bool trackChanges) =>
        await FindAll(trackChanges)
           .Include(c => c.Category)
           .OrderBy(c => c.Name)
           .ToListAsync();

        public async Task<Product?> GetProductAsync(Guid productId, bool trackChanges) => await FindByCondition(c => c.Id.Equals(productId), trackChanges)
        .Include(c => c.Category)
        .SingleOrDefaultAsync();

        public void CreateProduct(Product product) => Create(product);
    }


}