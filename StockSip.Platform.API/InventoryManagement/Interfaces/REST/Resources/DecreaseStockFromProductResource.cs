namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
/// This record represents a resource for decreasing stock from an existing product in a warehouse.
/// </summary>
public record DecreaseStockFromProductResource(DateTime ExpirationDate, int RemovedQuantity);