namespace Dotby.Domain.Entities.Models
{
    
    public class Cart 
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();  

         // Calculated property
        public decimal Total => Items.Sum(item => item.Quantity * item.unitPrice);     
    }
}