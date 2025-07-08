namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
/// This record represents a resource for adding stock to an existing product in a warehouse.
/// </summary>
public record AddStockToProductResource(DateOnly StockExpirationDate, int AddedQuantity);