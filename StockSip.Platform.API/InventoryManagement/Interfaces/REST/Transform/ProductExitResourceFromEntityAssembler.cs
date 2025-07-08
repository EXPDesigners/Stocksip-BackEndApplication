using Microsoft.OpenApi.Extensions;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming a ProductExit entity into a ProductExitResource.
/// </summary>
public static class ProductExitResourceFromEntityAssembler
{
    /// <summary>
    /// Transforms a ProductExit entity into a ProductExitResource.
    /// </summary>
    /// <param name="entity">
    /// The ProductExit entity to transform.
    /// </param>
    /// <returns>
    /// A ProductExitResource representing the ProductExit entity.
    /// </returns>
    public static ProductExitResource ToResourceFromEntity(ProductExit entity)
    {
        return new ProductExitResource(
            entity.ProductExitId,
            entity.ProductId,
            entity.WarehouseId,
            entity.ProductQuantity,
            entity.ProductBestBeforeDate.GetBestBeforeDate(),
            entity.ExitDate,
            entity.ExitReason.GetDisplayName()
        );
    }
}