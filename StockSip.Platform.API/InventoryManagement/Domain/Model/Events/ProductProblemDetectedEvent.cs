using StockSip.Platform.API.Shared.Domain.Model.Events;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Events;

public class ProductProblemDetectedEvent(
    string title, 
    string message, 
    string severity, 
    string type, 
    string profileId, 
    string productId,
    string warehouseId
    ) : IEvent 
{
    public string Title { get; } = title;
    public string Message { get; } = message;
    public string Severity { get; } = severity;
    public string Type { get; } = type;
    public string ProfileId { get; } = profileId;
    public string ProductId { get; } = productId;
    public string WarehouseId { get; } = warehouseId;
}