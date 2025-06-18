using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
/// Query to retrieve all products associated with a specific profile ID.
/// </summary>
/// <param name="ProfileId">
/// The ID of the profile for which products are to be retrieved.
/// </param>
public record GetAllProductsByProfileIdQuery(ProfileId ProfileId);