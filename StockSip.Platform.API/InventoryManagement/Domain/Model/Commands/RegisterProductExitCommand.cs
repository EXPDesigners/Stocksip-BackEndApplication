namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
/// This command is used to register a product exit from the inventory.
/// </summary>
/// <param name="WarehouseId">
/// The unique identifier of the warehouse where the product exit is registered.
/// </param>
/// <param name="ProductId">
/// The unique identifier of the product being exited from the inventory.
/// </param>
/// <param name="ExpirationDate">
/// The expiration date of the product being exited.
/// </param>
/// <param name="QuantityExited">
/// The quantity of the product that has exited the inventory.
/// </param>
/// <param name="ExitReason">
/// The reason for the product exit, represented as a string.
/// </param>
public record RegisterProductExitCommand(string WarehouseId, string ProductId, DateOnly ExpirationDate, int QuantityExited, string ExitReason);