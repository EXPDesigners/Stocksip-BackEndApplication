namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
/// This query is used to retrieve all product exits for a specific product in a specific warehouse.
/// </summary>
/// <param name="WarehouseId">
/// The unique identifier for the warehouse.
/// </param>
/// <param name="ProductId">
/// The unique identifier for the product.
/// </param>
public record GetAllProductExitsByProductIdAndWarehouseIdQuery(string WarehouseId, string ProductId);