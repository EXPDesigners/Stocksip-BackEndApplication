using Microsoft.OpenApi.Extensions;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming a Product entity into a ProductResource.
/// </summary>
public static class ProductInventoryResourceFromEntityAssembler
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
    public static ProductInventoryResource ToResourceFromEntity(Inventory entity)
    {
        var product = entity.Product;
        
        return new ProductInventoryResource(
            product.ProductId,
            product.ProductName.Name,
            product.LiquorType.GetDisplayName(),
            product.UnitPrice.Amount,
            product.MinimumStock.GetMinimumStock(),
            product.ImageUrl.ToString(),
            entity.ProductStock.Stock,
            entity.ProductState.GetDisplayName(),
            entity.ProductBestBeforeDate.BestBeforeDate
        );
    }
}