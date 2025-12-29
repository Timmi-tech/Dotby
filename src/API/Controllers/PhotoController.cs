using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using Dotby.Application.Services.Contracts;
using Dotby.Application.DTOs;

[ApiController]
[Route("api/image")]
public class PhotosController : ControllerBase
{
    private readonly IPhotoService _photoService;

    public PhotosController(IPhotoService photoService)
    {
        _photoService = photoService;
    }

    /// <summary>
    /// Uploads an image to Cloudinary.
    /// </summary>
    /// <param name="file">The image file to upload.</param>
    /// <returns>The uploaded image result.</returns>
    [HttpPost("upload")]
    [Consumes("multipart/form-data")]  // ✅ Ensure Swagger knows it's a file upload

    [SwaggerOperation(
        Summary = "Uploads a photo",
        Description = "Upload an image file"
    )]
    public async Task<IActionResult> Upload([FromForm] FileUploadDto uploadDto)
    {
        if (uploadDto.File == null || uploadDto.File.Length == 0)
            return BadRequest(new { message = "No file uploaded or file is empty." });

            var result = await _photoService.UploadImageAsync(uploadDto.File);

            if (result.Error != null)
                return BadRequest(new { message = result.Error.Message });

            return Ok(new
            {
                message = "Image uploaded successfully.",
                publicId = result.PublicId,
                url = result.SecureUrl.ToString()
            });
        }
    /// <summary>
    /// Uploads an image to Cloudinary.
    /// </summary>
    /// <param name="file">The image file to upload.</param>
    /// <returns>The uploaded image result.</returns>
    [HttpPost("upload-multiple")]
    [Consumes("multipart/form-data")]  // ✅ Ensure Swagger knows it's a file upload

    [SwaggerOperation(
        Summary = "Uploads a photo",
        Description = "Upload an image file"
    )]
    public async Task<IActionResult> UploadImages([FromForm] UploadImagesDto uploadImagesDto)
    {

        if (uploadImagesDto.Files == null || !uploadImagesDto.Files.Any())
            return BadRequest(new { message = "No files uploaded or files are empty." });
        var results = await _photoService.UploadImagesAsync(uploadImagesDto.Files);
        var uploadedImages = results.Select(result => new
        {
            publicId = result.PublicId,
            url = result.SecureUrl.ToString()
        }).ToList();
        return Ok(new
        {
            message = "Images uploaded successfully.",
            images = uploadedImages
        });

    }

    [HttpDelete("{publicId}")]
    public async Task<IActionResult> Delete(string publicId)
    {
        if (string.IsNullOrWhiteSpace(publicId))
            return BadRequest(new { message = "Invalid Public ID." });

        try
        {
            var result = await _photoService.DeleteImageAsync(publicId);

            if (result.Error != null)
                return BadRequest(new { message = result.Error.Message });

            return Ok(new { message = "Image deleted successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while deleting the image.", error = ex.Message });
        }
    }
}
