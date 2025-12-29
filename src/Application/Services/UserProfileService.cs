using Dotby.Application.Services.Contracts;
using Dotby.Domain;
using Dotby.Domain.Contracts;
using AutoMapper;
using Dotby.Application.DTOs;

namespace Dotby.Application.Services
{
    internal sealed class UserProfileService : IUserProfileService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;

        public UserProfileService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IPhotoService photoService)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _photoService = photoService;

        }
        public async Task<UserProfileDto> GetUserProfileAsync(string userId, bool trackChanges)
        {
            var user = await _repository!.User.GetUserProfileAsync(userId, trackChanges);
            if (user is null)
            {
                throw new UserProfileNotFoundException(userId);
            }

            var userDto = _mapper.Map<UserProfileDto>(user);
            return userDto;
        }

        public async Task UpdateProfileAsync(string userId, UserUpdateProfileDto userUpdateProfileDto, bool trackChanges)
        {
            var userUpdateEntity = await _repository!.User.GetUserProfileAsync(userId, trackChanges);
            if (userUpdateEntity is null)
            {
                throw new UserProfileNotFoundException(userId);
            }
            // If a new photo is provided, upload it and update the entity
            if (userUpdateProfileDto.Image  != null)
            {
                var uploadResult = await _photoService!.UploadImageAsync(userUpdateProfileDto.Image);userUpdateEntity.ProfileImageUrl = uploadResult.SecureUrl?.ToString(); // or use Url if you prefer
            }

            _mapper.Map(userUpdateProfileDto, userUpdateEntity);
            await _repository.SaveAsync();
        }
    }
    
}