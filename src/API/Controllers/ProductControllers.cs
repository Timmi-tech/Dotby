using Dotby.Application.DTOs;
using Dotby.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Dotby.API.Controllers{

    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IServiceManager _service;
        public ProductsController(IServiceManager service) 
        { 
            _service = service;
        }
        // Get all products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _service.ProductService.GetAllProductAsync(trackChanges: false);
            return Ok(products);
        }

        // Get a single product
        [HttpGet("{productId:guid}", Name = "ProductById")]
        public async Task<IActionResult> GetProduct(Guid productId)
        {
            var product = await _service.ProductService.GetProductAsync(productId,  trackChanges: false);

            return Ok(product);
        }
         // Get products by category
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductsForCategory(Guid categoryId)
        {
            var products = await _service.ProductService.GetProductsAsync(categoryId, trackChanges: false);

            return Ok(products);
        }
         // Get a single product in a category
        [HttpGet("category/{categoryId}/{ProductId:guid}")]
        public async Task<IActionResult> GetProductForCategory(Guid categoryId, Guid ProductId)
        {
            var product = await _service.ProductService.GetProductAsync(categoryId, ProductId, trackChanges: false);
            return Ok(product);
        }
        [HttpPost]
        [Consumes("multipart/form-data")] 
        [SwaggerOperation(
        Summary = "Uploads a photo",
        Description = "Upload an image file"
        )]
        public async Task<IActionResult> CreateProduct([FromForm] ProductForCreationDto product)
        {
            var createdProduct = await _service.ProductService.CreateProductAsync(product);

            return CreatedAtRoute("ProductById", new { productId = createdProduct.Id }, createdProduct);

        }

    }
}