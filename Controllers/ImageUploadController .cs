using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Imgur.API.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

[ApiController]
[Route("api")]
public class ImageUploadController : ControllerBase
{
    private readonly Cloudinary _cloudinary;
    public ImageUploadController(IConfiguration configuration)
    {
        var account = new Account(
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]);

        _cloudinary = new Cloudinary(account);
    }

    [HttpPost("upload-image")]
    public async Task<IActionResult> UploadImage(IFormFile image)
    {
        if (image == null || image.Length == 0)
            return BadRequest("No image file provided.");

        using var stream = image.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(image.FileName, stream),
            Transformation = new Transformation()
          .AspectRatio("1.0")
          .Crop("crop")
          .Width(1000).Height(1000).Crop("limit")
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        return Ok(new { imageUrl = uploadResult.SecureUrl.ToString() });
    }
}