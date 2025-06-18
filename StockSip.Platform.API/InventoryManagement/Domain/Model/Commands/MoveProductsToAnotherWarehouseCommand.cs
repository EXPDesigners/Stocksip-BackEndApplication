namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
/// Command to move the specified quantity of a product from one warehouse to another.
/// </summary>
/// <param name="ProductId">
/// The unique identifier of the product to be moved.
/// </param>
/// <param name="NewWarehouseId">
/// The unique identifier of the warehouse to which the product will be moved.
/// </param>
/// <param name="MovedQuantity">
/// The quantity of the product to be moved from the current warehouse to the new warehouse.
/// </param>
public record MoveProductsToAnotherWarehouseCommand(string ProductId, string OldWarehouseId, string NewWarehouseId, int MovedQuantity);