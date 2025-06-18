namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
/// Command to add stock to a existing product in a warehouse.
/// </summary>
/// <param name="ProductId">
/// The unique identifier of the product to which stock is being added.
/// </param>
/// <param name="AddedQuantity">
/// The quantity of stock to be added to the product.
/// </param>
public record AddStockToProductCommand(string ProductId, int AddedQuantity);