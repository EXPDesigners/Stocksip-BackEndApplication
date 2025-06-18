namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
/// Command to decrease stock from an existing product in a warehouse.
/// </summary>
/// <param name="ProductId">
/// 
/// </param>
/// <param name="RemovedQuantity"></param>
public record DecreaseStockFromProductCommand(string ProductId, string WarehouseId, int RemovedQuantity);