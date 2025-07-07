namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
/// This class represents a query to get the count of product usages for a specific account.
/// </summary>
public record GetProductsCountUsagesQuery(string AccountId);