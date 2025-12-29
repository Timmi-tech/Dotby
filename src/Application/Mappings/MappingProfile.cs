using AutoMapper;
using Dotby.Application.DTOs;
using Dotby.Domain.Entities.Models;

namespace Dotby.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Username))
                .ForMember(u => u.FirstName, opt => opt.MapFrom(x => x.Firstname))
                .ForMember(u => u.LastName, opt => opt.MapFrom(x => x.Lastname))
                .ForMember(u => u.Email, opt => opt.MapFrom(x => x.Email));

            CreateMap<User, UserProfileDto>()
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Matricnumber, opt => opt.MapFrom(src => src.MatricNumber))
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src => src.ProfileImageUrl))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email));


            CreateMap<UserUpdateProfileDto, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<ProductForCreationDto, Product>();
            CreateMap<CategoryForCreationDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Product, ProductDto>();

            CreateMap<Cart, CartDto>()
             .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total));
            // Add the mapping configuration here
            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.unitPrice))
                .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.SubTotal));
            CreateMap<AddToCartDto, CartItem>();
            CreateMap<UpdateCartItemDto, CartItem>();

        }
    }
}