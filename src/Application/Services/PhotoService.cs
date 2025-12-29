using Dotby.Application.Services.Contracts;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Dotby.Application.DTOs;

namespace Dotby.Application.Services
{
    public class PhotoService(Cloudinary cloudinary) : IPhotoService
    {
        private readonly Cloudinary _cloudinary = cloudinary ?? throw new ArgumentNullException(nameof(cloudinary));

        // Helper method to validate image file extensions
        private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".gif", ".webp"];

        public async Task<ImageUploadResult> UploadImageAsync(IFormFile file)
        {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is empty or null.");

        var extension = Path.GetExtension(file.FileName).ToLower();
        if (!AllowedExtensions.Contains(extension))
            throw new ArgumentException("Invalid file type. Allowed types are: JPG, JPEG, PNG, GIF, WEBP.");

        var uploadResult = new ImageUploadResult();

        try
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation()
                    .Height(500).Width(500).Crop("fill").Gravity("face"),
                Folder = "IlE-Mi_Images"
            };

            uploadResult = await _cloudinary.UploadAsync(uploadParams).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error uploading image to Cloudinary.", ex);
        }

        return uploadResult;
    }
        public async Task<List<ImageUploadResult>> UploadImagesAsync(List<IFormFile> files)
        {
            if (files == null || !files.Any())
                throw new ArgumentException("No files provided.");

            var allowedExtensions = new HashSet<string> { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

            var uploadTasks = files
                .Where(file => file != null && file.Length > 0 &&
                               allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
                .Select(async file =>
                {
                    using var stream = file.OpenReadStream();
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation()
                            .Height(500).Width(500).Crop("fill").Gravity("face"),
                        Folder = "IlE-Mi_Images"
                    };
                    return await _cloudinary.UploadAsync(uploadParams).ConfigureAwait(false);  
                });

            var results = await Task.WhenAll(uploadTasks);
            return results.Where(r => r != null).ToList(); // Filter out failed/null results
        }

    public async Task<DeletionResult> DeleteImageAsync(string publicId)
        {
            if (string.IsNullOrWhiteSpace(publicId))
                throw new ArgumentException("Public ID cannot be null or empty.");

            try
            {
                var deleteParams = new DeletionParams(publicId);
                return await _cloudinary.DestroyAsync(deleteParams).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error deleting image from Cloudinary.", ex);
            }
        }
}
}