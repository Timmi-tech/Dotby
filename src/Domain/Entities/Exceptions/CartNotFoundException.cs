using Dotby.Domain;

public sealed class CartNotFoundException : NotFoundException
{
    public CartNotFoundException(string userId) : 
    base($"The Cart with id: {userId} doesn't exist in the database.")
    {

    }
}