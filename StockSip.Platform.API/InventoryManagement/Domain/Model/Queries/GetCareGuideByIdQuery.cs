namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
/// This query is used to retrieve a specific care guide by its Id.
/// </summary>
/// <param name="Id">
/// The unique identifier of the care guide to be retrieved.
/// </param>
public record GetCareGuideByIdQuery(string Id);