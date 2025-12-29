using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;


namespace Dotby.Application.Services.Contracts
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile file);
        Task<List<ImageUploadResult>> UploadImagesAsync(List<IFormFile> files);

        Task<DeletionResult> DeleteImageAsync(string publicId);
    }
}