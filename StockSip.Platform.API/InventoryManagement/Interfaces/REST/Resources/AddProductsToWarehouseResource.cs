namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
/// This record defines the resource for adding products to a warehouse.
/// </summary>
public record AddProductsToWarehouseResource(DateOnly ExpirationDate, int Quantity);