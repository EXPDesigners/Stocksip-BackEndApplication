namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
/// Command to remove a product from a warehouse.
/// </summary>
/// <param name="ProductId">
/// The unique identifier of the product to be removed.
/// </param>
/// <param name="WarehouseId">
/// The unique identifier of the warehouse from which the product will be removed.
/// </param>
public record DeleteProductFromWarehouseCommand(string ProductId, string WarehouseId);