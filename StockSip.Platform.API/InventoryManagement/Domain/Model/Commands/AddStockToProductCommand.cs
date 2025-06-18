namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
/// Command to add stock to an existing product in a warehouse.
/// </summary>
/// <param name="ProductId">
/// The unique identifier of the product to which stock is being added.
/// </param>
/// <param name="WarehouseId">
/// The unique identifier of the warehouse where the stock is being added.
/// </param>
/// <param name="AddedQuantity">
/// The quantity of stock to be added to the product.
/// </param>
public record AddStockToProductCommand(string ProductId, string WarehouseId, int AddedQuantity);