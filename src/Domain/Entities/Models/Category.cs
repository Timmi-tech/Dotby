using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotby.Domain.Entities.Models
{
   public class Category 
    {
        [Key]
        [Column("CategoryId")]
        public Guid Id {get; set;}

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, ErrorMessage = "Category Name cannot exeed 100 characters")]
        public string Name {get; set;} = string.Empty;
        public string Description { get; set; } = string.Empty;

        [StringLength(500)]
        public string? ImageUrl { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

    }

}