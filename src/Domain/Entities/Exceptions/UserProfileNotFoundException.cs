using Dotby.Domain;

public sealed class UserProfileNotFoundException : NotFoundException
{
    public UserProfileNotFoundException(string userId) :
    base($"The Seller with id: {userId} doesn't exist in the database.")
    {
    }
}