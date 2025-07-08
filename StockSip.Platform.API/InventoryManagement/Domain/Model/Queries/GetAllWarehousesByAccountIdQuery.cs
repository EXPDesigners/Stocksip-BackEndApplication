using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
/// This query retrieves all the warehouses from a specific account ID.
/// </summary>
public record GetAllWarehousesByAccountIdQuery(AccountId AccountId);