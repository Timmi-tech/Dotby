using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Dotby.Application.DTOs
{

    public record CategoryDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string ImageUrl { get; init; } = string.Empty;

    }

    public record CategoryForCreationDto
    {
        [Required(ErrorMessage = "Category name is required")]
        public string Name { get; init; } = string.Empty;

        [MaxLength(1000, ErrorMessage = "Category description cannot exceed 1000 characters")]
        [Required(ErrorMessage = "Category description is required")]
        public string Description { get; init; } = string.Empty;

        [Required(ErrorMessage = "Category image is required")]
        public IFormFile? Image { get; init; }
    }

}