using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Dotby.Application.DTOs
{
    public record ProductDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string ImageUrl { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public int StockQuantity { get; init; }
        public CategoryDto Category { get; init; } = new CategoryDto();
        public SellerDto Seller { get; init; } = new SellerDto();
        
    }
    public record ProductForCreationDto
    {
        [Required(ErrorMessage = "Product name is required")]
        public string Name { get; init; } = string.Empty;
        [Required(ErrorMessage = "Product description is required")]
        [MaxLength(1000, ErrorMessage = "Product description cannot exceed 1000 characters")]
        public string Description { get; init; } = string.Empty;
        [Required(ErrorMessage = "Product image is required")]
        public IFormFile? Image { get; init; } 
        [Required(ErrorMessage = "Product price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Product price must be greater than 0")]
        public decimal Price { get; init; }
        [Required(ErrorMessage = "Product stock quantity is required")]
        public int StockQuantity { get; init; }
        [Required(ErrorMessage = "Category ID is required")]
        public Guid CategoryId { get; init; }
        [Required(ErrorMessage = "Seller ID is required")]
        public Guid SellerId { get; init; }
        public bool IsActive {get; set;} 
    }

    
    public record SellerDto
    {
        public string Id { get; init; } = string.Empty;
        // public string FirstName { get; init; } = string.Empty;
        // public string LastName { get; init; } = string.Empty;
        public string FullName { get; init; } = string.Empty;
        public string ShopName { get; init; } = string.Empty;
        public string ShopDescription { get; init; } = string.Empty;
        public string ShopAddress { get; init; } = string.Empty;
    }
}