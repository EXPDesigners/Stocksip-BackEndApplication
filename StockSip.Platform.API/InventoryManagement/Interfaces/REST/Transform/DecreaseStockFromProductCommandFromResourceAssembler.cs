using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming a DecreaseStockFromProductResource into a DecreaseStockFromProductCommand.
/// </summary>
public static class DecreaseStockFromProductCommandFromResourceAssembler
{
    /// <summary>
    /// Method to transform a DecreaseStockFromProductResource into a DecreaseStockFromProductCommand.
    /// </summary>
    /// <param name="resource"> The DecreaseStockFromProductResource to transform. </param>
    /// <param name="productId"> The identifier of the product that will decrease its stock. </param>
    /// <param name="warehouseId"> The identifier of the warehouse that stores the product whose stock will be updated. </param>
    /// <returns> The DecreaseStockFromProductCommand created from the resource. </returns>
    public static DecreaseStockFromProductCommand ToCommandFromResource(DecreaseStockFromProductResource resource, string productId, string warehouseId)
    {
        return new DecreaseStockFromProductCommand(
            productId, 
            warehouseId, 
            resource.ExpirationDate,
            resource.RemovedQuantity);
    }
}