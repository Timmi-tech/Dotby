using Dotby.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Dotby.Application.Services.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Dotby.Domain.Entities.Models;
using Dotby.Domain.Entities.ConfigurationsModels;
using CloudinaryDotNet;
using Dotby.Domain.Contracts;


namespace Dotby.Application.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IPhotoService> _photoService;
        private readonly Lazy<IUserProfileService> _userProfileService;
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<ICartService> _cartService;
        public ServiceManager
        (
            ILoggerManager logger,
            IRepositoryManager repositoryManager,
            IMapper mapper,
            UserManager<User> userManager,
            IOptions<JwtConfiguration> configuration,
            Cloudinary cloudinary
        )
        {
            _authenticationService = new Lazy<IAuthenticationService>(() =>
                new AuthenticationService(logger, mapper, userManager, configuration));
            _photoService = new Lazy<IPhotoService>(() => new PhotoService(cloudinary));
            _userProfileService = new Lazy<IUserProfileService>(() => new UserProfileService(repositoryManager, logger, mapper, PhotoService));
            _categoryService = new Lazy<ICategoryService>(() =>
           new CategoryService(repositoryManager, logger, mapper, PhotoService));
            _productService = new Lazy<IProductService>(() =>
                new ProductService(repositoryManager, logger, mapper, PhotoService));
            _cartService = new Lazy<ICartService>(() =>
                new CartService(repositoryManager, logger, mapper));
        }
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
        public IPhotoService PhotoService => _photoService.Value;
        public IUserProfileService UserProfileService => _userProfileService.Value;
        public ICategoryService CategoryService => _categoryService.Value;
        public IProductService ProductService => _productService.Value;
        public ICartService CartService => _cartService.Value;
    }
}