namespace StockSip.Platform.API.InventoryManagement.Application.Internal.OutboundServices;

public interface ICloudinaryService
{
    string UploadImage(IFormFile file);
    
    bool DeleteImage(string imageUrl);
}