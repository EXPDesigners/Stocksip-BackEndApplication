namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
/// This query is used to retrieve a product by its ID, warehouse ID, and expiration date.
/// </summary>
/// <param name="ProductId">
/// The unique identifier of the product.
/// </param>
/// <param name="WarehouseId">
/// The unique identifier of the warehouse where the product is stored.
/// </param>
/// <param name="ExpirationDate">
/// The expiration date of the product.
/// </param>
public record GetProductByIdAndWarehouseIdAndExpirationDateQuery(string ProductId, string WarehouseId, DateTime ExpirationDate);