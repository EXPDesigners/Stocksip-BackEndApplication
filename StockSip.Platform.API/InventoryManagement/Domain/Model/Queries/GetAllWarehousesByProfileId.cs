using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

/// This query is used to retrieve all warehouses associated with a specific profile ID.
/// <summary>
/// This record defines a query to get all warehouses by a specific profile ID.
/// </summary>
/// <param name="ProfileId">The unique identifier of a profile</param>
public record GetAllWarehousesByProfileId(ProfileId ProfileId);