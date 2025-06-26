using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using StockSip.Platform.API.InventoryManagement.Application.Internal.OutboundServices;
using StockSip.Platform.API.InventoryManagement.Infrastructure.FileStorage.Cloudinary.Configuration;

namespace StockSip.Platform.API.InventoryManagement.Infrastructure.FileStorage.Cloudinary.Services;

public class CloudinaryService : ICloudinaryService
{
    private readonly CloudinaryDotNet.Cloudinary _cloudinary;

    public CloudinaryService(IOptions<CloudinarySettings> cloudinarySettings)
    {
        var settings = cloudinarySettings.Value;
        var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
        _cloudinary = new CloudinaryDotNet.Cloudinary(account);
    }

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

    public bool DeleteImage(string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new ArgumentException("Image URL cannot be null or empty.", nameof(imageUrl));

        var uri = new Uri(imageUrl);
        var parts = uri.AbsolutePath.Split('/');

        if (parts.Length < 2)
            throw new ArgumentException("Invalid Cloudinary URL format.");

        var fileName = Path.GetFileNameWithoutExtension(parts[^1]);
        
        if (fileName.Equals("default-warehouse_whqolq", StringComparison.OrdinalIgnoreCase))
            return false;

        var publicId = fileName;

        var deletionParams = new DeletionParams(publicId);
        var result = _cloudinary.Destroy(deletionParams);

        return result.Result == "ok";
    }
}