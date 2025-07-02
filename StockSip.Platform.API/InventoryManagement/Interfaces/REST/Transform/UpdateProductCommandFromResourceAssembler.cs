using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming an UpdateProductResource into an UpdateProductCommand.
/// </summary>
public static class UpdateProductCommandFromResourceAssembler
{
    /// <summary>
    /// Method to transform an UpdateProductResource into an UpdateProductCommand.
    /// </summary>
    /// <param name="resource">
    /// The UpdateProductResource containing the product details to be updated.
    /// </param>
    /// <param name="productId"> The identifier of the product that its information will be updated. </param>
    /// <returns>
    /// The UpdateProductCommand that encapsulates the product update operation.
    /// </returns>
    public static UpdateProductCommand ToCommandFromResource(UpdateProductResource resource, string productId)
    {
        return new UpdateProductCommand(
            productId,
            resource.Name,
            resource.BrandName,
            resource.LiquorType,
            resource.UnitPriceAmount,
            resource.MinimumStock, 
            resource.UpdatedImage);
    }
}