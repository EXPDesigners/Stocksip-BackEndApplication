namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
/// This query is used to retrieve all product exits associated with a specific warehouse.
/// </summary>
/// <param name="WarehouseId">
/// The unique identifier of the warehouse for which product exits are being queried.
/// </param>
public record GetAllProductExitsByWarehouseIdQuery(string WarehouseId);