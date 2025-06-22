namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
/// This command is used to retrieve inventories by its product and warehouse id.
/// </summary>
public record GetInventoriesByProductIdAndWarehouseIdQuery(string ProductId, string WarehouseId);