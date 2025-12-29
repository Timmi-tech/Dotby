namespace Dotby.Application.Services.Contracts
{

    public interface IServiceManager
    {
        IAuthenticationService AuthenticationService { get; }
        IUserProfileService UserProfileService { get; }
        ICategoryService CategoryService { get; }
        IProductService ProductService { get; }
        ICartService CartService { get; }
    }
}
