using Dotby.Domain;

public sealed class CartItemNotFoundException : NotFoundException
{
    public CartItemNotFoundException(Guid productId) :
    base($"The CartItem with id: {productId} doesn't exist in the database.")
    {

    } 
}