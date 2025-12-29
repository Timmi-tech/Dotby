using Dotby.Application.DTOs;

namespace Dotby.Application.Services.Contracts
{
    public interface IUserProfileService
    {
        Task<UserProfileDto> GetUserProfileAsync(string userId, bool trackChanges);
        Task UpdateProfileAsync(string userId, UserUpdateProfileDto userUpdateProfileDto, bool trackChanges);
    }
}