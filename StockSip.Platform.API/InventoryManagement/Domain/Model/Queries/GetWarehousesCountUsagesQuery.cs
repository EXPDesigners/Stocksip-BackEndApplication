namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
/// This query is used to retrieve the benefit usages for a specific account.
/// </summary>
public record GetWarehousesCountUsagesQuery(string AccountId);