namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
/// Retrieves the inventory details for a specific product in a specific warehouse.
/// </summary>
/// <param name="ProductId">
/// The unique identifier of the product for which inventory details are to be retrieved.
/// </param>
/// <param name="WarehouseId">
/// The unique identifier of the warehouse where the product's inventory is stored.
/// </param>
public record GetInventoryByProductIdAndWarehouseIdQuery(string ProductId, string WarehouseId);