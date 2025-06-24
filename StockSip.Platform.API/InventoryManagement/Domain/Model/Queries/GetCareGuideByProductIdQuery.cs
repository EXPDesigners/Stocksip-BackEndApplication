namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
/// Query used to retrieve a care guide by the product id.
/// </summary>
/// <param name="ProductId">
/// The unique identifier of the product that has a care guide assigned.
/// </param>
public record GetCareGuideByProductIdQuery(string ProductId);