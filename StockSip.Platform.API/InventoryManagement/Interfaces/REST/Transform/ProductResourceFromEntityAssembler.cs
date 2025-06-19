using Microsoft.OpenApi.Extensions;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming a Product entity into a ProductResource.
/// </summary>
public static class ProductResourceFromEntityAssembler
{
    /// <summary>
    /// This method transforms a Product entity into a ProductResource.
    /// </summary>
    /// <param name="entity">
    /// The Product entity to be transformed into a ProductResource.
    /// </param>
    /// <returns>
    /// A ProductResource that contains the details of the product.
    /// </returns>
    public static ProductResource ToResourceFromEntity(Product entity)
    {
        return new ProductResource(
            entity.ProductId,
            entity.ImageUrl.ToString(), 
            entity.ProductName.GetFullName(),
            entity.Brand,
            entity.LiquorType.GetDisplayName(),
            entity.UnitPrice.Amount,
            entity.MinimumStock.GetMinimumStock(),
            entity.ProviderId?.ToString());
    }
}