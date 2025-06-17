using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
/// Method to retrieve all products provided by a specific provider.
/// </summary>
/// <param name="ProviderId">
/// The unique identifier of the provider whose products are to be retrieved.
/// </param>
public record GetAllProductsByProviderIdQuery(ProviderId ProviderId);