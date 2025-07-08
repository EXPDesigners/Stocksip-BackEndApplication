namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
/// Command to retrieve a product by its unique identifier.
/// </summary>
/// <param name="ProductId">
/// The unique identifier of the product to retrieve.
/// </param>
public record GetProductByIdQuery(string ProductId);