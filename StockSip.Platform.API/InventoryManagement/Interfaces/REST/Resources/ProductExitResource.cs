namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
/// This record defines the product exit resource.
/// </summary>
public record ProductExitResource(string Id, 
    string ProductId, 
    string WarehouseId, 
    int Quantity, 
    DateOnly ProductExpirationDate, 
    DateTime ExitDate, 
    string ExitReason);