namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
/// Query used to get an inventory object.
/// </summary>
public record GetInventoryByProductIdAndWarehouseIdAndBestBeforeDateQuery(string ProductId, string WarehouseId, DateOnly BestBeforeDate);