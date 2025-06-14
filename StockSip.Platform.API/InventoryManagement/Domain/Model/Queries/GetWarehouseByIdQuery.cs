namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

/// This query is used to retrieve a warehouse by its ID.
/// <summary>
/// This record defines a query to get a warehouse by its unique identifier.
/// </summary>
/// <param name="WarehouseId">the unique identifier for the warehouse</param>
public record GetWarehouseByIdQuery(long WarehouseId);