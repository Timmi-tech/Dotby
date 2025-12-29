using AutoMapper;
using Dotby.Application.DTOs;
using Dotby.Application.Services.Contracts;
using Dotby.Domain;
using Dotby.Domain.Contracts;
using Dotby.Domain.Entities.Models;

namespace Dotby.Application.Services
{

    internal sealed class CategoryService : ICategoryService
    {

        private readonly IRepositoryManager _reposiotry;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;


        public CategoryService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, IPhotoService photoService)
        {
            _reposiotry = repositoryManager;
            _logger = logger;
            _mapper = mapper;
            _photoService = photoService;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(bool trackChanges)
        {
            var categories = await _reposiotry.Category.GetAllCategoriesAsync(trackChanges);

            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return categoriesDto;
        }
        public async Task<CategoryDto> GetCategoryAsync(Guid categoryId, bool trackChanges)
        {
            var category = await _reposiotry.Category.GetCategoryAsync(categoryId, trackChanges);

            if (category == null)
            {
                throw new CategoryNotFoundException(categoryId);
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryForCreationDto category)
        {
            if (category.Image == null)
            {
                throw new ArgumentNullException(nameof(category.Image), "Image cannot be null");
            }

            var uploadResult = await _photoService.UploadImageAsync(category.Image);
            var imageUrl = uploadResult.Url.ToString();


            var categoryEntity = _mapper.Map<Category>(category);
            categoryEntity.ImageUrl = imageUrl;

            _reposiotry.Category.CreateCategory(categoryEntity);
            await _reposiotry.SaveAsync();


            var categoryToReturn = _mapper.Map<CategoryDto>(categoryEntity);
            return categoryToReturn;

        }
    }
}