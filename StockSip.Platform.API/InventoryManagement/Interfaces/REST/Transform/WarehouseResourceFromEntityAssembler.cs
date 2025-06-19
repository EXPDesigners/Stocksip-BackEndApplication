using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming a Warehouse entity into a WarehouseResource.
/// </summary>
public static class WarehouseResourceFromEntityAssembler
{
    /// <summary>
    /// Transforms a Warehouse entity into a WarehouseResource.
    /// </summary>
    /// <param name="entity">The Warehouse entity to transform.</param>
    /// <returns>A WarehouseResource representing the Warehouse entity.</returns>
    public static WarehouseResource ToResourceFromEntity(Warehouse entity)
    {
        return new WarehouseResource(
            entity.WarehouseId,
            entity.Name,
            entity.Address.Street,
            entity.Address.City,
            entity.Address.District,
            entity.Address.PostalCode,
            entity.Address.Country,
            entity.Temperature.MaxTemperature,
            entity.Temperature.MinTemperature,
            entity.Capacity.TotalCapacity,
            entity.ImageUrl.ToString()
        );
    }
    
    public static IEnumerable<WarehouseResource> ToResourcesFromEntities(IEnumerable<Warehouse> entities)
    {
        return entities.Select(ToResourceFromEntity);
    }
}