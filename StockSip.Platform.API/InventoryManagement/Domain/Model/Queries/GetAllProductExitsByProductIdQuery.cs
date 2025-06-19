namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
/// This query is used to retrieve all product exits associated with a specific product.
/// </summary>
/// <param name="ProductId">
/// The unique identifier of the product for which exits are being queried.
/// </param>
public record GetAllProductExitsByProductIdQuery(string ProductId);