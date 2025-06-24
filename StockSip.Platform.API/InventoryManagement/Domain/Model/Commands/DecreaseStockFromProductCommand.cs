namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
/// Command to decrease stock from an existing product in a warehouse.
/// </summary>
/// <param name="ProductId">
/// The unique identifier of the product.
/// </param>
/// <param name="WarehouseId">
/// The unique identifier of the warehouse.
/// </param>
/// <param name="ExpirationDate">
/// The expiration date of the product stock to be removed.
/// </param>
/// <param name="RemovedQuantity">
/// The quantity of stock to be removed from the product.
/// </param>
public record DecreaseStockFromProductCommand(string ProductId, string WarehouseId, DateTime ExpirationDate, int RemovedQuantity);