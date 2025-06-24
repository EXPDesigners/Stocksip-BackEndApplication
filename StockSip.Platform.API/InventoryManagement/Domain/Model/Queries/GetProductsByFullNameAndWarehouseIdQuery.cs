namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
/// This record represents a query to retrieve products based on their full name.
/// </summary>
/// <param name="WarehouseId">
/// The unique identifier of the warehouse where the products are stored.
/// </param>
/// <param name="BrandName">
/// The name of the brand associated with the product.
/// </param>
/// <param name="LiquorType">
/// The type of liquor associated with the product, such as "Whiskey", "Vodka", etc.
/// </param>
/// <param name="AdditionalName">
/// The additional name or description of the product, which can be null if not applicable.
/// </param>
public record GetProductsByFullNameAndWarehouseIdQuery(string WarehouseId, string BrandName, string LiquorType, string? AdditionalName);