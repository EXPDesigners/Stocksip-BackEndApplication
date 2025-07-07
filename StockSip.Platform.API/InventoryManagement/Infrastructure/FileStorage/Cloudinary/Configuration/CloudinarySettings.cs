namespace StockSip.Platform.API.InventoryManagement.Infrastructure.FileStorage.Cloudinary.Configuration;

/// <summary>
/// This class holds the configuration settings for Cloudinary.
/// </summary>
public class CloudinarySettings
{
    /// <summary>
    /// This property specifies the cloud name for Cloudinary.
    /// </summary>
    public string CloudName { get; set; } = null;
    
    /// <summary>
    /// The API key for Cloudinary.
    /// </summary>
    public string ApiKey { get; set; } = null;
    
    /// <summary>
    /// The API secret for Cloudinary.
    /// </summary>
    public string ApiSecret { get; set; } = null;
}