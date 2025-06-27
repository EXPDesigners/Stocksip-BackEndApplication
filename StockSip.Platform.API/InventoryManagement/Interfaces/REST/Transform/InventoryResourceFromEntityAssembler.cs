using Microsoft.OpenApi.Extensions;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming an Inventory entity to an InventoryResource.
/// </summary>
public static class InventoryResourceFromEntityAssembler
{
    /// <summary>
    /// Transforms an Inventory entity to an InventoryResource.
    /// </summary>
    /// <param name="entity">
    /// The entity to be transformed.
    /// </param>
    /// <returns>
    /// The created InventoryResource.
    /// </returns>
    public static InventoryResource ToResourceFromEntity(Inventory entity)
    {
        return new InventoryResource(
            entity.InventoryId,
            entity.ProductId,
            entity.WarehouseId,
            entity.BestBeforeDate.BestBeforeDate,
            entity.ProductStock.Stock,
            entity.ProductState.GetDisplayName());
    }
}