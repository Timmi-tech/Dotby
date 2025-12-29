using Dotby.Domain.Contracts;

namespace Dotby.Infrastructure.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext? _repositoryContext;
        private readonly Lazy<IUserProfileRepository>? _userProfileRepository;
        private readonly Lazy<ICategoryRepository> _categoryRepository;
        private readonly Lazy<IProductRepository> _productRepository;
        private readonly Lazy<ICartRepository> _cartRepository;
        private readonly Lazy<ICartItemRepository> _cartItemRepository;


        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _userProfileRepository = new Lazy<IUserProfileRepository>(() => new UserProfileRepository(repositoryContext));
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(_repositoryContext));
            _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(_repositoryContext));
            _cartRepository = new Lazy<ICartRepository>(() => new CartRepository(_repositoryContext));
            _cartItemRepository = new Lazy<ICartItemRepository>(() => new CartItemRepository(_repositoryContext));
        }
        public IUserProfileRepository User => _userProfileRepository!.Value;
        public ICategoryRepository Category => _categoryRepository.Value;
        public IProductRepository Product => _productRepository.Value;
        public ICartRepository Cart => _cartRepository.Value;
        public ICartItemRepository CartItem => _cartItemRepository.Value;
        public async Task SaveAsync() => await _repositoryContext!.
        SaveChangesAsync();
    }
}