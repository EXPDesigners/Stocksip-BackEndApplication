namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
/// This command is used to delete a product by its unique identifier.
/// </summary>
/// <param name="ProductId">The unique identifier of the product to be deleted.</param>
public record DeleteProductCommand(string ProductId);