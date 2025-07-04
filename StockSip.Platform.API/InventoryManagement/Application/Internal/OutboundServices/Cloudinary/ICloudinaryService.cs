namespace StockSip.Platform.API.InventoryManagement.Application.Internal.OutboundServices.Cloudinary;

/// <summary>
/// This interface defines methods for interacting with Cloudinary, a cloud-based image and video management service.
/// </summary>
public interface ICloudinaryService
{
    /// <summary>
    /// This method uploads an image file to Cloudinary and returns the URL of the uploaded image.
    /// </summary>
    /// <param name="file">The image file to be uploaded.</param>
    /// <returns>The URL of the uploaded image.</returns>
    string UploadImage(IFormFile file);
    
    /// <summary>
    /// This method deletes an image from Cloudinary using its URL.
    /// </summary>
    /// <param name="imageUrl">The URL of the image to be deleted.</param>
    /// <returns>A boolean indicating whether the deletion was successful.</returns>
    bool DeleteImage(string imageUrl);
}