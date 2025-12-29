namespace Dotby.Application.DTOs
{
    public record CartDto 
    {
        public Guid Id { get; init; }
        public string UserId { get; init; } = string.Empty;
        public ICollection<CartItemDto> Items { get; init; } = new List<CartItemDto>();
        public decimal Total { get; init; }
    }

    public record CartItemDto
    {
        public Guid Id { get; init; }
        public Guid ProductId { get; init; }
        public string ProductName { get; init; } = string.Empty;
        public int Quantity { get; init; }
        public decimal UnitPrice { get; init; }
        public decimal Subtotal { get; init; }
    }
    public record AddToCartDto
    {
        public Guid ProductId { get; init; }
        public int Quantity { get; init; } = 1;
    }
    
    public record UpdateCartItemDto
    {
        public int Quantity { get; init; }
    }
}