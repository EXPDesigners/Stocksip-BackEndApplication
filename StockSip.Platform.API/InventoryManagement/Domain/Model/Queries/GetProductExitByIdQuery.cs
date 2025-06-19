namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
/// THis query is used to retrieve a product exit by its ID.
/// </summary>
/// <param name="ProductExitId">
/// The unique identifier of the product exit.
/// </param>
public record GetProductExitByIdQuery(string ProductExitId);