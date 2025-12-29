using Dotby.Domain;

public sealed class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid productId) : 
    base($"The Product with id: {productId} doesn't exist in the database.")
    {
    }
}