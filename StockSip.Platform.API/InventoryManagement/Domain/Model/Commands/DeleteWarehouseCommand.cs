namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
/// This command is used to delete a warehouse from the inventory management system.
/// </summary>
/// <param name="WarehouseId">The unique identifier of the warehouse to be deleted.</param>
public record DeleteWarehouseCommand(int WarehouseId);