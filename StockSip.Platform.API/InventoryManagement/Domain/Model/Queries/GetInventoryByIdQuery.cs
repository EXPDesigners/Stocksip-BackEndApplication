namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
/// This command is used to retrieve a specific inventory by its id.
/// </summary>
/// <param name="InventoryId">
/// The unique identifier of the inventory.
/// </param>
public record GetInventoryByIdQuery(string InventoryId);