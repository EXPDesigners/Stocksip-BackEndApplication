using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

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
/// <param name="ExpirationDate">
/// The expiration date of the product for which inventory details are to be retrieved.
/// </param>
public record GetInventoryByProductIdAndWarehouseIdAndExpirationDateQuery(string ProductId, string WarehouseId, ProductExpirationDate ExpirationDate);