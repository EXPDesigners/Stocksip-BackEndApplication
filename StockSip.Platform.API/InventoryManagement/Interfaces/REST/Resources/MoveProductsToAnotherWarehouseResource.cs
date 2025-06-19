namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
/// This record represents a resource for moving products from one warehouse to another.
/// </summary>
public record MoveProductsToAnotherWarehouseResource(string NewWarehouseId, DateTime MovedStockExpirationDate, int MovedQuantity);