using Dotby.Application.DTOs;
using Dotby.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Dotby.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly IServiceManager _service;
        public CategoriesController(IServiceManager service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
           var categories = await _service.CategoryService.GetAllCategoriesAsync(trackChanges: false);

            return Ok(categories);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var category = await _service.CategoryService.GetCategoryAsync(id, trackChanges: false);

            return Ok(category);
        }
        [HttpPost]
        [Consumes("multipart/form-data")] 
        [SwaggerOperation(
        Summary = "Uploads a photo",
        Description = "Upload an image file"
        )]
        public async Task<IActionResult> CreateCategory([FromForm] CategoryForCreationDto category)
        {
            var createdCategory = await _service.CategoryService.CreateCategoryAsync(category);

            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory);
        }
    }

}

