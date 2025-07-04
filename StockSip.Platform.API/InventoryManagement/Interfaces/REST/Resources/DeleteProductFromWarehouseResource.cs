namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
/// This record represents a resource for deleting a product from a warehouse.
/// </summary>
public record DeleteProductFromWarehouseResource(DateOnly ExpirationDate);