using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using StockSip.Platform.API.InventoryManagement.Application.Internal.OutboundServices;
using StockSip.Platform.API.InventoryManagement.Infrastructure.FileStorage.Cloudinary.Configuration;

namespace StockSip.Platform.API.InventoryManagement.Infrastructure.FileStorage.Cloudinary.Services;

/// <summary>
/// This class implements the ICloudinaryService interface, providing methods to upload and delete images using Cloudinary.
/// </summary>
public class CloudinaryService : ICloudinaryService
{
    private readonly CloudinaryDotNet.Cloudinary _cloudinary;

    /// <summary>
    /// This constructor initializes the Cloudinary service with the provided Cloudinary settings.
    /// </summary>
    /// <param name="cloudinarySettings">The Cloudinary settings containing the cloud name, API key, and API secret.</param>
    public CloudinaryService(IOptions<CloudinarySettings> cloudinarySettings)
    {
        var settings = cloudinarySettings.Value;
        var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
        _cloudinary = new CloudinaryDotNet.Cloudinary(account);
    }

    /// <summary>
    /// This method uploads an image to Cloudinary and returns the secure URL of the uploaded image.
    /// </summary>
    /// <param name="file">The image file to be uploaded.</param>
    /// <returns>The secure URL of the uploaded image.</returns>
    public string UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File cannot be null or empty.", nameof(file));
        
        using var stream = file.OpenReadStream();

        var publicId = Guid.NewGuid().ToString();

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            PublicId = publicId,
            Folder = "StockSip-WA-inventories"
        };
        
        var uploadResult = _cloudinary.Upload(uploadParams);
        
        if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            return uploadResult.SecureUrl.ToString();
        
        throw new Exception($"Upload failed: {uploadResult.Error?.Message}");
    }

    /// <summary>
    /// This method deletes an image from Cloudinary using its URL.
    /// </summary>
    /// <param name="imageUrl">The URL of the image to be deleted.</param>
    /// <returns>A boolean indicating whether the deletion was successful.</returns>
    public bool DeleteImage(string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new ArgumentException("Image URL cannot be null or empty.", nameof(imageUrl));
        
        var protectedImages = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "default-warehouse_whqolq",
            "default-product_lcmtsm"
        };

        var uri = new Uri(imageUrl);
        var parts = uri.AbsolutePath.Split('/');

        if (parts.Length < 3)
            throw new ArgumentException("Invalid Cloudinary URL format.");

        var folder = parts[^2];
        var fileName = Path.GetFileNameWithoutExtension(parts[^1]);
        
        if (protectedImages.Contains(fileName))
            return false;

        var publicId = $"{folder}/{fileName}";

        var deletionParams = new DeletionParams(publicId);
        var result = _cloudinary.Destroy(deletionParams);

        return result.Result == "ok";
    }
}