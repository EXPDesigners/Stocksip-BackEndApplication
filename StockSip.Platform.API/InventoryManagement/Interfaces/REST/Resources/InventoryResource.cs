namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
/// This record defines the InventoryResource
/// </summary>
public record InventoryResource(string Id, string ProductId, string WarehouseId, DateOnly BestBeforeDate, int Stock, string ProductState);