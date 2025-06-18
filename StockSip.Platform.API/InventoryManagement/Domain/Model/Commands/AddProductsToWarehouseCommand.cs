namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
/// Command to add a product with stock to a specific warehouse.
/// </summary>
/// <param name="ProductId">
/// The unique identifier of the product to be added.
/// </param>
/// <param name="WarehouseId">
/// The unique identifier of the warehouse where the product with its stock will be added.
/// </param>
/// <param name="Quantity">
/// The quantity of the product to be added to the warehouse.
/// </param>
public record AddProductsToWarehouseCommand(string ProductId, string WarehouseId, int Quantity);