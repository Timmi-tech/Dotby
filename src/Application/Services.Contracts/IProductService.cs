using Dotby.Application.DTOs;

namespace Dotby.Application.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync(Guid categoryId, bool trackChanges);
        Task<IEnumerable<ProductDto>> GetAllProductAsync(bool trackChanges);
        Task<ProductDto> GetProductAsync(Guid categoryId, Guid Id, bool trackChanges);
        Task<ProductDto> GetProductAsync(Guid productId, bool trackChanges);
        Task<ProductDto> CreateProductAsync(ProductForCreationDto product);

    }
}