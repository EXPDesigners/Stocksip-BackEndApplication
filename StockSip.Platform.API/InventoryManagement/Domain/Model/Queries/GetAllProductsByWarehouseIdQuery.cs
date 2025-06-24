namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
/// Retrieves all products associated with a specific warehouse.
/// </summary>
/// <param name="WarehouseId">
/// The unique identifier of the warehouse for which products are to be retrieved.
/// </param>
public record GetAllProductsByWarehouseIdQuery(string WarehouseId);