using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Imgur.API.Models;

[ApiController]
[Route("api")]
public class ImageUploadController : ControllerBase {
  private readonly IConfiguration _configuration;
  private readonly IHttpClientFactory _httpClientFactory;

  public ImageUploadController(IConfiguration configuration, IHttpClientFactory httpClientFactory) {
    _configuration = configuration;
    _httpClientFactory = httpClientFactory;
  }

  [HttpPost("upload-image")]
  public async Task<IActionResult> UploadImage(IFormFile image) {
    if (image == null || image.Length == 0)
      return BadRequest("No image file provided.");

    var apiClient = new ApiClient(_configuration["Imgur:ClientId"], _configuration["Imgur:ClientSecret"]);
    var httpClient = _httpClientFactory.CreateClient();

    var imageEndpoint = new ImageEndpoint(apiClient, httpClient);

    using var memoryStream = new MemoryStream();
    await image.CopyToAsync(memoryStream);
    memoryStream.Position = 0;

    var imageUpload = await imageEndpoint.UploadImageAsync(memoryStream);

    return Ok(new { imageUrl = imageUpload.Link });
  }
}