using AutoMapper;
using Dotby.Application.DTOs;
using Dotby.Application.Services.Contracts;
using Dotby.Domain;
using Dotby.Domain.Contracts;
using Dotby.Domain.Entities.Models;

namespace Dotby.Application.Services{
    internal sealed class ProductService : IProductService{
        private readonly IRepositoryManager _reposiotry;
        private readonly ILoggerManager _logger;
        private readonly IMapper  _mapper;
        private readonly IPhotoService _photoService;  

        public ProductService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, IPhotoService photoService){
            _reposiotry = repositoryManager;
            _logger = logger;
            _mapper = mapper;
            _photoService = photoService; 
        }
        public async Task<IEnumerable<ProductDto>> GetProductsAsync(Guid categoryId, bool trackChanges) 
        {
            var category = await _reposiotry.Category.GetCategoryAsync(categoryId, trackChanges);
            if (category == null)
            {
                throw new CategoryNotFoundException(categoryId);
            }
            var productsFromDb = await _reposiotry.Product.GetProductsAsync(categoryId, trackChanges);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(productsFromDb);
            return productsDto;
        }
        public async Task<ProductDto> GetProductAsync(Guid categoryId, Guid Id, bool trackChanges)
        {
            var category = await _reposiotry.Category.GetCategoryAsync(categoryId, trackChanges);
            if (category == null)
            {
                throw new CategoryNotFoundException(categoryId);
            }
            var productFromDb = await _reposiotry.Product.GetProductAsync(categoryId, Id, trackChanges);
            if (productFromDb == null)
            {
                throw new ProductNotFoundException(Id);
            }
            var productDto = _mapper.Map<ProductDto>(productFromDb);
            return productDto;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProductAsync(bool trackChanges)
        {
            var productsFromDb = await _reposiotry.Product.GetAllProductAsync(trackChanges);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(productsFromDb);
            return productsDto;
        }

        public async Task<ProductDto> GetProductAsync(Guid productId, bool trackChanges)
        {
            var product = await _reposiotry.Product.GetProductAsync(productId, trackChanges);

            if(product == null) {
                throw new ProductNotFoundException(productId);
            }

            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }
        // public async Task<ProductDto> CreateProductAsync(ProductForCreationDto product)
        // {
        //     var productEntity = _mapper.Map<Product>(product);
        //     _reposiotry.Product.CreateProduct(productEntity);
        //     await _reposiotry.SaveAsync();

        //     var producToReturn = _mapper.Map<ProductDto>(productEntity);
        //     return producToReturn;
        // }
         public async Task<ProductDto> CreateProductAsync(ProductForCreationDto product)
    {
        if (product.Image == null)
        {
             throw new ArgumentNullException(nameof(product.Image), "Image cannot be null");
        }

        // Upload image
        var uploadResult = await _photoService.UploadImageAsync(product.Image);  
        var imageUrl = uploadResult.Url.ToString();  


        var productEntity = _mapper.Map<Product>(product);
        productEntity.ImageUrl = imageUrl;  

        // Save the product to the database
        _reposiotry.Product.CreateProduct(productEntity);
        await _reposiotry.SaveAsync();

        // Map and return the Product DTO
        var productToReturn = _mapper.Map<ProductDto>(productEntity);
        return productToReturn;
    }
}
}